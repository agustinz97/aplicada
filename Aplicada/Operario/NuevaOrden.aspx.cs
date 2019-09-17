using Aplicada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicada.Operario
{
    public partial class NuevaOrden : System.Web.UI.Page
    {
        List<Servicio> serviciosElegidos = new List<Servicio>();

        private List<Marca> GetMarcas()
        {
            using (var db = new Entities1())
            {
                return db.Marcas.ToList();
            }
        }

        private List<Modelo> GetModelos(Marca marca)
        {
            using (var db = new Entities1())
            {
                return db.Modelos.Where(x => x.Marca.Id == marca.Id).ToList();
            }
        }

        private List<Servicio> GetServicios(int idModelo)
        {
            using (var db = new Entities1())
            {
                return db.Servicios.Where(x => x.modelo_id == idModelo).ToList();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["serviciosElegidos"] != null)
            {
                serviciosElegidos = (List<Servicio>)Session["serviciosElegidos"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Marca> marcas = GetMarcas();

                ddMarcas.DataSource = marcas;
                ddMarcas.DataBind();

                ddModelos.DataSource = GetModelos(marcas.First());
                ddModelos.DataBind();

                Session.Remove("serviciosElegidos");
            }
        }

        protected void Buscar_Modelos(object sender, EventArgs e)
        {
            using (var context = new Entities1())
            {
                DropDownList dd = (DropDownList)sender;
                int idMarca = Int32.Parse(dd.SelectedValue);
                Marca marca = context.Marcas.Where(x => x.Id == idMarca).First();

                ddModelos.DataSource = GetModelos(marca);
                ddModelos.DataBind();
            }
        }

        protected void Buscar_Patente(object sender, EventArgs e)
        {
            var patente = txtPatente.Text;

            using (var context = new Entities1())
            {
                Vehiculo vehiculo = context.Vehiculos.Where(x => x.patente == patente).FirstOrDefault();
                datosAuto.Visible = true; //muestro seccion del formulario
                datosCliente.Visible = true; //muestro seccion cliente

                if (vehiculo != null)
                {
                    //TODO: vehiculo encontrado                  

                    txtAnio.Enabled = false;
                    txtAnio.Text = vehiculo.anio;

                    ddMarcas.Enabled = false;
                    ddMarcas.ClearSelection();
                    ddMarcas.Items.FindByValue(vehiculo.Modelo.marca_id.ToString()).Selected = true;

                    ddModelos.DataSource = GetModelos(vehiculo.Modelo.Marca);
                    ddModelos.DataBind();
                    ddModelos.Enabled = false;
                    ddModelos.ClearSelection();
                    ddModelos.Items.FindByValue(vehiculo.modelo_id.ToString()).Selected = true;

                    txtNombre.Enabled = false;
                    txtNombre.Text = vehiculo.Cliente.nombre;

                    txtApellido.Enabled = false;
                    txtApellido.Text = vehiculo.Cliente.apellido;

                    txtDni.Enabled = false;
                    txtDni.Text = vehiculo.Cliente.dni;

                    txtDireccion.Enabled = false;
                    txtDireccion.Text = vehiculo.Cliente.direccion;

                    txtTelefono.Enabled = false;
                    txtTelefono.Text = vehiculo.Cliente.telefono;

                    txtEmail.Enabled = false;
                    txtEmail.Text = vehiculo.Cliente.email;
                }
                else
                {
                    txtAnio.Enabled = true;
                    txtAnio.Text = String.Empty;

                    ddMarcas.Enabled = true;
                    ddMarcas.ClearSelection();
                    ddMarcas.Items[0].Selected = true;

                    ddModelos.Enabled = true;
                    ddModelos.ClearSelection();
                    int id = Int32.Parse(ddMarcas.SelectedValue);
                    Marca m = context.Marcas.Where(x => x.Id == id).First();
                    ddModelos.DataSource = GetModelos(m);
                    ddModelos.DataBind();

                    txtNombre.Enabled = true;
                    txtNombre.Text = String.Empty;

                    txtApellido.Enabled = true;
                    txtApellido.Text = String.Empty;

                    txtDni.Enabled = true;
                    txtDni.Text = String.Empty;

                    txtDireccion.Enabled = true;
                    txtDireccion.Text = String.Empty;

                    txtEmail.Enabled = true;
                    txtEmail.Text = String.Empty;

                    txtTelefono.Enabled = true;
                    txtTelefono.Text = string.Empty;
                }
                
            }
            
        }

        protected void Volver(object sender, EventArgs e)
        {
            parteUno.Visible = true;
            parteDos.Visible = false;
        }

        protected void Siguiente(object sender, EventArgs e)
        {

            if( txtPatente.Text == "" ||
                txtAnio.Text == "" ||
                txtApellido.Text == "" ||
                txtNombre.Text == "" ||
                txtDni.Text == "" )
            {
                lblMessage.Text = "Complete todos los campos obligatorios";
            }
            else
            {
                parteUno.Visible = false;
                parteDos.Visible = true;

                using (var context = new Entities1())
                {
                    int idModelo = Int32.Parse(ddModelos.SelectedValue);
                    Modelo modelo = context.Modelos.Where(x => x.Id == idModelo).First();
                    ddServicios.DataSource = GetServicios(modelo.Id);
                    ddServicios.DataBind();
                }
            }

        }

        protected void Agregar_Servicio(object sender, EventArgs e)
        {
            using (var context = new Entities1())
            {
                if (Int32.Parse(ddServicios.SelectedValue) > 0)
                {
                    int value = Int32.Parse(ddServicios.SelectedValue);
                    Servicio servicioElegido = context.Servicios.Where(x => x.Id == value).FirstOrDefault();


                    if (serviciosElegidos.Count < 5)
                    {
                        if (serviciosElegidos.Find(x => x.Id == servicioElegido.Id) == null)
                        {
                            serviciosElegidos.Add(servicioElegido);
                            Session["serviciosElegidos"] = serviciosElegidos;
                        }
                    }

                    listaServicios.DataSource = serviciosElegidos;
                    listaServicios.DataBind();
                }
                
            }
        }

        protected void Quitar_Servicio(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = Int32.Parse(btn.CommandArgument);

            serviciosElegidos.RemoveAll(x => x.Id == id);
            Session["serviciosElegidos"] = serviciosElegidos;

            listaServicios.DataSource = serviciosElegidos;
            listaServicios.DataBind();
 
        }

        protected void Crear_Presupuesto(object sender, EventArgs e)
        {

            if (serviciosElegidos.Count > 0)
            {
                using (var context = new Entities1())
                {
                    Ordene orden = new Ordene();
                    orden.fecha = DateTime.Now;

                    OrdenesEstado oe = new OrdenesEstado();
                    oe.estado_id = 1;
                    oe.fecha = DateTime.Now;
                    orden.OrdenesEstados.Add(oe);

                    foreach (Servicio s in serviciosElegidos)
                    {
                        OrdenesServicio os = new OrdenesServicio();
                        os.servicio_id = s.Id;
                        orden.OrdenesServicios.Add(os);
                    }

                    Vehiculo vehiculo = context.Vehiculos.Where(x => x.patente == txtPatente.Text).FirstOrDefault();

                    if (vehiculo == null)
                    {
                        //El vehiculo no esta cargado
                        vehiculo = new Vehiculo();
                        vehiculo.anio = txtAnio.Text;
                        vehiculo.modelo_id = Int32.Parse(ddModelos.SelectedValue);
                        vehiculo.patente = txtPatente.Text;

                        Cliente clienteExistente = context.Clientes.Where(x => x.dni == txtDni.Text).FirstOrDefault();

                        if (clienteExistente == null)
                        {
                            //Es cliente nuevo
                            Cliente c = new Cliente();
                            c.nombre = txtNombre.Text;
                            c.apellido = txtApellido.Text;
                            c.dni = txtDni.Text;
                            c.email = txtEmail.Text;
                            c.telefono = txtTelefono.Text;
                            c.direccion = txtDireccion.Text;

                            vehiculo.Cliente = c;
                        }
                        else
                        {
                            //Un cliente ya registrado agrega nuevo auto
                            vehiculo.Cliente = clienteExistente;
                        }

                        context.Vehiculos.Add(vehiculo);
                    }

                    orden.Vehiculo = vehiculo;
                    orden.Cliente = vehiculo.Cliente;

                    context.Ordenes.Add(orden);
                    context.SaveChanges();

                    Response.Redirect("~/Operario/Presupuesto.aspx?id=" + orden.Id);
                }
            }
            else
            {
                lblMessage2.Text = "Debe seleccionar al menos un servicio.";
            }
 
        }

        protected void Buscar_Cliente(object sender, EventArgs e)
        {
            using (var context = new Entities1())
            {
                Cliente c = context.Clientes.Where(x => x.dni == txtDni.Text).FirstOrDefault();

                if (c != null)
                {
                    txtNombre.Enabled = false;
                    txtNombre.Text = c.nombre;

                    txtApellido.Enabled = false;
                    txtApellido.Text = c.apellido;

                    txtDireccion.Enabled = false;
                    txtDireccion.Text = c.direccion;

                    txtTelefono.Enabled = false;
                    txtTelefono.Text = c.telefono;

                    txtEmail.Enabled = false;
                    txtEmail.Text = c.telefono;
                }
                else
                {
                    txtNombre.Enabled = true;
                    txtNombre.Text = String.Empty;

                    txtApellido.Enabled = true;
                    txtApellido.Text = String.Empty;

                    txtDireccion.Enabled = true;
                    txtDireccion.Text = String.Empty;

                    txtTelefono.Enabled = true;
                    txtTelefono.Text = String.Empty;

                    txtEmail.Enabled = true;
                    txtEmail.Text = String.Empty;
                }
            }
        }

        protected bool Falta_Producto(int id)
        {

            using (var db = new Entities1())
            {

                Servicio s = db.Servicios.Where(x => x.Id == id).FirstOrDefault();

                foreach (ServiciosProducto sp in s.ServiciosProductos)
                {
                    if (sp.Producto.stock < 1)
                    {
                        return true;
                    }
                }

                return false;
            }

        }
    }
}