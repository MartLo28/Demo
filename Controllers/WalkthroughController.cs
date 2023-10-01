using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using System.Collections.Generic;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkthroughController : ControllerBase
    {
        // Placeholder for walkthrough steps
        private static readonly List<WalkthroughStep> Steps = new List<WalkthroughStep>
        {
            new WalkthroughStep { Title = "Step 1", Description = "This is step 1", Order = 1 },
            new WalkthroughStep { Title = "Step 2", Description = "This is step 2", Order = 2 },
            // Add more steps here
        };

        [HttpGet]
        public IActionResult GetSteps()
        {
            return Ok(Steps);
        }
    }
}
