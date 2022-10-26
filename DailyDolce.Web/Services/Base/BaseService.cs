using DailyDolce.Web.Dtos;
using DailyDolce.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DailyDolce.Web.Services.Base {
    public class BaseService : IBaseService {
        private readonly IHttpClientFactory _httpClient;

        public BaseService(IHttpClientFactory httpClient) {
            _httpClient = httpClient;
        }

        public async Task<T> SendRequest<T>(ApiRequest apiRequest) {

            try {

                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null) {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8,
                        "application/json");
                }

                switch (apiRequest.ApiType) {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                var client = _httpClient.CreateClient("DailyDolceApi");
                client.DefaultRequestHeaders.Clear();
                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);

                HttpResponseMessage apiResponseMessage = await client.SendAsync(message);

                var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponse;

            } catch (Exception ex) {

                var response = new ResponseDto() {
                    Success = false,
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>() { ex.Message }
                };

                var serResponse = JsonConvert.SerializeObject(response);
                var apiResponse = JsonConvert.DeserializeObject<T>(serResponse);

                return apiResponse;
            }
        }

        public void Dispose() {
            GC.SuppressFinalize(true);
        }
    }
}
