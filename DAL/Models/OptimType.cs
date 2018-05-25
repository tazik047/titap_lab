using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public enum OptimType
	{
		[Display(Name = "Минимум")]
		Minimum,
		[Display(Name = "Максимум")]
		Maximum
	}
}