using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class Vector
	{
		public int Id { get; set; }

		[Required]
		public int AlternativeId { get; set; }

		[Required]
		public int MarkId { get; set; }

		public virtual Mark Mark { get; set; }

		public virtual Alternative Alternative { get; set; }
	}
}