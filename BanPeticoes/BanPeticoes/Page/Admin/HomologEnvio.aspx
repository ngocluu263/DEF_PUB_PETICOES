<%@ Page Title="" Language="C#" MasterPageFile="~/Page/Admin/Site.master" AutoEventWireup="true" CodeFile="HomologEnvio.aspx.cs" 
Inherits="HomologEnvio" ValidateRequest="false" Trace="false" %>

<%@ Register src="~/Resources/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Styles/FormPesquisaTab.css" rel="Stylesheet" />
    <link href="../../Styles/FormGridView.css" rel="Stylesheet" />
    <link href="../../Styles/CustomModal.css" rel="Stylesheet" />
    <script src="../../Scripts/TabForm.js" type="text/javascript"></script>
    <script src="../../Scripts/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../Scripts/MSGAguarde.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.mask.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.mask.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function(){
            $("#[id$='txtData']").mask('00/00/0000');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContent" Runat="Server">
    <ul class="nav navbar-nav side-nav">
        <li><a href="../Inicio.aspx"><i class="fa fa-dashboard"></i> Visão Geral</a></li>
        <li><a href="../CadPeca.aspx"><i class="fa fa-file-text-o"></i> Cadastro de Peça Jurídica</a></li>
        <li><a href="../PesPeca.aspx"><i class="fa fa-search-minus"></i> Pesquisa Geral</a></li>
        <li><a href="../PesPecaTags.aspx"><i class="fa fa-search-minus"></i> Pesquisa por Tags</a></li>
        <li><a href="../EnvioPeca.aspx"><i class="fa fa-files-o"></i> Envio de Peça Jurídica</a></li>
        <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-cog"></i> Configurações <b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li><a href="HomologEnvio.aspx"><i class="fa fa-check-square-o"></i> Peças Enviadas</a></li>
              <li><a href="CadMateria.aspx"><i class="fa fa-file-text-o"></i> Cadastro Matérias</a></li>
              <li><a href="CadTipoPeca.aspx"><i class="fa fa-file-text-o"></i> Cadastro Tipos de Peça</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <uc1:MessageBox ID="MessageBox1" runat="server" />
<form action="PesPeca.aspx" role="form">
<asp:Panel ID="panAcesso" runat="server" Visible="false">
    <div id="page-wrapper">
        <div class="row">
          <div class="col-lg-12">
            <h1>Peças Enviadas <small>Homologação</small></h1>
          </div>
        </div>

        <div id="divProcessando"></div>
        
        <%-- PAINEL DE CONSULTA DAS PEÇAS --%>
        <asp:Panel ID="panConsulta" runat="server">
            <div class="row>
            <div class="col-lg-12">
                 <div class="panel panel-primary">
                      <div class="panel-heading">
                           <h3 class="panel-title"><i class="fa fa-file-text-o"></i> Pesquisa de Peça Jurídica Enviada</h3>
                      </div>
                      <div class="panel-body">
                           <div class="col-lg-4">
                                <div id="divTags" runat="server" class="form-group">
                                    <label>Data</label>
                                    <asp:Textbox ID="txtData" runat="server" cssClass="form-control"></asp:Textbox>
                                </div>
                                <p>
                                    <asp:Button ID="btnPesquisarTags" runat="server" CssClass = "btn btn-primary" 
                                        Text="Pesquisar" onclick="btnPesquisarTags_Click" />
                                </p>    
                           </div>
                           <div class="col-lg-11">
                                <div class="EU_TableScroll" id="showData" style="display: block">
                                    <asp:GridView ID="gvPeticoes" runat="server" CssClass="EU_DataTable" 
                                        AllowPaging="True" DataKeyNames="PECN_ID" width="100%" 
                                        AutoGenerateColumns="False" 
                                        onselectedindexchanging="gvPeticoes_SelectedIndexChanging" 
                                        onrowdeleting="gvPeticoes_RowDeleting" >
                                        <Columns>
                                            <asp:BoundField DataField="PECN_ID" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="PECN_TEXTO" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="PECN_NUMERO" HeaderText="Número">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TPMN_DESCRICAO" HeaderText="Tipo Matéria">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TPEN_DESCRICAO" HeaderText="Tipo Peça">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PECN_TAGS" HeaderText="Tags">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PECN_EMENTA" HeaderText="Ementa">
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Homologar" 
                                                SelectImageUrl="~/images/Preview-icon.png" ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:CommandField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Excluir" 
                                                ShowDeleteButton="True" DeleteImageUrl="~/images/File-Delete-icon24.png">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                           </div>
                      </div>
                 </div>
            </div>
            </div>
        </asp:Panel>
        
        <%-- PAINEL DE HOMOLOGAÇÃO DE INFORMAÇÕES --%>
        <asp:Panel ID="panHomolog" runat="server" visible="false">
            <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-file-text-o"></i> Confirme Todos os Campos para Homologação da Peça</h3>
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
                           <asp:Button ID="btnHomologar" runat="server" Text="Homologar" 
                               cssClass="btn btn-primary" onclick="btnHomologar_Click"/>
                           <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                               cssClass="btn btn-danger" onclick="btnCancelar_Click"/>
                      </p>
                 </div>
                </div>
                </div>
                </div>
            </div>
        </div>
        </asp:Panel>

	    <div class="row">
		      <div id="myModal" class="modal fade in" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
              <div class="modal-dialog">
              <div class="modal-content">
              <div class="modal-header">
                  <a class="btn btn-default" data-dismiss="modal"><span class="fa fa-times"></span></a>
                  <h4 class="modal-title" id="myModalLabel">Deseja confirmar a exclusão?</h4>
              </div>
              <div class="modal-body">
                  <p>
                    <asp:Button ID="btnSim" runat="server" Text="Sim" 
                               cssClass="btn btn-primary" onclick="btnSim_Click"/>
                    <asp:Button ID="btnNao" runat="server" Text="Não" 
                               cssClass="btn btn-danger" onclick="btnNao_Click"/>
                  </p>
              </div>
              <div class="modal-footer">
                  <div class="btn-group">
                  </div>
              </div>
 
              </div><!-- /.modal-content -->
              </div><!-- /.modal-dalog -->
              </div><!-- /.modal -->
        </div>
    </div>
</asp:Panel>
    <asp:HiddenField ID="hfPeca" runat="server" />
    <asp:HiddenField ID="hfUsuario" runat="server" />
</form>
</asp:Content>

