FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SNDClinico.Web/SNDClinico.Web.csproj", "SNDClinico.Web/"]
RUN dotnet restore "SNDClinico.Web/SNDClinico.Web.csproj"

COPY . .
WORKDIR /src/SNDClinico.Web
RUN dotnet publish "SNDClinico.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SNDClinico.Web.dll"]
