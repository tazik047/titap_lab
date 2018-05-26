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
			settings.Converters.Add(new StringEnumConverter());
			settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			return new ContentResult
			{
				ContentType = "application/json",
				Content = JsonConvert.SerializeObject(obj, settings)
			};
		}
	}
}