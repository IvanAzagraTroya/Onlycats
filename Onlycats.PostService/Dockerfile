# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Onlycats.PostService/Onlycats.PostService.csproj", "Onlycats.PostService/"]
RUN ls -l Onlycats.PostService # Verifica los archivos después de la copia
RUN dotnet restore "Onlycats.PostService/Onlycats.PostService.csproj"
COPY . .
WORKDIR "/src/Onlycats.PostService"
RUN dotnet build "Onlycats.PostService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Onlycats.PostService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Agrega un comando para listar los subdirectorios y luego ejecuta la aplicación
ENTRYPOINT ["sh", "-c", "ls -l; dotnet Onlycats.PostService.dll"]
