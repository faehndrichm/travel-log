FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /src
COPY . ./
RUN dotnet publish journey-service/journey-service.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=builder /out .
ENTRYPOINT ["dotnet", "journey-service.dll"]
