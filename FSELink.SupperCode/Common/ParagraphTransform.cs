using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSELink.SupperCode.Common
{
    public class ParagraphTransform
    {

        
        /// <summary>
        /// 根据监管码生成规则获取生产段码的类型
        /// </summary>
        /// <param name="paragraphtype"></param>
        /// <returns></returns>
        public static  ParagraphType GetParagrapType(string paragraphtype)
        {
            ParagraphType temp;
            switch(paragraphtype.Trim())
            {
                case "0":
                case "FixValue":
                case "固定值":
                    temp = ParagraphType.FixValue;
                    break;
                case "1":
                case "Date":
                case "日期码":
                case "日期":
                    temp = ParagraphType.Date;
                    break;
                case "2":
                case "Serial":
                case "顺序码":
                    temp =ParagraphType.Serial;
                    break;
                case "3":
                case "Random":
                case "随机码":
                    temp =ParagraphType.Random;
                    break;
                case "BatchNo":
                case "批次号":
                    temp = ParagraphType.BatchNo;
                    break;
                case "开始盒号":
                case "StartBoxCode":
                    temp = ParagraphType.StartBoxCode;
                    break;
                case "箱顺序号":
                case "箱顺序码":
                case "CaseSerival":
                    temp = ParagraphType.CaseSerival;
                    break;
                case "盒码序号":
                case "BoxSerial":
                    temp = ParagraphType.BoxSerial;
                    break;
                case "包码序号":
                case "PackageSerial":
                    temp = ParagraphType.PackageSerial;
                    break;
                case "结束盒号":
                case "EndBoxCode":
                    temp = ParagraphType.EndBoxCode;
                    break;
                case "开始包号":
                case "StartPackageCode":
                    temp = ParagraphType.StartPackageCode;
                    break;
                case "结束包号":
                case "EndPackageCode":
                    temp = ParagraphType.EndPackageCode;
                    break;
                default:
                    temp=ParagraphType.Random;
                    break;
            }
            return temp;
        }


        /// <summary>
        /// 根据监管码生成规则获取生产段码的类型
        /// </summary>
        /// <param name="paragraphtype"></param>
        /// <returns></returns>
        public static DataType GetDataType(string paragraphtype)
        {
            DataType temp;
            switch (paragraphtype.Trim())
            {
                case "0":
                case "Number":
                case "数字":
                    temp = DataType.Number;
                    break;
                case "2":
                case "CharAndNumber":
                case "数字字母":
                    temp = DataType.NumberAndChar;
                    break;
                case "Char":
                case "字母":
                case "1":
                    temp = DataType.Char;
                    break;
               
                default:
                    temp = DataType.Number;
                    break;
            }
            return temp;
        }

        public static CodeType GetCodeType(string strCodeType)
        {
            CodeType codeType=new CodeType();
            switch(strCodeType.Trim().ToLower())
            {
                case "setlabel":
                    codeType = CodeType.SetLabel;
                    break;
                case "scatterlabel":
                    codeType = CodeType.ScatterLabel;
                    break;
            }

            return codeType;
        }

        public static OrderType GetOrderType(string strOrderType)
        {
            OrderType orderType = new OrderType();
            switch (strOrderType.Trim().ToLower())
            {
                case "xm":
                    orderType = OrderType.XM;
                    break;
                case "mszz":
                    orderType = OrderType.MSZZ;
                    break;
            }

            return orderType;
        }



        /// <summary>
        /// 根据监管码生成规则获取生产段码的类型
        /// </summary>
        /// <param name="paragraphtype"></param>
        /// <returns></returns>
        public static RangType GetRangType(string paragraphtype)
        {
            RangType temp;
            switch (paragraphtype.Trim())
            {
                case "顺序流水":
                    temp = RangType.Serial;
                    break;
                case "区间流水":
                    temp = RangType.RangSerial;
                    break;
                default:
                    temp = RangType.Serial;
                    break;
            }
            return temp;
        }
        

    }
}
