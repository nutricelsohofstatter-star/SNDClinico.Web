
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SNDClinico.Web.Components;
using SNDClinico.Web.Components.Security;
using SNDClinico.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// CONEXÃO COM O POSTGRESQL
// =====================================================

var connectionString =
    builder.Configuration.GetConnectionString("SndDatabase");

if (string.IsNullOrWhiteSpace(connectionString))
{
    var databaseUrl =
        Environment.GetEnvironmentVariable("DATABASE_URL");

    if (!string.IsNullOrWhiteSpace(databaseUrl))
    {
        connectionString = ConverterDatabaseUrl(databaseUrl);
    }
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "A conexão com o PostgreSQL não foi configurada. " +
        "Configure ConnectionStrings:SndDatabase ou DATABASE_URL.");
}

// =====================================================
// ENTITY FRAMEWORK CORE
// =====================================================

builder.Services.AddDbContext<SndDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// =====================================================
// DATA PROTECTION
// =====================================================

builder.Services
    .AddDataProtection()
    .SetApplicationName("SNDClinico")
    .PersistKeysToDbContext<SndDbContext>();

// =====================================================
// ASP.NET CORE IDENTITY
// =====================================================

builder.Services
    .AddIdentity<SndIdentityUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 12;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan =
            TimeSpan.FromMinutes(15);

        options.Lockout.AllowedForNewUsers = true;

        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<SndDbContext>()
    .AddDefaultTokenProviders();

// =====================================================
// CONFIGURAÇÃO DOS COOKIES
// =====================================================

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "SNDClinico.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;

    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;

    options.LoginPath = "/acesso";
    options.AccessDeniedPath = "/acesso";
});

// =====================================================
// AUTORIZAÇÃO
// =====================================================

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddCascadingAuthenticationState();

// =====================================================
// LIMITAÇÃO DE TENTATIVAS DE LOGIN
// =====================================================

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("acesso", limiter =>
    {
        limiter.PermitLimit = 10;
        limiter.Window = TimeSpan.FromMinutes(5);
        limiter.QueueLimit = 0;
        limiter.AutoReplenishment = true;
    });
});

// =====================================================
// BLAZOR
// =====================================================

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// =====================================================
// CONSTRUÇÃO DA APLICAÇÃO
// =====================================================

var app = builder.Build();

// =====================================================
// PIPELINE HTTP
// =====================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRateLimiter();

app.UseAuthorization();

app.UseAntiforgery();

// =====================================================
// ENDPOINTS DE AUTENTICAÇÃO
// =====================================================

app.MapSndAccount();

// =====================================================
// ARQUIVOS E COMPONENTES
// =====================================================

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// =====================================================
// CRIAÇÃO DA CONTA INICIAL
// =====================================================

await AccountEndpoints.CriarContaInicialAsync(
    app.Services,
    app.Configuration,
    app.Logger);

// =====================================================
// INICIALIZAÇÃO
// =====================================================

app.Run();

// =====================================================
// CONVERSÃO DA URL DO RENDER PARA CONNECTION STRING
// =====================================================

static string ConverterDatabaseUrl(string databaseUrl)
{
    if (!Uri.TryCreate(
        databaseUrl,
        UriKind.Absolute,
        out var uri))
    {
        throw new InvalidOperationException(
            "A variável DATABASE_URL não contém uma URL PostgreSQL válida.");
    }

    var usuarioSenha = uri.UserInfo.Split(':', 2);

    if (usuarioSenha.Length != 2)
    {
        throw new InvalidOperationException(
            "A URL PostgreSQL não contém usuário e senha válidos.");
    }

    var connection = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.IsDefaultPort ? 5432 : uri.Port,
        Database = Uri.UnescapeDataString(
            uri.AbsolutePath.Trim('/')),
        Username = Uri.UnescapeDataString(usuarioSenha[0]),
        Password = Uri.UnescapeDataString(usuarioSenha[1]),
        SslMode = SslMode.Require,
        Pooling = true,
        Timeout = 15,
        CommandTimeout = 30
    };

    return connection.ConnectionString;
}
