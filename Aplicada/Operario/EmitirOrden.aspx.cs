using Aplicada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicada.Operario
{
    public partial class EmitirOrden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["id"] != null)
            {

                int id = Int32.Parse(Request.Params["id"]);

                using (var db = new Entities1())
                {

                    Ordene orden = db.Ordenes.Where(x => x.Id == id).FirstOrDefault();

                    if (orden != null)
                    {
                        List<Estado> estados = new List<Estado>();
                        foreach (OrdenesEstado ordenestado in orden.OrdenesEstados)
                        {
                            estados.Add(ordenestado.Estado);
                        }

                        if (estados.Find(x => x.Id == 2) == null)
                        {
                            OrdenesEstado oe = new OrdenesEstado();
                            oe.estado_id = 2;
                            oe.fecha = DateTime.Now;
                            orden.OrdenesEstados.Add(oe);

                            orden.mecanico_id = "869c9903-b4a0-4e98-be52-f132bcbc4e2a";

                            foreach (OrdenesServicio os in orden.OrdenesServicios)
                            {
                                foreach (ServiciosProducto sp in os.Servicio.ServiciosProductos)
                                {
                                    sp.Producto.stock -= 1;
                                }
                            }

                            db.SaveChanges();

                            lblMessage.Text = "Orden Emitida";
                        }
                        else
                        {
                            lblMessage.Text = "La orden ya fue emitida anteriormente.";
                        }   

                    }
                    else
                    {
                        Response.Redirect("~/Error404.aspx");
                    }

                }

            }
            else
            {
                Response.Redirect("~/Error404.aspx");
            }

        }
    }
}