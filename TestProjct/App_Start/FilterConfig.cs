﻿using System.Web.Mvc;

namespace TestProjct
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new AuthorizeAttribute());
		}
	}
}