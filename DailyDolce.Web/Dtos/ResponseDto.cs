namespace DailyDolce.Web.Dtos
{
    public class ResponseDto
    {
        public object Data { get; set; }
        public bool Success { get; set; } = true;
        public string DisplayMessage { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
