

namespace Core.Toolkit.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data,0, false, message)
        {
        }

        public ErrorDataResult(T data) : base(data,0, false,ResultMessages.OperationUnSuccess)
        {
        }

        public ErrorDataResult(string message) : base(default,0, false, message)
        {
        }


        public ErrorDataResult() : base(default,0, false, ResultMessages.OperationUnSuccess)
        {
        }
    }
}
