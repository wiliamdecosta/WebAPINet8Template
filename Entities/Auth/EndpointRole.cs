using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Auth
{
	[Table(name: "role_endpoint")]
	public class EndpointRole
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(name: "role_endpoint_id")]
		public int Id { get; set; }

		[Column(name: "role_id")]
		[ForeignKey("Role")]
		public int RoleId { get; set; }


		[Column(name: "endpoint_id")]
		[ForeignKey("EndpointPath")]
		public int EndpointId { get; set; }


		[Column(name: "created_date")]
		public DateTime CreatedDate { get; set; }

		[Column(name: "updated_date")]
		public DateTime? UpdatedDate { get; set; }

		[JsonIgnore]
		public virtual Role Role { get; }

		[JsonIgnore]
		public virtual EndpointPath EndpointPath { get; }


	}
}
