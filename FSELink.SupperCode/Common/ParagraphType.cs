using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSELink.SupperCode.Common
{
    public enum ParagraphType
    {
        Random,  //随机值
        Date,   //日期值
        FixValue,  //固定值
        Serial, //顺序号
        BatchNo, //批次号
        StartBoxCode, //开始盒码
        EndBoxCode, //结束盒码
        StartPackageCode, //开始包码
        EndPackageCode, //开始包码
        CaseSerival,//箱序号
        BoxSerial,     //盒序号
        PackageSerial //包序号

    }

}
