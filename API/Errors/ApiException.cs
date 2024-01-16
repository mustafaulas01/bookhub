using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int _statusCode, string _message = null,string detail=null) : base(_statusCode, _message)
        {
            this.Details=detail;
        }

        public string Details { get; set; }
    }
}