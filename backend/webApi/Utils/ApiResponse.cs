using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Utils
{
    public class ApiResponse<TResult> where TResult : class
    {
        public bool Success { get; set; } = true;
        public TResult Result { get; set; }

        public ApiResponse(TResult result, bool success = true)
        {
            Result = result;
            Success = success;
        }

        public static ApiResponse<TResult> ApiOk(TResult result)
        {
            return new ApiResponse<TResult>(result);
        }

        public static ApiResponse<TResult> ApiError(TResult result)
        {
            return new ApiResponse<TResult>(result, false);
        }
    }
}
