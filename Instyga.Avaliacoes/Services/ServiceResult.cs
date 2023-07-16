using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacoes.Models;

namespace Instyga.Avaliacoes.Services
{
    public class ServiceResult<T> where T : Model
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }

        private ServiceResult()
        {

        }

        public static ServiceResult<T> Ok(string message, T model)
        {
            return new ServiceResult<T>()
            {
                Success = true,
                Model = model,
                Message = message
            };
        }
        public static ServiceResult<T> Error(string message)
        {
            return new ServiceResult<T>()
            {
                Success = false,
                Model = null,
                Message = message
            };
        }
    }
}
