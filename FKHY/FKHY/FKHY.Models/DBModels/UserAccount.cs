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
    
    public partial class UserAccount
    {
        public long AccountId { get; set; }
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public int BankId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public bool Default { get; set; }
        public System.DateTime DateDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}