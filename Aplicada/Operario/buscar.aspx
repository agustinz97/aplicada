<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="buscar.aspx.cs" Inherits="Aplicada.Operario.buscar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <div class="card-header">
            <h5 class="card-title">Buscar</h5>
        </div>
        <div class="card-body">
            <div class="form-row">
                <div class="col-md-6">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Nº de orden/patente</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="stringBusqueda" aria-describedby="basic-addon3" />
                    </div>
                </div>
                <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" OnClick="Buscar"/>
            </div>
            <div class="form-row mt-5" runat="server" id="divDatos" visible="false">
                <div class="col-md-6 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Nº de orden</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtId" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                <div class="col-md-6 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Patente</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtPatente" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Cliente</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtXCliente" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">DNI</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtDni" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                
            </div>
            <div class="form-row mt-3" runat="server" id="divMecanico" visible="false">
                <div class="col-md-3">
                    <asp:Label Text="Mecanico" runat="server" />
                    <asp:DropDownList runat="server"
                        ID="ddMecanicos"
                        DataTextField="username"
                        DataValueField="id"
                        CssClass="form-control"
                        AutoPostBack="true"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0" Text="Seleccione un mecanico" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-row my-3">
                <div class="col-md-12">
                    <asp:Button Text="Entregar" runat="server" OnClick="Operar" Visible="false" ID="btnEntregar" CssClass="btn btn-success float-right"/>
                    <asp:Button Text="A taller" runat="server" OnClick="Operar" Visible="false" ID="btnTaller" CssClass="btn btn-success float-right"/>
                </div>
            </div>
            <div class="form-row mt-5">
                <div class="col-md-12">
                    <asp:Label Text="" ID="lblMessage" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
