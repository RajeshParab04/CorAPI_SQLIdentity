using sampleapi.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapi.Model
{
    public class ResponseModel
    {
      public  ResponseModel(ResponseCode responseCode,string responsemessage,object dataset)
        {
            ResponseCode = responseCode;
            ResponseMessage = responsemessage;
            Dataset = dataset;
        }
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object Dataset { get; set; }
    }
}
