﻿using Bornholm_Sleagts.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bornholm_Sleagts.Controllers
{
    public class RoleViewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireManager)]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
