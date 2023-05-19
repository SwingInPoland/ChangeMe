using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChangeMe.API.Configuration.Authorization;

public static class AuthorizationChecker
{
    public static void CheckAllEndpoints()
    {
        var assembly = typeof(Startup).Assembly;
        var allControllerTypes = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ControllerBase)));

        var notProtectedActionMethods = new List<string>();
        foreach (var controllerType in allControllerTypes)
        {
            var controllerHasPermissionAttribute = controllerType.GetCustomAttribute<AuthorizeAttribute>();
            if (controllerHasPermissionAttribute is not null)
                continue;

            var actionMethods = controllerType.GetMethods()
                .Where(x => x.IsPublic && x.DeclaringType == controllerType);

            foreach (var publicMethod in actionMethods)
            {
                var hasPermissionAttribute = publicMethod.GetCustomAttribute<AuthorizeAttribute>();
                if (hasPermissionAttribute is not null)
                    continue;

                var noPermissionRequired = publicMethod.GetCustomAttribute<AllowAnonymousAttribute>();

                if (noPermissionRequired is null)
                    notProtectedActionMethods.Add($"{controllerType.Name}.{publicMethod.Name}");
            }
        }

        if (notProtectedActionMethods.Any())
        {
            var errorBuilder = new StringBuilder();
            errorBuilder.AppendLine("Invalid authorization configuration: ");

            foreach (var notProtectedActionMethod in notProtectedActionMethods)
                errorBuilder.AppendLine($"Method {notProtectedActionMethod} is not protected. ");

            throw new ApplicationException(errorBuilder.ToString());
        }
    }
}