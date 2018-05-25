using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class Alternative
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public virtual ICollection<Vector> Vectors { get; set; }
	}
}