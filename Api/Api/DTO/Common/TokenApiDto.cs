namespace Api.DTO.Common;

public class TokenApiDto // send 2 things to user if login succ
{
    public string AccessToken { get; set; } = string.Empty; // my JWT token
    // public string RefeshToken { get; set; } = string.Empty; - có vẻ sẽ ko cần dùng đến
}
