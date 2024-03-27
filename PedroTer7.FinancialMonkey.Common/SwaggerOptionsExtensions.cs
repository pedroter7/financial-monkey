using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace PedroTer7.FinancialMonkey.Common;

public static partial class SwaggerOptionsExtensions
{
    public static SwaggerOptions ConfigureServers(this SwaggerOptions options)
    {
        options.PreSerializeFilters.Add((doc, req) =>
        {
            var basePath = GetBasePath(req);
            var host = GetHost(req);
            var protocol = GetProtocol(req);
            var port = GetPort(req);
            var server = new OpenApiServer { Url = $"{protocol}://{host}{(string.IsNullOrEmpty(port) ? "" : ":")}{port}{basePath}" };
            doc.Servers.Clear();
            doc.Servers.Add(server);
        });

        return options;
    }

    private static string GetBasePath(HttpRequest req) => GetHeaderValue(req, "X-Forwarded-Prefix", _ => "");

    private static string GetHost(HttpRequest req) => GetHeaderValue(req, "X-Forwarded-Host", r => r.Host.Value);

    private static string GetProtocol(HttpRequest req) => GetHeaderValue(req, "X-Forwarded-Proto", r => r.Scheme);

    private static string GetPort(HttpRequest req) => GetHeaderValue(req, "X-Forwarded-Port", _ => "");

    private static string GetHeaderValue(HttpRequest req, string header, Func<HttpRequest, string> getDefault)
    {
        var v = req.Headers[header].ToString();
        return string.IsNullOrEmpty(v) ? getDefault(req) : v;
    }

}
