using Application.Models.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ControllerExtension
{
    public static IActionResult ReturnResponse(this ControllerBase controller, OperationResult operation)
    {
        object response = operation.Value;
        if (!operation.Succeeded && response is string)
            response = new { Message = response };

        return operation.Status switch
        {
            OperationResultStatus.Ok => controller.Ok(response),
            OperationResultStatus.Created => controller.Created(string.Empty, response),
            OperationResultStatus.InvalidRequest => controller.BadRequest(response),
            OperationResultStatus.NotFound => controller.NotFound(response),
            OperationResultStatus.Unauthorized => controller.UnprocessableEntity(response),
            OperationResultStatus.Unprocessable => controller.UnprocessableEntity(response),
            _ => controller.UnprocessableEntity(response)
        };
    }
}