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
    
    public partial class TeacherDaily
    {
        public int TeacherDailyId { get; set; }
        public System.DateTime YearMonth { get; set; }
        public System.TimeSpan AM_Start { get; set; }
        public System.TimeSpan AM_End { get; set; }
        public System.TimeSpan PM_Start { get; set; }
        public System.TimeSpan PM_End { get; set; }
        public System.TimeSpan Weekend_Start { get; set; }
        public System.TimeSpan Weekend_End { get; set; }
        public int TeacherId { get; set; }
        public System.DateTime DataChange_LastTime { get; set; }
        public System.DateTime DataCreate_Time { get; set; }
    
        public virtual Teacher Teacher { get; set; }
    }
}
