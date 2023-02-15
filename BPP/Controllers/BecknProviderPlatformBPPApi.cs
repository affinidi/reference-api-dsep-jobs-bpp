using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using bpp.Attributes;
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
        [ValidateModelState]
        [SwaggerOperation("SearchPost")]
        // [SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult SearchPost([FromBody] SearchBody body)
        {
            Task.Run(() =>
           {
               _ = _searchHandler.SearchAndReply(body);
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
<<<<<<< Updated upstream

            _selectHandler.SelectAndReply(body);
=======
            Task.Run(() =>
            {
                _ = _selectHandler.SelectAndReply(body);
            }).ConfigureAwait(false);

>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            _ = _confirmHandler.SaveApplication(body);
=======
            Task.Run(() =>
            {
                _ = _confirmHandler.SaveApplication(body);
            }).ConfigureAwait(false);

>>>>>>> Stashed changes

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Initialize an order by providing billing and/or shipping details</remarks>
<<<<<<< Updated upstream
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



=======
        /// <param name = "body" ></param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/init")]
        [ValidateModelState]
        [SwaggerOperation("InitPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult InitPost([FromBody] InitBody body)
        {
            Task.Run(() =>
            {
                _ = _InitHandler.InitJobApplication(body);
            }).ConfigureAwait(false);
>>>>>>> Stashed changes



        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Fetch the latest order object</remarks>
        /// <param name="body"></param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/status")]
        [ValidateModelState]
        [SwaggerOperation("StatusPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult StatusPost([FromBody] StatusBody body)
        {
<<<<<<< Updated upstream
            _stausHandler.SendStatus(body);
=======
            Task.Run(() =>
            {
                _ = _stausHandler.SendStatus(body);
            }).ConfigureAwait(false);

>>>>>>> Stashed changes

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }


    }
}
