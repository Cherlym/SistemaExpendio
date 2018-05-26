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
    public partial class AgregarProducto : Form
    {
        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void txtid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;

            }else if (Char.IsControl(e.KeyChar)){
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }
        //AGREGAR PRODUCTOS A MI BASE DE DATOS PRODUCTOS.
        private void btnagregar_Click(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            c.AbrirConexion();

            if (txtid.Text == "" || txtnombre.Text == "" || txtcantidad.Text == "" || txtprecio.Text == "")
            {
                MessageBox.Show("TODOS LOS CAMPOS DEBEN ESTAR LLENOS");
            }

            else
            {

                try
                {
                    int variable = c.AgregarProducto("insert into Producto(ID_Producto,Nombre,Cantidad,Precio) values (@Id_producto,@Nombre, @Cantidad,@Precio)",txtid.Text, txtnombre.Text, Convert.ToInt32(txtcantidad.Text), Convert.ToInt32(txtprecio.Text));
                    if (variable > 0)
                    {
                        MessageBox.Show("EL PRODUCTO SE GUARDO CORRECTAMENTE");
                        txtid.Text = "";
                        txtnombre.Text = "";
                        txtcantidad.Text = "";
                        txtprecio.Text = "";
                        c.cerrarConexion();
                    }

                }catch(Exception ef)
                {
                    MessageBox.Show("EL ID YA EXISTE", "ERROR");
                    txtid.Text = "";
                }


            }

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

       
        //MI BOTON CANCELAR PARA BORRAR CAMPOS.
        private void button1_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtcantidad.Text = "";
            txtprecio.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarProducto f = new AgregarProducto();
            f.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
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
