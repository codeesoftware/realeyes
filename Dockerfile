
ARG CODE_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${CODE_VERSION} AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Realeyes.Api/Realeyes.Api.csproj Realeyes.Api/
COPY Realeyes.Application/Realeyes.Application.csproj Realeyes.Application/
COPY Realeyes.Domain/Realeyes.Domain.csproj Realeyes.Domain/
COPY Realeyes.Infrastructure/Realeyes.Infrastructure.csproj Realeyes.Infrastructure/
COPY Realeyes.Application.Test/Realeyes.Application.Test.csproj Realeyes.Application.Test/

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${CODE_VERSION} as runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Realeyes.Api.dll"]