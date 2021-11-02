using NetCore.SqlELink;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Entities
{
    [ELinkTable("TraceCodeRule")]
    public class TraceCodeRule : BaseClass
    {
        [ELinkColumn(ColumnName = "CodeType",  ColumnDescription ="玩法编号", Length = 32, ColumnDataType = "varchar")]
        public string CodeType { get; set; }

        [ELinkColumn(ColumnName = "SortId", ColumnDescription = "排序ID", ColumnDataType = "int")]
        public int SortId { get; set; }

        [ELinkColumn(ColumnName = "PharaType",  ColumnDescription = "玩法名称", ColumnDataType = "varchar", Length =64)]
        public string PharaType { get; set; }

        [ELinkColumn(ColumnName = "DataType",  ColumnDescription = "数据类型", ColumnDataType = "varchar", Length =64)]
        public string DataType { get; set; }

        [ELinkColumn(ColumnName = "DataLenght",  ColumnDescription = "数据长度", ColumnDataType = "int")]
        public int DataLenght { get; set; }

        [ELinkColumn(ColumnName = "ParaContent",  ColumnDescription = "固定值内容", ColumnDataType = "varchar", Length =64)]
        public string ParaContent { get; set; }

    }
}
