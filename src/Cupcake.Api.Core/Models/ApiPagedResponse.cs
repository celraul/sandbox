namespace Cupcake.Api.Core.Models;

public class ApiPagedResponse<T> : ApiResponse<T>
{
    public int Total { get; set; }
    public int PageSize { get; set; }

    public ApiPagedResponse(T data, int total, int pageSize, bool success = true)
    {
        Data = data;
        Total = total;
        PageSize = pageSize;
        Success = success;
    }
}
