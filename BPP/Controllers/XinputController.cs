using System;
using bpp.Helpers;
using bpp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bpp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XinputController : ControllerBase
    {
        XinputHandler _inputHandler;
        public XinputController(XinputHandler xinputHandler)
        {
            _inputHandler = xinputHandler;
        }

        [HttpGet]
        [Route("formid/{id}")] // to get the XInput Form, ID will be Job ID 
        public string Form(string id)
        {
            Console.WriteLine("request for Xinput form");
            return _inputHandler.BuildXinput(id);

        }

        [HttpPost]
        [Route("submit/{id}")]
        public string SubmitForm(string id, [FromBody] XinputData xinputData)
        {
            Console.WriteLine("Submission for Xinput form");
            return _inputHandler.SaveXinputData(id, xinputData);
        }
    }
}

