//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aplicada.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ordene
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordene()
        {
            this.OrdenesEstados = new HashSet<OrdenesEstado>();
            this.OrdenesServicios = new HashSet<OrdenesServicio>();
        }
    
        public int Id { get; set; }
        public Nullable<int> vehiculo_id { get; set; }
        public Nullable<int> cliente_id { get; set; }
        public string mecanico_id { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string operario_id { get; set; }
        public Nullable<int> forma_pago { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenesEstado> OrdenesEstados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenesServicio> OrdenesServicios { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
