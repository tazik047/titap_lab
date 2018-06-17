using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
	public class DataContext : DbContext
	{
		public DataContext()
		{
			Database.SetInitializer(new CustomDbInitializer());
		}

		public DbSet<Alternative> Alternatives { get; set; }

		public DbSet<Criterion> Criterions { get; set; }

		public DbSet<Mark> Marks { get; set; }

		public DbSet<LPR> LPRs { get; set; }

		public DbSet<Result> Results { get; set; }

		public DbSet<Vector> Vectors { get; set; }

		public virtual void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class
		{
			Entry(entity).State = state;
		}

		class CustomDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
		{
			protected override void Seed(DataContext context)
			{
				base.Seed(context);

				var lprs = new[]
				{
					new LPR {Name = "Валерий"},
					new LPR {Name = "Владимир"},
					new LPR {Name = "Игорь"},
					new LPR {Name = "Станислав"}
				};

				var criterions = new[]
				{
					new Criterion
					{
						Name = "Диагональ экрана",
						Edlzmer = "дюймы",
						OptimType = OptimType.Maximum,
						ScaleType = "Интервальная",
						Type = CriterionType.Quantitative,
						Marks = new List<Mark>
						{
							new Mark {Name = "9-12.5"},
							new Mark {Name = "13"},
							new Mark {Name = "14"},
							new Mark {Name = "15-15.6"},
							new Mark {Name = "16-17"},
						}
					},
					new Criterion
					{
						Name = "Цена",
						Edlzmer = "грн",
						OptimType = OptimType.Minimum,
						ScaleType = "Интервальная",
						Type = CriterionType.Quantitative,
						Marks = new List<Mark>
						{
							new Mark {Name = "<10000"},
							new Mark {Name = "10000-20000"},
							new Mark {Name = ">20000"},
						}
					},
					new Criterion
					{
						Name = "ОЗУ",
						Edlzmer = "гб",
						OptimType = OptimType.Maximum,
						ScaleType = "Интервальная",
						Type = CriterionType.Quantitative,
						Marks = new List<Mark>
						{
							new Mark {Name = "<=2"},
							new Mark {Name = "4"},
							new Mark {Name = "6-8"},
							new Mark {Name = "10-12"},
							new Mark {Name = "16-24"},
							new Mark {Name = ">=32"},
						}
					},
					new Criterion
					{
						Name = "Количество ядер процессора",
						Edlzmer = "к-во",
						OptimType = OptimType.Maximum,
						ScaleType = "Числовая",
						Type = CriterionType.Quantitative,
						Marks = new List<Mark>
						{
							new Mark {Name = "2"},
							new Mark {Name = "4"},
							new Mark {Name = "6"},
							new Mark {Name = "8"},
						}
					},
					new Criterion
					{
						Name = "Объем SSD",
						Edlzmer = "гб",
						OptimType = OptimType.Maximum,
						ScaleType = "Числовая",
						Type = CriterionType.Quantitative,
						Marks = new List<Mark>
						{
							new Mark {Name = "0"},
							new Mark {Name = "128"},
							new Mark {Name = "256"},
							new Mark {Name = "512"},
							new Mark {Name = "1024"},
						}
					}
				};

				var alternative = new[]
				{
					new Alternative
					{
						Name = "Ноутбук Asus VivoBook Max X541NC (X541NC-GO021) Chocolate Black",
						Vectors = new List<Vector>
						{
							new Vector { Mark = criterions[0].Marks.ToList()[3]},
							new Vector { Mark = criterions[1].Marks.ToList()[0]},
							new Vector { Mark = criterions[2].Marks.ToList()[1]},
							new Vector { Mark = criterions[3].Marks.ToList()[0]},
							new Vector { Mark = criterions[4].Marks.ToList()[0]}
						}
					},
					new Alternative
					{
						Name = "Ноутбук Lenovo IdeaPad 320-15IKB (80XL03GMRA) Platinum Grey",
						Vectors = new List<Vector>
						{
							new Vector { Mark = criterions[0].Marks.ToList()[3]},
							new Vector { Mark = criterions[1].Marks.ToList()[1]},
							new Vector { Mark = criterions[2].Marks.ToList()[2]},
							new Vector { Mark = criterions[3].Marks.ToList()[0]},
							new Vector { Mark = criterions[4].Marks.ToList()[0]}
						}
					},
					new Alternative
					{
						Name = "Ноутбук HP Pavilion Power 15-cb013ur (2CM41EA) Black",
						Vectors = new List<Vector>
						{
							new Vector { Mark = criterions[0].Marks.ToList()[3]},
							new Vector { Mark = criterions[1].Marks.ToList()[2]},
							new Vector { Mark = criterions[2].Marks.ToList()[2]},
							new Vector { Mark = criterions[3].Marks.ToList()[1]},
							new Vector { Mark = criterions[4].Marks.ToList()[0]}
						}
					},
					new Alternative
					{
						Name = "Ноутбук Acer Extensa 15 EX2519 (NX.EFAEU.061) Black",
						Vectors = new List<Vector>
						{
							new Vector { Mark = criterions[0].Marks.ToList()[3]},
							new Vector { Mark = criterions[1].Marks.ToList()[0]},
							new Vector { Mark = criterions[2].Marks.ToList()[1]},
							new Vector { Mark = criterions[3].Marks.ToList()[1]},
							new Vector { Mark = criterions[4].Marks.ToList()[0]}
						}
					},
					new Alternative
					{
						Name = "Ноутбук HP 250 G6 (3QM16ES) Dark Ash",
						Vectors = new List<Vector>
						{
							new Vector { Mark = criterions[0].Marks.ToList()[3]},
							new Vector { Mark = criterions[1].Marks.ToList()[1]},
							new Vector { Mark = criterions[2].Marks.ToList()[1]},
							new Vector { Mark = criterions[3].Marks.ToList()[0]},
							new Vector { Mark = criterions[4].Marks.ToList()[0]}
						}
					}
				};

				context.LPRs.AddRange(lprs);
				context.SaveChanges();

				context.Criterions.AddRange(criterions);
				context.SaveChanges();

				context.Alternatives.AddRange(alternative);
				context.SaveChanges();
			}
		}
	}
}