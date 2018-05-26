using Punto_de_Venta.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.views
{
    
    public partial class Ticket : Form
    {
        DateTime dt = DateTime.Today;
        public Ticket()
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion();
            InitializeComponent();
            txtfecha.Text = dt.ToString();
            TicketProductos.DataSource = conexion.Venta_Producto("select * from venta");
            conexion.cerrarConexion();
        }

        private void Ticket_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'venta'.
            this.ventaTableAdapter.Fill(this.expendioDataSet2.venta);
            Conexion conexion = new Conexion();
            conexion.AbrirConexion();
            TicketProductos.DataSource = conexion.Venta_Producto("select * from venta");

        }

        //CUANDO SE CIERRA EL TICKET QUE SE BORRE DE LA BASE DE DATOS
        private void Ticket_FormClosed(object sender, FormClosedEventArgs e)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion();
            conexion.EliminarVenta_Producto("Delete from venta");
            conexion.cerrarConexion();
        }
    }
}
