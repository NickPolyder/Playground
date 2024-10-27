namespace Playground.SimpleResults;


/// <summary>
/// A result object to wrap responses from git.
/// </summary>
public class Result
{
    /// <summary>
    /// Information regarding the result.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Is the result successful ?
    /// </summary>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Is the result Failed ?
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// A code to explain the result.
    /// </summary>
    public int Code { get; private set; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="isSuccess"></param>
    /// <param name="message"></param>
    /// <param name="code"></param>
    protected Result(bool isSuccess, string message, int code)
    {
        Message = message;
        IsSuccess = isSuccess;
        Code = code;
    }

    /// <summary>
    /// Returns a successful result.
    /// </summary>
    /// <returns></returns>
    public static Result Ok() => Ok(string.Empty);

    /// <summary>
    /// Returns a successful result with a message and optionally a code.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="code">The code is based on <see cref="System.Net.HttpStatusCode"/></param>
    /// <returns></returns>
    public static Result Ok(string message, int code = 200)
    {
        return new Result(true, message, code);
    }

    /// <summary>
    /// Returns a failed result with a message and optionally a code.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="code">The code is based on <see cref="System.Net.HttpStatusCode"/></param>
    /// <returns></returns>
    public static Result Fail(string message, int code = 400)
    {
        return new Result(false, message, code);
    }
    /// <summary>
    /// Returns a successful result with a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Ok<T>(T value) => Ok(value, string.Empty);

    /// <summary>
    /// Returns a successful result with a value, message and optionally a code.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <param name="code">The code is based on <see cref="System.Net.HttpStatusCode"/></param>
    /// <returns></returns>
    public static Result<T> Ok<T>(T value, string message, int code = 200)
    {
        return new Result<T>(true, message, code, value);
    }

    /// <summary>
    /// Returns a failed result with a message and optionally a code.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="code">The code is based on <see cref="System.Net.HttpStatusCode"/></param>
    /// <returns></returns>
    public static Result<T> Fail<T>(string message, int code = 400)
    {
        return new Result<T>(false, message, code);
    }
    /// <summary>
    /// Returns a failed result with a message and optionally a code.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <param name="code">The code is based on <see cref="System.Net.HttpStatusCode"/></param>
    /// <returns></returns>
    public static Result<T> Fail<T>(T value, string message, int code = 400)
    {
        return new Result<T>(false, message, code, value);
    }
}

/// <summary>
/// A Generic Result with a value.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// True when the <see cref="Value"/> is populated.
    /// </summary>
    public bool HasValue { get; private set; }
    /// <summary>
    ///  the contained value.
    /// </summary>
    public T? Value { get; private set; }

    internal Result(bool isSuccessful, string message, int code)
        : base(isSuccessful, message, code)
    {
        HasValue = false;
    }

    internal Result(bool isSuccessful, string message, int code, T value)
        : base(isSuccessful, message, code)
    {
        HasValue = true;
        Value = value;
    }
}