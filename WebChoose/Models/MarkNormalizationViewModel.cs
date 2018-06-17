using DAL.Models;

namespace WebChoose.Models
{
	public class MarkNormalizationViewModel
	{
		public OptimType Type { get; set; }

		public Mark[] Marks { get; set; }

		public int Etalon { get; set; }
	}
}