<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WAT.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Styles" runat="server">
    <link rel="stylesheet" href="/Recursos/css/login.css" type="text/css" />
    <style>
        .form-floating label {
            color: #333;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-half">
        <div class="section left-section">
            <div class="background">
                <span style="top: 10%; left: 20%; --direction: -0.3;"></span>
                <span style="top: 50%; left: 60%; --direction: 0.2;"></span>
            </div>
            <div class="form-container active" id="login-form">
                <h1>Inicio Sesión</h1>
                <p><a class="toggle-link" href="Registro.aspx">¿Aún no tienes cuenta? Regístrate aquí</a></p>
                <div class="form-floating">
                    <div class="form-floating mb-3">
                        <asp:DropDownList ID="ddlRol" CssClass="form-select mb-3" runat="server" AppendDataBoundItems="true" required>
                            <asp:ListItem Text="-- Selecciona --" Value="" Enabled="true" />
                            <asp:ListItem Text="Talento" Value="talent" />
                            <asp:ListItem Text="Empresa" Value="company" />
                        </asp:DropDownList>
                        <label for="ddlRol" class="form-label">Que Eres?</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtLoginEmail" CssClass="form-control mb-3" runat="server" placeholder="Correo Electrónico" TextMode="Email" required></asp:TextBox>
                        <label for="txtLoginEmail" class="form-label">Correo Electrónico</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtLoginPassword" CssClass="form-control mb-3" runat="server" placeholder="Contraseña" TextMode="Password" required></asp:TextBox>
                        <label for="txtLoginPassword" class="form-label">Contraseña</label>
                    </div>
                    <asp:Button ID="btnLogin" CssClass="btn btn-primary w-100" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
                </div>
                <p><span class="toggle-link" onclick="toggleForms('register')">¿Aún no tienes cuenta? Regístrate aquí</span></p>
            </div>

            <div class="form-container" id="register-text">
                <h1>¡Bienvenido! Regístrate para unirte</h1>
            </div>
        </div>

        <div class="section right-section">
            <div class="background">
                <span style="top: 20%; left: 30%; --direction: -0.3;"></span>
                <span style="top: 60%; left: 70%; --direction: 0.2;"></span>
            </div>
            <div class="form-container" id="register-form">
                <h1>Registro</h1>
                <div class="form-floating">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server" placeholder="Nombre Completo"></asp:TextBox>
                        <label for="txtFullName" class="form-label">Nombre Completo</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" TextMode="Email" placeholder="Correo Electrónico"></asp:TextBox>
                        <label for="txtEmail" class="form-label">Correo Electrónico</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtIdNumber" CssClass="form-control" runat="server" TextMode="Number" placeholder="Número de Identificación"></asp:TextBox>
                        <label for="txtIdNumber" class="form-label">Número de Identificación</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" TextMode="Phone" placeholder="Teléfono"></asp:TextBox>
                        <label for="txtPhone" class="form-label">Teléfono</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:DropDownList ID="ddlRegisterRol" CssClass="form-select" runat="server">
                            <asp:ListItem Text="-- Selecciona --" Value="" Enabled="true" />
                            <asp:ListItem Text="Talento" Value="talent" />
                            <asp:ListItem Text="Empresa" Value="company" />
                        </asp:DropDownList>
                        <label for="ddlRegisterRol" class="form-label">Que Eres?</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                        <label for="txtPassword" class="form-label">Contraseña</label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtConfirmPassword" CssClass="form-control" runat="server" TextMode="Password" placeholder="Confirmar Contraseña"></asp:TextBox>
                        <label for="txtConfirmPassword" class="form-label">Confirmar Contraseña</label>
                    </div>
                    <asp:Button ID="btnRegister" CssClass="btn btn-primary w-100" runat="server" Text="Registrar" OnClick="btnRegister_Click" />
                </div>
                <p><span class="toggle-link" onclick="toggleForms('login')">¿Ya tienes cuenta? Inicia sesión aquí</span></p>
            </div>
            <div class="form-container active" id="login-text">
                <h1>¡Bienvenido! Inicia sesión para continuar</h1>
            </div>
        </div>
    </div>


    <script>
        function toggleForms(target) {
            const loginForm = document.getElementById('login-form');
            const registerForm = document.getElementById('register-form');
            const loginText = document.getElementById('login-text');
            const registerText = document.getElementById('register-text');

            if (target === 'register') {
                loginForm.classList.remove('active');
                loginText.classList.remove('active');
                registerForm.classList.add('active');
                registerText.classList.add('active');
            } else {
                registerForm.classList.remove('active');
                registerText.classList.remove('active');
                loginForm.classList.add('active');
                loginText.classList.add('active');
            }
        }
    </script>
</asp:Content>
