using System;

namespace Torrnox.Web;

public static class WebConstants
{
    public const string ApiPrefix = "/api";

    public static string WithPrefix(string path)
    {
        return $"{ApiPrefix.TrimStart('/').TrimEnd('/')}/{path.TrimStart('/').TrimEnd('/')}";
    }
}
