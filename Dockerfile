FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

COPY . .

RUN dotnet build -c Release

ENTRYPOINT ["dotnet", "run", "--no-launce-profile", "--project","election"]