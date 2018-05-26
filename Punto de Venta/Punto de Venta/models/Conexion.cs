using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Punto_de_Venta.models
{
    class Conexion
    {
        //HACEMOS LA CONEXION
        string cadenaConexion = "Data Source=VERONICA;Initial Catalog=expendio;Integrated Security=true";
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        SqlDataReader read;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter sqa = new SqlDataAdapter();
    
        public void AbrirConexion()
        {
            try
            {
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
                Console.WriteLine("CONEXION ABIERTA");

            }
            catch(Exception)
            {
                Console.WriteLine("HA OCURRIDO UN ERROR");
                conexion.Dispose();
             }
        }

        public void cerrarConexion()
        {
            conexion.Close();
        }

        public int AgregarProducto(string codigo, string id_producto, string nombre, int cantidad, int precio)
        {
            comando = new SqlCommand(codigo, conexion);
            comando.CommandType = System.Data.CommandType.Text;
            comando.Parameters.AddWithValue("@Id_producto", id_producto); //creando coleccion de los parametros.
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Cantidad", cantidad);
            comando.Parameters.AddWithValue("@Precio", precio);
            
            int filas = comando.ExecuteNonQuery();


            return filas;

        }
        public int AgregarVenta(string codigo, int numero_venta, string producto_id, string descripcion, int cantidad, int precio, DateTime fecha)
        {
            comando = new SqlCommand(codigo, conexion);
            comando.CommandType = System.Data.CommandType.Text;
            comando.Parameters.AddWithValue("@ID_Venta", numero_venta); //creando coleccion de los parametros.
            comando.Parameters.AddWithValue("@ID_Producto", producto_id);
            comando.Parameters.AddWithValue("@Nombre", descripcion);
            comando.Parameters.AddWithValue("@Cantidad", cantidad);
            comando.Parameters.AddWithValue("@Precio", precio);
            comando.Parameters.AddWithValue("@Fecha", fecha);
            int filas = comando.ExecuteNonQuery();
            return filas;

        }

        public int HacerReporte(string codigo, int Id_venta, string Producto, int Cantidad, int Total, DateTime Fecha)
        {
            comando = new SqlCommand(codigo, conexion);
            comando.CommandType = System.Data.CommandType.Text;
            comando.Parameters.AddWithValue("@Id_venta", Id_venta); //creando coleccion de los parametros.
            comando.Parameters.AddWithValue("@Producto", Producto);
            comando.Parameters.AddWithValue("@Cantidad", Cantidad);
            comando.Parameters.AddWithValue("@Total", Total);
            comando.Parameters.AddWithValue("@Fecha", Fecha);
            int filas = comando.ExecuteNonQuery();
            return filas;

        }

        public int ModificarProducto(string codigo)
        {
            int filas = 0;
            comando = new SqlCommand(codigo, conexion);
            comando.CommandType = System.Data.CommandType.Text;
            filas = comando.ExecuteNonQuery();
            return filas;
        }

        public bool Buscarid(long id)
        {
            bool respuesta = false;
            comando = new SqlCommand("select ID_Producto from Producto",conexion);
            read = comando.ExecuteReader();

            while (read.Read())
            {
                if (id == Convert.ToInt64(read["ID_Producto"].ToString()))
                {
                    respuesta = true;
                }

            }
            read.Close();
            return respuesta;
        }

        public string BuscarNombre(string cadena)
        {
            string nombre;
            comando = new SqlCommand(cadena, conexion);
            comando.CommandType = CommandType.Text;
            nombre = comando.ExecuteScalar().ToString();
            return nombre;
        }

        public int BuscarCantidades(string cadena)
        {
            int numero;
            comando = new SqlCommand(cadena, conexion);
            comando.CommandType = CommandType.Text;
            numero = Convert.ToInt32(comando.ExecuteScalar());
            return numero;
        }

        public int EliminarProducto(string cadena )
        {
            int filas;
            comando = new SqlCommand(cadena, conexion);
            comando.CommandType = CommandType.Text;
            filas = comando.ExecuteNonQuery();
            return filas;
        }

        //
        public DataTable Producto_tabla(string cadena)
        {
            sqa.SelectCommand = new SqlCommand(cadena, conexion);
            sqa.Fill(ds);
            dt=ds.Tables[0];
            return dt;

        }

        public DataTable Venta_Producto(string cadena)
        {
            sqa = new SqlDataAdapter();
            ds = new DataSet();
            dt = new DataTable();
            sqa.SelectCommand = new SqlCommand(cadena, conexion);
            sqa.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }

        public DataTable Venta_Reportes(string sql)
        {
            sqa = new SqlDataAdapter();
            ds = new DataSet();
            dt = new DataTable();
            sqa.SelectCommand = new SqlCommand(sql, conexion);
            sqa.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }

        public int idventa(string sql)
        {
            int id = 0; 
            comando = new SqlCommand(sql, conexion);
            comando.CommandType = CommandType.Text;
            try
            {
                id = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (InvalidCastException)
            {
                id = 0;
            }
            
            return id;
        }

        public void EliminarVenta_Producto(string sql)
        {
            comando = new SqlCommand(sql, conexion);
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
        }
    }
}
