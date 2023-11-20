using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Generators;

namespace CrowdFundingAPI.Typings;

public class AngularActionCallGenerator : MethodCodeGenerator
{
    public override RtFunction? GenerateNode(MethodInfo element, RtFunction result, TypeResolver resolver)
    {
        result = base.GenerateNode(element, result, resolver);
        if (result == null) return null;

        result.AccessModifier = null;

        // override return type to corresponding observable
        var retType = result.ReturnType;
        var isVoid = retType is RtSimpleTypeName { TypeName: "void" };

        result.ReturnType =
            isVoid ? new RtSimpleTypeName("Observable<void>") : new RtSimpleTypeName("Observable", retType);

        var methodAttribute = element.GetCustomAttributes<HttpMethodAttribute>().FirstOrDefault();
        var path = GetPath(element, methodAttribute);

        var parameters = element.GetParameters();
        var cancellationTokenParam = parameters
            .FirstOrDefault(p => p.ParameterType == typeof(CancellationToken));

        // remove cancellation token(s)
        if (cancellationTokenParam != null)
        {
            result.Arguments.RemoveAll(r => cancellationTokenParam?.Name == r.Identifier.IdentifierName);
        }

        // support only single param for now
        if (result.Arguments.Count > 1)
        {
            result.Arguments.RemoveRange(1, result.Arguments.Count - 1);
        }

        var param = element.GetParameters()
            .FirstOrDefault(p => p != cancellationTokenParam);

        var method = methodAttribute?.HttpMethods.FirstOrDefault() ?? "GET";
        var isQueryParam = method.Equals("GET", StringComparison.OrdinalIgnoreCase);

        bool IsPrimitive(ParameterInfo p) => p.ParameterType.IsValueType || p.ParameterType == typeof(string);
        bool IsFromQuery(ParameterInfo p) => p.CustomAttributes.Any(a => a.AttributeType == typeof(FromQueryAttribute));
        bool IsFromBody(ParameterInfo p) => p.CustomAttributes.Any(a => a.AttributeType == typeof(FromBodyAttribute));

        var sb = new StringBuilder();
        sb.AppendLine("const options = {");
        if (param != null)
        {
            sb.AppendLine(isQueryParam switch
            {
                true when IsPrimitive(param) => $"\tparams: {{ {param.Name}: encodeURIComponent({param.Name}) }},",
                true => $"\tparams: {{ {param.Name}: encodeURIComponent(JSON.stringify({param.Name})) }},",
                false when IsFromQuery(param) => $"\tparams: {{ {param.Name}: encodeURIComponent(JSON.stringify({param.Name})) }},",
                false when IsPrimitive(param) && !IsFromBody(param) => "\tbody: null,",
                false => $"\tbody: {param.Name},",
            });
        }

        sb.AppendLine("\theaders: { accept: 'application/json' }};");
        sb.AppendFormat(
            CultureInfo.InvariantCulture,
            "return this.http.request<any>('{0}',`${{environment.baseUrl}}{1}`, options)",
            method,
            path);

        result.Body = new RtRaw(sb.ToString());

        return result;
    }

    private static string GetPath(MethodInfo element, HttpMethodAttribute? methodAttribute)
    {
        var routeTemplate = element.DeclaringType?.GetCustomAttributes<RouteAttribute>().FirstOrDefault()?.Template;
        var actionRoute = methodAttribute?.Template;

        string GetRouteValue(MethodInfo m, string val) => m.GetParameters().Any(p => p.Name == val) ? $"${{{val}}}" : "0";
        string RouteParam(string p) => @"\{" + p + @":?\w*\??\}";
        string? AdjustedRoute(MethodInfo method) => actionRoute != null ?
            Regex.Replace(actionRoute, RouteParam(@"(\w+)"), m => GetRouteValue(method, m.Groups[1].Value)) : null;

        return $"/{routeTemplate}/{AdjustedRoute(element)}";
    }
}