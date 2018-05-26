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
    public partial class Catalogo : Form
    {
        public Catalogo()
        {
            InitializeComponent();
        }

        private void Catalogo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'Producto'.
            this.productoTableAdapter1.Fill(this.expendioDataSet1.Producto);
          
            Conexion conexion = new Conexion();
            conexion.AbrirConexion();
            dataGrid1.DataSource = conexion.Producto_tabla("Select * from Producto");
            conexion.cerrarConexion();
        }

        private void btnmenu_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            Close();
         }

        private void btnreportes_Click(object sender, EventArgs e)
        {
            Reportes r = new Reportes();
            r.Show();
            this.Hide();
        }
    }
}
