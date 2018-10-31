using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KepCom
{
    public  class OpcHelperItem
    {
        public string Tag { get; set; }//变量名
        public string Value { get; set; }//数值
        public DateTime Time { get; set; }//时间戳
        public string Quality { get; set; }//质量
    }
}
