using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Auth
{
	[Table(name: "user_role")]
	public class UserRole
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(name: "user_role_id")]
		public int Id { get; set; }

		[Column(name: "role_id")]
		[ForeignKey("Role")]
		public int RoleId { get; set; }


		[JsonIgnore]
		public virtual Role Role { get; set; }

		[Column(name: "user_id")]
		[ForeignKey("User")]
		public int UserId { get; set; }

		[JsonIgnore]
		public virtual User User { get; set; }

		[Column(name: "created_date")]
		public DateTime CreatedDate { get; set; }

		[Column(name: "updated_date")]
		public DateTime? UpdatedDate { get; set; }

	}
}
