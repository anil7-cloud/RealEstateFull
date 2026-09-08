using System.IdentityModel.Tokens.Jwt;

namespace REAL_ESTATE_CLEAN.Infrastructure.Auth;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("No token");
            return;
        }

        context.Items["UserId"] = "1";

        await _next(context);
    }
}
