using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Entities
{
   public class GeneratCodePara
   {
       /// <summary>
       /// 导出配置规则
       /// </summary>
        public SystemConfig GenerateConfig;

        /// <summary>
        /// 订单产品码防伪码规则
        /// </summary>
        public List<OrderFangWeiCodeRule>  OrderTraceFWRules;

        /// <summary>
        /// 订单箱码防伪 码的规则
        /// </summary>
        public List<OrderFangWeiCodeRule>  OrderBoxFWRules;

        /// <summary>
        /// 订单列表
        /// </summary>

        public List<RequestOrder> RequestOrders=new List<RequestOrder>();


        /// <summary>
        /// 追溯码规则
        /// </summary>
        public List<TraceCodeRule> TraceCodeRule;


        /// <summary>
        /// 订单箱码规则
        /// </summary>
        public List<TraceCodeRule> BoxCodeRule;



        /// <summary>
        /// 垛码规则
        /// </summary>
        public List<TraceCodeRule> DoCodeRule;

        /// <summary>
        /// 发码成功后的订单
        /// </summary>
        public List<RequestOrder> SucessOrders=new List<RequestOrder>();
        
        /// <summary>
        /// 发码错误的订单
        /// </summary>
        public List<RequestOrder> ErrorOrders=new List<RequestOrder>();


        public List<string> ErrorMessages=new List<string>();

        public GeneratCodePara Clone()
        {
            GeneratCodePara temp = new GeneratCodePara();
            temp.BoxCodeRule = this.BoxCodeRule;
            temp.DoCodeRule = this.DoCodeRule;
            temp.ErrorMessages = this.ErrorMessages;
            temp.ErrorOrders = this.ErrorOrders;
            temp.GenerateConfig = this.GenerateConfig;
            temp.OrderBoxFWRules = this.OrderBoxFWRules;
            temp.OrderTraceFWRules = this.OrderTraceFWRules;
            temp.RequestOrders = this.RequestOrders;
            temp.SucessOrders = this.SucessOrders;
            temp.TraceCodeRule = this.TraceCodeRule;
            return temp;
        }
    }
}
