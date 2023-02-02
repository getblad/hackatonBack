using Microsoft.AspNetCore.Mvc.Filters;

namespace RestApiASPNET.Services.Logging;


public class LogAttribute : ActionFilterAttribute
{
    private readonly ILogger _logger;

    public LogAttribute(Type loggerType)
    {
        var services = new ServiceCollection();
        services.AddSingleton(loggerType, typeof(ILogger));
        var provider = services.BuildServiceProvider();
        _logger = provider.GetService<ILogger>()!;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Log information before the action is executed
        _logger.LogInformation("Action Executing: " + filterContext.ActionDescriptor.DisplayName);
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        // Log information after the action is executed
        if (filterContext.Exception == null)
        {
            _logger.LogInformation("Action Executed: " + filterContext.ActionDescriptor.DisplayName);
        }
        else
        {
            // Log the error
            _logger.LogError(filterContext.Exception, "Error in action: " + filterContext.ActionDescriptor.DisplayName);
        }
    }
}