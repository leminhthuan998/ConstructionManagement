
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Utils
{
    public class ServiceResponse<TResult> where TResult: class
    {
        public bool Success { get; set; } = true;
        public TResult Result { get; set; }

        public ServiceResponse(TResult result, bool success = true)
        {
            Result = result;
            Success = success;
        }

        public static ServiceResponse<TResult> ServiceOk(TResult result)
        {
            return new ServiceResponse<TResult>(result);
        }

        public static ServiceResponse<TResult> ServiceError(TResult result)
        {
            return new ServiceResponse<TResult>(result, false);
        }
    }
}