using ProyectoFinal_PuntoVenta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_PuntoVenta.Views
{
    public partial class Pagar : Form
    {
        int venta=0;
        int feria=0;
        int paga=0;
        public Pagar()
        {
            InitializeComponent();
        }

        public void VentaTotal(int ventaT)
        {
            venta = ventaT;
            TotalVenta.Text = "$"+ venta.ToString();
        }


        public int Venta { get { return venta; } }
        public int Feria { get { return feria; } }
        public int Paga { get { return paga; } }

        private void PagarBtn_Click(object sender, EventArgs e)
        {

            if (PagaTxB.Text=="")
            {
                MessageBox.Show("Favor de ingresar datos en el campo de paga");
            }
            else if (Convert.ToInt32(PagaTxB.Text)<venta)
            {
                MessageBox.Show("La transaccion no se puede llevar a cabo, falta de efectivo");
            }
            else
            {
                paga = Convert.ToInt32(PagaTxB.Text);
                feria = (paga - venta);
                PagaTxB.Text = "";
                TotalVenta.Text = "";
                FeriaTxB.Text = "$" + feria.ToString();
            }
        }

        private void CancelarPagaBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desea cancelar la compra?", "Modificar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("La compra ha sido cancelada");
                PagaTxB.Text = "";
                TotalVenta.Text = "";
                venta = 0;
            }
        }

        private void Pagar_FormClosed(object sender, FormClosedEventArgs e)
        {
            venta = 0;
        }
    }
}