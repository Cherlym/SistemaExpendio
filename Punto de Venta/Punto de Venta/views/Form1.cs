using Punto_de_Venta.models;
using Punto_de_Venta.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta
{
    public partial class Form1 : Form
    {
        decimal total = 0; 
        public object[] ID_Producto { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Catalogo c = new Catalogo();
            c.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto ag = new AgregarProducto();
            ag.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            c.AbrirConexion();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar m = new Modificar();
            m.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar el = new Eliminar();
            el.Show();
        }

        //METER EL CODIGO Y QUE LO BUSQUE PARA HACER LA VENTA.
         private void Codigo_KeyPress(object sender, KeyPressEventArgs e)
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

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                
                string cadenaConexion = String.Format("Data Source=VERONICA;Initial Catalog=expendio;Integrated Security=true");

                string sq = "select*from Producto where ID_Producto = " + Codigo.Text;

                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                SqlCommand comando = new SqlCommand(sq, conexion);
                SqlDataReader read = comando.ExecuteReader();
                DataTable tabla = new DataTable();
                bool hola = false;
                while (read.Read())
                {
                    if (Convert.ToInt64(read["ID_Producto"]) == Convert.ToInt64(Codigo.Text))
                        {
                            int precio = Convert.ToInt32(read["Precio"]);
                            dataGridView1.Rows.Add(read["ID_Producto"],read["Nombre"],txtcantidad.Text,Convert.ToInt32(txtcantidad.Text)*precio);
                          
                    }
                }
                conexion.Close();
            }
        }
        // TE ARROJA EL TOTAL DE LA COMPRA.
        private void btntotal_Click(object sender, EventArgs e)
        {
            txtTotal.Text = total.ToString();
        }

        private void btnCambio_Click(object sender, EventArgs e)
        {
            int cambio = 0;
            if (txtEfectivo.Text == "" && txtTotal.Text=="")
            {
                MessageBox.Show("NO DEJE CAMPOS VACIOS");
            }
            else
            {
                if (Convert.ToInt32(txtTotal.Text)==0)
                {
                    MessageBox.Show("LA COMPRA ES DE 0 PESOS");
                    txtTotal.Text = "";
                    txtEfectivo.Text = "";
                }
                else
                {
                    if (Convert.ToInt32(txtEfectivo.Text) < Convert.ToInt32(txtTotal.Text))
                    {
                        MessageBox.Show("FALTA EFECTIVO");
                    }
                    else
                    {
                        cambio = Convert.ToInt32(txtEfectivo.Text) - Convert.ToInt32(txtTotal.Text);
                        txtCambio.Text = cambio.ToString();
                    }
                }
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Codigo.Text ="";
            txtcantidad.Text ="";
            
        }
        //muestra el ticket
        private void button1_Click_1(object sender, EventArgs e)
        {
            Ticket T = new Ticket();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion();
            if (txtCambio.Text=="")
            {
                MessageBox.Show("NO SE HA REALIZADO LA COMPRA");
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) //OBTENER ID Y METER DATOS EN LAS TABLAS QUE VA CADA UNO
                {
                    DateTime fecha = DateTime.Today;
                    int id = conexion.idventa("select max(ID_Venta) from venta");
                    int id2 = conexion.idventa("select max(ID_Venta) from Reporte");
                    id++;
                    id2++;
                    int filas = conexion.AgregarVenta("insert into venta (ID_Venta,ID_Producto,Nombre,Cantidad,Precio,Fecha)values(@ID_Venta,@ID_Producto,@Nombre,@Cantidad,@Precio,@Fecha)", id, dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value), Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value), fecha);
                    int filas2 = conexion.HacerReporte("insert into Reporte (Id_venta,Producto,Cantidad,Total,Fecha)values(@Id_venta,@Producto,@Cantidad,@Total,@Fecha)",id2, dataGridView1.Rows[i].Cells[1].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value),Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value), fecha);
                }
                txtTotal.Text = "";
                txtCambio.Text = "";
                txtEfectivo.Text = "";
                dataGridView1.Rows.Clear();
                T.Show();
                total = 0;
            }
            conexion.cerrarConexion();
        }

        //CUANDO CAMBIAMOS LA CANTIDAD DE MI CAMPO TXTCANTIDAD.
        private void txtcantidad_TextChanged(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //MessageBox.Show("ENTER");
                string cadenaConexion = String.Format("Data Source=VERONICA;Initial Catalog=expendio;Integrated Security=true");

                string sq = "select*from Producto where ID_Producto = " + Codigo.Text;
                Conexion conexion2 = new Conexion();
                conexion2.AbrirConexion();
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                int cantidad = conexion2.BuscarCantidades("select Cantidad from Producto where ID_Producto='"+ Convert.ToInt64(Codigo.Text) + "'");
                if (Convert.ToInt32(txtcantidad.Text) <= 0)
                {
                    MessageBox.Show("NO VALIDA");
                }
                else
                {
                    if (Convert.ToInt32(txtcantidad.Text) > cantidad) //PARA VER SI TENEMOS LA CANTIDAD NECESARIA PARA LA VENTA.
                    {
                        MessageBox.Show("NO SE CUENTA CON LA CANTIDAD DEL PRODUCTO SELECCIONADO");
                    }
                    else
                    {
                        int cantidadNueva = cantidad - Convert.ToInt32(txtcantidad.Text);
                        long cod = Convert.ToInt64(Codigo.Text);
                        conexion2.ModificarProducto("UPDATE Producto SET Cantidad = '" + cantidadNueva + "' where ID_Producto = '" +cod+ "'");
                        SqlCommand comando = new SqlCommand(sq, conexion);
                        SqlDataReader read = comando.ExecuteReader();
                        DataTable tabla = new DataTable();
                        while (read.Read())
                        {
                            if (Convert.ToInt32(read["ID_Producto"]) == Convert.ToInt32(Codigo.Text))
                            {
                                int precio = Convert.ToInt32(read["Precio"]);
                                dataGridView1.Rows.Add(read["ID_Producto"], read["Nombre"], txtcantidad.Text, Convert.ToInt32(txtcantidad.Text) * precio);
                               total = total + Convert.ToInt32(read["Precio"]) * Convert.ToInt32(txtcantidad.Text);

                            }
                        }
                       
                    }
                    
                }
                
            }

        }

        //para que agregue la venta al datagrid.
        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
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

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                
                string cadenaConexion = String.Format("Data Source=VERONICA;Initial Catalog=expendio;Integrated Security=true");

                string sq = "select*from Producto where ID_Producto = " + Codigo.Text;
                Conexion conexion2 = new Conexion();
                conexion2.AbrirConexion();
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                int cantidad = conexion2.BuscarCantidades("select Cantidad from Producto where ID_Producto='" + Convert.ToInt64(Codigo.Text) + "'");
                if (Convert.ToInt32(txtcantidad.Text) <= 0)
                {
                    MessageBox.Show("NO VALIDA");
                }
                else
                {
                    if (Convert.ToInt32(txtcantidad.Text) > cantidad)
                    {
                        MessageBox.Show("NO SE CUENTA CON LA CANTIDAD DEL PRODUCTO SELECCIONADO");
                    }
                    else
                    {
                        int cantidadNueva = cantidad - Convert.ToInt32(txtcantidad.Text);
                        long cod = Convert.ToInt64(Codigo.Text);
                        conexion2.ModificarProducto("UPDATE Producto SET Cantidad = '" + cantidadNueva + "' where ID_Producto = '" + cod + "'");
                        SqlCommand comando = new SqlCommand(sq, conexion);
                        SqlDataReader read = comando.ExecuteReader();
                        DataTable tabla = new DataTable();
                        while (read.Read())
                        {
                            if (Convert.ToInt64(read["ID_Producto"]) == Convert.ToInt64(Codigo.Text))
                            {
                                int precio = Convert.ToInt32(read["Precio"]);
                                dataGridView1.Rows.Add(read["ID_Producto"], read["Nombre"], txtcantidad.Text, Convert.ToInt32(txtcantidad.Text) * precio);
                                total = total + Convert.ToInt32(read["Precio"]) * Convert.ToInt32(txtcantidad.Text);

                            }
                        }
                    }
                }
                Codigo.Text = "";
                txtcantidad.Text = "";
            }
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
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
        //CANCELAR LA VENTA.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA VENTA?", "CANCELAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
            }
                
        }
    }
}