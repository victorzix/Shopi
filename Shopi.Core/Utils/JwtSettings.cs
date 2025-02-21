namespace Shopi.Core.Utils;

public class JwtSettings
{
    public string Secret { get; set; }
    public int ExpireTime { get; set; }
    public string Emitter { get; set; }
    public string Audit { get; set; }
}