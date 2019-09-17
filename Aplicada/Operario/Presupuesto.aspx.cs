using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicada.Models;

namespace Aplicada.Operario
{
    public partial class Presupuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null)
            {
                int id = Int32.Parse(Request.Params["id"]);

                using (var context = new Entities1())
                {
                    Ordene orden = context.Ordenes.Where(x => x.Id == id).FirstOrDefault();

                    if (orden != null)
                    {
                        lblCodigo.Text = "#" + orden.Id;
                        lblFecha.Text = "Fecha: " + ((DateTime)orden.fecha).ToShortDateString();

                        lblVehiculo.Text = "Patente: "+orden.Vehiculo.patente + " - Modelo: "+orden.Vehiculo.Modelo.nombre + " - Año: "+orden.Vehiculo.anio;

                        lblCliente.Text = orden.Cliente.apellido + ", " + orden.Cliente.nombre + " - DNI: " + orden.Cliente.dni;

                        List<Servicio> servicios = new List<Servicio>();

                        foreach(OrdenesServicio os in orden.OrdenesServicios){

                            servicios.Add(os.Servicio);

                        }

                        listaServicios.DataSource = servicios;
                        listaServicios.DataBind();

                        float precioFinal = 0;
                        foreach (Servicio s in servicios)
                        {
                            precioFinal += Precio_Servicio(s.Id);
                        }

                        lblTotal.Text = precioFinal.ToString();
                    }
                    else
                    {
                        Response.Redirect("~/Error404");
                    }
                }
            }
            else
            {
                Response.Redirect("~/Error404.aspx");
            }
            
        }

        protected float Precio_Servicio(int id)
        {
            using (var db = new Entities1())
            {

                Servicio s = db.Servicios.Where(x => x.Id == id).FirstOrDefault();
                List<Producto> productos = new List<Producto>();

                foreach (ServiciosProducto sp in s.ServiciosProductos)
                {
                    productos.Add(sp.Producto);
                }

                float precio = (float) s.precio_base;
                foreach (Producto p in productos)
                {
                    precio += (float) p.precio;
                }

                return precio;
            }
        }

        protected void Emitir(object sender, EventArgs e)
        {

            using (var db = new Entities1())
            {
                
                int id= Int32.Parse(Request.Params["id"]);
                Ordene orden= db.Ordenes.Where(x => x.Id == id).FirstOrDefault();

                List<Servicio> servicios = new List<Servicio>();
                foreach (OrdenesServicio os in orden.OrdenesServicios)
                {
                    servicios.Add(os.Servicio);
                }

                bool falta = false;
                foreach (Servicio s in servicios)
                {
                    
                    foreach(ServiciosProducto sp in s.ServiciosProductos){

                        if (sp.Producto.stock < 1)
                        {
                            falta = true;
                        }

                    }
                }

                if (falta)
                {
                    lblMessage.Text = "Uno o varios servicios no disponibles por falta de stock";
                }
                else
                {
                    Response.Redirect("~/Operario/EmitirOrden.aspx?id=" + Request.Params["id"]);
                }
            }

        }

    }
}