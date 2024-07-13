﻿using Infrastructure.Db;
using Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Infrastructure.Middlewares
{
	public class EndpointAuthorizationMiddleware : IMiddleware
	{

		private ApplicationDbContext dbContext;

		public EndpointAuthorizationMiddleware(ApplicationDbContext _dbContext)
		{
			dbContext = _dbContext;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var path = context.Request.Path.Value.ToLower();
			var method = context.Request.Method;
			List<string> publicEndpoints = [
				"/uploads",
				"/swagger",
				"/api/v1/user/register_admin",
				"/api/v1/user/register_user",
				"/api/v1/auth/login",
				"/api/v1/auth/refresh_token",
				"/api/v1/auth/logout",
			];
			bool isPublicAccess = false;

			if (path == "/" || path == "") //index
			{
				isPublicAccess = true;
			}
			else
			{
				foreach (string publicPath in publicEndpoints)
				{
					if (path.StartsWith(publicPath))
					{
						isPublicAccess = true;
						break;
					}
				}
			}

			if (isPublicAccess)
			{
				await next(context);
				return;
			}

			string pattern = @"/\d+$";  // pattern untuk mencocokkan angka di akhir string
			string replacement = "/{id}";
			path = Regex.Replace(path, pattern, replacement);

			var endpoint = await dbContext.Endpoints
			.Where(e => e.PathRoute.ToLower() == path && e.HttpMethod == method)
			.Select(e => new
			{
				EndpointId = e.Id,
				e.PathRoute,
				e.HttpMethod,
				EndpointRoles = e.EndpointRoles.Select(er => new
				{
					er.RoleId,
					RoleName = er.Role.Name
				})
			})
			.FirstOrDefaultAsync();

			//var endpointTest = (from ePoint in dbContext.Endpoints
			//                    join eRole in dbContext.EndpointRoles
			//                    on ePoint.Id equals eRole.EndpointId
			//                    join roles in dbContext.Roles
			//                    on eRole.RoleId equals roles.Id
			//                    select new
			//                    {
			//                        ePoint.Id,
			//                        eRole.CreatedDate,
			//                        roles.Name,
			//                    }).ToList();

			var problem = BaseResponse<string?>.Builder()
				   .Code(StatusCodes.Status404NotFound)
				   .Message("Endpoint Not Found")
				   .Data(null);
			context.Response.ContentType = "application/json";

			if (endpoint == null)
			{
				context.Response.StatusCode = StatusCodes.Status404NotFound;
				await context.Response.WriteAsync(problem.ToJSONString());
				return;
			}

			var user = context.User;
			if (user == null || !user.Identity.IsAuthenticated)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				problem.Message("You are not Authorized User");
				problem.Code(StatusCodes.Status401Unauthorized);
				await context.Response.WriteAsync(problem.ToJSONString());
				return;
			}

			var userId = int.Parse(user.Claims.First(c => c.Type == "UserId").Value);
			var userRoles = await dbContext.UserRoles
				.Where(ur => ur.UserId == userId)
				.Select(ur => ur.Role.Name)
				.ToListAsync();

			var hasAccess = endpoint.EndpointRoles.Any(er => userRoles.Contains(er.RoleName));

			if (!hasAccess)
			{
				context.Response.StatusCode = StatusCodes.Status403Forbidden;
				problem.Message("Ooopss It's Forbidden");
				problem.Code(StatusCodes.Status403Forbidden);
				await context.Response.WriteAsync(problem.ToJSONString());
				return;
			}

			await next(context);
		}
	}
}
