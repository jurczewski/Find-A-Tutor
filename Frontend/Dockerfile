FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

WORKDIR /src
COPY . .
WORKDIR /src/Frontend/Find-A-Tutor.Frontend
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "find-a-tutor.frontend.dll"]