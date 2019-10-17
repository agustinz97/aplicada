using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicada.Models;
using Microsoft.AspNet.Identity;

namespace Aplicada.Operario
{
    public partial class Presupuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Request.Params["id"] != null)
            {
                int id = Int32.Parse(Request.Params["id"]);
                using (var context = new Entities1())
                {
                    Ordene orden = context.Ordenes.Where(x => x.Id == id).FirstOrDefault();

                    if (orden != null)
                    {
                            lblCodigo.Text = "#" + orden.Id.ToString("000000");
                            lblFecha.Text = "Fecha: " + ((DateTime)orden.fecha).ToShortDateString();

                            lblVehiculo.Text = "Patente: " + orden.Vehiculo.patente + " - Modelo: " + orden.Vehiculo.Modelo.nombre + " - Año: " + orden.Vehiculo.anio;

                            lblCliente.Text = orden.Cliente.apellido + ", " + orden.Cliente.nombre + " - DNI: " + orden.Cliente.dni;

                            lblVencimiento.Text += orden.fecha.Value.AddDays(15).ToShortDateString();

                            OrdenesEstado oe = orden.OrdenesEstados.Where(x => x.estado_id == 1).First();
                            Empleado emp = context.Empleados.Where(x => x.usuario_id == oe.usuario_id).FirstOrDefault();

                            if (emp != null)
                            {
                                string nombreEmpleado = emp.nombre + " " + emp.apellido;
                                lblOperario.Text += nombreEmpleado;
                            }

                            List<Servicio> servicios = new List<Servicio>();

                            foreach (OrdenesServicio os in orden.OrdenesServicios)
                            {

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
                        divPresupeusto.Visible = false;
                        divError.Visible = true;
                        txtError.Text = "Nº de orden no encontrado.";
                     }
                        
                }
            }
            else
            {
                divPresupeusto.Visible = false;
                divError.Visible = true;
                txtError.Text = "Nº de orden no encontrado.";
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

        
    }
}