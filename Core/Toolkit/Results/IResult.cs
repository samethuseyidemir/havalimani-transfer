using System.Text.Json.Serialization;

namespace Core.Toolkit.Results
{
    public interface IResult
    {
        [JsonIgnore]
        bool IsSuccess { get; }
        string Message { get; }
        Error Error { get; }
    }
}
