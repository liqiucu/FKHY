//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FKHY.Models.DBModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public int DocumentIndex { get; set; }
        public string FileType { get; set; }
        public Nullable<System.DateTime> WebCreate_Time { get; set; }
        public string Url { get; set; }
        public string File_Id { get; set; }
        public Nullable<System.DateTime> ThirdCreate_Time { get; set; }
        public int CourceId { get; set; }
    
        public virtual Course Course { get; set; }
    }
}
