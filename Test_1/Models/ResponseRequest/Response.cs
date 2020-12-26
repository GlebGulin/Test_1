using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResponseRequest
{
    public class Response
    {
        public int code { get; set; }
        public string message { get; set; }
        public string details { get; set; }
    }
}
