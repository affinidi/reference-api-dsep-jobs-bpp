using System;
using bpp.Helpers;
using bpp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bpp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XinputController : ControllerBase
    {
        XinputHandler _inputHandler;
        ILogger _logger;
        public XinputController(XinputHandler xinputHandler, ILoggerFactory loggerfactory)
        {
            _inputHandler = xinputHandler;
            _logger = loggerfactory.CreateLogger<XinputController>();
        }

        [HttpGet]
        [Route("formid/{id}")] // to get the XInput Form, ID will be Job ID 
        public string Form(string id)
        {
            _logger.LogInformation("request for Xinput form");
            return _inputHandler.BuildXinput(id);

        }

        [HttpPost]
        [Route("submit/{id}")]
        public string SubmitForm(string id, [FromBody] XinputData xinputData)
        {
            _logger.LogInformation("Submission for Xinput form");
            return _inputHandler.SaveXinputData(id, xinputData);
        }
    }
}

