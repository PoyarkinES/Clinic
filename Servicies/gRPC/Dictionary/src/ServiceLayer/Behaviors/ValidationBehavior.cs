using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;
        public ValidationBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                throw new Exception(
                    $"Command Validation Errors for type {typeof(TRequest).Name}",
                            new ValidationException("Validation exception", failures));
            }

            var response = await next();
            return response;
        }
    }
}
