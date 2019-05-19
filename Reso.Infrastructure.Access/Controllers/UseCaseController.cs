using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Model.RequestModel;
using DataService.Service;
using DataService.Service.ServiceAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reso.Infrastructure.Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseCaseController : ControllerBase
    {
        private IServiceProvider _serviceProvider;
        private IUseCaseService _useCaseService;
        public UseCaseController(IUseCaseService useCaseService, IServiceProvider serviceProvider)
        {
            _useCaseService = useCaseService;
            _serviceProvider = serviceProvider;
        }
        
        [HttpGet]
        public IActionResult Get([FromQuery]UseCaseRequestModel requestModel)
        {
            try
            {
                var result = _useCaseService.Get(requestModel);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(UseCaseCreateModel createModel)
        {
            try
            {
                var applicationService = ServiceFactory.CreateService<IApplicationService>(_serviceProvider);
                if (applicationService.GetById(createModel.ApplicationId, null, null, null) == null)
                {
                    return BadRequest(new { error = "Application Id is not Existed" });
                }

                if (_useCaseService.GetByName(createModel.UseCaseName, createModel.ApplicationId, null, null) != null)
                {
                    return BadRequest(new { error = "UseCase Name with this Application Id is Existed" });
                }
                var result = _useCaseService.Create(createModel);
                if(result == null)
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

        [HttpPut]
        public IActionResult Put(UseCaseUpdateModel updateModel)
        {
            try
            {
                var applicationService = ServiceFactory.CreateService<IApplicationService>(_serviceProvider);
                if (applicationService.GetById(updateModel.ApplicationId, null, null, null) == null)
                {
                    return BadRequest(new { error = "Application Id is not Existed" });
                }

                if (_useCaseService.GetById(updateModel.UseCaseId, updateModel.ApplicationId, null, null) == null)
                {
                    return BadRequest(new { error = "UseCase Name with this Application Id is not Existed" });
                }
                var result = _useCaseService.Update(updateModel);
                if (result == null)
                {
                    return BadRequest(new { error = "Update UseCase Fail!" });
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