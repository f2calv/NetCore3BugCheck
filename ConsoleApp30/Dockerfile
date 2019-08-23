FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ConsoleApp30/ConsoleApp30.csproj", "ConsoleApp30/"]
COPY ["SharedLib/SharedLib.csproj", "SharedLib/"]
RUN dotnet restore "ConsoleApp30/ConsoleApp30.csproj"
COPY . .
WORKDIR "/src/ConsoleApp30"
RUN dotnet build "ConsoleApp30.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp30.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp30.dll"]