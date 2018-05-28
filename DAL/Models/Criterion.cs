using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class Criterion : IKeyValueConvertable
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int Range { get; set; }

		[Required]
		public int Weight { get; set; }

		[Required]
		public CriterionType Type { get; set; }

		[Required]
		public OptimType OptimType { get; set; }

		[Required]
		public string Edlzmer { get; set; }

		[Required]
		public string ScaleType { get; set; }

		public virtual ICollection<Mark> Marks { get; set; }

		public KeyValuePair<int, string> GetKeyValuePair()
		{
			return new KeyValuePair<int, string>(Id, Name);
		}
	}
}