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
    
    public partial class PublicCource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PublicCource()
        {
            this.Favs = new HashSet<Fav>();
        }
    
        public int PublicCourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeacherName { get; set; }
        public string TeacherImg { get; set; }
        public System.DateTime Create_Time { get; set; }
        public string VedeoUrl { get; set; }
        public string CoverImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fav> Favs { get; set; }
    }
}
