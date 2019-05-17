using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataService.Model.ViewModel.Enums;

namespace Reso.Infrastructure.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        
        [HttpGet("application-origin")]
        public IActionResult GetApplicationOriginEnum()
        {
            return Ok(Enums.Get<ApplicationOriginEnum>());
        }

        [HttpGet("application-type")]
        public IActionResult GetApplicationTypeEnum()
        {
            return Ok(Enums.Get<ApplicationTypeEnum>());
        }

        [HttpGet("application-status")]
        public IActionResult GetApplicationStatusEnum()
        {
            return Ok(Enums.Get<ApplicationStatusEnum>());
        }

        [HttpGet("application-stage")]
        public IActionResult GetApplicationStageEnum()
        {
            return Ok(Enums.Get<ApplicationStageEnum>());
        }

        [HttpGet("application-category")]
        public IActionResult GetApplicationCategoryEnum()
        {
            return Ok(Enums.Get<ApplicationCategoryEnum>());
        }
    }
}