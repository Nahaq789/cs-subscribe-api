using MediatR;
using Subscribe.API.Application.Services;
using Subscribe.API.Exceptions;

namespace Subscribe.API.Application.Behaviors;

public class JwtTokenAuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public JwtTokenAuthenticationBehavior(IJwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
    {
        _jwtTokenService = jwtTokenService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string? token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(token) || !_jwtTokenService.ValidateJwtToken(token)) throw new JwtTokenException("トークンが無効です。");
        return await next();
    }
}