

namespace Core.Toolkit.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false, ResultMessages.OperationUnSuccess)
        {

        }

        public ErrorResult(string message) : base(false, message)
        {

        }
    }
}
