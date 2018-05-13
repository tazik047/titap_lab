using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
	public class Result
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "LPR")]
		public int LPRId { get; set; }

		[Required]
		[Display(Name = "Alternative")]
		public int AlternativeId { get; set; }

		[Required]
		[Display(Name = "Range")]
		public int Range { get; set; }

		[Required]
		[Display(Name = "Weight")]
		public int Weight { get; set; }

		public virtual LPR LPR { get; set; }

		public virtual Alternative Alternative { get; set; }
	}
}