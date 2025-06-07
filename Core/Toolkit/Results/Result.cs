using System.Text.Json.Serialization;
namespace Core.Toolkit.Results
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            IsSuccess = success;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        [JsonIgnore]
        public bool IsSuccess { get; }

        public string Message { get; }

        public Error Error
        {
            get
            {
                if (!IsSuccess)
                    return new Error
                    {
                        Message = Message
                    };
                return null;
            }
        }
    }
}
