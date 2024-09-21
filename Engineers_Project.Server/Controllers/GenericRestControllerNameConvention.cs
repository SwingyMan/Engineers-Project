using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Engineers_Project.Server.Controllers;

[AttributeUsage(AttributeTargets.Class)]
public class GenericRestControllerNameConvention : Attribute, IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerType.IsGenericType ||
            controller.ControllerType.GetGenericTypeDefinition() != typeof(GenericController<,>)) return;
        var entityType = controller.ControllerType.GenericTypeArguments[0];
        controller.ControllerName = entityType.Name;
        controller.RouteValues["Controller"] = entityType.Name;
    }
}