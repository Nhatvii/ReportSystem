using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Response
{
    public class SuccessResponse
    {
        public SuccessResponse(int statusCode, string msg)
        {
            StatusCode = statusCode;
            Message = msg;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
