using NetCore.SqlELink;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Entities
{
    /// <summary>
    /// 彩票资料库
    /// </summary>
    [ELinkTable("RequestOrder")]
    public class RequestOrder : BaseClass
    {
        [ELinkColumn(ColumnName = "CodeType", ColumnDescription = "订单类型", ColumnDataType = "varchar", Length = 32)]
        public string CodeType { get; set; }

        [ELinkColumn(ColumnName = "CustName", ColumnDescription = "客户名称", ColumnDataType = "varchar", Length = 56)]
        public string CustName { get; set; }

        [ELinkColumn(ColumnName = "OrderType", ColumnDescription = "订单类型", ColumnDataType = "varchar", Length = 256)]
        public string OrderType { get; set; }

        [ELinkColumn(ColumnName = "Year", ColumnDescription = "年份", ColumnDataType = "int")]
        public int Year { get; set; }

        [ELinkColumn(ColumnName = "Month", ColumnDescription = "月份", ColumnDataType = "int")]
        public int Month { get; set; }

        [ELinkColumn(ColumnName = "OrderNo", ColumnDescription = "订单号", ColumnDataType = "varchar",Length =32)]
        public string OrderNo { get; set; }

        [ELinkColumn(ColumnName = "OrderName", ColumnDescription = "订单名称", ColumnDataType = "varchar",Length =126)]
        public string OrderName { get; set; }

        [ELinkColumn(ColumnName = "OrderCount", ColumnDescription = "订单数量", ColumnDataType = "int")]
        public int OrderCount { get; set; }

        [ELinkColumn(ColumnName = "BoxCodeCount", ColumnDescription = "箱码数量", ColumnDataType = "int")]
        public int BoxCodeCount { get; set; }

        [ELinkColumn(ColumnName = "TraceCodeCount", ColumnDescription = "产品码数量", ColumnDataType = "int")]
        public int TraceCodeCount { get; set; }

        [ELinkColumn(ColumnName = "TotalCount", ColumnDescription = "发布码总数量", ColumnDataType = "int")]
        public int TotalCount { get; set; }

        [ELinkColumn(ColumnName = "OrderAmount", ColumnDescription = "订单金额", ColumnDataType = "float")]
        public double? OrderAmount { get; set; }

        [ELinkColumn(ColumnName = "BatchRatio", ColumnDescription = "套标比例", ColumnDataType = "varchar",Length =128)]
        public string BatchRatio { get; set; }

        [ELinkColumn(ColumnName = "LossRate", ColumnDescription = "损耗率", ColumnDataType = "int")]
        public int LossRate { get; set; }

        [ELinkColumn(ColumnName = "IsBoxCodeFW", ColumnDescription = "箱码密码", ColumnDataType = "varchar",Length =1)]
        public string IsBoxCodeFW { get; set; }

        [ELinkColumn(ColumnName = "IsTraceCodeFW", ColumnDescription = "产品码密码", ColumnDataType = "varchar",Length =1)]
        public string IsTraceCodeFW { get; set; }

        [ELinkColumn(ColumnName = "Memo", ColumnDescription = "订单备注", ColumnDataType = "varchar",Length =128)]
        public string Memo { get; set; }

        [ELinkColumn(ColumnName = "OrderStatus", ColumnDescription = "订单备注", ColumnDataType = "int")]
        public int OrderStatus { get; set; }


         [ELinkColumn(ColumnName = "DownLoadFile",DefaultValue ="", ColumnDescription = "订单备注", ColumnDataType = "varchar",Length =128)]
        public string DownLoadFile { get; set; }

         [ELinkColumn(ColumnName = "ZipPassword", DefaultValue ="", ColumnDescription = "数据解压密码", ColumnDataType = "varchar",Length =128)]
        public string ZipPassword { get; set; }




    }
}
