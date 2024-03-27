FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Src
COPY ./PedroTer7.FinancialMonkey.IdentityServer ./PedroTer7.FinancialMonkey.IdentityServer
COPY ./PedroTer7.FinancialMonkey.Common ./PedroTer7.FinancialMonkey.Common
COPY ./run_migrations.sh ./run_migrations.sh
RUN chmod +x ./run_migrations.sh
RUN dotnet tool install --tool-path /dotnetef dotnet-ef
ENV PATH=${PATH}:/dotnetef
ENV CUSTOMCONNSTR_IdentityServer=""
ENTRYPOINT ["./run_migrations.sh"]