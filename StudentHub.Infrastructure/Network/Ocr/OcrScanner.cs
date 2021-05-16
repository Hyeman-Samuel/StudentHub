using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Infrastructure.Network.Ocr
{
    public class OcrScanner : IOcrScanner
    {
        private readonly OcrConfiguration _ocrConfiguration;
        public OcrScanner(OcrConfiguration ocrConfiguration)
        {
            _ocrConfiguration = ocrConfiguration;
        }

        public async Task<string> RunImageOcrScan(string link)
        {
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("Content-type", "application/json");
                httpClient.DefaultRequestHeaders.Add(Constants.AppId, _ocrConfiguration.AppId);
                httpClient.DefaultRequestHeaders.Add(Constants.AppKey, _ocrConfiguration.AppKey);
                var body = new OcrScanRequestBody { src = link };
               HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync<OcrScanRequestBody>(new Uri(Constants.OcrApiDomainV3),body);
                if (responseMessage.IsSuccessStatusCode)
                {
                    OcrScanResponseBody ocrScanResponseBody = await responseMessage.Content.ReadFromJsonAsync<OcrScanResponseBody>();
                    //Get Response body
                    return "latex";
                }
                else
                {
                    return null;
                }
                //return null ;
            }
        }
    }
}
