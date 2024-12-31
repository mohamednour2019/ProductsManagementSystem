using FluentValidation;
using MediatR;
using ProductManagementSystem.Domain.Base.Dto;

namespace ProductManagementSystem.API.ValidatorBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                return await next();
            }
            else
            {
                ApiResponse<bool> response = new();
                response.CreateFailedResponse(false, validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                return (TResponse)(object)response;
            }
        }

    }
}
