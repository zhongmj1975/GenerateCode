using NetCore.SqlELink;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Entities
{
    [ELinkTable("TraceCodes")]
    public class TraceCodes : BaseClass
    {
        [ELinkColumn(ColumnName = "Barcode", ColumnDescription = "产品码",IsPrimaryKey =true, ColumnDataType = "varchar", Length = 32)]
        public string Barcode { get; set; }


        [ELinkColumn(ColumnName = "FwCode", ColumnDescription = "产品码防伪码", ColumnDataType = "varchar", Length = 32)]
        public string FwCode { get; set; }

         [ELinkColumn(ColumnName = "BoxCode", ColumnDescription = "箱码", ColumnDataType = "varchar", Length = 32)]
        public string BoxCode { get; set; }


        [ELinkColumn(ColumnName = "BoxFwCode", ColumnDescription = "箱码防伪码",IsPrimaryKey =true, ColumnDataType = "varchar", Length = 32)]
        public string BoxFwCode { get; set; }

         [ELinkColumn(ColumnName = "DuoCode", ColumnDescription = "垛码",IsPrimaryKey =true, ColumnDataType = "varchar", Length = 32)]
        public string DuoCode { get; set; }

        [ELinkColumn(ColumnName = "CodeType", ColumnDescription = "组名称", ColumnDataType = "varchar",  Length = 16)]
        public string CodeType { get; set; }

        [ELinkColumn(ColumnName = "OrderId", ColumnDescription = "订单ID", ColumnDataType = "bigint",  Length = 16)]
        public long OrderId { get; set; }

         [ELinkColumn(ColumnName = "Year", ColumnDescription = "年份", ColumnDataType = "int")]
        public int Year { get; set; }

         [ELinkColumn(ColumnName = "Month", ColumnDescription = "月份", ColumnDataType = "int")]
        public int Month { get; set; }
        
    }
}
