//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCYN.Linq.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class user_groups
    {
        public int id { get; set; }
        public string title { get; set; }
        public Nullable<int> grade { get; set; }
        public Nullable<int> upgrade_exp { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<int> point { get; set; }
        public Nullable<int> discount { get; set; }
        public Nullable<byte> is_default { get; set; }
        public Nullable<byte> is_upgrade { get; set; }
        public Nullable<byte> is_lock { get; set; }
    }
}
