FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
ENV WEBAPP_PROJECT_DIR=./PedroTer7.FinancialMonkey.AuthService
COPY ./PedroTer7.FinancialMonkey.AuthService ${WEBAPP_PROJECT_DIR}
RUN dotnet restore ${WEBAPP_PROJECT_DIR}
RUN dotnet publish ${WEBAPP_PROJECT_DIR} -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENV IdentityServer__BaseUrl=""
ENTRYPOINT ["dotnet", "PedroTer7.FinancialMonkey.AuthService.dll"]