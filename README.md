# SSL Handshake failed with OpenSSL error
This is a demo repository replicating SSL issues .NET Core 3.0 preview 8.
For the full thread see [see Github issue 40538](https://github.com/dotnet/corefx/issues/40538).

# Conclusion
The Debian Buster image has raised the OpenSSL TLS security level to 2. 
Adding the below to the Dockerfile in the .NET Core 3.0 app downlevels the OpenSSL TLS security level to 1, which is a level then compatible with the current Oanda SSL certificate on (https://api-fxpractice.oanda.com);

> RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed \
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&& mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf