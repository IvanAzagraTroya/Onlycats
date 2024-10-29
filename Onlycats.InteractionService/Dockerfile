FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

USER root
RUN apt-get update && apt-get install -y curl
USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Onlycats.InteractionService/Onlycats.InteractionService.csproj", "Onlycats.InteractionService/"]
RUN dotnet restore "./Onlycats.InteractionService/Onlycats.InteractionService.csproj"
COPY . .
WORKDIR "/src/Onlycats.InteractionService"
RUN dotnet build "./Onlycats.InteractionService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Onlycats.InteractionService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onlycats.InteractionService.dll"]