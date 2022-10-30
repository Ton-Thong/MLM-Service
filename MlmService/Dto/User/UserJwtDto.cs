using Newtonsoft.Json;

namespace MlmService.Dto.User;
public class UserJwtDto
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("tenantId")]
    public Guid TenantId { get; set; }
}