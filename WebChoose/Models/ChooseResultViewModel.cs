using System;
using System.Collections.Generic;
using DAL.Models;

namespace WebChoose.Models
{
	public class ChooseResultViewModel
	{
		public string LprName { get; set; }

		public Dictionary<int, double> NormKoeficients { get; set; }

		public Alternative[] Alternatives { get; set; }

		public Tuple<string, double>[] Results { get; set; }
	}
}