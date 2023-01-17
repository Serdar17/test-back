namespace Web.Middleware;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Session.GetString("Token"); 
        if (!string.IsNullOrEmpty(token))  
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        await _next(context);
    }
}