using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class CommonAstract
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime ModifyedDate { get; set;}
        public string ModifierBy { get; set;}

    }
}