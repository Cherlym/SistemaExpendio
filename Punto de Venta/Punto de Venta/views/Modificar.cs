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
    public partial class Modificar : Form
    {
        public Modificar()
        {
            InitializeComponent();
        }

        private void txtidModificar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;

            }
            else if (Char.IsControl(e.KeyChar)){
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
         //BUSCAMOS PRODUCTO
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            Conexion c1 = new Conexion();
            c1.AbrirConexion();
            if (txtidModificar.Text == "")
            {
                MessageBox.Show("LA BUSQUEDA NO SE PUEDE LLEVAR A CABO LOS VALORES VACIOS", "AVISO");
            }
            else
            {
                try 
                {
                    txtnombre.Text = c1.BuscarNombre("Select Nombre from Producto where ID_Producto= '" + (txtidModificar.Text) + "'");
                    txtcantidad.Text = (c1.BuscarCantidades("Select Cantidad from Producto where ID_Producto= '" + (txtidModificar.Text) + "'")).ToString();
                    txtprecio.Text = (c1.BuscarCantidades("Select Precio from Producto where ID_Producto= '" + (txtidModificar.Text) + "'")).ToString();
                }
                catch(Exception e45)
                {
                    MessageBox.Show("EL ID: " + txtidModificar.Text + " QUE INGRESO NO  EXISTE FAVOR DE REVISARLO");
                    txtidModificar.Text = "";
                }
            }
            c1.cerrarConexion();
        }

        
        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)){
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

       private void btncancelar_Click(object sender, EventArgs e)
        {
            txtidModificar.Text = "";
            txtnombre.Text = "";
            txtcantidad.Text = "";
            txtprecio.Text = "";
        }

        //GUARDAR LOS CAMBIOS PARA MODIFICAR LOS PRODUCTOS.
        private void btnaceptar_Click(object sender, EventArgs e)
        {
            Conexion c1 = new Conexion();
            c1.AbrirConexion();
            if (txtidModificar.Text == "" || txtnombre.Text == "" || txtcantidad.Text == "" || txtprecio.Text == "")
            {
                MessageBox.Show("ERROR, FAVOR DE NO DEJAR CAMPOS EN BLANCO");
            }
            else
            {
                if (MessageBox.Show("¿ESTA SEGURO QUE DESEA MODIFICAR EL PRODUCTO?","Modificacion",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    //VER CUANTAS FILAS FUERON AFECTADAS.
                    int filas = c1.ModificarProducto("update Producto set ID_Producto="+(txtidModificar.Text)+", Nombre='"+txtnombre.Text+"', Cantidad="+Convert.ToInt32(txtcantidad.Text)+", Precio="+Convert.ToInt32(txtprecio.Text)+" where ID_Producto="+(txtidModificar.Text)+"");
                    if (filas > 0)
                    {
                        MessageBox.Show("LA MODIFICACION FUE EXITOSA");
                        txtidModificar.Text = "";
                        txtnombre.Text = "";
                        txtcantidad.Text = "";
                        txtprecio.Text = "";
                    }
                }
            }
            c1.cerrarConexion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;

            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
