<%@ Page Title="" Language="C#" MasterPageFile="~/Page/Site.master" AutoEventWireup="true" CodeFile="PesPecaTags.aspx.cs" 
Inherits="PesPeticao" ValidateRequest="false" Trace="false" %>

<%@ Register src="~/Resources/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/FormPesquisaTab.css" rel="Stylesheet" />
    <link href="../Styles/FormGridView.css" rel="Stylesheet" />
    <link href="../Styles/FormModal.css" rel="Stylesheet" />
    <script src="../Scripts/TabForm.js" type="text/javascript"></script>
    <script src="../Scripts/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../Scripts/MSGAguarde.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContent" Runat="Server">
    <ul class="nav navbar-nav side-nav">
        <li><a href="Inicio.aspx"><i class="fa fa-dashboard"></i> Visão Geral</a></li>
        <li><a href="CadPeca.aspx"><i class="fa fa-file-text-o"></i> Cadastro de Peça Jurídica</a></li>
        <li><a href="PesPeca.aspx"><i class="fa fa-search-minus"></i> Pesquisa Geral</a></li>
        <li class="active"><a href="PesPecaTags.aspx"><i class="fa fa-search-minus"></i> Pesquisa por Tags</a></li>
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
<form action="PesPeca.aspx" role="form">
    <div id="page-wrapper">
        <div class="row">
          <div class="col-lg-12">
            <h1>Pesquisa <small>Peça Jurídica</small></h1>
          </div>
        </div>

        <div class="container">
        <div id="divProcessando"></div>
            <div class="row">
                <div class="col-md-11 col-md-offset-0">
                    <!-- Nav tabs category -->
                    <ul class="nav nav-tabs faq-cat-tabs">
                        <li class="active"><a href="#faq-cat-1" data-toggle="tab">Pesquisa Tags</a></li>
                    </ul>
    
                    <!-- Tab panes -->
                    <div class="tab-content faq-cat-content">
                
                    <!-- CONTEUDO DA TAB 1 -->
                    <div class="tab-pane active in fade" id="faq-cat-1">
                    <div class="panel-group" id="accordion-cat-1">
                         <div class="panel panel-primary">
                              <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-file-text-o"></i> Pesquisa de Peça Jurídica por Parâmetros</h3>
                              </div>
                              <div class="panel-body">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:RadioButtonList runat="server" ID="rbOpcoes" cssClass="checkbox" 
                                            RepeatDirection="Horizontal" AutoPostBack="True" 
                                            onselectedindexchanged="rbOpcoes_SelectedIndexChanged">
                                            <asp:ListItem Value="Tags">Tags&nbsp</asp:ListItem>
                                            <asp:ListItem Value="Ementa">Ementa&nbsp</asp:ListItem>
                                            <asp:ListItem Value="NumeroPeticao">Número da Petição&nbsp</asp:ListItem>
                                            <asp:ListItem Value="Conteudo">Conteúdo&nbsp</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="divTags" runat="server" class="form-group">
                                        <label>Tags</label>
                                        <asp:Textbox ID="txtTags" runat="server" cssClass="form-control"></asp:Textbox>
                                        <asp:CheckBox ID="cbTipoCons" runat="server" Text="&nbspPesquisar por nome exato"></asp:CheckBox>
                                    </div>
                                    <div id="divEmenta" runat="server" class="form-group" visible="false">
                                        <label>Ementa</label>
                                        <asp:Textbox ID="txtEmenta" runat="server" cssClass="form-control"></asp:Textbox>
                                    </div>
                                    <div id="divNumeroPeticao" runat="server" class="form-group" visible="false">
                                        <label>Número da Petição</label>
                                        <asp:Textbox ID="txtNumeroPet" runat="server" cssClass="form-control"></asp:Textbox>
                                    </div>
                                    <div id="divConteudo" runat="server" class="form-group" visible="false">
                                        <label>Conteúdo</label>
                                        <asp:Textbox ID="txtConteudo" runat="server" cssClass="form-control"></asp:Textbox>
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
                                        onpageindexchanging="gvPeticoes_PageIndexChanging">
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
                                            <asp:CommandField ButtonType="Image" HeaderText="Visualizar" 
                                                SelectImageUrl="~/images/Preview-icon.png" ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                </div>
                            <div class="col-lg-6">
                                <div ID="divConsulta" runat="server" Visible="false">
                                       <div class="form-group">
                                           <label>Ementa</label>
                                           <asp:Textbox ID="txtEmentaAvan" runat="server" cssClass="form-control"></asp:Textbox>
                                       </div>
                                       <div class="form-group">
                                           <label>Tipo de Matéria</label>
                                           <asp:DropDownList ID="ddlTipoMateria" runat="server" cssClass="form-control"></asp:DropDownList>
                                       </div>
                                       <div class="form-group">
                                           <label>Tipo de Peça</label>
                                           <asp:DropDownList ID="ddlTipoPeca" runat="server" cssClass="form-control"></asp:DropDownList>
                                       </div>
                                       <p>
                                           <asp:Button ID="btnPesquisarTagsAvan" runat="server" 
                                               CssClass = "btn btn-primary" Text="Pesquisa Avançada" 
                                               onclick="btnPesquisarTagsAvan_Click" />
                                       </p>
                                 </div>
                            </div>
                          </div>
                          </div>
                    </div>
                    </div>
                    <!-- FIM -->

                    </div>
                </div>
                <div class="container">
	                <div class="row">
		                <div id="myModal" class="modal fade in" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                        <div class="modal-content">
                        <div class="modal-header">
                            <a class="btn btn-default" data-dismiss="modal"><span class="fa fa-times"></span></a>
                            <h4 class="modal-title" id="myModalLabel"><%= hfPeticao.Value %></h4>
                        </div>
                        <div class="modal-body">
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
                                                                       height: 328, width: 860,
                                                                       scayt_autoStartup: false
                                                                   });
                                                                </script>
                        </span>
                        </div>
                        <div class="modal-footer">
                            <div class="btn-group">
                            <%--<button class="btn btn-primary">Imprimir</button>--%>
                            </div>
                        </div>
 
                        </div><!-- /.modal-content -->
                        </div><!-- /.modal-dalog -->
                        </div><!-- /.modal -->
                  </div>
               </div>      
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfPeticao" runat="server" />
</form>
</asp:Content>

