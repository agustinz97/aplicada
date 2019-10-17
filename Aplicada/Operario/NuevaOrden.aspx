<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="Aplicada.Operario.NuevaOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="card" runat="server" id="divInicial">
            <div class="card-header">
                <h4 class="card-title">Nueva orden</h4>
            </div>
            <div class="card-body" runat="server" id="parteUno">
                <div class="form-row">
                    <div class="form-group col-md-12">
                        <asp:Label Text="Patente" runat="server" />
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <asp:TextBox runat="server" 
                                     CssClass="form-control"
                                     placeholder="Ingrese la patente"
                                     ID="txtPatente"/>
                            </div>
                            <div class="col-md-2">
                                <asp:Button Text="Buscar" 
                                    runat="server" 
                                    OnClick="Buscar_Patente"
                                    CssClass="btn btn-primary btn-block"/>
                            </div>
                            <div class="col-md-6">
                                <asp:Label Text="" ID="errorPatente" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-row" runat="server" id="datosAuto" visible="false">
                    <div class="form-group col-md-3">
                        <asp:Label Text="Año de fabricacion:" runat="server" />
                        <asp:TextBox runat="server" 
                                     CssClass="form-control"
                                     placeholder="Año de fabricación"
                                     ID="txtAnio"/>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label Text="Marca" runat="server" />
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
                        <asp:Label Text="Modelo" runat="server" />
                        <asp:DropDownList runat="server" 
                                          ID="ddModelos" 
                                          DataTextField="nombre" 
                                          DataValueField="id"
                                          CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row" runat="server" visible="false" id="datosCliente">
                    <div class="col-md-10">
                        <br />
                        <h6 class="card-subtitle">Datos del propietario</h6>
                        <hr />
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label Text="Documento" runat="server" />
                        <div class="form-row">
                            <div class="form-group col-md-8">
                                <asp:TextBox runat="server" 
                                     id="txtDni"
                                     CssClass="form-control"
                                     placeholder="Documento"/>
                            </div>
                            <div class="form-group col-md-4">
                                <asp:Button Text="Buscar" runat="server" 
                                            CssClass="btn btn-primary btn-block"
                                            OnClick="Buscar_Cliente"/>
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-md-4">
                        <asp:Label Text="Nombre" runat="server" />
                        <asp:TextBox runat="server" 
                                     id="txtNombre"
                                     CssClass="form-control"
                                     placeholder="Nombre"/>
                    </div>
                    <div class="form-group col-md-4">
                        <asp:Label Text="Apellido" runat="server" />
                        <asp:TextBox runat="server" 
                                     id="txtApellido"
                                     CssClass="form-control"
                                     placeholder="Apellido"/>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label Text="Direccion" runat="server" />
                        <asp:TextBox runat="server" 
                                     id="txtDireccion"
                                     CssClass="form-control"
                                     placeholder="Direccion"/>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label Text="Telefono" runat="server" />
                        <asp:TextBox runat="server" 
                                     id="txtTelefono"
                                     CssClass="form-control"
                                     placeholder="Telefono"/>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label Text="Email" runat="server" />
                        <asp:TextBox runat="server" 
                                     id="txtEmail"
                                     CssClass="form-control"
                                     placeholder="Correo electrònico"/>
                    </div>
                    <div class="col-md-12">
                        
                        <asp:Button Text="Siguiente" runat="server" 
                                    CssClass="btn btn-primary float-right"
                                    OnClick="Siguiente"/>

                        <asp:Button Text="Modificar propietario" runat="server" 
                                    OnClick="Habilitar_Inputs" 
                                    CssClass="btn btn-primary float-right mr-2"/>
                    </div>
                </div>
                <div class="form-row mt-3">
                    <asp:Label Text="" ID="lblMessage" runat="server"/>
                </div>
            </div>
            <div runat="server" id="parteDos" visible="false" class="card-body">
                <div class="form-row">
                    <div class="col-md-3">
                        <asp:Label Text="Categoria" runat="server" />
                        <asp:DropDownList runat="server" 
                                          ID="ddCategorias" 
                                          DataTextField="nombre" 
                                          DataValueField="id"
                                          CssClass="form-control"
                                          OnSelectedIndexChanged="Categoria_Elegida"
                                          AutoPostBack="true"
                                          AppendDataBoundItems="true">
                            <asp:ListItem Value="0" Text="Todas" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label Text="Servicios" runat="server" />
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
                                    <asp:Label Text='<%# Eval("nombre")+" - $"+Precio_Servicio(Int32.Parse(Eval("Id").ToString())) %>' runat="server" />
                                    <span class="badge badge-danger" <%#Falta_Producto(Int32.Parse(Eval("Id").ToString()), 1) ? "" : "hidden"%>>
                                        <i class="fas fa-exclamation-triangle"></i>
                                    </span>
                                    <span class="badge badge-warning" <%#Falta_Producto(Int32.Parse(Eval("Id").ToString()), 10) ? "" : "hidden"%>>
                                        <i class="fas fa-exclamation-triangle"></i>
                                    </span>
                                    <asp:Button Text="Quitar" runat="server" CssClass="float-right btn btn-danger" OnClick="Quitar_Servicio" CommandArgument='<%# Eval("Id") %>'/>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="form-row my-5">
                    <asp:Label ID="precioTotal" runat="server" Text="Precio total: $0" />
                </div>
                <div class="form-row my-3">
                    <div class="col-md-3">
                        <asp:Label Text="Mecanico" runat="server" />
                        <asp:DropDownList runat="server" 
                                          ID="ddMecanicos" 
                                          DataTextField="apellido" 
                                          DataValueField="id"
                                          CssClass="form-control"
                                          AutoPostBack="true"
                                          OnSelectedIndexChanged="Mecanico_Elegido"
                                          AppendDataBoundItems="true">
                            <asp:ListItem Value="0" Text="Seleccione un mecanico" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="col-md-12">
                        <asp:Button Text="Presupuestar" runat="server"
                                    id="btnPresupuesto"
                                    CssClass="btn btn-primary float-right"
                                    OnClick="Guardar_Orden"/>

                        <asp:Button Text="A Taller" runat="server"
                                    id="btnTaller"
                                    CssClass="btn btn-primary float-right mr-2"
                                    OnClick="Guardar_Orden"
                                    Enabled="false"/>

                        <asp:Button Text="Descartar" runat="server" 
                                    CssClass="btn btn-danger float-left mr-2"
                                    OnClick="Descartar"/>
                    </div>
                </div>
                <div class="form-row mt-3">
                    <asp:Label Text="" ID="lblMessage2" runat="server"/>
                </div>
            </div>
            
        </div>

</asp:Content>
