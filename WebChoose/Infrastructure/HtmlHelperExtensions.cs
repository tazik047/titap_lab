﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;

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

		public static MvcHtmlString CustomDropDownFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, int>> property, string placeholder)
		{
			var metadata = ModelMetadata.FromLambdaExpression(property, helper.ViewData);
			var navigationProperty = metadata.ContainerType.GetProperty(metadata.PropertyName.Replace("Id", string.Empty));
			var repositoryType = typeof(IRepository<>).MakeGenericType(navigationProperty.PropertyType);

			var repositoryProperty = typeof(IUnitOfWork).GetProperties().First(p => repositoryType.IsAssignableFrom(p.PropertyType));
			using (var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>())
			{
				dynamic repository = repositoryProperty.GetValue(unitOfWork);
				var items = ((IEnumerable<object>) repository.Get())
					.Cast<IKeyValueConvertable>()
					.Select(p =>
					{
						var item = p.GetKeyValuePair();
						return new SelectListItem
						{
							Text = item.Value,
							Value = item.Key.ToString(),
							Selected = (int) metadata.Model == item.Key
						};
					})
					.OrderBy(p => p.Text)
					.ToList();
				return helper.DropDownListFor(property, items, placeholder, new { @class = "form-control" });
			}
		}
	}
}