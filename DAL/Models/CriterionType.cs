using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public enum CriterionType
	{
		[Display(Name = "Качественный")]
		Qualitative,
		[Display(Name = "Количественный")]
		Quantitative
	}
}