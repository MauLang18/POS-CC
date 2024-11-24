# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y tzdata
RUN mkdir -p /app/wwwroot && \
    chmod 755 /app/wwwroot
WORKDIR /app
EXPOSE 8080
ENV TZ=America/Costa_Rica

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["POS.Api/POS.Api.csproj", "POS.Api/"]
COPY ["POS.Application/POS.Application.csproj", "POS.Application/"]
COPY ["POS.Domain/POS.Domain.csproj", "POS.Domain/"]
COPY ["POS.Infrastructure/POS.Infrastructure.csproj", "POS.Infrastructure/"]
COPY ["POS.Utilities/POS.Utilities.csproj", "POS.Utilities/"]
RUN dotnet restore "./POS.Api/POS.Api.csproj"
COPY . . 
WORKDIR "/src/POS.Api"
RUN dotnet build "./POS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "./POS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN chmod 755 /app
EXPOSE 8080

# Set the Vault token from environment variable
ARG VAULT_TOKEN
ENV VAULT_TOKEN=$VAULT_TOKEN

ENTRYPOINT ["dotnet", "POS.Api.dll"]
