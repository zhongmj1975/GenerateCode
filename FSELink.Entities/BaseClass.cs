using NetCore.SqlELink;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Entities
{
    public class BaseClass
    {
        [ELinkColumn(ColumnName = "ID", ColumnDescription = "标识号",  ColumnDataType = "int", IsIdentity = true,IsPrimaryKey =true)]
        public long Id { get; set; }

        [ELinkColumn(ColumnName = "Createby", ColumnDescription = "创建用户", ColumnDataType = "varchar",Length =32)]
        public string Createby { get; set; }

        [ELinkColumn(ColumnName = "CreateDate", ColumnDescription = "创建日期", ColumnDataType = "datetime")]
        public DateTime CreateDate { get; set; }

         [ELinkColumn(ColumnName = "ModifyBy", ColumnDescription = "更改用户", ColumnDataType = "varchar",Length =32 )]
        public string ModifyBy { get; set; }

        [ELinkColumn(ColumnName = "ModifyDate", ColumnDescription = "更改日期", ColumnDataType = "datetime")]
        public DateTime ModifyDate { get; set; }



    }
}
