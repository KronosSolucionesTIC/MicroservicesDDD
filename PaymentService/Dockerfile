#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Usa una imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5002

# Imagen para la compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia solo el archivo de la solución y los proyectos
COPY ["PaymentService.csproj", "PaymentService/"]
WORKDIR "/src/PaymentService"

# Restaura dependencias
RUN dotnet restore "PaymentService.csproj"

# Copia el resto de los archivos
COPY . .

# Compila el proyecto
RUN dotnet build "PaymentService.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "PaymentService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.dll"]