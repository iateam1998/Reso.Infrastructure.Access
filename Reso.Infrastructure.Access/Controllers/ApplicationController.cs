using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Model.RequestModel;
using DataService.Service.ServiceAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reso.Infrastructure.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]ApplicationRequestModel requestModel)
        {
            try
            {
                var result = _applicationService.Get(requestModel);
                return Ok(new { data = result});
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post(ApplicationCreateModel createModel)
        {
            try
            {
                if (_applicationService.GetByName(createModel.ApplicationName, createModel.Stage, createModel.Category, null) != null)
                {
                    return BadRequest(new { error = "This Application is Existed"});
                }
                var result = _applicationService.Create(createModel);
                if (result == null)
                {
                    return BadRequest(new { error = "Create UseCase Fail!" });
                }
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}