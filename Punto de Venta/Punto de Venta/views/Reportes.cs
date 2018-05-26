using Punto_de_Venta.models;
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

namespace Punto_de_Venta.views
{
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            // carga datos en la tabla 'Reporte'.
            this.reporteTableAdapter.Fill(this.expendioDataSet3.Reporte);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        //METE LOS DATOS A MI TABLA DE REPORTES JUNTO CON EL TOTAL DE CADA VENTA.
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            DateTime months = new DateTime();
            DateTime years = new DateTime();
            months = Convert.ToDateTime(dateTimePicker1.Value.Date.ToString());
            years = Convert.ToDateTime(dateTimePicker1.Value.Date.ToString());
            conexion.AbrirConexion();
            switch (months.Month)
            {
                case 1:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0101' and '" + years.Year + "0131' group by Producto");
                    break;
                case 2:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0201' and '" + years.Year + "0228' group by Producto");
                    break;
                case 3:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0301' and '" + years.Year + "0331' group by Producto");
                    break;
                case 4:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0401' and '" + years.Year + "0430' group by Producto");
                    break;
                case 5:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0501' and '" + years.Year + "0531' group by Producto");
                    break;
                case 6:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0601' and '" + years.Year + "0630' group by Producto");
                    break;
                case 7:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) from Reporte where fecha between '" + years.Year + "0701' and '" + years.Year + "0731' group by Producto");
                    break;
                case 8:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total Reporte where fecha between '" + years.Year + "0801' and '" + years.Year + "0831' group by Producto");
                    break;
                case 9:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "0901' and '" + years.Year + "0930' group by Producto");
                    break;
                case 10:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "1001' and '" + years.Year + "1031' group by Producto");
                    break;
                case 11:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "1101' and '" + years.Year + "1130' group by Producto");
                    break;
                case 12:
                    ReporteVentas.DataSource = conexion.Venta_Reportes("select Producto,sum(Cantidad) as Cantidad,sum(Total) as Total from Reporte where fecha between '" + years.Year + "1201' and '" + years.Year + "1231' group by Producto");
                    break;
                default:
                    break;
            }
            conexion.cerrarConexion();
        }
    }
}
