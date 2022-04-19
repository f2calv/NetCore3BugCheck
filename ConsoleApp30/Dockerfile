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

FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS final

#make SSL work with downlevel Oanda certificate.
#https://github.com/dotnet/corefx/issues/40538
#to check CipherString level type the following in Bash;
#cat /etc/ssl/openssl.cnf | grep -i CipherString
#returns "CipherString = DEFAULT@SECLEVEL=2"
#now we lower CipherString to 1 with;
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed \
	&& mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp30.dll"]