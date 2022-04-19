FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConsoleApp60/ConsoleApp60.csproj", "ConsoleApp60/"]
COPY ["SharedLib/SharedLib.csproj", "SharedLib/"]
RUN dotnet restore "ConsoleApp60/ConsoleApp60.csproj"
COPY . .
WORKDIR "/src/ConsoleApp60"
RUN dotnet build "ConsoleApp60.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp60.csproj" -c Release -o /app/publish

#alpine below fails because '/etc/ssl/openssl.cnf' doesn't even exist
#FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine AS final

#full image works because '/etc/ssl/openssl.cnf' exists
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final

#now make SSL work with downlevel Oanda certificate.
#https://github.com/dotnet/corefx/issues/40538
#to check CipherString level type the following in Bash;
#cat /etc/ssl/openssl.cnf | grep -i CipherString
#returns "CipherString = DEFAULT@SECLEVEL=2"
#now we lower CipherString to 1 with;
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed \
	&& mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp60.dll"]