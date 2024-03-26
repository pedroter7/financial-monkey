FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
COPY ./PedroTer7.FinancialMonkey.FinancialProductsService ./PedroTer7.FinancialMonkey.FinancialProductsService
COPY ./PedroTer7.FinancialMonkey.Common ./PedroTer7.FinancialMonkey.Common
RUN dotnet restore ./PedroTer7.FinancialMonkey.FinancialProductsService
RUN dotnet publish ./PedroTer7.FinancialMonkey.FinancialProductsService -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENV CUSTOMCONNSTR_MongoDb=""
ENTRYPOINT ["dotnet", "PedroTer7.FinancialMonkey.FinancialProductsService.dll"]