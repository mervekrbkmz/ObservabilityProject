FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5251

ENV ASPNETCORE_URLS=http://+:5251

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Observability.App/Observability.App.csproj", "Observability.App/"]
RUN dotnet restore "Observability.App/Observability.App.csproj"
COPY . .
WORKDIR "/src/Observability.App"
RUN dotnet build "Observability.App.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Observability.App.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Observability.App.dll"]
