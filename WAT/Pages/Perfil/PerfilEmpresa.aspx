<%@ Page Title="Perfil de Usuario" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="PerfilEmpresa.aspx.cs" Inherits="WAT.Pages.PerfilEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../../Recursos/css/PerfilEmpresa.css" />

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <section class="PerfilSection d-flex justify-content-center mt-4">
        <div id="left-container">
            <div class="info-label">Tpo Empresa: <asp:Label ID="lblTrabajoActual" runat="server" /></div>
            <div class="info-label">Ciudad: <asp:Label ID="lblCiudad" runat="server" /></div>
            <div class="info-label">Proyecto actual: <asp:Label ID="lblProyectoActual" runat="server" /></div>
        </div>

        <div id="profile-container" class="ml-4">
            <div class="text-center">
                <asp:Image ID="imgBanner" runat="server" CssClass="profile-banner img-fluid" ImageUrl="~/Recursos/paisaje-800x409.jpg" AlternateText="Banner" />
                <div class="profile-header mt-4">
                    <asp:Image ID="imgProfilePicture" runat="server" CssClass="profile-picture rounded-circle" ImageUrl="~/Recursos/FotoPerfil.jpg" AlternateText="Profile Picture" />
                    <asp:Label ID="lblUsername" runat="server" CssClass="profile-username mt-2" />
                </div>
                <asp:Label ID="lblDescription" runat="server" CssClass="profile-description mt-2" />
            </div>
            <button type="button" class="btn btn-primary mt-4" data-toggle="modal" data-target="#editProfileModal">Editar Perfil</button>
        </div>
    </section>

    <!-- Modal -->
    <div class="modal fade" id="editProfileModal" tabindex="-1" role="dialog" aria-labelledby="editProfileModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editProfileModalLabel">Editar Perfil</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <%--<asp:FileUpload ID="fuProfilePicture" runat="server" CssClass="upload-button form-control mb-3" />
                    <asp:FileUpload ID="fuBannerImage" runat="server" CssClass="upload-button form-control mb-3" />--%>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control mb-3" TextMode="MultiLine" Rows="4" Placeholder="Descripción" />
                    <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control mb-3" Placeholder="Ciudad" />
                    <asp:TextBox ID="txtTrabajoActual" runat="server" CssClass="form-control mb-3" Placeholder="Trabajo actual" />
                    <asp:TextBox ID="txtProyectoActual" runat="server" CssClass="form-control mb-3" Placeholder="Proyecto actual" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnSaveChanges" runat="server" CssClass="btn btn-primary" Text="Guardar Cambios" OnClick="btnSaveChanges_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
