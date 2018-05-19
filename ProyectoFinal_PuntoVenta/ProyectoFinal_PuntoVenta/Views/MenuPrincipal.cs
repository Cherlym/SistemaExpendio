using ProyectoFinal_PuntoVenta.Models;
using ProyectoFinal_PuntoVenta;
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
using ProyectoFinal_PuntoVenta.Views;

namespace ProyectoFinal_PuntoVenta
{
    public partial class MenuPrincipal : Form
    {
        int codigo=0;
        int TotalVenta=0;
        string nombrePro;
        int precioPro;
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrin_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'productoVenta.VentaProducto' Puede moverla o quitarla según sea necesario.
            this.ventaProductoTableAdapter.Fill(this.productoVenta.VentaProducto);
            // TODO: esta línea de código carga datos en la tabla 'puntoVentaDataSet2.Producto' Puede moverla o quitarla según sea necesario.
            this.productoTableAdapter2.Fill(this.puntoVentaDataSet2.Producto);
            // TODO: esta línea de código carga datos en la tabla 'puntoVenta.Venta' Puede moverla o quitarla según sea necesario.
            this.ventaTableAdapter.Fill(this.puntoVenta.Venta);
            // TODO: esta línea de código carga datos en la tabla 'puntoVentaDataSet.Producto' Puede moverla o quitarla según sea necesario.
            this.productoTableAdapter.Fill(this.puntoVentaDataSet.Producto);
            ConnectionSQLServer conexion = new ConnectionSQLServer();
            conexion.OpenConnection();
            ProductosDT.DataSource = conexion.TablaProductos("select * from Producto");
            GridVentas.DataSource = conexion.TablaVentaProducto("select * from VentaProducto");
            DatetimeReporte.Value = DateTime.Now;
            conexion.LlenaComboBox(comboBox1);
            comboBox1.SelectedItem = null;
            conexion.CloseConnection();
        }

        private void AgregarBtn_Click(object sender, EventArgs e)
        {
            ConnectionSQLServer conexion = new ConnectionSQLServer();
            conexion.OpenConnection();
            bool validar=false;
            if (CodigoTxB.Text == "" || NombreTxB.Text == "" || PrecioUnidadTxB.Text == "" || CantidadTxB.Text == "")
            {
                MessageBox.Show("Favor de llenar todos los parametros para poder guardar el producto");
            }
            else
            {
                foreach (DataGridViewRow row in ProductosDT.Rows)
                {
                    string codigo = Convert.ToString(row.Cells["CodigoColumn"].Value);
                    string nombre = Convert.ToString(row.Cells["NombreColumn"].Value);
                    if (CodigoTxB.Text == codigo || NombreTxB.Text == nombre)
                    {
                        validar = true;
                    }
                }
                if (validar==true)
                {
                    MessageBox.Show("El codigo o el nombre que desea ingresar ya existe, favor de colocar otro codigo o nombre");
                    CodigoTxB.Text = "";
                    NombreTxB.Text = "";
                }
                else
                {
                    int filas = conexion.IngresarDatos("Insert Into Producto(Codigo, Nombre, PrecioUnidad, Cantidad) Values(@Codigo,@Nombre,@PrecioUnidad,@Cantidad)", Int32.Parse(CodigoTxB.Text), NombreTxB.Text, Int32.Parse(PrecioUnidadTxB.Text), Int32.Parse(CantidadTxB.Text));
                    if (filas > 0)
                    {
                        MessageBox.Show("Lo datos se guardaron correctamente");
                        CodigoTxB.Text = "";
                        NombreTxB.Text = "";
                        PrecioUnidadTxB.Text = "";
                        CantidadTxB.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema al guardar los datos");
                    }
                }
            }
            ProductosDT.DataSource = conexion.TablaProductos("select * from Producto");
            conexion.ActualizarItems(comboBox1);
            conexion.CloseConnection();
        }

        private void EliminarBtn_Click(object sender, EventArgs e)
        {
            ConnectionSQLServer conexion = new ConnectionSQLServer();
            conexion.OpenConnection();
            if (CodigoTxB.Text=="")
            {
                MessageBox.Show("No se puede eliminar datos vacios");
            }
            else
            {
                if (MessageBox.Show("Seguro que desea eliminar?","Eliminar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conexion.EliminarDatos("delete from producto where codigo = '" + CodigoTxB.Text + "'");
                    ProductosDT.DataSource = conexion.TablaProductos("select * from Producto");
                    CodigoTxB.Text = "";
                    NombreTxB.Text = "";
                    PrecioUnidadTxB.Text = "";
                    CantidadTxB.Text = "";
                }
            }
            conexion.ActualizarItems(comboBox1);
            conexion.CloseConnection();
        }

        private void ModificarBtn_Click(object sender, EventArgs e)
        {
            ConnectionSQLServer conexion = new ConnectionSQLServer();
            conexion.OpenConnection();
            if (CodigoTxB.Text == "" || NombreTxB.Text=="" || PrecioUnidadTxB.Text==""|| CantidadTxB.Text=="")
            {
                MessageBox.Show("No se puede modificar datos vacios");
            }
            else
            {
                if (MessageBox.Show("Seguro que desea modificar?", "Modificar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ProductosDT.CurrentRow.Cells[0].Value = CodigoTxB.Text;
                    ProductosDT.CurrentRow.Cells[1].Value = NombreTxB.Text;
                    ProductosDT.CurrentRow.Cells[2].Value = PrecioUnidadTxB.Text;
                    ProductosDT.CurrentRow.Cells[3].Value = CantidadTxB.Text;
                    int filas = conexion.ModificarDatos("UPDATE producto SET Codigo='" + CodigoTxB.Text + "',Nombre='" +NombreTxB.Text+ "',PrecioUnidad='" + PrecioUnidadTxB.Text + "',Cantidad='" + CantidadTxB.Text + "' where Codigo='" + codigo + "'");
                    if (filas > 0)
                    {
                        MessageBox.Show("Se ha modificado correctamente");
                        ProductosDT.DataSource = conexion.TablaProductos("select * from Producto");
                        CodigoTxB.Text = "";
                        NombreTxB.Text = "";
                        PrecioUnidadTxB.Text = "";
                        CantidadTxB.Text = "";
                    }
                }
            }
            conexion.ActualizarItems(comboBox1);
            conexion.CloseConnection();
        }

        private void ProductosDT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ConnectionSQLServer conexion = new ConnectionSQLServer();
            conexion.OpenConnection();
            if ((CodigoTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[0].Value))=="" ||
                (NombreTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[1].Value))=="" ||
                (PrecioUnidadTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[2].Value))==""||
                (CantidadTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[3].Value))=="")
            {
                CodigoTxB.Text = "";
                NombreTxB.Text = "";
                PrecioUnidadTxB.Text = "";
                CantidadTxB.Text = "";
            }
            else
            {
                codigo = Convert.ToInt32(ProductosDT.CurrentRow.Cells[0].Value);
                CodigoTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[0].Value);
                NombreTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[1].Value);
                PrecioUnidadTxB.Text =Convert.ToString(this.ProductosDT.CurrentRow.Cells[2].Value);
                CantidadTxB.Text = Convert.ToString(this.ProductosDT.CurrentRow.Cells[3].Value);
                CodigoTxB.Enabled = true;
                NombreTxB.Enabled = true;
                PrecioUnidadTxB.Enabled = true;
                CantidadTxB.Enabled = true;
            }
            conexion.CloseConnection();
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            ConnectionSQLServer connection = new ConnectionSQLServer();
            connection.OpenConnection();
            if (CantidadProTxB.Text=="" || NombrePro.Text=="" || comboBox1.SelectedItem.ToString()=="")
            {
                MessageBox.Show("Favor de llenar todos los campos");
            }
            else if (Convert.ToInt32(CantidadProTxB.Text)<=0)
            {
                MessageBox.Show("No se permiten cantidades negativas y tampoco cantidades en cero");
                CantidadProTxB.Text = "";
            }
            else
            {
                int cod = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                int can = connection.VerCantidad("select Cantidad from Producto where Codigo='" + cod + "'");
                if (Convert.ToInt32(CantidadProTxB.Text)>can)
                {
                    MessageBox.Show("Lo sentimos la cantidad que ingreso revasa la cantidad en almacen");
                    comboBox1.SelectedItem = null;
                    CantidadProTxB.Text = "";
                }
                else
                {
                    int cantidadF = can - Convert.ToInt32(CantidadProTxB.Text);
                    connection.ModificarDatos("UPDATE Producto SET Cantidad='" + cantidadF + "' where Codigo='" + cod + "'");
                    int ventaT = precioPro * Convert.ToInt32(CantidadProTxB.Text);
                    int filas = connection.IngresarDatosVenta("Insert Into VentaProducto (Nombre,PrecioUnidad,Cantidad,TotalVenta) Values(@Nombre,@PrecioUnidad,@Cantidad,@TotalVenta)", nombrePro, precioPro, Convert.ToInt32(CantidadProTxB.Text), ventaT);
                    if (filas > 0)
                    {
                        TotalVenta += ventaT;
                        NombrePro.Text = "";
                        CantidadProTxB.Text = "";
                        comboBox1.SelectedItem = null;
                    }
                }
            }
            ProductosDT.DataSource = connection.TablaProductos("select * from Producto");
            GridVentas.DataSource = connection.TablaVentaProducto("select * from VentaProducto");
            connection.CloseConnection();
        }

        private void PagarBtn_Click(object sender, EventArgs e)
        {
            ConnectionSQLServer connection = new ConnectionSQLServer();
            connection.OpenConnection();
            Pagar pagar = new Pagar();
            pagar.Show();
            DateTime date = Convert.ToDateTime(DatetimeReporte.Value = DateTime.Now);
            pagar.VentaTotal(TotalVenta);
            TotalVenta = 0;
            int ventaT = pagar.Venta;
            int feria = pagar.Feria;
            int paga = pagar.Paga;
            int idVenta = connection.SeleccionarIdVenta("SELECT MAX(IdVenta) FROM Venta GROUP BY IdVenta");
            if (idVenta == 0)
            {
                idVenta = 1;
            }
            foreach (DataGridViewRow row in GridVentasReporte.Rows)
            {
                int ids = Convert.ToInt32(row.Cells["IdVenta"].Value);
                if (idVenta==ids)
                {
                    idVenta++;
                }
            }
                connection.IngresarDatosReporte("insert into Venta (IdVenta,VentaTotal,Pago,Feria,Fecha) values (@IdVenta,@VentaTotal,@Pago,@Feria,@Fecha)", idVenta, ventaT, paga, feria, date);
                GridVentasReporte.DataSource = connection.TablaVenta("select * from Venta");
                connection.EliminarDatosVenta("delete from VentaProducto");
                GridVentas.DataSource = connection.TablaVentaProducto("select * from VentaProducto");
                idVenta += 1;
            
            connection.CloseConnection();
        }

        private void CancelarBtn_Click(object sender, EventArgs e)
        {
            ConnectionSQLServer connection = new ConnectionSQLServer();
            connection.OpenConnection();
            if (MessageBox.Show("Seguro que desea cancelar la compra?", "Modificar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                connection.EliminarDatosVenta("delete from VentaProducto");
                GridVentas.DataSource = connection.TablaVentaProducto("select * from VentaProducto");
                TotalVenta = 0;
                connection.CloseConnection();
            }
        }

        //Validaciones de los componentes del programa

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ConnectionSQLServer connection = new ConnectionSQLServer();
            connection.OpenConnection();
            Console.WriteLine(comboBox1.SelectedValue);
            if (comboBox1.SelectedItem != null )
            {
                int cod = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                if (cod == -1)
                {
                    MessageBox.Show("El codigo que se selecciono no existe");
                }
                else
                {
                    NombrePro.Text = connection.VerNombre("select Nombre from Producto where Codigo='" + cod + "'");
                    precioPro = connection.verPrecios("select PrecioUnidad from Producto where Codigo='" + cod + "'");
                    nombrePro = NombrePro.Text;
                }
            }
            else
            {
                NombrePro.Text = "";
            }
            connection.CloseConnection();
        }


        private void MenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConnectionSQLServer connection = new ConnectionSQLServer();
            connection.OpenConnection();
            connection.EliminarDatosVenta("delete from VentaProducto");
            TotalVenta = 0;
            connection.CloseConnection();
        }

        private void CodigoVentaTxB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==-1)
            {
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem==null)
            {
                NombrePro.Text = "";
            }

        }

        private void CantidadProTxB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DatetimeReporte_ValueChanged(object sender, EventArgs e)
        {
            ConnectionSQLServer connection = new ConnectionSQLServer();
            connection.OpenConnection();
            connection.CloseConnection();
        }

        private void CodigoTxB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PrecioUnidadTxB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void CantidadTxB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
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