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
    
    public partial class StudentClassCource
    {
        public int StudentClassCourceId { get; set; }
        public int ClassCourceId { get; set; }
        public int OrderId { get; set; }
        public string StudentClassId { get; set; }
        public Nullable<System.DateTime> StudentJoin_Time { get; set; }
        public string StudentClassAddress { get; set; }
        public string StudentPlayBackAddress { get; set; }
        public string TeacherComment { get; set; }
        public string StudentComment { get; set; }
        public Nullable<System.DateTime> TeacherComment_Time { get; set; }
        public Nullable<System.DateTime> StudentCommentDate { get; set; }
        public bool IsShowHome { get; set; }
    
        public virtual ClassCource ClassCource { get; set; }
        public virtual Order Order { get; set; }
    }
}
