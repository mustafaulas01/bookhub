using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {

        public ApiResponse(int _statusCode,string _message=null)
        {
            StatusCode=_statusCode;
            Message=_message??GetDefaultMessageForStatusCode(_statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
           return statusCode switch 
           {
            400=>"A Bad request, you have made",
            401=>"Authorized, you are not",
            404=>"Resource found, it was not",
            500=>"Error occured about path",
            _=> null
           };
        }

    }
}