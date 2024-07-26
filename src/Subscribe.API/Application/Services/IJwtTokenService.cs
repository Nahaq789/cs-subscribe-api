namespace Subscribe.API.Application.Services;

/// <summary>
/// JWTトークンを生成するサービスです。
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// JWTトークンを復元し認証
    /// </summary>
    /// <param name="token">jwtトークン</param>
    bool ValidateJwtToken(string token);
}