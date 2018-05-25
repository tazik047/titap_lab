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
		public string Name { get; set; }

		[Required]
		public int Range { get; set; }

		[Required]
		public int NumMark { get; set; }

		[Required]
		public int NormMark { get; set; }

		public virtual Criterion Criterion { get; set; }
	}
}