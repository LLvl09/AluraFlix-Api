using System.Text;
using AluraFlix.Data;
using AluraFlix.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//adicionando automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//adicionando parametros no escopo
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<VideoService>();
//Configurando banco de dados
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseMySql("Server=localhost;User ID=root;Password=ads.Microsoft2;Database=AluraFlix", ServerVersion.Parse("8.0.29")));

//Adicionando autenticacao e configurando autorizacao
builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("regular", policy => policy.RequireRole("regular"));
});
var app = builder.Build();

app.UseHttpsRedirection();
//deixan o /Video/Free como anonimo
//adicionando autorizacao
app.Map("/Video", () => { Results.Ok(); }).RequireAuthorization("admin");
app.Map("/Categoria", () => { Results.Ok(); }).RequireAuthorization("admin");
//adicionando autenticacao e autorizacao
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
