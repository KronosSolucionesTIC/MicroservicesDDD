# Usa una imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

# Imagen para la compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia solo el archivo de la solución y los proyectos
COPY ["AuthorizationService.csproj", "AuthorizationService/"]
WORKDIR "/src/AuthorizationService"

# Restaura dependencias
RUN dotnet restore "AuthorizationService.csproj"

# Copia el resto de los archivos
COPY . .

# Compila el proyecto
RUN dotnet build "AuthorizationService.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "AuthorizationService.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorizationService.dll"]
