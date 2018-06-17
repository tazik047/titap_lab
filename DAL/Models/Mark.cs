using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class Mark : IKeyValueConvertable
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
		[Range(0.0, 1.0)]
		public double NormMark { get; set; }

		public virtual Criterion Criterion { get; set; }

		public KeyValuePair<int, string> GetKeyValuePair()
		{
			return new KeyValuePair<int, string>(Id, Name);
		}
	}
}