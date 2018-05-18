using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ProyectoFinal_PuntoVenta;
using System.Windows.Forms;

namespace ProyectoFinal_PuntoVenta.Models
{
    class ConnectionSQLServer
    {
        string connectString = "Data Source=VERONICA;Initial Catalog=PuntoVenta;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection();
        DataTable table = new DataTable();
        DataSet dataSet = new DataSet();
        SqlCommand sc;
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader reader;

        public void OpenConnection()
        {
            try
            {
                sqlConnection.ConnectionString = connectString;
                sqlConnection.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("Error en Conexion"); 
                sqlConnection.Dispose();
            }
        }

        public void CloseConnection()
        {
            sqlConnection.Close();
        }

        public DataTable TablaProductos(string sql)
        {
            da.SelectCommand = new SqlCommand(sql,sqlConnection);
            da.Fill(dataSet);
            table = dataSet.Tables[0];
            return table;
        }

        public DataTable TablaVentaProducto(string sql)
        {
            da = new SqlDataAdapter();
            table = new DataTable();
            dataSet = new DataSet();
            da.SelectCommand = new SqlCommand(sql,sqlConnection);
            da.Fill(dataSet);
            table = dataSet.Tables[0];
            return table;
        }

        public DataTable TablaVenta(string sql)
        {
            da = new SqlDataAdapter();
            table = new DataTable();
            dataSet = new DataSet();
            da.SelectCommand = new SqlCommand(sql, sqlConnection);
            da.Fill(dataSet);
            table = dataSet.Tables[0];
            return table;
        }

        public int IngresarDatos(string sql,int codigo,string nombre, int precioUnidad, int cantidad)
        {
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            sc.Parameters.AddWithValue("@Codigo", codigo);
            sc.Parameters.AddWithValue("@Nombre", nombre);
            sc.Parameters.AddWithValue("@PrecioUnidad", precioUnidad);
            sc.Parameters.AddWithValue("@Cantidad", cantidad);
            int filasAfectadas = sc.ExecuteNonQuery();
            return filasAfectadas;
        } 

        public int IngresarDatosVenta(string sql, string nombre, int precio,int cantidad,int ventaT)
        {
            int filas = 0;
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            sc.Parameters.AddWithValue("@Nombre",nombre);
            sc.Parameters.AddWithValue("@PrecioUnidad", precio);
            sc.Parameters.AddWithValue("@Cantidad", cantidad);
            sc.Parameters.AddWithValue("@TotalVenta", ventaT);
            filas = sc.ExecuteNonQuery();
            return filas;
        }

        public int IngresarDatosReporte(string sql, int idVenta, int ventaTotal, int pago, int feria,DateTime time)
        {
            int filas = 0;
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            sc.Parameters.AddWithValue("@IdVenta", idVenta);
            sc.Parameters.AddWithValue("@VentaTotal", ventaTotal);
            sc.Parameters.AddWithValue("@Pago", pago);
            sc.Parameters.AddWithValue("@Feria", feria);
            sc.Parameters.AddWithValue("@Fecha", time);
            filas = sc.ExecuteNonQuery();
            return filas;
        }

        public int EliminarDatos(string sql)
        {
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            int filasAfectadas = sc.ExecuteNonQuery();
            return filasAfectadas;
        }

        public void EliminarDatosVenta(string sql)
        {
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            sc.ExecuteNonQuery();
        }

        public int ModificarDatos(string sql)
        {
            int filas=0;
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            filas = sc.ExecuteNonQuery();
            return filas;
        }

        public int verPrecios(string sql)
        {
            int precio = 0;
            sc = new SqlCommand(sql,sqlConnection);
            sc.CommandType = CommandType.Text;
            precio = Convert.ToInt32(sc.ExecuteScalar());
            return precio;
        }

        public int VerCantidad(string sql)
        {
            int Cantidad = 0;
            sc = new SqlCommand(sql,sqlConnection);
            sc.CommandType = CommandType.Text;
            Cantidad = Convert.ToInt32(sc.ExecuteScalar());
            return Cantidad;
        }

        public string VerNombre(string sql)
        {
            string respuesta;
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            respuesta = sc.ExecuteScalar().ToString();
            return respuesta;
        }

        public void LlenaComboBox(ComboBox box)
        {
            sc = new SqlCommand("select Codigo from Producto",sqlConnection);
            reader = sc.ExecuteReader();
            while (reader.Read())
            {
                box.Items.Add(reader["Codigo"].ToString());
            }
            reader.Close();
        }

        public void ActualizarItems(ComboBox box)
        {
            box.Items.Clear();
            sc = new SqlCommand("select Codigo from Producto", sqlConnection);
            reader = sc.ExecuteReader();
            while (reader.Read())
            {
                box.Items.Add(reader["Codigo"].ToString());
            }
            reader.Close();
        }

        public int SeleccionarIdVenta(string sql)
        {
            int id = 0;
            sc = new SqlCommand(sql, sqlConnection);
            sc.CommandType = CommandType.Text;
            id = Convert.ToInt32(sc.ExecuteScalar());
            return id;
        }
    }
}