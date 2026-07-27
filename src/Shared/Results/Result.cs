namespace DemoMVC.Shared.Results;

public class Result
{
    public bool Success { get; }
    public bool IsFailure => !Success;
    public IReadOnlyList<string> Errors { get; }
    protected Result(bool success, IEnumerable<string>? errors = null)
    {
        Success = success;
        Errors = errors?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
    }
    public static Result Ok()
        => new(true);
    public static Result Fail(params string[] errors)
        => new(false, errors.Length > 0 ? errors : new[] { "Ocorreu um erro inesperado." });
    public static Result Fail(IEnumerable<string> errors)
        => new(false, errors);
}
public class Result<T> : Result
{
    public T Data { get; }
    private Result(T data) : base(true)
    {
        Data = data;
    }
    private Result(IEnumerable<string> errors) : base(false, errors)
    {
        Data = default!;
    }
    public static Result<T> Ok(T data)
        => data is not null
            ? new Result<T>(data)
            : throw new ArgumentException(nameof(data), "É necessário um valor válido.");
    public new static Result<T> Fail(params string[] errors)
        => new(errors);
    public new static Result<T> Fail(IEnumerable<string> errors)
        => new(errors);
}