<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Presupuesto.aspx.cs" Inherits="Aplicada.Operario.Presupuesto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card" runat="server" id="divPresupeusto">
        <div class="card-body" style="border:2px solid grey;">
            <div class="form-row mb-3">
                <div class="col-md-6">
                    <h4 class="card-title">Presupuesto</h4>
                </div>
            </div>
            <div class="form-row mb-3">
                <div class="col-md-12">
                    <asp:Label Text="" ID="lblCodigo" runat="server" />
                    <asp:Label Text="" ID="lblFecha" runat="server" CssClass="float-right"/>
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-6 py-3" style="border-bottom: 2px solid grey;border-top: 2px solid grey;">
                    <div class="">
                        <h5>Vehiculo</h5>
                    </div>
                    <div class="">
                        <asp:Label Text="" ID="lblVehiculo" runat="server" />
                    </div>
                </div>
                <div class="col-md-6 pl-3 py-3" style="border-left: 2px solid grey; border-bottom: 2px solid grey; border-top: 2px solid grey;" >
                    <div class="">
                        <h5>Cliente</h5>
                    </div>
                    <div class="">
                        <asp:Label Text="" ID="lblCliente" runat="server" />
                    </div>
                </div>
            </div>
            <div class="form-row my-2">
                <div class="col-md-12">
                    <asp:Label Text="Presupuesto vigente hasta el dia: " ID="lblVencimiento" runat="server" />
                </div>
                <div class="col-md-12">
                    <asp:Label Text="Atendido por " id="lblOperario" runat="server" />
                </div>
                <div class="col-md-12">
                    <asp:Label Text="Documento no válido como factura" runat="server" />
                </div>
            </div>
            <div class="form-row mt-5">
                <h5>Servicios</h5>
                <div class="col-md-12">
                    <asp:ListView ID="listaServicios" runat="server">
                        <LayoutTemplate>
                            <ul class="list-group">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />    
                            </ul>                
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li class="list-group-item">
                                <asp:Label Text='<%# Eval("nombre") %>' runat="server" />
                                <asp:Label Text='<%# "$" + Precio_Servicio(Int32.Parse(Eval("id").ToString())) %>' runat="server" CssClass="float-right"/>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div class="form-row mt-5">
                <div class="col-md-12">
                    <h4 class="float-right">Total: $<asp:Label Text="" ID="lblTotal" runat="server" CssClass=""/></h4>
                </div>
            </div>
            <div class="form-row mb-2">
                <asp:Label Text="" ID="lblMessage" runat="server" />
            </div>
        </div>
    </div>
    <div class="form-row my-3">
                <div class="col-md-12">
                    <asp:Button Text="Imprimir" runat="server" CssClass="btn btn-info"/>
                </div>
            </div>
    <div runat="server" id="divError" visible="false">
        <asp:Label Text="" id="txtError" runat="server" />
    </div>

</asp:Content>
