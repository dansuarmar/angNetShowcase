﻿using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace NetBacked_Api.Controllers
{
    public class VersionController : ControllerBase
    {
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult Index()
        {
            var assembly = Assembly.GetEntryAssembly()?.GetName().Version;
            return Ok(assembly);
        }
    }
}
