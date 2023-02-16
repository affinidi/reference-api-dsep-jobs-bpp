using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using search.Models;

namespace search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XinputController : ControllerBase
    {
        OpensearchHandler _opensearchHandler;
        public XinputController(OpensearchHandler opensearchHandler)
        {
            _opensearchHandler = opensearchHandler;
        }

        [HttpPost]
        [Route("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Post(XinputData xinputData)
        {
            if (string.IsNullOrEmpty(xinputData.xinputFormID))
            {
                return StatusCode(400, xinputData);
            }
            else
            {
                var result = _opensearchHandler.SaveXinput(xinputData);
                return Ok(result);
            }
        }
    }
}

