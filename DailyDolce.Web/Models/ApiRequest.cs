using static DailyDolce.Web.SD;

namespace DailyDolce.Web.Models {
    public class ApiRequest {
        public object Data { get; set; }
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public string AccessToken { get; set; }
    }
    
}
