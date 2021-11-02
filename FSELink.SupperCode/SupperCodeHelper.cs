using FSELink.Bussiness;
using FSELink.Entities;
using FSELink.SupperCode.Common;
using FSELink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSELink.SupperCode
{
    public class SupperCodeHelper
    {
        static TraceCodesManager codesManager = new TraceCodesManager();
        static RequestOrderManager orderManager = new RequestOrderManager();
        static SystemConfigManager configManager = new SystemConfigManager();
        static OrderFangWeiCodeRuleManager fangWeiCodeRuleManager = new OrderFangWeiCodeRuleManager();
        static TraceCodeRuleManager traceCodeRuleManager = new TraceCodeRuleManager();
        static TraceCodesManager traceCodesManager = new TraceCodesManager();

        static object objLocke = new object();


        public static bool IsServerStart = false;


        public async static Task<string> GenerateCodeAsync()
        {
            try
            {
                GeneratCodePara para = await GetGeneratorPara();
                if (para.RequestOrders.Count > 0)
                {
                   new Task(()=> { GenerateByOrder(para); }).Start();
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteException(ex);
            }
            return "";
        }



        public async static Task<string> ExportFileAsync()
        {
            try
            {
                GeneratCodePara para = await GetExportPara();
                if (para.RequestOrders.Count > 0)
                {
                    new Task(() => { ExportFileAsync(para); }).Start();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
            return "";
        }



        private async static Task<bool> ExportFileAsync(GeneratCodePara para)
        {
            GeneratCodePara generatCodePara = new GeneratCodePara();
            bool blSucess = false;
            try
            {
                foreach (RequestOrder order in para.RequestOrders)
                {
                    if (!IsServerStart) break;
                    generatCodePara = new GeneratCodePara();
                    generatCodePara.RequestOrders.Add(order);
                    generatCodePara.TraceCodeRule = para.TraceCodeRule;
                    generatCodePara.BoxCodeRule = para.BoxCodeRule;
                    generatCodePara.DoCodeRule = para.DoCodeRule;
                    generatCodePara.OrderBoxFWRules = para.OrderBoxFWRules;
                    generatCodePara.OrderTraceFWRules = para.OrderTraceFWRules;
                    generatCodePara.GenerateConfig = para.GenerateConfig;
                    await ExportOrderDataToFile(generatCodePara);
                }
                if (!IsServerStart)
                {
                    blSucess = IsServerStart;
                    foreach (RequestOrder order in para.RequestOrders) order.OrderStatus = 3;
                    await orderManager.Update(para.RequestOrders);
                }
                else
                    blSucess = true;

            }
            catch(Exception ex)
            {
                LogHelper.WriteException(ex);
                blSucess = false;
            }
            return blSucess;

        }





        /// <summary>
        /// 获取生成数据的相关参数
        /// </summary>
        /// <returns></returns>
        private async static  Task<GeneratCodePara> GetGeneratorPara()
        {
            

            GeneratCodePara para = new GeneratCodePara();
            para.RequestOrders = await orderManager.GetList(t => t.OrderStatus == 1);
            if (para.RequestOrders.Count == 0) return para;
            List<SystemConfig> systemConfigs = await configManager.GetList();
            
            List<TraceCodeRule> TraceCodeRule = await traceCodeRuleManager.GetList(t => t.CodeType == RuleType.TraceCodeRule.ToString());
            List<TraceCodeRule> BoxCodeRule = await traceCodeRuleManager.GetList(t => t.CodeType == RuleType.BoxCodeRule.ToString());
            List<TraceCodeRule> DoCodeRule = await traceCodeRuleManager.GetList(t => t.CodeType == RuleType.DoCodeRule.ToString()) ;

            para.GenerateConfig = systemConfigs.Count <1 ? null : systemConfigs[0];
            para.TraceCodeRule = TraceCodeRule;
            para.BoxCodeRule = BoxCodeRule;
            para.DoCodeRule = DoCodeRule;
            
            foreach(RequestOrder order in para.RequestOrders)
            {
                order.OrderStatus = 2;
            }
            if(para.RequestOrders.Count>0)  await orderManager.Update(para.RequestOrders);
            return para;
        }

        /// <summary>
        /// 获取导出数据的相关参数
        /// </summary>
        /// <returns></returns>
        private async static Task<GeneratCodePara> GetExportPara()
        {
            GeneratCodePara para = new GeneratCodePara();
            para.RequestOrders = await orderManager.GetList(t => t.OrderStatus == 3);
            if (para.RequestOrders.Count == 0) return para;
            List<SystemConfig> systemConfigs = await configManager.GetList();

            List<TraceCodeRule> TraceCodeRule = await traceCodeRuleManager.GetList(t => t.CodeType == RuleType.TraceCodeRule.ToString());
            List<TraceCodeRule> BoxCodeRule = await traceCodeRuleManager.GetList(t => t.CodeType == RuleType.BoxCodeRule.ToString());
            List<TraceCodeRule> DoCodeRule = await traceCodeRuleManager.GetList(t => t.CodeType == RuleType.DoCodeRule.ToString());

            para.GenerateConfig = systemConfigs.Count < 1 ? null : systemConfigs[0];
            para.TraceCodeRule = TraceCodeRule;
            para.BoxCodeRule = BoxCodeRule;
            para.DoCodeRule = DoCodeRule;
            foreach (RequestOrder order in para.RequestOrders)
            {
                order.OrderStatus = 4;
            }
            if (para.RequestOrders.Count > 0) await orderManager.Update(para.RequestOrders);

            return para;
        }



        private async static Task<bool> GenerateByOrder(GeneratCodePara para)
        {
            GeneratCodePara generatCodePara = new GeneratCodePara();
            bool blSucess = false;
            foreach (RequestOrder order in para.RequestOrders)
            {
                generatCodePara = new GeneratCodePara();
                generatCodePara.RequestOrders.Add(order);
                generatCodePara.TraceCodeRule = para.TraceCodeRule;
                generatCodePara.BoxCodeRule = para.BoxCodeRule;
                generatCodePara.DoCodeRule = para.DoCodeRule;
                generatCodePara.OrderBoxFWRules = para.OrderBoxFWRules;
                generatCodePara.OrderTraceFWRules = para.OrderTraceFWRules;
                generatCodePara.GenerateConfig = para.GenerateConfig;
                generatCodePara.OrderTraceFWRules =await fangWeiCodeRuleManager.GetList(t => t.OrderID == order.Id);
                generatCodePara.OrderBoxFWRules= await fangWeiCodeRuleManager.GetList(t => t.OrderID == order.Id);
                await CreateCodByOneOrderAsync(generatCodePara);
            }
            if (!IsServerStart)
            {
                foreach (RequestOrder order in para.RequestOrders)
                {
                    order.OrderStatus = 1;
                    await traceCodesManager.Delete(t => t.OrderId == order.Id);
                }
                await orderManager.Update(para.RequestOrders);
                blSucess = false;
            }
            else
                blSucess = true;

            return blSucess;

        }


        private async static Task<bool> CreateCodByOneOrderAsync(GeneratCodePara para)
        {
            bool blTemp = false;
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                List<TraceCodes> traceCodes = await CreateCodeAsync(para);
                if (traceCodes.Count > 0)
                {
                    codesManager.SqlBulkCopy(traceCodes);
                    if (para.SucessOrders.Count > 0) await orderManager.Update(para.SucessOrders);
                    if (para.ErrorOrders.Count > 0) await orderManager.Update(para.ErrorOrders);
                    foreach (string str in para.ErrorMessages) LogHelper.WriteException(new Exception(str));
                    LogHelper.WriteLog("订单号："+para.RequestOrders[0].OrderNo+"   发布数量所花时间：" + Convert.ToDouble(stopwatch.ElapsedMilliseconds * 1.000 / 1000 ) + "秒");
                }
                stopwatch = null;
                blTemp = true;
            }
            catch(Exception ex)
            {
                LogHelper.WriteException(ex);
                foreach (RequestOrder order in para.RequestOrders) order.OrderStatus = 1;
                await orderManager.Update(para.RequestOrders);
                blTemp = false;
            }
            return blTemp;
        }

        private async static  Task<List<TraceCodes>> CreateCodeAsync(GeneratCodePara para)
        {
            List<TraceCodes> traceCodes = new List<TraceCodes>();
            
            DataTable DtCodes = new DataTable();
            DtCodes.Columns.Add(new DataColumn("Barcode"));
            DtCodes.PrimaryKey= new DataColumn[] { DtCodes.Columns["Barcode"] };
            foreach (RequestOrder Order in para.RequestOrders)
            {
                if(Order.CodeType.Trim()==CodeType.ScatterLabel.ToString()) //生成散标
                {
                    Int64 traceCodeCount = Convert.ToInt64(Order.TraceCodeCount * (1 + para.GenerateConfig.LoseRatio * 0.01));
                    Int64  boxCodeCount = Convert.ToInt64(Order.BoxCodeCount * (1+para.GenerateConfig.LoseRatio*0.01));
                    string strTraceCode = "",strFwCode="",strBoxCode="",strBoxFwCode="",strDoCode="";
                    #region 生产产品码
                    for (Int64 ind=0;ind< traceCodeCount; ind++ )
                    {
                        do
                        {
                            strTraceCode = await GetCode(ind, para.TraceCodeRule);
                        } while (DtCodes.Select("Barcode='"+ strTraceCode + "'").Count() > 0);
                        
                        if (Order.IsTraceCodeFW == "Y" && para.OrderTraceFWRules!=null)
                            strFwCode = await GetCode(ind, para.OrderTraceFWRules);
                        traceCodes.Add(new TraceCodes
                        {
                            Barcode = strTraceCode,
                            FwCode = strFwCode,
                            BoxCode = strBoxCode,
                            BoxFwCode = strBoxFwCode,
                            DuoCode = strDoCode,
                            OrderId = Order.Id,
                            Year = DateTime.Now.Year,
                            Month = DateTime.Now.Month,
                            CreateDate=DateTime.Now,
                            ModifyDate=DateTime.Now,
                            Createby="Admin",
                            ModifyBy="Admin",
                            CodeType = "0"
                        }) ;
                        DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strTraceCode);

                        #region 服务停止是退出发布数据
                        if (!IsServerStart)
                        {
                            traceCodes.Clear();
                            return traceCodes;
                        }
                        #endregion

                    }
                    #endregion
                    #region 生成箱码
                    for (Int64 ind = 0; ind < boxCodeCount; ind++)
                    {
                        do
                        {
                            strTraceCode = await GetCode(ind, para.BoxCodeRule);
                        } while (DtCodes.Select("Barcode='" + strTraceCode + "'").Count() > 0);

                        if (Order.IsTraceCodeFW == "Y" && para.OrderTraceFWRules!=null)
                            strFwCode = await GetCode(ind, para.OrderTraceFWRules);
                        traceCodes.Add(new TraceCodes
                        {
                            Barcode = strTraceCode,
                            FwCode = strFwCode,
                            BoxCode = strBoxCode,
                            BoxFwCode = strBoxFwCode,
                            DuoCode = strDoCode,
                            OrderId = Order.Id,
                            Year = DateTime.Now.Year,
                            Month = DateTime.Now.Month,
                            CreateDate = DateTime.Now,
                            ModifyDate = DateTime.Now,
                            Createby = "Admin",
                            ModifyBy = "Admin",
                            CodeType = "1"
                        });
                        DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strTraceCode);
                        #region 服务停止是退出发布数据
                        if (!IsServerStart)
                        {
                            traceCodes.Clear();
                            return traceCodes;
                        }
                        #endregion

                    }
                    #endregion
                    Order.OrderStatus = 3;
                   /// Order.OrderAmount = (float)Math.Round(Order.OrderAmount, 2);
                    para.SucessOrders.Add(Order);
                }
                else if(Order.CodeType.Trim()==CodeType.SetLabel.ToString().Trim()) //生套标签
                {
                    string[] BatchRation = Order.BatchRatio.Split(':');

                    int DuoCount = int.Parse(BatchRation[0]);
                    int BoxCountOfPerDuo= int.Parse(BatchRation[1]);
                    int ProductCountOfPerBox= int.Parse(BatchRation[2]);
                    Int64 OrderCount = Convert.ToInt64(Order.OrderCount * (1 + para.GenerateConfig.LoseRatio * 0.01));
                    Int64 boxCodeCount = Convert.ToInt64(Order.BoxCodeCount * (1 + para.GenerateConfig.LoseRatio * 0.01));
                    string strTraceCode = "", strFwCode = "", strBoxCode = "", strBoxFwCode = "", strDoCode = "";


                    if (DuoCount > 1)
                    {
                        BoxCountOfPerDuo = BoxCountOfPerDuo / DuoCount;
                        ProductCountOfPerBox = ProductCountOfPerBox / DuoCount;
                        DuoCount = DuoCount / DuoCount;
                        if (BoxCountOfPerDuo <1)
                        {
                            Order.OrderStatus = 1;
                            //Order.OrderAmount = (float)Math.Round(Order.OrderAmount, 2);
                            para.ErrorOrders.Add(Order);
                            para.ErrorMessages.Add("包装比例错误(订单号：" + Order.OrderNo + "---" + Order.BatchRatio + ")：垛码比例大于0时，箱码数量必须大于0");
                            continue;
                        }
                        if (DuoCount > BoxCountOfPerDuo)
                        {
                            Order.OrderStatus = 1;
                            //Order.OrderAmount = (float)Math.Round(Order.OrderAmount, 2);
                            para.ErrorOrders.Add(Order);
                            para.ErrorMessages.Add("包装比例错误(订单号：" + Order.OrderNo + "---" + Order.BatchRatio + ")：包装比例中箱码数量必须小于垛数量");
                            continue;
                        }
                    }
                    if(BoxCountOfPerDuo<1)
                    {
                        Order.OrderStatus = 1;
                        //Order.OrderAmount = (float)Math.Round(Order.OrderAmount, 2);
                        para.ErrorOrders.Add(Order);
                        para.ErrorMessages.Add("包装比例错误(订单号：" + Order.OrderNo + "---" + Order.BatchRatio + ")：包装比例中箱码数量大于0");
                        continue;
                    }
                   ProductCountOfPerBox = ProductCountOfPerBox / BoxCountOfPerDuo;

                    
                    #region 生成套标
                    for (Int64 ind = 0; ind < OrderCount; ind++)
                    {


                        if (DuoCount > 0)
                        {
                             
                            do
                            {
                                strDoCode = await GetCode(ind, para.DoCodeRule);
                            } while (DtCodes.Select("Barcode='" + strDoCode + "'").Count() > 0);

                            traceCodes.Add(new TraceCodes
                            {
                                Barcode = strDoCode,
                                FwCode = "",
                                BoxCode = "",
                                BoxFwCode = "",
                                DuoCode = "",
                                OrderId = Order.Id,
                                Year = DateTime.Now.Year,
                                Month = DateTime.Now.Month,
                                CreateDate = DateTime.Now,
                                ModifyDate = DateTime.Now,
                                Createby = "Admin",
                                ModifyBy = "Admin",
                                CodeType = "2"
                            });
                            DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strDoCode);


                            for (int ind1=0;ind1< BoxCountOfPerDuo; ind1++)
                            {
                                do
                                {
                                    strBoxCode = await GetCode(ind, para.BoxCodeRule);
                                } while (DtCodes.Select("Barcode='" + strBoxCode + "'").Count() > 0);

                                if (Order.IsBoxCodeFW == "Y" && para.OrderBoxFWRules != null)
                                    strBoxFwCode = await GetCode(ind, para.OrderBoxFWRules);
                                traceCodes.Add(new TraceCodes
                                {
                                    Barcode = strBoxCode,
                                    FwCode = strBoxFwCode,
                                    BoxCode = "",
                                    BoxFwCode = "",
                                    DuoCode = strDoCode,
                                    OrderId = Order.Id,
                                    Year = DateTime.Now.Year,
                                    Month = DateTime.Now.Month,
                                    CreateDate = DateTime.Now,
                                    ModifyDate = DateTime.Now,
                                    Createby = "Admin",
                                    ModifyBy = "Admin",
                                    CodeType = "1"
                                });
                                DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strBoxCode);

                                //if (BoxCountOfPerDuo > 1)
                                //    ProductCountOfPerBox = ProductCountOfPerBox / BoxCountOfPerDuo;

                                for (int ind2 = 0; ind2 < ProductCountOfPerBox; ind2++)
                                {
                                    do
                                    {
                                        strTraceCode = await GetCode(ind, para.TraceCodeRule);
                                    } while (DtCodes.Select("Barcode='" + strTraceCode + "'").Count() > 0);

                                    if (Order.IsTraceCodeFW == "Y" && para.OrderTraceFWRules != null)
                                        strFwCode = await GetCode(ind, para.OrderTraceFWRules);
                                    traceCodes.Add(new TraceCodes
                                    {
                                        Barcode = strTraceCode,
                                        FwCode = strFwCode,
                                        BoxCode = strBoxCode,
                                        BoxFwCode = strBoxFwCode,
                                        DuoCode = strDoCode,
                                        OrderId = Order.Id,
                                        Year = DateTime.Now.Year,
                                        Month = DateTime.Now.Month,
                                        CreateDate = DateTime.Now,
                                        ModifyDate = DateTime.Now,
                                        Createby = "Admin",
                                        ModifyBy = "Admin",
                                        CodeType = "0"
                                    });
                                    DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strTraceCode);
                                }

                                #region 服务停止是退出发布数据
                                if (!IsServerStart)
                                {
                                    traceCodes.Clear();
                                    return traceCodes;
                                }
                                #endregion

                            }
                        }
                        else 
                        {
                            //if (BoxCountOfPerDuo > 1)
                            //{
                            //    ProductCountOfPerBox = ProductCountOfPerBox / BoxCountOfPerDuo;
                            //    BoxCountOfPerDuo = BoxCountOfPerDuo / BoxCountOfPerDuo;
                            //}
                            do
                            {
                                strBoxCode = await GetCode(ind, para.BoxCodeRule);
                            } while (DtCodes.Select("Barcode='" + strBoxCode + "'").Count() > 0);
                            if (Order.IsBoxCodeFW == "Y" && para.OrderBoxFWRules!=null)
                                strBoxFwCode = await GetCode(ind, para.OrderBoxFWRules);
                            traceCodes.Add(new TraceCodes
                            {
                                Barcode = strBoxCode,
                                FwCode = strBoxFwCode,
                                BoxCode = "",
                                BoxFwCode = "",
                                DuoCode = "",
                                OrderId = Order.Id,
                                Year = DateTime.Now.Year,
                                Month = DateTime.Now.Month,
                                CreateDate = DateTime.Now,
                                ModifyDate = DateTime.Now,
                                Createby = "Admin",
                                ModifyBy = "Admin",
                                CodeType = "1"
                            });
                            DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strBoxCode);
                            #region 服务停止是退出发布数据
                            if (!IsServerStart)
                            {
                                traceCodes.Clear();
                                return traceCodes;
                            }
                            #endregion

                            for (int ind2 = 0; ind2 < ProductCountOfPerBox; ind2++)
                            {
                                do
                                {
                                    strTraceCode = await GetCode(ind, para.TraceCodeRule);
                                } while (DtCodes.Select("Barcode='" + strTraceCode + "'").Count() > 0);

                                if (Order.IsTraceCodeFW == "Y" && para.OrderTraceFWRules != null)
                                    strFwCode = await GetCode(ind2, para.OrderTraceFWRules);
                                traceCodes.Add(new TraceCodes
                                {
                                    Barcode = strTraceCode,
                                    FwCode = strFwCode,
                                    BoxCode = strBoxCode,
                                    BoxFwCode = strBoxFwCode,
                                    DuoCode = "",
                                    OrderId = Order.Id,
                                    Year = DateTime.Now.Year,
                                    Month = DateTime.Now.Month,
                                    CreateDate = DateTime.Now,
                                    ModifyDate = DateTime.Now,
                                    Createby = "Admin",
                                    ModifyBy = "Admin",
                                    CodeType = "0"
                                });
                                DtCodes.Rows.Add(DtCodes.NewRow()["Barcode"] = strTraceCode);
                                #region 服务停止是退出发布数据
                                if (!IsServerStart)
                                {
                                    traceCodes.Clear();
                                    return traceCodes;
                                }
                                #endregion

                            }


                        }

                    }
                    #endregion

                    Order.OrderStatus = 3;
                   // Order.OrderAmount = (float)Math.Round(Order.OrderAmount, 2);
                    para.SucessOrders.Add(Order);
                }
            }
            #region 服务停止是退出发布数据
            if (!IsServerStart)
                traceCodes.Clear();

            #endregion


            return traceCodes;
        }


        private async  static Task<string> GetCode(Int64 codeIndex,List<TraceCodeRule> codeRule)
        {
            
            string strCode = "";
            lock(objLocke)
            {
            
                foreach (TraceCodeRule rule in codeRule)
                {
                    strCode+= ParagraphCode.GetCode(codeIndex, rule);
                }
            }
            return strCode;
        }


        private async static Task<string> GetCode(Int64 codeIndex, List<OrderFangWeiCodeRule> codeRule)
        {

            string strCode = "";
            //lock (objLocke)
            //{
                foreach (OrderFangWeiCodeRule rule in codeRule)
                {
                    strCode += await ParagraphCode.GetCode(codeIndex, rule);
                }
            //}
            return strCode;
        }
    
    
        private async static Task<bool> ExportOrderDataToFile(GeneratCodePara para)
        {
            RequestOrder order = para.RequestOrders[0];
            List<TraceCodes> traceCodes;
            List<TraceCodes> boxCodes;
            List<TraceCodes> duoCodes;
            List<TraceCodes> OrderCodes;
            string strUrl = "";
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDuo = new StringBuilder(), sbBox = new StringBuilder();
            bool blTemp = false;
            int duoCount = 0, boxCount = 0, traceCodeCount = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string strSavedirectory = order.CustName + "(" + order.OrderNo + ")_" + order.TotalCount;
            string strSavePath = SystemInfo.DataExportPath + "\\" + strSavedirectory;
            if (!Directory.Exists(strSavePath))
                Directory.CreateDirectory(strSavePath);

            if (ParagraphTransform.GetOrderType(order.OrderType) == OrderType.XM) strUrl = para.GenerateConfig.XMURL;
            else strUrl = para.GenerateConfig.MSZZURL;
            if (strUrl.IndexOf("?code=") < 0)
                strUrl += "?code=";

            OrderCodes= await codesManager.GetList(t => t.OrderId == order.Id);
            if (ParagraphTransform.GetCodeType(para.RequestOrders[0].CodeType) == CodeType.SetLabel)
            {
                string[] BatchRation = order.BatchRatio.Split(':');

                int DuoCount = int.Parse(BatchRation[0]);
                int BoxCountOfPerDuo = int.Parse(BatchRation[1]);
                int ProductCountOfPerBox = int.Parse(BatchRation[2]);

                if(DuoCount> 0)
                {
                    duoCount = DuoCount / DuoCount * order.OrderCount;
                    boxCount = (BoxCountOfPerDuo / DuoCount) * order.OrderCount;
                    traceCodeCount=(ProductCountOfPerBox/DuoCount)* order.OrderCount;
                }
                else
                {
                    boxCount = (BoxCountOfPerDuo / BoxCountOfPerDuo) * order.OrderCount;
                    traceCodeCount = (ProductCountOfPerBox / BoxCountOfPerDuo) * order.OrderCount;
                }


                string temp = "";
                temp="产品码";
                if(order.IsTraceCodeFW.ToUpper()=="Y")
                    temp+=",防伪密码";
                temp+=",箱/包码";
                if (order.IsBoxCodeFW.ToUpper() == "Y")
                    temp += ",箱/包防伪密码";


               // duoCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType=="2");

                duoCodes = OrderCodes.Where(t => t.CodeType == "2").ToList();
                if(duoCodes.Count>0)
                {
                    sbDuo = new StringBuilder();
                    sbBox = new StringBuilder();

                    temp += ",垛码";
                    sb.AppendLine(temp);                    
                    sbDuo.AppendLine("垛码");
                    temp = "箱码";
                    if (order.IsBoxCodeFW.ToUpper() == "Y")
                        temp+=",箱码密码";
                    temp += ",垛码";
                    sbBox.AppendLine(temp);
                    foreach (TraceCodes duo in duoCodes)
                    {
                        //boxCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType == "1" && t.DuoCode==duo.Barcode );
                        boxCodes = OrderCodes.Where(t=>t.CodeType=="1" && t.DuoCode==duo.Barcode).ToList();
                        sbDuo.AppendLine(duo.Barcode);
                        foreach (TraceCodes box in boxCodes)
                        {
                            //traceCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType == "0" && t.BoxCode==box.Barcode );
                            traceCodes = OrderCodes.Where(t => t.CodeType == "0" && t.BoxCode == box.Barcode && t.DuoCode==duo.Barcode).ToList();
                            if (order.IsBoxCodeFW.ToUpper() == "Y")
                                sbBox.AppendLine(strUrl+ box.Barcode + "," +box.FwCode +","+duo.Barcode);
                            else
                                sbBox.AppendLine(box.Barcode+"," +duo.Barcode);
                            foreach (TraceCodes code in traceCodes)
                            {
                                temp = strUrl + code.Barcode;
                                if (order.IsTraceCodeFW.ToUpper() == "Y")
                                    temp += "," + code.FwCode;
                                if (order.IsBoxCodeFW.ToUpper() == "Y")
                                    temp += ","+strUrl + box.Barcode+","+ box.FwCode;
                                else
                                    temp += "," + box.Barcode ;
                                temp += "," + code.DuoCode;
                                sb.AppendLine(temp);
                            }
                            if (!IsServerStart) return false;
                        }
                    }
                }
                else
                {
                    //boxCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType == "1" );
                    sbBox = new StringBuilder();
                    temp = "箱码";                    
                    if (order.IsBoxCodeFW.ToUpper() == "Y")
                        temp+=",箱码密码";
                    sbBox.AppendLine(temp);
                    boxCodes = OrderCodes.Where(t => t.CodeType == "1").ToList();
                    foreach (TraceCodes box in boxCodes)
                    {
                       // traceCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType == "0" && t.BoxCode == box.BoxCode);
                        traceCodes = OrderCodes.Where(t => t.CodeType == "0" && t.BoxCode==box.Barcode).ToList();
                        if (order.IsBoxCodeFW.ToUpper() == "Y")
                            sbBox.AppendLine(strUrl+ box.Barcode + "," + box.FwCode);
                        else
                            sbBox.AppendLine(box.Barcode );
                        foreach (TraceCodes code in traceCodes)
                        {
                            temp = strUrl + code.Barcode;
                            if (order.IsTraceCodeFW.ToUpper() == "Y")
                                temp += "," + code.FwCode;                            
                            if (order.IsBoxCodeFW.ToUpper() == "Y")
                                temp += "," +strUrl+ box.FwCode;
                            else
                                temp += "," + code.BoxCode;
                            sb.AppendLine(temp);
                        }
                        if (!IsServerStart) return false;
                    }

                }


                string TraceCodefileName = order.CustName + "(" + order.OrderNo + ")_产品码_" + traceCodeCount + ".txt";
                string BoxCodefileName = order.CustName + "(" + order.OrderNo + ")_箱码_" + boxCount + "_.txt";
                string DuoCodefileName = order.CustName + "(" + order.OrderNo + ")_垛码_" + duoCount + ".txt";

                TraceCodefileName = strSavePath + "\\" + TraceCodefileName;
                StreamWriter sr = File.AppendText(TraceCodefileName);
                sr.Write(sb.ToString());
                sr.Close();
                if (sbBox.Length>0)
                {
                    BoxCodefileName = strSavePath + "\\" + BoxCodefileName;
                    sr = File.AppendText(BoxCodefileName);
                    sr.Write(sbBox.ToString());
                    sr.Close();
                }
                if (sbDuo.Length>0)
                {
                    DuoCodefileName = strSavePath + "\\" + DuoCodefileName;
                    sr = File.AppendText(DuoCodefileName);
                    sr.Write(sbDuo.ToString());
                    sr.Close();
                }

            }
            else
            {
                sb = new StringBuilder();
                boxCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType == "1" );
                traceCodes = await codesManager.GetList(t => t.OrderId == order.Id && t.CodeType == "0");
                string strtemp= "箱码";
                if (order.IsBoxCodeFW.ToUpper() == "Y")
                    strtemp += ",箱码密码";
                sb.AppendLine(strtemp);
                foreach(TraceCodes code in boxCodes)
                {
                    if (order.IsBoxCodeFW.ToUpper() == "Y") 
                        strtemp =strUrl+ code.Barcode+ "," + code.FwCode;
                    else
                        strtemp = code.Barcode;
                    sb.AppendLine(strtemp);
                }
                if (!IsServerStart) return false;
               // string fileName = order.CustName + "(" + order.OrderNo + ")_箱码_" + boxCodes.Count + ".txt";
                string fileName = order.CustName + "(" + order.OrderNo + ")_箱码_" + order.BoxCodeCount + ".txt";
                fileName = strSavePath + "\\" + fileName;
                StreamWriter sr = File.AppendText(fileName);
                sr.Write(sb.ToString());
                sr.Close();



                sb = new StringBuilder();
                strtemp = "追溯码";
                if (order.IsTraceCodeFW.ToUpper() == "Y")
                    strtemp += ",防伪密码";
                sb.AppendLine(strtemp);
                foreach (TraceCodes code in traceCodes)
                {
                    strtemp =strUrl+ code.Barcode;
                    if (order.IsTraceCodeFW.ToUpper() == "Y") strtemp += "," + code.FwCode;
                    sb.AppendLine(strtemp);
                }
                if (!IsServerStart) return false;
               // fileName = order.CustName + "(" + order.OrderNo + ")_产品码_" + traceCodes.Count + ".txt";
                fileName = order.CustName + "(" + order.OrderNo + ")_产品码_" + order.TraceCodeCount + ".txt";
                fileName = strSavePath + "\\" + fileName;
                sr = File.AppendText(fileName);
                sr.Write(sb.ToString());
                sr.Close();


            }



            string zipfilename = SystemInfo.DataExportPath+"\\" +strSavedirectory + ".zip";
            if (File.Exists(zipfilename))
                File.Delete(zipfilename);
            ZipHelper.ZipDirectory(strSavePath, zipfilename, para.GenerateConfig.ZipPassword);
            foreach(string file in Directory.GetFiles(strSavePath)) File.Delete(file);
            Directory.Delete(strSavePath);
            order.DownLoadFile = zipfilename;
            order.ZipPassword = para.GenerateConfig.ZipPassword;
            order.OrderStatus = 5;
            await orderManager.Update(order);
            blTemp = true;

            LogHelper.WriteLog("订单号：" + order.OrderNo + "   数据导出所花时间：" + Convert.ToDouble((stopwatch.ElapsedMilliseconds * 1.00 / 1000)) + "秒");
            stopwatch = null;
            return blTemp;
        }
    
       
    }
}
