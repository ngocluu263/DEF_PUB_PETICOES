<%@ Page Title="" Language="C#" MasterPageFile="~/Page/Site.master" AutoEventWireup="true" 
CodeFile="CadPeca.aspx.cs" ValidateRequest="false" Trace="false" Inherits="CadPeticao" culture="auto" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register src="~/Resources/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../Scripts/ckeditor/ckeditor.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContent" Runat="Server">
    <ul class="nav navbar-nav side-nav">
        <li><a href="Inicio.aspx"><i class="fa fa-dashboard"></i> Visão Geral</a></li>
        <li class="active"><a href="CadPeca.aspx"><i class="fa fa-file-text-o"></i> Cadastro de Peça Jurídica</a></li>
        <li><a href="PesPeca.aspx"><i class="fa fa-search-minus"></i> Pesquisa Geral</a></li>
        <li><a href="PesPecaTags.aspx"><i class="fa fa-search-minus"></i> Pesquisa por Tags</a></li>
        <li><a href="EnvioPeca.aspx"><i class="fa fa-files-o"></i> Envio de Peça Jurídica</a></li>
        <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-cog"></i> Configurações <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><a href="Admin/HomologEnvio.aspx"><i class="fa fa-check-square-o"></i> Peças Enviadas</a></li>
              <li><a href="Admin/CadMateria.aspx"><i class="fa fa-file-text-o"></i> Cadastro Matérias</a></li>
              <li><a href="Admin/CadTipoPeca.aspx"><i class="fa fa-file-text-o"></i> Cadastro Tipos de Peça</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
<uc1:MessageBox ID="MessageBox1" runat="server" />
<form action="CadPeca.aspx" role="form">
<asp:Panel ID="panVisualizacao" runat="server">
    <div id="page-wrapper">
        <div class="row">
          <div class="col-lg-12">
            <h1>Cadastro <small>Peça Jurídica</small></h1>
          </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-file-text-o"></i> Informe Todos os Campos para Cadastro da Peça</h3>
                    </div>
                <div class="panel-body">
                    <div class="col-lg-6">
                      <div class="form-group">
                          <label>Tipo da Matéria</label>
                          <asp:DropDownList ID="ddlTipoMateria" runat="server" cssClass="form-control"></asp:DropDownList>
                      </div>
                      <div class="form-group">
                          <label>Tipo da Peça</label>
                          <asp:DropDownList ID="ddlTipoPeca" runat="server" cssClass="form-control"></asp:DropDownList>
                      </div>
                      <div class="form-group">
                          <label>Tags</label>
                          <asp:Textbox ID="txtTags" runat="server" cssClass="form-control" MaxLength="200"></asp:Textbox>
                      </div>
                      <div class="form-group">
                          <label>Ementa</label>
                          <asp:Textbox ID="txtEmenta" runat="server" cssClass="form-control"
                           Height="180px" TextMode="MultiLine" MaxLength="400"></asp:Textbox>
                      </div>
                      <%--<div class="form-group">
                          <label>Modelos</label>
                          <asp:DropDownList ID="ddlModeloPeticao" runat="server" cssClass="form-control"></asp:DropDownList>
                      </div>--%>
                    </div>
                    <div class="col-lg-11">
                      <div class="form-group">
                        <span class="wide">
                        <asp:TextBox id = "ftbPeticao" TextMode="MultiLine" runat="server" ></asp:TextBox>
                                         <script type="text/javascript">

                                             CKEDITOR.replace('<%=ftbPeticao.ClientID %>',
                                                                       {
                                                                           toolbar:
                                                                        [
                                                                           ['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
                                                                    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
                                                                    ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
                                                                    ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
                                                                    '/',
                                                                    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
                                                                    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
                                                                    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                                                                    ['Link', 'Unlink', 'Anchor'],
                                                                    ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
                                                                    '/',
                                                                    ['Styles', 'Format', 'Font', 'FontSize'],
                                                                    ['TextColor', 'BGColor'],
                                                                    ['Maximize', 'ShowBlocks', '-', 'About']

                                                                        ],
                                                                           height: 400,
                                                                           scayt_autoStartup: false
                                                                       });
                                                                    </script>
                        </span>
                      </div>
                      <div class="col-lg-6">
                      <p>
                           <asp:Button ID="btnSalvar" runat="server" Text="Salvar" 
                               cssClass="btn btn-primary" onclick="btnSalvar_Click"/>
                           <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                               cssClass="btn btn-danger" onclick="btnCancelar_Click"/>
                      </p>
                 </div>
                </div>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
    <asp:HiddenField ID="hfUsuario" runat="server" />
    <asp:HiddenField ID="hfPetNumero" runat="server" />
</form>
</asp:Content>

