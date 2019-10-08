using Aplicada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

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

        private List<Servicio> GetServicios(int idModelo, int categoria)
        {
            using (var db = new Entities1())
            {
                if (categoria == 0)
                {
                    return db.Servicios.Where(x => x.modelo_id == idModelo).ToList();
                }
                else
                {
                    
                    return db.Servicios.Where(x => x.modelo_id == idModelo)
                                                    .Where(x => x.Categoria1.id == categoria).ToList();
                }
            }
        }

        private List<Categoria> GetCategorias()
        {
            using (var db = new Entities1())
            {
                return db.Categorias.ToList();
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
            lblMessage.Text = "";
            lblMessage2.Text = "";
            errorPatente.Text = "";
            ddServicios.DataSource = null;
            ddServicios.DataBind();
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

        protected void Categoria_Elegida(object sender, EventArgs e)
        {
            using (var db = new Entities1())
            {
                int idModelo = Int32.Parse(ddModelos.SelectedValue);
                int categoria = Int32.Parse(ddCategorias.SelectedValue);

                ddServicios.Items.Clear();
                ddServicios.Items.Add(new ListItem("Elija los servicios"));
                ddServicios.DataSource = GetServicios(idModelo, categoria);
                ddServicios.DataBind();
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

        private bool PatenteValida(string patente)
        {
            Regex vieja = new Regex(@"^[a-zA-Z]{3}\d{3}$");
            Regex nueva = new Regex(@"^[a-zA-Z]{2}\d{3}[a-zA-Z]{2}$");

            if(vieja.Match(patente).Success || nueva.Match(patente).Success){
                return true;
            }else{
                return false;
            }

        }

        protected void Habilitar_Inputs(object sender, EventArgs e)
        {
            txtDni.Enabled = true;
            txtDni.Text = "";

            txtDireccion.Enabled = true;
            txtDireccion.Text = "";

            txtNombre.Enabled = true;
            txtNombre.Text = "";

            txtApellido.Enabled = true;
            txtApellido.Text = "";

            txtEmail.Enabled = true;
            txtEmail.Text = "";

            txtTelefono.Enabled = true;
            txtTelefono.Text = "";
        }

        protected void Buscar_Patente(object sender, EventArgs e)
        {
            var patente = txtPatente.Text;

            if (!PatenteValida(patente))
            {
                errorPatente.Text = "La patente ingresada no es valida";
            }
            else
            {
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
                    ddCategorias.DataSource = GetCategorias();
                    ddCategorias.DataBind();
                    ddServicios.DataSource = GetServicios(modelo.Id, 0);
                    ddServicios.DataBind();

                    
                    List<AspNetUser> mecanicos = new List<AspNetUser>();
                    
                    foreach (var user in context.AspNetUsers)
                    {
                        if (user.AspNetRoles.Any(x => x.Name == "Mecanico"))
                        {

                            List<Ordene> ordenesMecanico = context.Ordenes.Where(x => x.mecanico_id == user.Id).ToList();

                            bool ocupado = false;
                            foreach (var orden in ordenesMecanico)
                            {

                                List<OrdenesEstado> estados = orden.OrdenesEstados.OrderByDescending(x => x.fecha).ToList();

                                Estado ultimoEstado = estados.FirstOrDefault().Estado;

                                if (ultimoEstado.Id == 2 || ultimoEstado.Id == 3)
                                {
                                    ocupado = true;
                                }
                            }

                            if (!ocupado)
                            {
                                mecanicos.Add(user);
                            }
                        }
                    }

                    ddMecanicos.DataSource = mecanicos;
                    ddMecanicos.DataBind();
                }
            }

        }

        protected void Mecanico_Elegido(object sender, EventArgs e)
        {
            if (ddMecanicos.SelectedIndex > 0)
            {
                btnTaller.Enabled = true;
            }
            else
            {
                btnTaller.Enabled = false;
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
                            precioTotal.Text = "Precio total: $" + Precio_Total();
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

            precioTotal.Text = "Precio total: $"+Precio_Total();
        }

        protected void Guardar_Orden(object sender, EventArgs e)
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
                    oe.usuario_id = User.Identity.GetUserId();
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

                        context.Vehiculos.Add(vehiculo);
                    }

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

                    orden.Vehiculo = vehiculo;
                    orden.Cliente = vehiculo.Cliente;

                    Button btn = (Button)sender;

                    if (btn.ID == btnPresupuesto.ID)
                    {
                        context.Ordenes.Add(orden);
                        context.SaveChanges();

                        Response.Redirect("~/Operario/Presupuesto?id="+orden.Id);
                    }
                    else
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
                            oe.estado_id = 2;
                            oe.fecha = DateTime.Now;
                            oe.usuario_id = User.Identity.GetUserId();
                            orden.OrdenesEstados.Add(oe);
                            orden.mecanico_id = ddMecanicos.SelectedValue;

                            context.Ordenes.Add(orden);
                            context.SaveChanges();

                            lblMessage2.Text = "Orden enviada a taller";
                        }
                        else
                        {
                            lblMessage2.Text = "Hay faltante de productos";
                        }

                    }

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

                float precio = (float)s.precio_base;
                foreach (Producto p in productos)
                {
                    precio += (float)p.precio;
                }

                return precio;
            }
        }

        protected float Precio_Total()
        {
            float precioTotal = 0f;

            foreach (Servicio s in serviciosElegidos)
            {
                precioTotal += Precio_Servicio(s.Id);
            }

            return precioTotal;
        }

        protected void Descartar(object sender, EventArgs e)
        {
            Response.Redirect("~/operario/nuevaorden.aspx");
        }
    }
}