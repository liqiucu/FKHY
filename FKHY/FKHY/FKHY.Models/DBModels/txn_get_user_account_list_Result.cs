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
    
    public partial class txn_get_user_account_list_Result
    {
        public long AccountId { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public Nullable<bool> Default { get; set; }
        public decimal Balance { get; set; }
        public decimal FrozenBalance { get; set; }
    }
}
