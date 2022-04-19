FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ConsoleApp22/ConsoleApp22.csproj", "ConsoleApp22/"]
COPY ["SharedLib/SharedLib.csproj", "SharedLib/"]
RUN dotnet restore "ConsoleApp22/ConsoleApp22.csproj"
COPY . .
WORKDIR "/src/ConsoleApp22"
RUN dotnet build "ConsoleApp22.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp22.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp22.dll"]