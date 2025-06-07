
namespace Core.Toolkit.Results
{
    public interface IDataResult<out T> : IResult
    {
        T ResultObject { get; }

        int ResultCount { get; }
    }
}
