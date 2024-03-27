#!/bin/bash
wait=5
echo Waiting for ${wait} seconds for database to get ready
sleep ${wait}
dotnet-ef database update --project ./PedroTer7.FinancialMonkey.IdentityServer/PedroTer7.FinancialMonkey.IdentityServer.csproj
exit $?