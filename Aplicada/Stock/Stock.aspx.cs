using Aplicada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicada.Stock
{
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            using (var db = new Entities1())
            {

                if (!IsPostBack)
                {
                    ddCategorias.DataSource = db.Categorias.ToList();
                    ddCategorias.DataBind();

                    List<Producto> productos = db.Productos.ToList();
                    listaProductos.DataSource = productos;
                    listaProductos.DataBind();
                }

            }
        }

        protected void Buscar(object sender, EventArgs e)
        {

            using (var db = new Entities1())
            {
                List<Producto> productos = db.Productos.ToList();;

                //Filtro por cadena de texto
                string busqueda = txtSearch.Text;
                if (!String.IsNullOrEmpty(busqueda) && !String.IsNullOrWhiteSpace(busqueda))
                {
                    int id;

                    if (Int32.TryParse(txtSearch.Text, out id))
                    {
                        productos = productos.Where(x => x.Id == id).ToList();
                    }
                    else
                    {
                        productos = productos.Where(x => x.nombre.Contains(busqueda)).ToList();
                        lblMsg.Text = "Busco por nombre";
                    }
                }

                //filtro por categoria elegida
                int idCategoria = Int32.Parse(ddCategorias.SelectedValue);
                if (idCategoria > 0)
                {
                    productos = productos.Where(x => x.Categoria.id == idCategoria).ToList();
                }

                listaProductos.DataSource = productos;
                listaProductos.DataBind();
            }

        }

        protected void Agregar(object sender, EventArgs e)
        {

            int id = Int32.Parse(((Button)sender).CommandArgument);

            using (var db = new Entities1())
            {

                Producto p = db.Productos.Where(x => x.Id == id).FirstOrDefault();

                if(p != null){

                    lblNombre.Text = "Producto: " + p.nombre;
                    lblPrecio.Text = "Precio: $" + p.precio;
                    lblStock.Text = "Existencias: " + p.stock;
                    lblCategoria.Text = "Categoria: " + p.Categoria.nombre;
                    lblId.Text = "Codigo: #"+p.Id.ToString("00000");

                    listSection.Visible = false;
                    addSection.Visible = true;
                }

            }

        }

        protected void Guardar(object sender, EventArgs e)
        {
            int cantidad;

            if (Int32.TryParse(txtCantidad.Text, out cantidad))
            {

                using (var db = new Entities1())
                {

                    int id = Int32.Parse(lblId.Text);
                    Producto p = db.Productos.Where(x => x.Id == id).FirstOrDefault();

                    if (p != null)
                    {

                        p.stock += cantidad;

                        db.SaveChanges();

                    }

                }

            }
        }
    }

}