namespace Playground.SimpleResults;

public class ResultExtensions
{
    
}


public static class HttpResult
{

    private const int NotFoundCode = 404;
    public static Result NotFound(string message) => Result.Fail(message, NotFoundCode);
    
    public static Result<T> NotFound<T>(string message) => Result.Fail<T>(message, NotFoundCode);

    public static Result<T> NotFound<T>(T value, string message) => Result.Fail<T>(value, message, NotFoundCode);

    private const int UnauthorisedCode = 401;
    public static Result Unauthorised(string message) => Result.Fail(message, UnauthorisedCode);

    public static Result<T> Unauthorised<T>(string message) => Result.Fail<T>(message, UnauthorisedCode);

    public static Result<T> Unauthorised<T>(T value, string message) => Result.Fail<T>(value, message, UnauthorisedCode);

    private const int ForbiddenCode = 403;
    public static Result Forbidden(string message) => Result.Fail(message, ForbiddenCode);

    public static Result<T> Forbidden<T>(string message) => Result.Fail<T>(message, ForbiddenCode);

    public static Result<T> Forbidden<T>(T value, string message) => Result.Fail<T>(value, message, ForbiddenCode);


    private const int BadRequestCode = 400;
    public static Result BadRequest(string message) => Result.Fail(message, BadRequestCode);

    public static Result<T> BadRequest<T>(string message) => Result.Fail<T>(message, BadRequestCode);

    public static Result<T> BadRequest<T>(T value, string message) => Result.Fail<T>(value, message, BadRequestCode);


    private const int CreatedCode = 201;
    public static Result Created(string message) => Result.Ok(message, CreatedCode);
    public static Result<T> Created<T>(T value) => Created(value, string.Empty);
    public static Result<T> Created<T>(T value, string message) => Result.Ok<T>(value, message, CreatedCode);

    private const int OkayCode = 200;
    public static Result Okay(string message) => Result.Ok(message, OkayCode);
    
    public static Result<T> Okay<T>(T value) => Okay(value, string.Empty);
    public static Result<T> Okay<T>(T value, string message) => Result.Ok<T>(value, message, OkayCode);
}