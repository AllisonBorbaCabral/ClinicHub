namespace DemoMVC.Shared.Results;

public class Result
{
    public bool Success { get; protected set; }
    public List<string> Errors { get; protected set; } = new();

    public static Result Ok()
        => new() { Success = true };

    public static Result Fail(params string[] errors)
        => new()
        {
            Success = false,
            Errors = errors.ToList()
        };

    public static Result Fail(IEnumerable<string> errors)
        => new()
        {
            Success = false,
            Errors = errors.ToList()
        };
}

public class Result<T> : Result
{
    public T? Data { get; private set; }

    public static Result<T> Ok(T data)
        => new()
        {
            Success = true,
            Data = data
        };

    public new static Result<T> Fail(params string[] errors)
        => new()
        {
            Success = false,
            Errors = errors.ToList()
        };
}