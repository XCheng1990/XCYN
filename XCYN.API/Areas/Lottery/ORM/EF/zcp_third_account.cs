//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCYN.API.Areas.Lottery.ORM.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class zcp_third_account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string account_name { get; set; }
        public string card { get; set; }
        public string icon { get; set; }
        public string icon_pay { get; set; }
        public Nullable<int> is_soloShow { get; set; }
        public string user_id { get; set; }
        public Nullable<bool> state { get; set; }
        public Nullable<System.DateTime> add_time { get; set; }
    }
}