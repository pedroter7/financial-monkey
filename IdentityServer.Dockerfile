FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
COPY ./PedroTer7.FinancialMonkey.IdentityServer ./PedroTer7.FinancialMonkey.IdentityServer
COPY ./PedroTer7.FinancialMonkey.Common ./PedroTer7.FinancialMonkey.Common
RUN dotnet restore ./PedroTer7.FinancialMonkey.IdentityServer
RUN dotnet publish ./PedroTer7.FinancialMonkey.IdentityServer -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENV CUSTOMCONNSTR_IdentityServer=""
ENTRYPOINT ["dotnet", "PedroTer7.FinancialMonkey.IdentityServer.dll"]