using FSELink.Entities;
using FSELink.SupperCode.Common;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FSELink.SupperCode
{
    public class ParagraphCode
    {

        private static string GenerateNumberRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            string temp = result.ToString();
            return temp;
        }

        private  static string GenerateCharRandomCode(int length)
        {
            var result = new StringBuilder();
            string strString = "ABCDEFGHIJKLMNOPQRSTUVWZ";
            var strChar = "";
            var ranNumber = new Random();
            for (var i = 0; i < length; i++)
            {
                int pos = ranNumber.Next(1, 9);
                strChar = strString.Substring(pos, 1).ToUpper();
                result.Append(strChar);
            }
            return result.ToString();
        }


        private static string GenerateCharNumberRandomCode(int length)
        {
            var result = new StringBuilder();
            string temp = "";
            temp = Guid.NewGuid().ToString("N");
            var strChar="";
            var ranNumber = new Random();
            for (var i = 0; i < length; i++)
            {
                
                do
                {
                    int pos= ranNumber.Next(1, 9);
                    strChar = temp.Substring(pos, 1).ToUpper();
                    if (strChar == "O" || strChar == "0" || strChar == "I" || strChar == "1")
                        temp = Guid.NewGuid().ToString("N");
                }while(strChar=="O" || strChar=="0"|| strChar=="I" || strChar=="1");

                result.Append(strChar);
            }
            return result.ToString();
        }


        private static string GenerateDateCharCode(string date)
        {
            var result = new StringBuilder();
            string temp = "";
            temp = Guid.NewGuid().ToString("N");
            var strChar = "";
            var ranNumber = new Random();
            foreach (char s in date)
            {
                int number = int.Parse(s.ToString());
                do
                {                    
                    strChar = temp.Substring(number, 1).ToUpper();
                    if(strChar == "O" || strChar == "0" || strChar == "I" || strChar == "1")
                        temp = Guid.NewGuid().ToString("N");
                } while (strChar == "O" || strChar == "0" || strChar == "I" || strChar == "1");

                result.Append(strChar);
            }
            return result.ToString();
        }



        ///// <summary>
        ///// 获取码数据
        ///// </summary>
        ///// <param name="paraType"></param>
        ///// <param name="codeLen"></param>
        ///// <param name="codeFormat"></param>
        ///// <param name="currentValue"></param>
        ///// <returns></returns>
        //public async static Task<string> GetParagraphCode(ParagraphType paraType, int codeLen = 0, string codeFormat = null, Int64 currentValue=0)
        //{
        //    string TempCode = "";
        //    if (paraType == ParagraphType.Date && (codeFormat.Trim() == "" || codeFormat==null))
        //        throw new Exception("获取日期码段时，日期格式不能为空");
        //    if ((paraType == ParagraphType.Random || paraType==ParagraphType.Serial) && codeLen==0)
        //        throw new Exception("获取随机数据时，码段的长度不能为0");
        //    if ( paraType ==ParagraphType.Serial && codeLen == 0)
        //        throw new Exception("获取序列码段时，码段的长度不能为0");


        //   switch(paraType)
        //   {
        //       case ParagraphType.Date:
        //           TempCode = DateTime.Now.ToString(codeFormat);
        //           break;
        //       case ParagraphType.Random:

        //           TempCode =await GenerateNumberRandomCode(codeLen);
        //           break;
        //       case ParagraphType.Serial:
        //           TempCode = (currentValue).ToString().PadLeft(codeLen, '0');
        //           break;
        //        default:
        //            TempCode = (currentValue).ToString().PadLeft(codeLen, '0');
        //            break;


        //    }

        //   return TempCode;
        //}


        ///// <summary>
        ///// 获取码数据
        ///// </summary>
        ///// <param name="paraType"></param>
        ///// <param name="codeLen"></param>
        ///// <param name="codeFormat"></param>
        ///// <param name="currentValue"></param>
        ///// <returns></returns>
        //public async static Task<string> GetParagraphCode(CodeType codeType, 
        //                                      ParagraphType paraType, 
        //                                      DataType dataType, 
        //                                      int codeLen = 0, 
        //                                      string codeFormat = null,
        //                                      Int64 currentValue = 0, 
        //                                      int CaseIndex = 0, 
        //                                      int BoxIndex = 0, 
        //                                      int PackageIndex = 0)
        //{
        //    string TempCode = "";
        //    if (paraType == ParagraphType.Date && (codeFormat.Trim() == "" || codeFormat == null))
        //        throw new Exception("获取日期码段时，日期格式不能为空");
        //    if ((paraType == ParagraphType.Random || paraType == ParagraphType.Serial) && codeLen == 0)
        //        throw new Exception("获取随机数据时，码段的长度不能为0");
        //    if (paraType == ParagraphType.Serial && codeLen == 0)
        //        throw new Exception("获取序列码段时，码段的长度不能为0");

        //    switch (paraType)
        //    {
        //        case ParagraphType.Date:
        //            TempCode = DateTime.Now.ToString(codeFormat);
        //            break;
        //        case ParagraphType.Random:
        //            if (dataType == DataType.Number)
        //                TempCode =await GenerateNumberRandomCode(codeLen);
        //            else
        //                TempCode =await GenerateCharNumberRandomCode(codeLen);
        //            break;
        //        case ParagraphType.FixValue:
        //            TempCode = codeFormat.Trim();
        //            break;
        //        case ParagraphType.Serial:
        //            TempCode = (currentValue).ToString().PadLeft(codeLen, '0');
        //            break;

        //        case ParagraphType.StartPackageCode:
        //            TempCode = (PackageIndex).ToString().PadLeft(codeLen, '0');
        //            break;
        //        case ParagraphType.EndPackageCode:
        //            TempCode = (PackageIndex).ToString().PadLeft(codeLen, '0');
        //            break;

        //        case ParagraphType.StartBoxCode:
        //            TempCode = (BoxIndex).ToString().PadLeft(codeLen, '0');
        //            break;
        //        case ParagraphType.EndBoxCode:
        //            TempCode = (BoxIndex).ToString().PadLeft(codeLen, '0');
        //            break;

        //        case ParagraphType.CaseSerival:
        //            TempCode = (CaseIndex).ToString().PadLeft(codeLen, '0');
        //            break;
        //        default:
        //            TempCode = (currentValue).ToString().PadLeft(codeLen, '0');
        //            break;


        //    }
  
        //    return TempCode;
        //}








        //public async static Task<string> GetRandomCode(DataType dataType,int codeLength)
        //{
        //    string strtemp = "";
        //    if(dataType==DataType.Number)
        //        strtemp =await GenerateNumberRandomCode(codeLength);
        //    else
        //        strtemp = await GenerateCharNumberRandomCode(codeLength);
        //    return strtemp;
        //}

        //public static string GetDateCode(string dateFormat, int codeLength)
        //{
        //    string strtemp = "";
        //    strtemp = DateTime.Now.ToString(dateFormat);
        //    if (strtemp.Length < codeLength)
        //        strtemp = strtemp.PadLeft(codeLength, '0');
        //    else
        //        strtemp = strtemp.Substring(0, codeLength);
        //    return strtemp;
        //}

        //public static string GetSerialCode(int currentValue, int codeLenght)
        //{
        //    string strtemp = "";
        //    strtemp = (currentValue).ToString().Trim().PadLeft(codeLenght, '0');
        //    return strtemp;
        //}

        //public static string GetFixValue(string currentValue)
        //{
        //    return currentValue.Trim();
        //}

        public static string GetCode(Int64 index, TraceCodeRule rule )
        {
            string strtemp = "";
            ParagraphType paragraphType = ParagraphTransform.GetParagrapType(rule.PharaType);
            DataType dataType = ParagraphTransform.GetDataType(rule.DataType);
            switch (paragraphType)
            {
                
                case ParagraphType.Date:
                    strtemp += DateTime.Now.ToString(rule.ParaContent);
                    break;
                case ParagraphType.FixValue:
                    strtemp = rule.ParaContent.Trim();
                    break;
                case ParagraphType.Serial:
                    strtemp += index.ToString().PadLeft(rule.DataLenght, '0');
                    break;
                case ParagraphType.Random:
                default:
                    switch (dataType)
                    {
                        case DataType.Number:
                            strtemp += GenerateNumberRandomCode(rule.DataLenght);
                            break;
                        case DataType.NumberAndChar:
                            strtemp +=  GenerateCharNumberRandomCode(rule.DataLenght);
                            break;
                        case DataType.Char:
                            strtemp +=  GenerateCharRandomCode(rule.DataLenght);
                            break;
                        default:
                            strtemp +=  GenerateCharNumberRandomCode(rule.DataLenght);
                            break;
                    }
                    break;
            }
            return strtemp;
        }




        public async static Task<string> GetCode(Int64 index, OrderFangWeiCodeRule rule)
        {
            string strtemp = "";
            ParagraphType paragraphType = ParagraphTransform.GetParagrapType(rule.PharaType);
            DataType dataType = ParagraphTransform.GetDataType(rule.DataType);
            switch (paragraphType)
            {

                case ParagraphType.Date:
                    strtemp += DateTime.Now.ToString(rule.ParaContent);
                    break;
                case ParagraphType.FixValue:
                    strtemp = rule.ParaContent.Trim();
                    break;
                case ParagraphType.Serial:
                    strtemp += index.ToString().PadLeft(rule.DataLenght, '0');
                    break;
                case ParagraphType.Random:
                default:
                    switch (dataType)
                    {
                        case DataType.Number:
                            strtemp +=  GenerateNumberRandomCode(rule.DataLenght);
                            break;
                        case DataType.NumberAndChar:
                            strtemp +=  GenerateCharNumberRandomCode(rule.DataLenght);
                            break;
                        case DataType.Char:
                            strtemp +=  GenerateCharRandomCode(rule.DataLenght);
                            break;
                        default:
                            strtemp += GenerateCharNumberRandomCode(rule.DataLenght);
                            break;
                    }
                    break;
            }
            return strtemp;
        }




        /// <summary>
        /// 获取码数据
        /// </summary>
        /// <param name="paraType"></param>
        /// <param name="codeLen"></param>
        /// <param name="codeFormat"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        //public static string GetCheckCode(ParagraphType paraType, int codeLen = 0, string codeFormat = null, Int64 currentValue = 0)
        //{
        //    string TempCode = "";
        //    if (paraType == ParagraphType.Date && (codeFormat.Trim() == "" || codeFormat == null))
        //        throw new Exception("获取日期码段时，日期格式不能为空");
        //    if ((paraType == ParagraphType.Random || paraType == ParagraphType.Serial) && codeLen == 0)
        //        throw new Exception("获取随机数据时，码段的长度不能为0");
        //    if (paraType == ParagraphType.Serial && codeLen == 0)
        //        throw new Exception("获取序列码段时，码段的长度不能为0");


        //    switch (paraType)
        //    {
        //        case ParagraphType.Date:
        //            TempCode = DateTime.Now.ToString(codeFormat);
        //            break;
        //        case ParagraphType.Random:
        //            var temp = new Random().NextDouble();
        //            string strTemp = temp.ToString();
        //            strTemp = strTemp.Substring(2, strTemp.Length - 2);
        //            TempCode = strTemp.Substring(0, codeLen);
        //            break;
        //        case ParagraphType.Serial:
        //            TempCode = (currentValue).ToString().PadLeft(codeLen, '0');
        //            break;


        //    }

        //    return TempCode;
        //}








    }
}
