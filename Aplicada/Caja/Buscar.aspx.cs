using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Aplicada.Models;

namespace Aplicada.Caja
{
    public partial class IndexCaja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (!IsPostBack)
            {
                BorrarDatos();
            }
        }

        private void BorrarDatos()
        {
            txtIdOrden.Text = "";
            txtPatente.Text = "";
            txtCliente.Text = "";
            txtDni.Text = "";

            divDatos.Visible = false;
        }

        protected void Buscar(object sender, EventArgs e)
        {
            String query = stringBusqueda.Text.Trim();

            if (!String.IsNullOrEmpty(query))
            {

                using (var db = new Entities1())
                {
                    int nOrden;
                    Ordene orden = null;
                    if(Int32.TryParse(query, out nOrden)){
                        orden = db.Ordenes.Where(x => x.Id == nOrden).FirstOrDefault();
                    }
                    else
                    {
                        orden = db.Ordenes.Where(x => x.Vehiculo.patente == query).OrderByDescending(x => x.Id).FirstOrDefault();
                    }

                    if (orden != null)
                    {
                        int ultimoEstado = orden.OrdenesEstados.OrderByDescending(x => x.fecha).First().Estado.Id;

                        if (ultimoEstado == 4)
                        {

                            txtIdOrden.Text = orden.Id.ToString("000000");
                            txtPatente.Text = orden.Vehiculo.patente;
                            txtCliente.Text = orden.Cliente.apellido + " " +orden.Cliente.nombre;
                            txtDni.Text = orden.Cliente.dni;

                            List<Servicio> servicios = new List<Servicio>();

                            foreach (OrdenesServicio os in orden.OrdenesServicios)
                            {

                                servicios.Add(os.Servicio);

                            }

                            float precioFinal = 0;
                            foreach (Servicio s in servicios)
                            {
                                precioFinal += Precio_Servicio(s.Id);
                            }

                            txtTotal.Text = precioFinal.ToString();

                            divDatos.Visible = true;
                        }
                        else if( ultimoEstado < 4)
                        {
                            lblError.Visible = true;
                            lblError.Text = "La orden aùn no esta finalizada.";
                            BorrarDatos();
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "La orden ya fue abonada anteriormente.";
                            BorrarDatos();
                        }
                    }
                    else
                    {
                        lblError.Text = "No se encontró una orden.";
                        lblError.Visible = true;
                        BorrarDatos();
                    }

                }

            }
        }

        protected void Pagar(object sender, EventArgs e)
        {
            int ordenID;

            if (Int32.TryParse(txtIdOrden.Text, out ordenID))
            {

                int metodoPago = Int32.Parse(ddMetodo.SelectedValue);

                if (metodoPago > 0)
                {

                    using (var db = new Entities1())
                    {

                        Ordene orden = db.Ordenes.Where(x => x.Id == ordenID).FirstOrDefault();

                        if (orden != null)
                        {
                            OrdenesEstado oe = new OrdenesEstado();
                            oe.fecha = DateTime.Now;
                            oe.estado_id = 5;
                            oe.usuario_id = User.Identity.GetUserId();
                            orden.OrdenesEstados.Add(oe);
                            orden.forma_pago = metodoPago;

                            db.SaveChanges();

                            //TODO: print ticket
                            lblError.Text = "La orden fue abonada correctamente.";
                            lblError.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "Algo salio mal, lo sentimos.";
                            lblError.Visible = true;
                        }

                    }

                }
                else
                {
                    lblError.Text = "Seleccione un metodo de pago";
                    lblError.Visible = true;
                }

            }
            else
            {
                lblError.Text = "No hay una orden seleccionada";
                lblError.Visible = true;
            }
        }

        protected void Metodo_Elegido(object sender, EventArgs e)
        {
            DropDownList metodos = (DropDownList)sender;

            int metodoPago = Int32.Parse(metodos.SelectedValue);

            if (metodoPago == 1)
            {
                divCalculadora.Visible = true;
            }
            else
            {
                divCalculadora.Visible = false;
            }

            if (metodoPago > 0)
            {
                btnPagar.Enabled = true;
            }
            else
            {
                btnPagar.Enabled = false;
            }
        }

        protected void Calcular(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTotal.Text) && !String.IsNullOrWhiteSpace(txtTotal.Text))
            {
                float total = float.Parse(txtTotal.Text);
                float efectivo;

                if (float.TryParse(txtEfectivo.Text, out efectivo))
                {
                    if (efectivo < total)
                    {
                        lblError.Text = "Monto ingresado invalido";
                        lblError.Visible = true;
                    }
                    else
                    {
                        txtVuelto.Text = (efectivo - total).ToString();
                    }
                }
                else
                {
                    lblError.Text = "Monto ingresado invalido";
                    lblError.Visible = true;
                }
            }
        }

        private float Precio_Servicio(int id)
        {
            using (var db = new Entities1())
            {

                Servicio s = db.Servicios.Where(x => x.Id == id).FirstOrDefault();
                List<Producto> productos = new List<Producto>();

                foreach (ServiciosProducto sp in s.ServiciosProductos)
                {
                    productos.Add(sp.Producto);
                }

                float precio = (float)s.precio_base;
                foreach (Producto p in productos)
                {
                    precio += (float)p.precio;
                }

                return precio;
            }
        }
    }
}