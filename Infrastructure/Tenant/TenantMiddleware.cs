namespace REAL_ESTATE_CLEAN.Infrastructure.Tenant;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var tenant = context.Request.Headers["X-Tenant"].FirstOrDefault();

        context.Items["TenantId"] = tenant ?? "1";

        await _next(context);
    }
}
