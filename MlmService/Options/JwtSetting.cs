namespace MlmService.Options;

public class JwtSettings
{
    public string Secret { get; set; }
    public string RefreshSecret { get; set; }
    public TimeSpan TokenLifetime { get; set; }
}