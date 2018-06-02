using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebChoose.Infrastructure
{
	public static class ControllerHelper
	{
		public static ActionResult ToJson(this Controller controller, object obj)
		{
			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new CustomStringEnumConverter());
			settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			return new ContentResult
			{
				ContentType = "application/json",
				Content = JsonConvert.SerializeObject(obj, settings)
			};
		}

		private class CustomStringEnumConverter : StringEnumConverter
		{
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				if (value == null)
				{
					writer.WriteNull();
				}
				else
				{
					writer.WriteValue(value.ToString().Translate());
				}
			}
		}
	}
}