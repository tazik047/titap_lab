using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class Mark
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Criterion")]
		public int CriterionId { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Range")]
		public int Range { get; set; }

		[Required]
		[Display(Name = "NumMark")]
		public int NumMark { get; set; }

		[Required]
		[Display(Name = "NormMark")]
		public int NormMark { get; set; }

		public virtual Criterion Criterion { get; set; }
	}
}