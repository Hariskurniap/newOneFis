namespace Common.Responses;

public class ApiResponse<T>
{
    public string StatusCode { get; set; } = "200";
    public string StatusDesc { get; set; } = "OK";
    public string Description { get; set; } = "Success";
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string description = "Data Found")
    {
        return new ApiResponse<T>
        {
            StatusCode = "200",
            StatusDesc = "OK",
            Description = description,
            Data = data
        };
    }

    public static ApiResponse<T> NotFound(string description = "Not Found")
    {
        return new ApiResponse<T>
        {
            StatusCode = "404",
            StatusDesc = "Not Found",
            Description = description,
            Data = default
        };
    }

    public static ApiResponse<T> Conflict(string description = "Conflict")
    {
        return new ApiResponse<T>
        {
            StatusCode = "409",
            StatusDesc = "Conflict",
            Description = description,
            Data = default
        };
    }
    public static ApiResponse<T> BadRequest(string description = "Bad Request")
    {
        return new ApiResponse<T>
        {
            StatusCode = "400",
            StatusDesc = "Bad Request",
            Description = description,
            Data = default
        };
    }

}
