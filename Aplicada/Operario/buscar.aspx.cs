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
    public partial class buscar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                txtDni.Text = "";
                txtId.Text = "";
                txtPatente.Text = "";
                txtXCliente.Text = "";

                divDatos.Visible = false;
                divMecanico.Visible = false;
            }
        }

        protected void Buscar(object sender, EventArgs e)
        {

            divDatos.Visible = false;
            divMecanico.Visible = false;
            btnTaller.Visible = false;
            btnEntregar.Visible = false;

            String query = stringBusqueda.Text.Trim();

            if (!String.IsNullOrEmpty(query))
            {

                using (var db = new Entities1())
                {
                    int nOrden;
                    Ordene orden = null;
                    if (Int32.TryParse(query, out nOrden))
                    {
                        orden = db.Ordenes.Where(x => x.Id == nOrden).FirstOrDefault();
                    }
                    else
                    {
                        orden = db.Ordenes.Where(x => x.Vehiculo.patente == query).OrderByDescending(x => x.Id).FirstOrDefault();
                    }

                    if (orden != null)
                    {

                        txtId.Text = orden.Id.ToString("000000");
                        txtPatente.Text = orden.Vehiculo.patente;
                        txtXCliente.Text = orden.Cliente.apellido + " " + orden.Cliente.nombre;
                        txtDni.Text = orden.Cliente.dni;
                        divDatos.Visible = true;

                        int ultimoEstado = orden.OrdenesEstados.OrderByDescending(x => x.fecha).First().Estado.Id;

                        switch (ultimoEstado)
                        {
                            case 1://es presupuesto
                                btnTaller.Visible = true;
                                btnEntregar.Visible = false;
                                divMecanico.Visible = true;
                                using (var context = new Entities1())
                                {
                                    List<Empleado> mecanicos = new List<Empleado>();

                                    foreach (var user in context.AspNetUsers)
                                    {
                                        if (user.AspNetRoles.Any(x => x.Name == "Mecanico"))
                                        {

                                            List<Ordene> ordenesMecanico = context.Ordenes.Where(x => x.mecanico_id == user.Id).ToList();

                                            bool ocupado = false;
                                            foreach (var o in ordenesMecanico)
                                            {

                                                List<OrdenesEstado> estados = o.OrdenesEstados.OrderByDescending(x => x.fecha).ToList();

                                                Estado ue = estados.FirstOrDefault().Estado;

                                                if (ue.Id == 2 || ue.Id == 3)
                                                {
                                                    ocupado = true;
                                                }
                                            }

                                            if (!ocupado)
                                            {
                                                Empleado emp = context.Empleados.Where(x => x.usuario_id == user.Id).FirstOrDefault();

                                                if (emp != null)
                                                {
                                                    mecanicos.Add(emp);
                                                }

                                            }
                                        }
                                    }

                                    ddMecanicos.DataSource = mecanicos;
                                    ddMecanicos.DataBind();
                                }
                                break;
                            case 2://esta asignada
                                lblMessage.Text = "La orden fue asignada y pronto estará en proceso";
                                break;
                            case 3://esta en proceso
                                lblMessage.Text = "La orden esta en proceso";
                                break;
                            case 4://esta finalizada
                                lblMessage.Text = "La orden se encuentra en proceso de pago";
                                break;
                            case 5://esta abonada
                                btnEntregar.Visible = true;
                                btnTaller.Visible = false;
                                break;
                            case 6://esta retirada
                                lblMessage.Text = "El vehiculo ya fue entregado";
                                break;
                            default:
                                lblMessage.Text = "Lo sentimos, algo salió mal.";
                                break;
                        }

                    }
                    else
                    {
                        lblMessage.Text = "No se encontró una orden.";
                    }

                }

            }

        }

        protected void Operar(object sender, EventArgs e)
        {
            int ordenID;
            if (Int32.TryParse(txtId.Text, out ordenID))
            {

                    using (var db = new Entities1())
                    {

                        Ordene orden = db.Ordenes.Where(x => x.Id == ordenID).FirstOrDefault();

                        if (orden != null)
                        {
                            Button btn = (Button)sender;

                            if (btn.ID == btnEntregar.ID)
                            {
                                OrdenesEstado oe = new OrdenesEstado();
                                oe.fecha = DateTime.Now;
                                oe.estado_id = 6;
                                oe.usuario_id = User.Identity.GetUserId();
                                orden.OrdenesEstados.Add(oe);

                                db.SaveChanges();

                                lblMessage.Text = "Operacion exitosa.";
                            }
                            else
                            {

                                if (ddMecanicos.SelectedIndex > 0)
                                {
                                    bool falta = false;
                                    foreach (OrdenesServicio os in orden.OrdenesServicios)
                                    {
                                        int idServicio = os.servicio_id.Value;
                                        if (Falta_Producto(idServicio, 1))
                                        {
                                            falta = true;
                                        }
                                    }

                                    if (!falta)
                                    {
                                        OrdenesEstado oe = new OrdenesEstado();
                                        oe.estado_id = 2;
                                        oe.fecha = DateTime.Now;
                                        oe.usuario_id = User.Identity.GetUserId();
                                        orden.OrdenesEstados.Add(oe);

                                        int idEmpleado = Int32.Parse(ddMecanicos.SelectedValue);
                                        Empleado emp = db.Empleados.Where(x => x.Id == idEmpleado).FirstOrDefault();

                                        orden.mecanico_id = emp.usuario_id;

                                        db.SaveChanges();

                                        lblMessage.Text = "Orden enviada a taller";
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Hay faltante de productos";
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Debe seleccionar un mecanico.";
                                }

                            }                         
                        
                        }
                        else
                        {
                            lblMessage.Text = "Algo salio mal, lo sentimos.";
                        }

                    }

            }
            else
            {
                lblMessage.Text = "No hay una orden seleccionada";
            }
        }

        protected bool Falta_Producto(int id, int cantidad)
        {

            using (var db = new Entities1())
            {

                Servicio s = db.Servicios.Where(x => x.Id == id).FirstOrDefault();

                foreach (ServiciosProducto sp in s.ServiciosProductos)
                {
                    if (sp.Producto.stock < cantidad)
                    {
                        return true;
                    }
                }

                return false;
            }

        }

        protected void Ver(object sender, EventArgs e)
        {
            Response.Redirect("~/Operario/Presupuesto?id="+txtId.Text);
        }
    }
}