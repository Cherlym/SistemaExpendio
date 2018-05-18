using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_PuntoVenta.Models
{
    class Venta
    {
        private int id;
        private int ventaTotal;
        private int pago;
        private int feria;
        private DateTime fecha;

        public Venta(int id, int ventaTotal, int pago, int feria, DateTime fecha)
        {
            this.id = id;
            this.ventaTotal = ventaTotal;
            this.pago = pago;
            this.feria = feria;
            this.fecha = fecha;
        }

        public int Id { get { return id; } set { id = value; } }
        public int VentaTotal { get { return ventaTotal; } set{ventaTotal=value;} }
        public int Pago { get { return pago; } set { pago = value; } }
        public int Feria { get { return feria; } set { feria = value; } }
        public DateTime Fecha { get { return fecha; } set {fecha = value; } }
    }
}