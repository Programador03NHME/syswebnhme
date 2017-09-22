//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysWebNHME.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Familias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Familias()
        {
            this.Articulos = new HashSet<Articulos>();
        }
    
        public int IDFamilia { get; set; }
        public string Descripcion { get; set; }
        public string DescCorta { get; set; }
        public string Tipo { get; set; }
        public Nullable<System.DateTime> FechaCrea { get; set; }
        public Nullable<System.DateTime> FechaModifica { get; set; }
        public Nullable<int> IDUsuarioCrea { get; set; }
        public string HostCrea { get; set; }
        public Nullable<int> IDUsuarioEdita { get; set; }
        public string HostEdita { get; set; }
        public Nullable<int> IDEstadoInventario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articulos> Articulos { get; set; }
        public virtual EstadoInventario EstadoInventario { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
