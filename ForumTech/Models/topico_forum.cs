//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForumTech.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class topico_forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public topico_forum()
        {
            this.postagem = new HashSet<postagem>();
        }
    
        public int id_topico_forum { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<postagem> postagem { get; set; }
    }
}
