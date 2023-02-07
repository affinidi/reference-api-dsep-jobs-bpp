
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using bpp.Attributes;
using bpp.Security;
using Microsoft.AspNetCore.Authorization;

using bpp.Helpers;
using System.Threading.Tasks;
using Beckn.Models;

namespace bpp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    //[SignResponse]
    public class BecknProviderPlatformBPPApiController : ControllerBase
    {

        private SearchHandler _searchHandler;
        private SelectHandler _selectHandler;
        private ConfirmHandler _confirmHandler;
        private StausHandler _stausHandler;

        public BecknProviderPlatformBPPApiController(SearchHandler searchHandler, SelectHandler selectHandler, ConfirmHandler confirmHandler, StausHandler stausHandler)
        {
            _searchHandler = searchHandler;
            _selectHandler = selectHandler;
            _confirmHandler = confirmHandler;
            _stausHandler = stausHandler;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This allows a user to search for Jobs and Internships</remarks>
        /// <param name="body">BAP searches for services</param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/search")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        // [SignResponse]
        [SwaggerOperation("SearchPost")]
        // [SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult SearchPost([FromBody] SearchBody body)
        {

            Task.Run(() =>
           {
               _searchHandler.SearchAndReply(body);
           }).ConfigureAwait(false);

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Select items from the catalog and build your order</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/select")]

        [ValidateModelState]
        [SwaggerOperation("SelectPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult SelectPost([FromBody] SelectBody body)
        {

            _selectHandler.SelectAndReply(body);

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        [HttpPost]
        [Route("/confirm")]

        [ValidateModelState]
        [SwaggerOperation("ConfirmPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult ConfirmPost([FromBody] ConfirmBody body)
        {
            _ = _confirmHandler.SaveApplication(body);

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Initialize an order by providing billing and/or shipping details</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        //[HttpPost]
        //[Route("/init")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        //[SwaggerOperation("InitPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        //public virtual IActionResult InitPost([FromBody] InitBody body)
        //{
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(InlineResponse200));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"message\" : {\n    \"ack\" : {\n      \"status\" : \"ACK\"\n    }\n  },\n  \"error\" : {\n    \"path\" : \"path\",\n    \"code\" : \"code\",\n    \"type\" : \"CONTEXT-ERROR\",\n    \"message\" : \"message\"\n  }\n}";

        //    var example = exampleJson != null
        //    ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
        //    : default(InlineResponse200);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}






        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Fetch the latest order object</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/status")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        [SwaggerOperation("StatusPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult StatusPost([FromBody] StatusBody body)
        {
            _stausHandler.SendStatus(body);

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Provide feedback on a service</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        //[HttpPost]
        //[Route("/rating")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        //[SwaggerOperation("RatingPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        //public virtual IActionResult RatingPost([FromBody]RatingBody body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(InlineResponse200));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"message\" : {\n    \"ack\" : {\n      \"status\" : \"ACK\"\n    }\n  },\n  \"error\" : {\n    \"path\" : \"path\",\n    \"code\" : \"code\",\n    \"type\" : \"CONTEXT-ERROR\",\n    \"message\" : \"message\"\n  }\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
        //                : default(InlineResponse200);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Cancel an order</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        //[HttpPost]
        //[Route("/cancel")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        //[SwaggerOperation("CancelPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        //public virtual IActionResult CancelPost([FromBody]CancelBody body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(InlineResponse200));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"message\" : {\n    \"ack\" : {\n      \"status\" : \"ACK\"\n    }\n  },\n  \"error\" : {\n    \"path\" : \"path\",\n    \"code\" : \"code\",\n    \"type\" : \"CONTEXT-ERROR\",\n    \"message\" : \"message\"\n  }\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
        //                : default(InlineResponse200);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Initialize an order by providing billing and/or shipping details</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        ///



        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Contact support</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        //[HttpPost]
        //[Route("/support")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        //[SwaggerOperation("SupportPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        //public virtual IActionResult SupportPost([FromBody]SupportBody body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(InlineResponse200));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"message\" : {\n    \"ack\" : {\n      \"status\" : \"ACK\"\n    }\n  },\n  \"error\" : {\n    \"path\" : \"path\",\n    \"code\" : \"code\",\n    \"type\" : \"CONTEXT-ERROR\",\n    \"message\" : \"message\"\n  }\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
        //                : default(InlineResponse200);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Track an active order</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        //[HttpPost]
        //[Route("/track")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        //[SwaggerOperation("TrackPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        //public virtual IActionResult TrackPost([FromBody]TrackBody body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(InlineResponse200));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"message\" : {\n    \"ack\" : {\n      \"status\" : \"ACK\"\n    }\n  },\n  \"error\" : {\n    \"path\" : \"path\",\n    \"code\" : \"code\",\n    \"type\" : \"CONTEXT-ERROR\",\n    \"message\" : \"message\"\n  }\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
        //                : default(InlineResponse200);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Remove object</remarks>
        /// <param name="body">TODO</param>
        /// <response code="200">Acknowledgement of message received</response>
        //[HttpPost]
        //[Route("/update")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        //[ValidateModelState]
        //[SwaggerOperation("UpdatePost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        //public virtual IActionResult UpdatePost([FromBody]UpdateBody body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(InlineResponse200));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"message\" : {\n    \"ack\" : {\n      \"status\" : \"ACK\"\n    }\n  },\n  \"error\" : {\n    \"path\" : \"path\",\n    \"code\" : \"code\",\n    \"type\" : \"CONTEXT-ERROR\",\n    \"message\" : \"message\"\n  }\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<InlineResponse200>(exampleJson)
        //                : default(InlineResponse200);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}
    }
}
