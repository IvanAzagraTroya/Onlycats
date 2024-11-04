FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

USER root
RUN apt-get update && apt-get install -y curl
USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Onlycats.UserService/Onlycats.UserService.csproj", "Onlycats.UserService/"]
RUN dotnet restore "Onlycats.UserService/Onlycats.UserService.csproj"
RUN dotnet tool install --global dotnet-ef
COPY . .
WORKDIR "/src/Onlycats.UserService"
RUN dotnet build "Onlycats.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Onlycats.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "Onlycats.UserService.dll"]