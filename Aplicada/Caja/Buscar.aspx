<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Buscar.aspx.cs" Inherits="Aplicada.Caja.IndexCaja" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <div class="card-header">
            <h5>Buscar Orden</h5>
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
                <div class="col-md-2">
                    <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" OnClick="Buscar" />
                </div>
            </div>
            <div class="form-row mt-5" runat="server" id="divDatos">
                <div class="col-md-6 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Nº de orden</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtIdOrden" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                <div class="col-md-6 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Patente</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtPatente" aria-describedby="basic-addon3" enabled="false"/>
                    </div>
                </div>
                <div class="col-md-6 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Cliente</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtCliente" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                <div class="col-md-6 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">DNI</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtDni" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                
            </div>
            <div class="form-row mt-5">
                <div class="col-md-12 mb-2">
                    <h5 class="card-subtitle">Datos de Pago</h5>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Total a pagar $</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtTotal" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server"
                                    ID="ddMetodo"
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="Metodo_Elegido">
                        <asp:ListItem Text="Metodo de pago" value="0"/>
                        <asp:ListItem Text="Efectivo" value="1"/>
                        <asp:ListItem Text="Tarjeta de credito" value="2"/>
                        <asp:ListItem Text="Tarjeta de debito" value="3"/>
                    </asp:DropDownList>
                </div>
                <asp:Button Text="Pagar" runat="server" CssClass="btn btn-success" OnClick="Pagar" Enabled="false" ID="btnPagar"/>
            </div>
            <div class="form-row mt-3" runat="server" id="divCalculadora" visible="false">
                <div class="col-md-4 mb-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Efectivo $</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtEfectivo" aria-describedby="basic-addon3" />
                    </div>
                </div>
                <div class="col-md-8">
                    <asp:Button Text="Calcular" runat="server" CssClass="btn btn-primary" OnClick="Calcular"/>
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="basic-addon3">Vuelto $</span>
                        </div>
                        <asp:TextBox runat="server" class="form-control" ID="txtVuelto" aria-describedby="basic-addon3" Enabled="false"/>
                    </div>
                </div>
            </div>
            <div class="form-row mt-3">
                <div class="col-md-12">
                    <asp:Label Text="" Visible="false" runat="server" id="lblError" CssClass="card-text"/>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
