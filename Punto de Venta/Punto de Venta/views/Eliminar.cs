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
    public partial class Eliminar : Form
    {
        public Eliminar()
        {
            InitializeComponent();
        }

        private void txtideliminar_KeyPress(object sender, KeyPressEventArgs e)
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

        //BUSCAR UN PRODUCTO Y ELIMINARLO.
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            Conexion c1 = new Conexion();
            c1.AbrirConexion();
            if (txtideliminar.Text == "")
            {
                MessageBox.Show("NO SE PUEDE REALIZAR LA BUSQUEDA CON VALORES VACIOS", "AVISO");
            }
            else
            {
                bool respuesta = c1.Buscarid(Convert.ToInt64(txtideliminar.Text));

                if (respuesta == true)
                {
                    txtnombre.Text = c1.BuscarNombre("Select Nombre from Producto where ID_Producto= '" + Convert.ToInt64(txtideliminar.Text) + "'");
                    txtcantidad.Text = (c1.BuscarCantidades("Select Cantidad from Producto where ID_Producto= '" + Convert.ToInt64(txtideliminar.Text) + "'")).ToString();
                    txtprecio.Text = (c1.BuscarCantidades("Select Precio from Producto where ID_Producto= '" + Convert.ToInt64(txtideliminar.Text) + "'")).ToString();
                }
                else
                {
                    MessageBox.Show("EL ID: " + txtideliminar.Text + " INGRESADO NO EXISTE ");
                    txtideliminar.Text = "";
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
            txtideliminar.Text = "";
            txtnombre.Text = "";
            txtcantidad.Text = "";
            txtprecio.Text = "";
        }

        //BOTON DE ACEPTAR PARA ELIMINAR EL PRODUCTO DE LA BD.
        private void btnaceptar_Click(object sender, EventArgs e)
        {
            Conexion c1 = new Conexion();
            c1.AbrirConexion();
            if (txtideliminar.Text == "" || txtnombre.Text == "" || txtcantidad.Text == "" || txtprecio.Text == "")
            {
                MessageBox.Show("ERROR, NO SE DEBEN DEJE CAMPOS VACIOS, POR FAVOR");
            }
            else
            {
                if (MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL PRODUCTO SELECCIONADO?", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int filas = c1.EliminarProducto("delete from Producto where ID_Producto='" + Convert.ToInt64(txtideliminar.Text) + "'");
                    if (filas > 0)
                    {
                        MessageBox.Show("SE ELIMINO EL PRODUCTO");
                        txtideliminar.Text = "";
                        txtnombre.Text = "";
                        txtcantidad.Text = "";
                        txtprecio.Text = "";
                    }
                }
            }
            c1.cerrarConexion();
        }

        private void txtideliminar_TextChanged(object sender, EventArgs e)
        {
            if (txtideliminar.Text == "")
            {
                txtnombre.Text = "";
                txtcantidad.Text = "";
                txtprecio.Text = "";

            }
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
