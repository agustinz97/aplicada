<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="Aplicada.Operario.NuevaOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="card">
            <div class="card-header">
                <h4 class="card-title">Nueva orden</h4>
            </div>
            <div class="card-body" runat="server" id="parteUno">
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:TextBox runat="server" 
                                     CssClass="form-control"
                                     placeholder="Ingrese la patente"
                                     ID="txtPatente"/>
                    </div>
                    <div class="form-group col-md-2">
                        <asp:Button Text="Buscar" 
                                    runat="server" 
                                    OnClick="Buscar_Patente"
                                    CssClass="btn btn-primary btn-block"/>
                    </div>
                </div>
                <div class="form-row" runat="server" id="datosAuto" visible="false">
                    <div class="form-group col-md-3">
                        <asp:TextBox runat="server" 
                                     CssClass="form-control"
                                     placeholder="Año de fabricación"
                                     ID="txtAnio"/>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:DropDownList runat="server" 
                                          ID="ddMarcas" 
                                          DataTextField="nombre" 
                                          DataValueField="id"
                                          CssClass="form-control"
                                          OnSelectedIndexChanged="Buscar_Modelos"
                                          AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:DropDownList runat="server" 
                                          ID="ddModelos" 
                                          DataTextField="nombre" 
                                          DataValueField="id"
                                          CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row" runat="server" visible="false" id="datosCliente">
                    <div class="col-md-12">
                        <br />
                        <h6 class="card-subtitle">Datos del propietario</h6>
                        <hr />
                    </div>
                    <div class="form-group col-md-3">
                        <asp:TextBox runat="server" 
                                     id="txtDni"
                                     CssClass="form-control"
                                     placeholder="Documento"/>
                    </div>
                    <div class="form-group col-md-1">
                        <asp:Button Text="Buscar" runat="server" 
                                    CssClass="btn btn-primary btn-block"
                                    OnClick="Buscar_Cliente"/>
                    </div>
                    <div class="form-group col-md-4">
                        <asp:TextBox runat="server" 
                                     id="txtNombre"
                                     CssClass="form-control"
                                     placeholder="Nombre"/>
                    </div>
                    <div class="form-group col-md-4">
                        <asp:TextBox runat="server" 
                                     id="txtApellido"
                                     CssClass="form-control"
                                     placeholder="Apellido"/>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:TextBox runat="server" 
                                     id="txtDireccion"
                                     CssClass="form-control"
                                     placeholder="Direccion"/>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:TextBox runat="server" 
                                     id="txtTelefono"
                                     CssClass="form-control"
                                     placeholder="Telefono"/>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:TextBox runat="server" 
                                     id="txtEmail"
                                     CssClass="form-control"
                                     placeholder="Correo electrònico"/>
                    </div>
                    <div class="col-md-12">
                        <asp:Button Text="Siguiente" runat="server" 
                                    CssClass="btn btn-primary float-right"
                                    OnClick="Siguiente"/>
                    </div>
                </div>
                <div class="form-row mt-3">
                    <asp:Label Text="" ID="lblMessage" runat="server"/>
                </div>
            </div>
            <div runat="server" id="parteDos" visible="false" class="card-body">
                <div class="form-row">
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" 
                                          ID="ddServicios" 
                                          DataTextField="nombre" 
                                          DataValueField="id"
                                          CssClass="form-control"
                                          OnSelectedIndexChanged="Agregar_Servicio"
                                          AutoPostBack="true"
                                          AppendDataBoundItems="true">
                            <asp:ListItem Value="0" Text="Elija los servicios" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row my-3">
                    <div class="col-md-12">
                        <h5>Servicios Elegidos</h5>
                    </div>
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
                                    <span class="badge badge-danger" <%#Falta_Producto(Int32.Parse(Eval("Id").ToString())) ? "" : "hidden"%>>
                                        <i class="fas fa-exclamation-triangle"></i>
                                    </span>
                                    <asp:Button Text="Quitar" runat="server" CssClass="float-right btn btn-danger" OnClick="Quitar_Servicio" CommandArgument='<%# Eval("Id") %>'/>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="form-row">
                    <div class="col-md-12">
                        <asp:Button Text="Volver" runat="server" 
                                    CssClass="btn btn-primary float-left"
                                    OnClick="Volver"/>
                        <asp:Button Text="Solicitar" runat="server" 
                                    CssClass="btn btn-primary float-right"
                                    OnClick="Crear_Presupuesto"/>
                    </div>
                </div>
                <div class="form-row mt-3">
                    <asp:Label Text="" ID="lblMessage2" runat="server"/>
                </div>
            </div>
            
        </div>

</asp:Content>
