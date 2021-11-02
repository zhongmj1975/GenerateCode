using System;
using System.Linq;
using System.Text;
using NetCore.SqlELink;

namespace FSELink.Entities
{
    ///<summary>
    ///
    ///</summary>
    [ELinkTable("SystemConfig")]
    public partial class SystemConfig : BaseClass
    {

        [ELinkColumn( ColumnName = "XMURL", ColumnDescription = "信码URL地址",  ColumnDataType = "varchar", Length = 256)]
        public string XMURL { get; set; }

        [ELinkColumn(ColumnName = "MSZZURL", ColumnDescription = "码上增值URL地址", ColumnDataType = "varchar",Length =256)]
        public string MSZZURL { get; set; }

        [ELinkColumn(ColumnName = "DataInterface",ColumnDescription = "码上增值数据上传URL地址", ColumnDataType = "varchar",Length =256)]
        public string DataInterface { get; set; }
        
        [ELinkColumn(ColumnName = "ZipPassword", ColumnDescription = "数据包解压密码", ColumnDataType = "varchar",Length =256)]
        public string ZipPassword { get; set; }
        

        [ELinkColumn(ColumnName = "LoseRatio", ColumnDescription = "生产损耗", ColumnDataType = "int")]
        public int LoseRatio { get; set; }



     
    }
}
