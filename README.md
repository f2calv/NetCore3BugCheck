# SSL Handshake failed with OpenSSL error

This is a demo repository replicating OpenSSL security level issues/solutions when connecting to a downlevel SSL certificate (i.e. low TLS version) from a .NET application running in a Linux container.

For the full thread see [see Github issue 40538](https://github.com/dotnet/corefx/issues/40538).

## Conclusion

The Debian Buster image has raised the OpenSSL TLS security level to 2.
Adding the below to the Dockerfile in the .NET Core 3.0 app downlevels the OpenSSL TLS security level to 1, which is a level then compatible with the current Oanda SSL certificate on (https://api-fxpractice.oanda.com);

```dockerfile
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed \
   && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf
```

## .NET 6.0 Update _2022-04-19_

.NET 6.0 Docker builds worked just fine with the above command for the first 6-months of the .NET 6.0 release cycle using a final build image of _mcr.microsoft.com/dotnet/aspnet:6.0-alpine_ however in March/April 2022 I discovered the builds started to fail with the following;

```dockerfile
Step 8/20 : RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed      && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf
 ---> Running in 4faf4e25e9ba
sed: /etc/ssl/openssl.cnf: No such file or directory
The command '/bin/sh -c sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed  && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf' returned a non-zero code: 1
```

The default `/etc/ssl/openssl.cnf` file had _been removed_ from the ASP.NET 6.0 Alpine image. On 2022-03-22 there was an [OpenSSL announcement on Debian.org](https://www.debian.org/News/2022/2022032602) regarding various relevant updates.

Long story short I sacrificed using the smaller image and changed to using the vanilla/non-alpine base image _mcr.microsoft.com/dotnet/aspnet:6.0_ and then the build works again as expected.

...then I discovered the SSL certificate on my desired destination URI had been updated anyway so I was able to remove the above :)

This repository sill serves as an example of how to handle connecting to downlevel SSL certificates.
