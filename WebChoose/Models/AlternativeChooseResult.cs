using System;

namespace WebChoose.Models
{
	public class AlternativeChooseResult
	{
		public string AlternativeName { get; set; }

		public double Result { get; set; }

		public Tuple<int, double>[] Marks { get; set; }
	}
}