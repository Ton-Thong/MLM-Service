using MlmService.Exceptions;
using Newtonsoft.Json;

namespace MlmService.Pagination;

public class PaginationFilter
{
    [JsonProperty("pageNumber")]
    public int PageNumber { get; set; }
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }
}