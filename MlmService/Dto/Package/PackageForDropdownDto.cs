using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MlmService.Dto.Package
{
    public class PackageForDropdownDto
    {
        [Required]
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
