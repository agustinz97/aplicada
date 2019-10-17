<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="Aplicada.Stock.Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-header">
            <h5 class="card-title">Gestión de Stock</h5>
        </div>
        <div class="card-body" runat="server" id="listSection">

            <asp:Label Text="Categoria" runat="server" />
            <div class="form-row mb-3">
                <div class="col-md-3">
                    <asp:DropDownList runat="server"
                        ID="ddCategorias"
                        DataTextField="nombre"
                        DataValueField="id"
                        CssClass="form-control"
                        AutoPostBack="true"
                        AppendDataBoundItems="true"
                        OnSelectedIndexChanged="Buscar">
                        <asp:ListItem Value="0" Text="Todas" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 ">
                    <asp:TextBox runat="server" placeholder="Ingrese el nombre o codigo" ID="txtSearch" CssClass="form-control"/>
                </div>
                <div class="col-md-2">
                    <asp:Button Text="Buscar" runat="server" id="btnBuscar" CssClass="btn btn-block btn-primary" OnClick="Buscar"/>
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-12">
                    <asp:ListView ID="listaProductos" runat="server">
                        <LayoutTemplate>
                            <table class="table table-hover">
                                <thead>
                                    <td> # </td>
                                    <td> Descripcion </td>
                                    <td> Cantidad </td>
                                    <td> Precio </td>
                                    <td> Categoria </td>
                                    <td> Acciones </td>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td> <%# Eval("id").ToString().PadLeft(5, '0') %> </td>
                                <td> <%# Eval("nombre") %> </td>
                                <td> <%# Eval("stock") %> </td>
                                <td> <%# "$ "+Eval("precio").ToString() %> </td>
                                <td> <%# Eval("categoria.nombre") %> </td>
                                <td> <asp:Button Text="+" runat="server" CssClass="btn btn-primary" OnClick="Agregar" CommandArgument='<%# Eval("Id") %>'/> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div class="card-body" runat="server" id="addSection" visible="false">
            <div class="form-row">
                <div class="col-md-12">
                    <h5>Detalle</h5>
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-10 mb-3">
                    <asp:Label Text="Mi producto" ID="lblNombre" runat="server" />
                </div>
                <div class="col-md-2">
                    <asp:Label Text="" ID="lblId" runat="server" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label Text="$2.500" ID="lblPrecio" runat="server" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label Text="36" ID="lblStock" runat="server" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label Text="Tren delantero" ID="lblCategoria" runat="server" />
                </div>
            </div>
            <asp:Label Text="Cantidad a agregar" runat="server" />
            <div class="form-row my-2">
                <div class="col-md-3">
                    <asp:TextBox runat="server" placeholder="Cantidad" ID="txtCantidad" CssClass="form-control"/>
                </div>
                <div class="col-md-2">
                    <asp:Button Text="Agregar" runat="server" CssClass="btn btn-success" OnClick="Guardar"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
