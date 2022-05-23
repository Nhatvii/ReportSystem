using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReportSystemData.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportSystemAPI.Handler
{
    public class ErrorResponseHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is ErrorResponse)
            {
                context.ExceptionHandled = true;
                string message = ((ErrorResponse)context.Exception).Error.Message;
                context.Result = new ObjectResult(new ErrorResponse(message, ((ErrorResponse)context.Exception).Error.ErrCode));
                
                return;
            }
        }
    }
}
