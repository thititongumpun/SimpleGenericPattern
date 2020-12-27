#build container
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /build
COPY . .
RUN dotnet run -p build/build.csproj

#runtime container
FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY --from=build /build/publish /app
WORKDIR /app

EXPOSE 5000

ENTRYPOINT ["dotnet", "Todo.dll"]