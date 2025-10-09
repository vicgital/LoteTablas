FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
COPY ["/LoteTablas.Api/", "/app/"]
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "LoteTablas.Api.dll"]