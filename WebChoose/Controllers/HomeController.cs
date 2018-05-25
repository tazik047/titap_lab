using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}
	}
}