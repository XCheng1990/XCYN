//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCYN.EasyUI.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class channel_field
    {
        public int id { get; set; }
        public Nullable<int> channel_id { get; set; }
        public Nullable<int> field_id { get; set; }
    
        public virtual article_attribute_field article_attribute_field { get; set; }
        public virtual channel channel { get; set; }
    }
}
