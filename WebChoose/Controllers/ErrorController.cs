using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebChoose.Controllers
{
	[AllowAnonymous]
	public class ErrorController : Controller
	{
		[HttpGet]
		public ActionResult NotFound()
		{
			Response.StatusCode = (int)HttpStatusCode.NotFound;

			return View();
		}

		[HttpGet]
		public ActionResult InternalError()
		{
			Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return View();
		}

		[HttpGet]
		public ActionResult Unauthorised()
		{
			Response.StatusCode = (int)HttpStatusCode.Unauthorized;

			return View();
		}
	}
}