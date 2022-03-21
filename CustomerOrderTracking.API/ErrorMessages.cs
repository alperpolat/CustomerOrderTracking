using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderTracking.Entities
{
    public class ErrorMessages
    {  //message
        public ErrorMessages(string Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
