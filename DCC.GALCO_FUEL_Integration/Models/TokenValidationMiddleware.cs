namespace DCC.GALCO_FUEL_Integration.Models
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _staticToken;
        private readonly string _connectionString;
        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _staticToken = configuration["Token:StaticToken"];
            _connectionString = configuration.GetConnectionString("DBCooonection");
        }

        public async Task InvokeAsync(HttpContext context)
        {
           // string statctoken = "Bearer asdfghjklMNBVCXZ1234567890poiuytrewqplmoknijbuhvygctfxrdzesawq"
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            if (token != _staticToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid or missing token." });
                return;
            }
            await _next(context);
        }
    }

}
