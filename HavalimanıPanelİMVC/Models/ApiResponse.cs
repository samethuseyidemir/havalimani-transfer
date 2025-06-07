using System.Text.Json.Serialization;

namespace HavalimaniPanelMVC.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("resultObject")]
        public T Data { get; set; }

        [JsonPropertyName("resultCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        public bool IsSuccess => Error == null;
    }
}
