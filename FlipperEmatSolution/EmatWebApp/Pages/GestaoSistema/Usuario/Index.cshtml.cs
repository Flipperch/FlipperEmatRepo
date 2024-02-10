using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmatWebApp.Pages.GestaoSistema.Usuario
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public string Message { get; set; } = "Request Initiation Waiting...";

        public async void OnPostCallAPI()
        {
            string Baseurl = "https://localhost:7098/WeatherForecast";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(Baseurl);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("SecureApiKey", "12345");
                    HttpResponseMessage response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        //API call success, Do your action
                    }

                    else
                    {
                        //API Call Failed, Check Error Details
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
