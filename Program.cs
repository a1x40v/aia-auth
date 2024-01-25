using System.Text;
using AiaAuth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<AuthService>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecret"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/products", (ProductService productService) =>
{
    return productService.GetProducts();
}).RequireAuthorization();

app.MapGet("/login", (AuthService authService) =>
{
    return authService.CreateToken("abcdef", "test@example.com");
});

app.Run();


