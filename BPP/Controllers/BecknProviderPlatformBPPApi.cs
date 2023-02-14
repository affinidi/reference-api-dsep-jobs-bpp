
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
        private InitHandler _InitHandler;

        public BecknProviderPlatformBPPApiController(SearchHandler searchHandler,
            SelectHandler selectHandler,
            ConfirmHandler confirmHandler,
            StausHandler stausHandler,
            InitHandler initHandler)
        {
            _searchHandler = searchHandler;
            _selectHandler = selectHandler;
            _confirmHandler = confirmHandler;
            _stausHandler = stausHandler;
            _InitHandler = initHandler;
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

            Task.Run(() =>
            {
                _selectHandler.SelectAndReply(body);
            }).ConfigureAwait(false);


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
            Task.Run(() =>
            {
                _confirmHandler.SaveApplication(body);
            }).ConfigureAwait(false);


            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Initialize an order by providing billing and/or shipping details</remarks>
        /// <param name = "body" > TODO </ param >
        /// < response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/init")]
        // [Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.SchemeName)]
        // [ValidateModelState]
        [SwaggerOperation("InitPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult InitPost([FromBody] InitBody body)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse200));

            Task.Run(() =>
            {
                _InitHandler.InitJobApplication(body);
            }).ConfigureAwait(false);

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }


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
            Task.Run(() =>
            {
                _stausHandler.SendStatus(body);
            }).ConfigureAwait(false);


            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }







    }
}
