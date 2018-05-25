using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebChoose.Infrastructure
{
	public static class HtmlHelperExtensions
	{
		private static string _tableTemplate;

		public static MvcHtmlString CreateBootstrapTable(this HtmlHelper helper, string jsonUrl, List<ColumnItem> columns, string attributeFunction = "", string tableId = "")
		{
			if (string.IsNullOrEmpty(_tableTemplate))
			{
				_tableTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/BootstrapTableTemplate.html"));
			}

			jsonUrl = VirtualPathUtility.ToAbsolute(jsonUrl);

			if (!string.IsNullOrEmpty(tableId))
			{
				tableId = "-" + tableId;
			}

			var tableTemplate = _tableTemplate;
			StringBuilder th = new StringBuilder();
			foreach (var column in columns)
			{
				string dateSort = string.Empty;
				if (column.Name.ToLower().Contains("date"))
				{
					dateSort = "data-sorter=\"sortDate\"";
				}

				th.AppendFormat("<th data-field='{0}' data-sortable='true' {2}>{1}</th>", column.Name, column.Title, dateSort);
			}

			return new MvcHtmlString(string.Format(tableTemplate, jsonUrl, attributeFunction, th, tableId));
		}
	}
}