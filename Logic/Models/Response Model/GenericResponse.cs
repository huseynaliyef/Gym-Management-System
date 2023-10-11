using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logic.Models.Response_Model
{
    public class GenericResponse<T> 
    { 
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public void Success(T data, int statusCode = 200)
        {
            Data = data;
            StatusCode = statusCode;
            Errors = null;
        }

        public void Error(int statusCode = 400, params string[] errors)
        {
            Data = default(T);
            StatusCode = statusCode;
            Errors = new List<string>(errors);
        }

        public void InternalError(params string[] errors)
        {
            Data = default(T);
            StatusCode = 500;
            Errors = new List<string>(errors);
        }
    }
}
