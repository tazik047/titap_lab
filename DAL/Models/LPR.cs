using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class LPR
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int Range { get; set; }

		public virtual ICollection<Result> Results { get; set; }
	}
}