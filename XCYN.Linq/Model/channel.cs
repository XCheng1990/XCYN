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
    
    public partial class channel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public channel()
        {
            this.channel_field = new HashSet<channel_field>();
        }
    
        public int id { get; set; }
        public Nullable<int> site_id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public Nullable<byte> is_albums { get; set; }
        public Nullable<byte> is_attach { get; set; }
        public Nullable<byte> is_spec { get; set; }
        public Nullable<int> sort_id { get; set; }
    
        public virtual channel_site channel_site { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<channel_field> channel_field { get; set; }
    }
}
