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
        private InitHandler _initHandler;

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
            _initHandler = initHandler;
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        [SwaggerOperation("SelectPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult SelectPost([FromBody] SelectBody body)
        {

            Task.Run(() =>
            {
                _ = _selectHandler.SelectAndReply(body);
            }).ConfigureAwait(false);



            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        [HttpPost]
        [Route("/confirm")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        [SwaggerOperation("ConfirmPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult ConfirmPost([FromBody] ConfirmBody body)
        {

            Task.Run(() =>
            {
                _ = _confirmHandler.SaveApplication(body);
            }).ConfigureAwait(false);



            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Initialize an order by providing billing and/or shipping details</remarks>
        /// <param name = "body" ></param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/init")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        [SwaggerOperation("InitPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult InitPost([FromBody] InitBody body)
        {
            Task.Run(() =>
            {
                _ = _initHandler.InitJobApplication(body);
            }).ConfigureAwait(false);

            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Fetch the latest order object</remarks>
        /// <param name="body"></param>
        /// <response code="200">Acknowledgement of message received</response>
        [HttpPost]
        [Route("/status")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        [SwaggerOperation("StatusPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Acknowledgement of message received")]
        public virtual IActionResult StatusPost([FromBody] StatusBody body)
        {

            Task.Run(() =>
            {
                _ = _stausHandler.SendStatus(body);
            }).ConfigureAwait(false);



            var response = new Ack() { Status = Ack.StatusEnum.ACKEnum };
            return new ObjectResult(response);
        }


    }
}
