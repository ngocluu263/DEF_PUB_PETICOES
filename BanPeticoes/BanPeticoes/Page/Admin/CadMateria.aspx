<%@ Page Title="" Language="C#" MasterPageFile="~/Page/Admin/Site.master" AutoEventWireup="true" CodeFile="CadMateria.aspx.cs" 
Inherits="CadMateria" ValidateRequest="false" Trace="false" %>

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
            <h1>Cadastro <small>Matérias</small></h1>
          </div>
        </div>

        <div id="divProcessando"></div>
        
        <%-- PAINEL DE CONSULTA DAS PEÇAS --%>
        <asp:Panel ID="panConsulta" runat="server">
            <div class="row>
            <div class="col-lg-12">
                 <div class="panel panel-primary">
                      <div class="panel-heading">
                           <h3 class="panel-title"><i class="fa fa-file-text-o"></i> Controle de Matérias</h3>
                      </div>
                      <div class="panel-body">
                           <div class="col-lg-6">
                                <asp:Button ID="btnNovo" runat="server" Text="Nova Matéria" cssClass="btn btn-default" onclick="btnNovo_Click"/>
                           </div>
                           <br />
                           <br />
                           <div class="col-lg-11">
                                <div class="EU_TableScroll" id="showData" style="display: block">
                                    <asp:GridView ID="gvPrincipal" runat="server" CssClass="EU_DataTable" 
                                        AllowPaging="True" DataKeyNames="TPMN_ID" width="100%" 
                                        AutoGenerateColumns="False" 
                                        onselectedindexchanging="gvPrincipal_SelectedIndexChanging" 
                                        onrowdeleting="gvPrincipal_RowDeleting" >
                                        <Columns>
                                            <asp:BoundField DataField="TPMN_ID" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="TPMN_DESCRICAO" HeaderText="Matéria">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Editar" 
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
        
        <%-- PAINEL DE CADASTRO DE INFORMAÇÕES --%>
        <asp:Panel ID="panCadastro" runat="server" visible="false">
            <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-file-text-o"></i> Preencha o Campo para Cadastro</h3>
                    </div>
                <div class="panel-body">
                    <div class="col-lg-6">
                      <div class="form-group">
                          <label>Matéria</label>
                          <asp:Textbox ID="txtMateria" runat="server" cssClass="form-control" MaxLength="200"></asp:Textbox>
                      </div>
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

