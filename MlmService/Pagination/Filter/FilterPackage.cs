using Newtonsoft.Json;

namespace MlmService.Pagination.Filter;

public class FilterPackage : PaginationFilter
{
    [JsonProperty("searchName")]
    public string? SearchName { get; set; }
    [JsonProperty("searchCode")]
    public string? SearchCode { get; set; }
    [JsonProperty("searchStatus")]
    public string? SearchStatus { get; set; }
    [JsonProperty("searchAmount")]
    public decimal? SearchAmount { get; set; }
    [JsonProperty("searchPv")]
    public decimal? SearchPv { get; set; }
    [JsonProperty("searchStartDate")]
    public string? SearchStartDate { get; set; }
    [JsonProperty("searchEndDate")]
    public string? SearchEndDate { get; set; }

    [JsonProperty("sortName")]
    public string ? SortName { get; set; }
    [JsonProperty("sortCode")]
    public string? SortCode { get; set; }
    [JsonProperty("sortAmount")]
    public string? SortAmount { get; set; }
    [JsonProperty("sortPv")]
    public string? SortPv { get; set; }
    [JsonProperty("sortDate")]
    public string? SortDate { get; set; }
}