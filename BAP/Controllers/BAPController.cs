using System;
using System.Collections.Generic;
using BAP.Models;
using BAP.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BAPController: ControllerBase
    {
		DataRetrievalService _dataRetrievalService;
        public BAPController(DataRetrievalService dataRetrievalService)
		{
			_dataRetrievalService = dataRetrievalService;
		}

        [EnableCors]
        [HttpGet]
		[Route("retrieve/operation={operation}&transationid={transationid}&messageid={messageid}")]
		public List<Job> GetBppData(string operation, string transationid, string messageid)
		{
			return _dataRetrievalService.GetDataForTransactionID(operation,transationid,messageid);
		}

        [EnableCors]
        [HttpPost]
        [Route("searchdsep")]
        public string SearchBPP(EUAPayload payload )
        {
            return _dataRetrievalService.SearchOnBPP(payload);
        }

        [EnableCors]
        [HttpPost]
        [Route("selectdsep")]
        public string SelectBPP(EUAPayload payload)
        {
            return _dataRetrievalService.SelectOnBPP(payload);
        }

        [EnableCors]
        [HttpPost]
        [Route("confirmDsep")]
        public string ConfirmDsep (EUAPayload payload)
        {
            return _dataRetrievalService.ConfirmOnDsep(payload);
        }
    }
}

