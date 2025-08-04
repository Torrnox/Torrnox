using System;
using System.Security.Cryptography;
using System.Text;
using RestSharp;

namespace Torrnox.Infrastructure.Extensions;

public static class RestExtensions
{
    public static string GetRequestHash(this RestClient client, RestRequest request)
    {
        var sb = new StringBuilder();

        sb.Append(request.Method.ToString());
        sb.Append(request.Resource);

        foreach (var param in request.Parameters.OrderBy(p => p.Name))
        {
            sb.Append(param.Name);
            sb.Append(param.Value?.ToString());
        }

        foreach (var param in client.DefaultParameters.OrderBy(p => p.Name))
        {
            sb.Append(param.Name);
            sb.Append(param.Value?.ToString());
        }

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(sb.ToString()));
        return Convert.ToBase64String(hash);
    }
}
