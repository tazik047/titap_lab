using System.Web.Mvc;

namespace WebChoose.Infrastructure
{
	public class ColumnItem
	{
		public ColumnItem(string name, MvcHtmlString title)
		{
			Name = name;
			Title = title;
		}

		public ColumnItem(string name, string title)
			: this(name, new MvcHtmlString(title))
		{
		}

		public string Name { get; set; }

		public MvcHtmlString Title { get; set; }
	}
}