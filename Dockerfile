FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY samples/ConsoleApp.csproj samples/
COPY src/DbConnector.Adapters/DbConnector.Adapters.csproj src/DbConnector.Adapters/
COPY src/DbConnector.Core/DbConnector.Core.csproj src/DbConnector.Core/
COPY src/DbConnector.Exceptions/DbConnector.Exceptions.csproj src/DbConnector.Exceptions/
COPY src/DbConnector.Repositories/UserRepository.csproj src/DbConnector.Repositories/
COPY src/DbConnector.Models/User.csproj src/DbConnector.Models/

RUN dotnet restore samples/ConsoleApp.csproj

COPY . .

RUN dotnet publish samples/ConsoleApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp.dll"]