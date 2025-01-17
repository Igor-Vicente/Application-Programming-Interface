﻿using Business.Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Client.Layer.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificator _notificator;
        private readonly IUser _user;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected MainController(INotificator notificator, IUser user)
        {
            _notificator = notificator;
            if (user.IsAuthenticated())
            {
                UsuarioId = user.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificator.GetAllNotifications()
            });
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyModelStateError(modelState);
            return CustomResponse();
        }

        private bool ValidOperation()
        {
            return !_notificator.HasNotification();
        }

        private void NotifyModelStateError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var message = error.Exception != null ? error.Exception.Message : error.ErrorMessage;
                NotifierError(message);
            }
        }

        protected void NotifierError(string message)
        {
            _notificator.Handler(message);
        }

    }
}
