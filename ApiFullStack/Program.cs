using System.Security.Claims;
using System.Text;
using ApiFullStack;
using ApiFullStack.Endpoints;
using ApiFullStack.Infra;
using ApiFullStack.Models;
using ApiFullStack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var root = AppDomain.CurrentDomain.BaseDirectory;
var dotenv = Path.Combine(root, ".env");
var dotEnv = new DotEnv();
dotEnv.CarregarCaminho(dotenv);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>();
builder.Services.AddSingleton<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
builder.Services.AddSingleton<TokenService>();


builder.Services.AddCors();

builder.Services.AddAuthentication(x => {

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(x => {

    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Config.Instancia.ChavePrivada ?? "")),

        ValidateIssuer = false,
        ValidateAudience = false
    };

    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = ctx =>
        {
            ctx.Request.Cookies.TryGetValue("acessToken", out var acessToken);
            if(!string.IsNullOrEmpty(acessToken))
                ctx.Token = acessToken;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", x => x.RequireRole("admin"))
    .AddPolicy("cliente", x => x.RequireRole("cliente"))
    .AddPolicy("adminOuCliente", policy => policy.RequireRole("admin", "cliente"));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .WithOrigins("http://localhost:5173") // Specify allowed origins
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials() // Allow credentials
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AdicionarEndpointUsuarios();
app.AdicionarEndpointsProdutos();
app.AdicionarLoginEndpoint();
app.AdicionarEndpointsGalpoes();

app.Run();