using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_PuntoVenta.Models
{
    class Producto
    {
        long codigo;
        string nombre;
        long precioUnidad;
        int cantidad;

        public Producto(long codigo, string nombre, long precioUnidad, int cantidad)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.precioUnidad = precioUnidad;
            this.cantidad = cantidad;
        }

        public long Codigo { get; set; }
        public string Nombre { get; set; }
        public long PrecioUnidad { get; set; }
        public int Cantidad { get; set; }
    }
}