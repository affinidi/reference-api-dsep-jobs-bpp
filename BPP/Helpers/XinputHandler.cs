using System;
using System.IO;
using System.Net.Http;
using System.Text;
using bpp.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace bpp.Helpers
{
    public class XinputHandler
    {
        ILogger _logger;
        public XinputHandler(ILoggerFactory loggerfactory)
        {
            _logger = loggerfactory.CreateLogger<XinputHandler>();
        }

        internal string BuildXinput(string id)
        {
            //id input can be used to create JOb specific Xinput, currently its generic TODO: form dynamic Xinput form
            var xForm = File.ReadAllText("StaticFiles/Xinput.txt");
            // This will be the ID given to BAP if they submit form
            // It wil be saved separately with Job ID On Init or confirm with JOb id and form ID
            var XinputFormID = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            var xinputData = new XinputData() { jobId = id, xinputFormID = XinputFormID };
            SaveXinput(xinputData);

            xForm = xForm.Replace("xinputformurl", Environment.GetEnvironmentVariable("bpp_Xinput_url") + "/submit/" + XinputFormID, StringComparison.CurrentCultureIgnoreCase);
            return xForm;
        }

        internal string SaveXinputData(string id, XinputData xinputData)
        {
            //TODO: Save input data with
            xinputData.xinputFormID = id;
            SaveXinput(xinputData);

            return id;
        }

        private void SaveXinput(XinputData xinputData)
        {

            HttpResponseMessage response = null;
            try
            {
                var json = JsonConvert.SerializeObject(xinputData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogInformation(" Saving Xinput for job id : {0}, XinputFomr id : {1}, {2}", xinputData.jobId, xinputData.xinputFormID, json);

                var url = Environment.GetEnvironmentVariable("save_xinput_url")?.ToString();
                url = url + "/save";
                using var client = new HttpClient();

                response = client.PostAsync(url, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation("Saved Xinput data Job id  :  {0}", xinputData.jobId);


                }
                else
                {
                    _logger.LogError("Error in saving Xinput data Job id  :  {0}", xinputData.jobId);
                    _logger.LogError("Error in saving Xinput data Job id  :  {0}", response.ReasonPhrase);
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Error While saving application");
                _logger.LogError(e.Message);
                _logger.LogError(response?.Content.ReadAsStringAsync().Result);
                throw e;

            }
        }
    }
}

