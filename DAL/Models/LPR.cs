using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class LPR
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Range")]
		public int Range { get; set; }

		public virtual ICollection<Result> Results { get; set; }
	}
}