//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Parqueadero.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Registro
    {
        public int IdRegistro { get; set; }
        public string Placa { get; set; }
        public Nullable<System.DateTime> FechaEntrada { get; set; }
        public Nullable<int> IdServicio { get; set; }
        public Nullable<int> IdUser { get; set; }
        public Nullable<bool> Estado { get; set; }
        public System.DateTime FechaSalida { get; set; }
        public float CostoFinal { get; set; }
    
        public virtual Servicio Servicio { get; set; }
        public virtual User User { get; set; }
    }
}
