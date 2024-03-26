FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
COPY ./PedroTer7.FinancialMonkey.AuthService ./PedroTer7.FinancialMonkey.AuthService
COPY ./PedroTer7.FinancialMonkey.Common ./PedroTer7.FinancialMonkey.Common
RUN dotnet restore ./PedroTer7.FinancialMonkey.AuthService
RUN dotnet publish ./PedroTer7.FinancialMonkey.AuthService -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENV IdentityServer__BaseUrl=""
ENTRYPOINT ["dotnet", "PedroTer7.FinancialMonkey.AuthService.dll"]