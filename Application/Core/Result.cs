namespace Application.Core;

public class Result<T>
{
    public T Value { get; set; }
    public bool IsSuccess { get; set; }
    public string Error { get; set; }

    public static Result<T> CreateSucces(T value) => new()
    {
        IsSuccess = true,
        Value = value
    };

    public static Result<T> CreateFailure(string error) => new()
    {
        IsSuccess = false,
        Error = error
    };
}
