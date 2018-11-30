using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloWord.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        public AboutController()
        {
        }

        [Route("phone")]
        public string Phone()
        {
            return "+10086";
        }

        [Route("area")]
        public string Country()
        {
            return "中国";
        }

    }
}