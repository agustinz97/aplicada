<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ordenes.aspx.cs" Inherits="Aplicada.Mecanico.Ordenes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">

        <div class="card-header">
            <h4 class="card-title">Asignaciones</h4>
        </div>

        <div class="card-body">

            <div class="form-row">
                <div class="col-md-12">
                        <asp:ListView ID="listaAsignaciones" runat="server">
                            <LayoutTemplate>
                                <ul class="list-group">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />    
                                </ul>                
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="list-group-item">
                                    <asp:Label Text='<%#"#"+Eval("id")+" Patente: "+ Eval("vehiculo.patente") + " - " +Eval("vehiculo.modelo.marca.nombre")+" "+Eval("vehiculo.modelo.nombre") %>' runat="server" />
                                    <asp:Button Text="Aceptar" runat="server" CssClass="float-right btn btn-primary" OnClick="Aceptar_Orden" CommandArgument='<%# Eval("Id") %>'/>
                                </li>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <h6>No tiene ordenes asignadas.</h6>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
            </div>

            <div class="form-row">
                <asp:Label Text="" id="lblMessage" runat="server" />
            </div>

        </div>

    </div>

    <div class="card mt-5">

        <div class="card-header">
            <h4 class="card-title">En proceso</h4>
        </div>

        <div class="card-body">

            <div class="form-row">
                <div class="col-md-12">
                        <asp:ListView ID="listaProceso" runat="server">
                            <LayoutTemplate>
                                <ul class="list-group">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />    
                                </ul>                
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="list-group-item">
                                    <asp:Label Text='<%#"#"+Eval("id")+" Patente: "+Eval("vehiculo.patente") + " - " +Eval("vehiculo.modelo.marca.nombre")+" "+Eval("vehiculo.modelo.nombre") %>' runat="server" />
                                    <asp:Button Text="Finalizar" runat="server" CssClass="float-right btn btn-primary" OnClick="Terminar_Orden" CommandArgument='<%# Eval("Id") %>'/>
                                </li>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <h6>No tiene ordenes en proceso.</h6>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
            </div>

        </div>

    </div>

    <div class="card mt-5">

        <div class="card-header">
            <h4 class="card-title">Retiro</h4>
        </div>

        <div class="card-body">

            <div class="form-row">
                <div class="col-md-12">
                        <asp:ListView ID="listaRetiro" runat="server">
                            <LayoutTemplate>
                                <ul class="list-group">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />    
                                </ul>                
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li class="list-group-item">
                                    <asp:Label Text='<%# "#"+Eval("id")+" Patente: "+Eval("vehiculo.patente") + " - " +Eval("vehiculo.modelo.marca.nombre")+" "+Eval("vehiculo.modelo.nombre") %>' runat="server" />
                                    <asp:Button Text="Entregar" runat="server" CssClass="float-right btn btn-primary" OnClick="Entregar_orden" CommandArgument='<%# Eval("Id") %>'/>
                                </li>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <h6>No tiene ordenes pendientes de retiro.</h6>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
            </div>

        </div>

    </div>

</asp:Content>
