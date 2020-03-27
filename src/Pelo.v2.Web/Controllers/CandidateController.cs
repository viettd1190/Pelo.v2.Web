using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class CandidateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}