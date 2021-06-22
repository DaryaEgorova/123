using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Maganizer_Project.WEB.Controllers
{
    public class ErrorController : Controller
    {
        [Route("missing")]
        public IActionResult Missing() 
        {
            return View("ErrorNotFound");
        }

        [Route("wrong")]
        public IActionResult Wrong()
        {
            return View("ErrorWentWrong");
        }
    }
}
