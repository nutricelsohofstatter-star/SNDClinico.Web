# SND Clínico — Base visual PSICOEDUCA+

Protótipo independente em Blazor .NET 10. A interface usa como referência a identidade do PSICOEDUCA+ (Inter, tons de azul, cards e modais).

## Executar
1. Instale o .NET 10 SDK.
2. Abra `SNDClinicoWeb.csproj` no Visual Studio ou execute `dotnet run` na pasta.
3. Abra o endereço local exibido no terminal.

## Recursos de demonstração
- Painel inicial com indicadores.
- Cadastro de paciente e classificação de triagem ilustrativa.
- Mapa diário de dieta adulto com cópia do último mapa anterior, seleção e edição.

## Limitações críticas
- Dados **somente em memória**, compartilhados entre os usuários da aplicação e perdidos ao reiniciar.
- **Sem login, autorização, auditoria ou armazenamento seguro.** Não utilizar na rede hospitalar, em produção ou com informações identificáveis de pacientes reais.
- Métodos clínicos, integração Salux, adequação de calorias e proteínas, GLIM e indicadores 48 horas ainda não estão implementados.
- A versão clínica deve incluir protocolos validados, controle de acesso, banco persistente, backups, isolamento adulto/pediátrico, logs e testes antes de uso real.
