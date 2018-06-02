using System.Collections.Generic;

namespace WebChoose.Infrastructure
{
	public static class TranslationHelper
	{
		private static readonly Dictionary<string, string> Translates = new Dictionary<string, string>
		{
			{"Name", "Название"},
			{"Range", "Ранг"},
			{"Weight", "Вес"},
			{"Type", "Тип"},
			{"OptimType", "Тип оптимальности"},
			{"Edlzmer", "Единицы измерения"},
			{"ScaleType", "Тип шкалы"},
			{"Maximum", "Максимум" },
			{"Minimum", "Минимум" },
			{"Qualitative", "Качественный" },
			{"Quantitative", "Количественный" },
			{"Criterion.Name", "Критерий" },
			{"Criterion", "Критерий" },
			{"NumMark","Количественный экв. оценки" },
			{"NormMark","Нормированная оценка" },
		};

		public static string Translate(this string value)
		{
			if (Translates.ContainsKey(value))
			{
				return Translates[value];
			}

			return value;
		}
	}
}