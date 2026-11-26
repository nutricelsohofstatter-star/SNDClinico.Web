
using System.Net;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SNDClinico.Web.Data;

namespace SNDClinico.Web.Components.Security;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapSndAccount(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/acesso", PaginaAcesso)
            .AllowAnonymous();

        endpoints.MapPost("/acesso", Entrar)
            .AllowAnonymous()
            .RequireRateLimiting("acesso");

        endpoints.MapPost("/sair", Sair)
            .RequireAuthorization();

        return endpoints;
    }

    private static IResult PaginaAcesso(
        HttpContext context,
        IAntiforgery antiforgery,
        string? returnUrl = null,
        string? erro = null)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            return Results.Redirect("/");
        }

        var tokens = antiforgery.GetAndStoreTokens(context);

        var tokenHtml = WebUtility.HtmlEncode(
            tokens.RequestToken ?? string.Empty);

        var destino = LimparRetorno(returnUrl);

        var mensagem = erro switch
        {
            "invalido" => "Usuário ou senha inválidos.",
            "bloqueado" => "Acesso temporariamente bloqueado. Tente novamente mais tarde.",
            _ => string.Empty
        };

        var mensagemHtml = string.IsNullOrWhiteSpace(mensagem)
            ? string.Empty
            : $"""<div class="mensagem-erro">{WebUtility.HtmlEncode(mensagem)}</div>""";

        var destinoHtml = WebUtility.HtmlEncode(destino);

        var html = $$"""
        <!DOCTYPE html>
        <html lang="pt-BR">
        <head>
            <meta charset="utf-8">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <meta name="theme-color" content="#102b46">
            <title>Acesso | SND Clínico</title>
            <style>
                * { box-sizing: border-box; }

                :root {
                    color-scheme: light;
                    --navy: #12364b;
                    --teal: #1b566d;
                    --ink: #183047;
                    --muted: #718296;
                    --line: #dce5ec;
                    --focus: rgba(35, 126, 157, .14);
                }

                body {
                    margin: 0;
                    min-height: 100vh;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    padding: 32px;
                    font-family: Inter, "Segoe UI", sans-serif;
                    color: var(--ink);
                    background:
                        radial-gradient(ellipse at 12% 10%, rgba(49, 115, 145, .08), transparent 35%),
                        #f2f5f8;
                }

                .container {
                    width: 100%;
                    max-width: 1120px;
                    min-height: 620px;
                    display: grid;
                    grid-template-columns: 1.02fr .98fr;
                    overflow: hidden;
                    background: #fff;
                    border: 1px solid rgba(218, 228, 236, .9);
                    border-radius: 24px;
                    box-shadow: 0 26px 75px rgba(20, 47, 67, .12);
                }

                .painel {
                    position: relative;
                    isolation: isolate;
                    display: flex;
                    flex-direction: column;
                    justify-content: space-between;
                    padding: 52px 54px;
                    color: #fff;
                    background: linear-gradient(145deg, #102d45 0%, #17465d 58%, #1b5369 100%);
                    overflow: hidden;
                }

                .painel::before,
                .painel::after {
                    content: "";
                    position: absolute;
                    z-index: -1;
                    pointer-events: none;
                    border: 1px solid rgba(220, 241, 249, .09);
                    border-radius: 50%;
                }

                .painel::before {
                    width: 510px;
                    height: 510px;
                    right: -270px;
                    top: 80px;
                    box-shadow: 0 0 0 42px rgba(220,241,249,.025),
                                0 0 0 84px rgba(220,241,249,.02);
                }

                .painel::after {
                    width: 310px;
                    height: 310px;
                    left: -210px;
                    bottom: -190px;
                }

                .marca {
                    display: flex;
                    align-items: center;
                    gap: 13px;
                    font-size: 13px;
                    font-weight: 750;
                    letter-spacing: 1.8px;
                }

                .simbolo {
                    width: 46px;
                    height: 46px;
                    display: grid;
                    place-items: center;
                    border: 1px solid rgba(255,255,255,.24);
                    border-radius: 14px;
                    background: rgba(255,255,255,.07);
                    box-shadow: inset 0 1px 0 rgba(255,255,255,.12);
                }

                .simbolo svg { width: 25px; height: 25px; }

                .painel-conteudo {
                    max-width: 440px;
                    padding: 42px 0;
                }

                .painel h1 {
                    margin: 0 0 18px;
                    font-size: clamp(34px, 4vw, 48px);
                    line-height: 1.12;
                    letter-spacing: -1.8px;
                    font-weight: 750;
                }

                .painel p {
                    max-width: 390px;
                    margin: 0;
                    color: #d5e3eb;
                    font-size: 15px;
                    line-height: 1.85;
                }

                .rodape-painel {
                    color: #b9cbd7;
                    font-size: 11px;
                    letter-spacing: .35px;
                }

                .formulario {
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                    padding: 64px clamp(38px, 5vw, 68px);
                }

                .formulario h2 {
                    margin: 0 0 9px;
                    color: #183047;
                    font-size: 29px;
                    letter-spacing: -.8px;
                    font-weight: 750;
                }

                .subtitulo {
                    margin: 0 0 34px;
                    color: var(--muted);
                    font-size: 14px;
                    line-height: 1.6;
                }

                label {
                    display: block;
                    margin: 19px 0 9px;
                    color: #40566b;
                    font-size: 13px;
                    font-weight: 650;
                }

                .campo {
                    position: relative;
                }

                .campo-icone {
                    position: absolute;
                    top: 50%;
                    left: 15px;
                    width: 18px;
                    height: 18px;
                    color: #8496a7;
                    transform: translateY(-50%);
                    pointer-events: none;
                }

                input {
                    width: 100%;
                    height: 52px;
                    padding: 0 15px 0 44px;
                    border: 1px solid var(--line);
                    border-radius: 10px;
                    outline: none;
                    color: var(--ink);
                    background: #fff;
                    font: inherit;
                    font-size: 14px;
                    transition: border-color .18s ease, box-shadow .18s ease, background .18s ease;
                }

                input::placeholder { color: #9aa8b6; }

                input:hover { border-color: #c5d3de; }

                input:focus {
                    border-color: #3986a2;
                    box-shadow: 0 0 0 4px var(--focus);
                    background: #fff;
                }

                .senha input { padding-right: 82px; }

                .mostrar {
                    position: absolute;
                    top: 0;
                    right: 7px;
                    height: 52px;
                    padding: 0 10px;
                    border: 0;
                    border-radius: 8px;
                    color: #426b80;
                    background: transparent;
                    cursor: pointer;
                    font: inherit;
                    font-size: 12px;
                    font-weight: 650;
                }

                .mostrar:hover { color: #173f56; background: #f4f7f9; }

                .botao {
                    width: 100%;
                    min-height: 52px;
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                    gap: 9px;
                    margin-top: 29px;
                    border: 0;
                    border-radius: 10px;
                    color: #fff;
                    background: linear-gradient(105deg, #17465f, #1b5870);
                    box-shadow: 0 8px 18px rgba(23, 70, 95, .16);
                    font: inherit;
                    font-size: 14px;
                    font-weight: 700;
                    cursor: pointer;
                    transition: transform .18s ease, box-shadow .18s ease, filter .18s ease;
                }

                .botao:hover {
                    transform: translateY(-1px);
                    box-shadow: 0 11px 22px rgba(23, 70, 95, .22);
                    filter: brightness(1.04);
                }

                .botao:active { transform: translateY(0); }

                .botao:focus-visible,
                .mostrar:focus-visible {
                    outline: 3px solid rgba(35, 126, 157, .25);
                    outline-offset: 3px;
                }

                .mensagem-erro {
                    margin: 0 0 18px;
                    padding: 13px 15px;
                    border: 1px solid #f1caca;
                    border-radius: 10px;
                    color: #a52d2d;
                    background: #fff5f5;
                    font-size: 13px;
                    line-height: 1.5;
                }

                .seguranca {
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    gap: 7px;
                    margin-top: 28px;
                    color: #8493a0;
                    font-size: 11px;
                    text-align: center;
                }

                .seguranca svg { width: 14px; height: 14px; }

                @media (max-width: 760px) {
                    body { padding: 16px; align-items: flex-start; }
                    .container {
                        max-width: 520px;
                        min-height: 0;
                        grid-template-columns: 1fr;
                        border-radius: 20px;
                    }
                    .painel { min-height: 260px; padding: 28px; }
                    .painel-conteudo { padding: 34px 0 26px; }
                    .painel h1 { font-size: 32px; letter-spacing: -1px; }
                    .painel p { font-size: 14px; }
                    .rodape-painel { margin-top: 8px; }
                    .formulario { padding: 36px 28px 40px; }
                    .subtitulo { margin-bottom: 25px; }
                }

                @media (max-width: 380px) {
                    .painel { min-height: 235px; }
                    .formulario { padding: 30px 21px 34px; }
                }
            </style>
        </head>
        <body>
            <main class="container">
                <section class="painel">
                    <div class="marca">
                        <div class="simbolo" aria-hidden="true">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor"
                                 stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round">
                                <path d="M12 3v18M3 12h18" />
                            </svg>
                        </div>
                        <span>SND CLÍNICO</span>
                    </div>
                    <div class="painel-conteudo">
                        <h1>Nutrição hospitalar com inteligência.</h1>
                        <p>
                            Gestão clínica e nutricional em um ambiente
                            integrado, organizado e seguro.
                        </p>
                    </div>
                    <div class="rodape-painel">
                        SISTEMA DE NUTRIÇÃO E DIETÉTICA
                    </div>
                </section>

                <section class="formulario">
                    <h2>Bem-vindo</h2>
                    <p class="subtitulo">
                        Entre com suas credenciais para continuar.
                    </p>

                    {{mensagemHtml}}

                    <form method="post" action="/acesso" onsubmit="marcarEnvio(this)">
                        <input type="hidden"
                               name="__RequestVerificationToken"
                               value="{{tokenHtml}}">
                        <input type="hidden"
                               name="returnUrl"
                               value="{{destinoHtml}}">

                        <label for="email">E-mail</label>
                        <div class="campo">
                            <svg class="campo-icone" viewBox="0 0 24 24" fill="none"
                                 stroke="currentColor" stroke-width="1.7"
                                 stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">
                                <rect x="3" y="5" width="18" height="14" rx="2" />
                                <path d="m3 7 9 6 9-6" />
                            </svg>
                            <input id="email"
                                   name="email"
                                   type="email"
                                   autocomplete="username"
                                   placeholder="seu@email.com"
                                   required>
                        </div>

                        <label for="password">Senha</label>
                        <div class="campo senha">
                            <svg class="campo-icone" viewBox="0 0 24 24" fill="none"
                                 stroke="currentColor" stroke-width="1.7"
                                 stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">
                                <rect x="4" y="10" width="16" height="11" rx="2" />
                                <path d="M8 10V7a4 4 0 0 1 8 0v3" />
                            </svg>
                            <input id="password"
                                   name="password"
                                   type="password"
                                   autocomplete="current-password"
                                   placeholder="Digite sua senha"
                                   required>
                            <button class="mostrar"
                                    type="button"
                                    onclick="alternarSenha()">
                                Mostrar
                            </button>
                        </div>

                        <button class="botao" type="submit">
                            <span class="texto-botao">Entrar no sistema</span>
                        </button>
                    </form>

                    <div class="seguranca">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor"
                             stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round"
                             aria-hidden="true">
                            <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10Z" />
                            <path d="m9 12 2 2 4-4" />
                        </svg>
                        Ambiente protegido · SND Clínico
                    </div>
                </section>
            </main>

            <script>
                function alternarSenha() {
                    const campo = document.getElementById("password");
                    const botao = document.querySelector(".mostrar");
                    const mostrar = campo.type === "password";

                    campo.type = mostrar ? "text" : "password";
                    botao.textContent = mostrar ? "Ocultar" : "Mostrar";
                    botao.setAttribute("aria-pressed", mostrar ? "true" : "false");
                }

                function marcarEnvio(formulario) {
                    const botao = formulario.querySelector(".botao");
                    const texto = formulario.querySelector(".texto-botao");

                    if (!formulario.checkValidity()) return;

                    botao.disabled = true;
                    botao.style.opacity = ".82";
                    botao.style.cursor = "wait";
                    texto.textContent = "Verificando acesso...";
                }
            </script>
        </body>
        </html>
        """;

        return Results.Content(html, "text/html; charset=utf-8");
    }

    private static async Task<IResult> Entrar(
        HttpContext context,
        UserManager<SndIdentityUser> userManager,
        SignInManager<SndIdentityUser> signInManager,
        IAntiforgery antiforgery)
    {
        await antiforgery.ValidateRequestAsync(context);

        var form = await context.Request.ReadFormAsync();

        var email = form["email"].ToString().Trim();
        var password = form["password"].ToString();
        var returnUrl = LimparRetorno(form["returnUrl"].ToString());

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            return Results.Redirect("/acesso?erro=invalido");
        }

        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario is null)
        {
            return Results.Redirect("/acesso?erro=invalido");
        }

        var resultado = await signInManager.PasswordSignInAsync(
            usuario,
            password,
            isPersistent: false,
            lockoutOnFailure: true);

        if (resultado.Succeeded)
        {
            return Results.LocalRedirect(returnUrl);
        }

        if (resultado.IsLockedOut)
        {
            return Results.Redirect("/acesso?erro=bloqueado");
        }

        return Results.Redirect("/acesso?erro=invalido");
    }

    private static async Task<IResult> Sair(
        HttpContext context,
        SignInManager<SndIdentityUser> signInManager,
        IAntiforgery antiforgery)
    {
        await antiforgery.ValidateRequestAsync(context);
        await signInManager.SignOutAsync();

        return Results.Redirect("/acesso");
    }

    private static string LimparRetorno(string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
            return "/";

        if (!returnUrl.StartsWith('/') ||
            returnUrl.StartsWith("//") ||
            returnUrl.StartsWith("/\\"))
        {
            return "/";
        }

        return returnUrl;
    }

    public static async Task CriarContaInicialAsync(
        IServiceProvider services,
        IConfiguration configuration,
        ILogger logger)
    {
        var email = configuration["SND_ADMIN_EMAIL"];
        var senha = configuration["SND_ADMIN_PASSWORD"];

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha))
        {
            logger.LogWarning(
                "Conta inicial não criada. Configure SND_ADMIN_EMAIL e SND_ADMIN_PASSWORD.");

            return;
        }

        using var scope = services.CreateScope();

        var userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<SndIdentityUser>>();

        var roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        const string roleName = "Administrador";

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var roleResult = await roleManager.CreateAsync(
                new IdentityRole(roleName));

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Não foi possível criar a função Administrador: " +
                    string.Join("; ", roleResult.Errors.Select(e => e.Description)));
            }
        }

        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario is null)
        {
            usuario = new SndIdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                NomeCompleto = "Administrador do SND Clínico"
            };

            var createResult =
                await userManager.CreateAsync(usuario, senha);

            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Não foi possível criar a conta inicial: " +
                    string.Join("; ", createResult.Errors.Select(e => e.Description)));
            }

            logger.LogInformation(
                "Conta inicial do SND Clínico criada.");
        }

        if (!await userManager.IsInRoleAsync(usuario, roleName))
        {
            var roleResult =
                await userManager.AddToRoleAsync(usuario, roleName);

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Não foi possível atribuir a função Administrador.");
            }
        }
    }
}
