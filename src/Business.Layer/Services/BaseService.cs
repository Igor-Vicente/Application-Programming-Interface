using Business.Layer.Interfaces;
using Business.Layer.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Layer.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notifier(validator);
            return false;
        }

        protected void Notifier(string message)
        {
            _notificator.Handler(message);
        }

        protected void Notifier(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notifier(error.ErrorMessage);
            }
        }
    }
}
