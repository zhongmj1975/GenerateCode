using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Utilities
{
   public enum BarcodeRuleType
    {

        CaseCode, //箱码规则
        BoxCode, //盒码规则,
        PackageCode, //包码规则,
        ClearCode1,  //明码规则1, 
        ClearCode2, //明码规则2, 
        CipherCode1,//暗码规则1,
        CipherCode2 //暗码规则2

    }
}
