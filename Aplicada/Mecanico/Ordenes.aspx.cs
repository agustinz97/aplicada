using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Aplicada.Models;

namespace Aplicada.Mecanico
{
    public partial class Ordenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string currentId = User.Identity.GetUserId();

            if (currentId == null)
            {
                Response.Redirect("~/");
            }

            using (var db = new Entities1())
            {
                List<Ordene> ordenesMecanico = db.Ordenes.Where(x => x.mecanico_id == currentId).ToList();

                List<Ordene> ordenesAsignadas = new List<Ordene>();
                List<Ordene> enProceso = new List<Ordene>();
                List<Ordene> paraRetiro = new List<Ordene>();
                foreach (Ordene o in ordenesMecanico)
                {

                    List<OrdenesEstado> ordenesEstado = o.OrdenesEstados.OrderByDescending(x => x.fecha).ToList();

                    Estado ultimoEstado = ordenesEstado.First().Estado;

                    if (ultimoEstado.Id == 2)
                    {
                        ordenesAsignadas.Add(o);
                    }
                    else if (ultimoEstado.Id == 3)
                    {
                        enProceso.Add(o);
                    }
                    else if (ultimoEstado.Id == 5){
                        paraRetiro.Add(o);
                    }
                }

                listaAsignaciones.DataSource = ordenesAsignadas;
                listaAsignaciones.DataBind();

                listaProceso.DataSource = enProceso;
                listaProceso.DataBind();

                listaRetiro.DataSource = paraRetiro;
                listaRetiro.DataBind();
            }

        }

        protected void Entregar_orden(object sender, EventArgs e)
        {

            Button btn = (Button)sender;

            int idOrden = Int32.Parse(btn.CommandArgument);

            using (var db = new Entities1())
            {
                Ordene orden = db.Ordenes.Where(x => x.Id == idOrden).First();

                OrdenesEstado oe = new OrdenesEstado();
                oe.fecha = DateTime.Now;
                oe.estado_id = 6;
                orden.OrdenesEstados.Add(oe);

                db.SaveChanges();
            }

            Response.Redirect("~/Mecanico/Ordenes.aspx");

        }

        protected void Terminar_Orden(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int idOrden = Int32.Parse(btn.CommandArgument);

            using (var db = new Entities1())
            {
                Ordene orden = db.Ordenes.Where(x => x.Id == idOrden).First();

                OrdenesEstado oe = new OrdenesEstado();
                oe.fecha = DateTime.Now;
                oe.estado_id = 4;
                orden.OrdenesEstados.Add(oe);

                db.SaveChanges();
            }

            Response.Redirect("~/Mecanico/Ordenes.aspx");
        }

        protected void Aceptar_Orden(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int idOrden = Int32.Parse(btn.CommandArgument);

            if (listaProceso.Items.Count() > 0)
            {
                lblMessage.Text = "Primero debe finalizar las ordenes en proceso.";
            }
            else
            {
                using (var db = new Entities1())
                {
                    Ordene orden = db.Ordenes.Where(x => x.Id == idOrden).First();

                    OrdenesEstado oe = new OrdenesEstado();
                    oe.fecha = DateTime.Now;
                    oe.estado_id = 3;
                    orden.OrdenesEstados.Add(oe);

                    db.SaveChanges();
                }

                Response.Redirect("~/Mecanico/Ordenes.aspx");
            }

        }
    }
}