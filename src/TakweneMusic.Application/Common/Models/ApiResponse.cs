namespace TakweneMusic.Application.Common.Models;

public class ApiResponse<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public T? Data { get; }

    internal ApiResponse(bool isSuccess, string message, T? data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> Success(T data, string message = "Request completed successfully.")
    {
        return new ApiResponse<T>(true, message, data);
    }

    public static ApiResponse<T> Failure(string message, T? data = default)
    {
        return new ApiResponse<T>(false, message, data);
    }
}

public static class ApiResponse
{
    public static ApiResponse<T> Success<T>(T data, string message = "Request completed successfully.")
    {
        return ApiResponse<T>.Success(data, message);
    }

    public static ApiResponse<T> Failure<T>(string message, T? data = default)
    {
        return ApiResponse<T>.Failure(message, data);
    }
}
