using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstBackEndProj.Models
{
    public class JsonResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}