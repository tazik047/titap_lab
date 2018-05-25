using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
	public class Result
	{
		public int Id { get; set; }

		[Required]
		public int LPRId { get; set; }

		[Required]
		public int AlternativeId { get; set; }

		[Required]
		public int Range { get; set; }

		[Required]
		public int Weight { get; set; }

		public virtual LPR LPR { get; set; }

		public virtual Alternative Alternative { get; set; }
	}
}