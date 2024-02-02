using Application.Models.Operations;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, OperationResult>
    where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<OperationResult> Handle(TRequest request,
        RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationFailure = _validators
            .Select(x => x.Validate(context))
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .FirstOrDefault();

        if (validationFailure is not null)
        {
            return new OperationResult(OperationResultStatus.InvalidRequest,
                value: validationFailure.ErrorMessage);
        }

        return await next();
    }
}