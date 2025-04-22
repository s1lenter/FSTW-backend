namespace FSTW_backend.Middlewares
{
    public class TokenHeaderMiddleware
    {
        RequestDelegate _next;
        public TokenHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("token", out string? token))
                context.Request.Headers.Authorization = $"Bearer {token}";
            await _next.Invoke(context);
        }
    }
}
