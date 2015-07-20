<%@ Page Title="" Language="C#" MasterPageFile="~/Page/Site.master" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContent" Runat="Server">
    <ul class="nav navbar-nav side-nav">
        <li class="active"><a href="Inicio.aspx"><i class="fa fa-dashboard"></i> Visão Geral</a></li>
        <li><a href="CadPeca.aspx"><i class="fa fa-file-text-o"></i> Cadastro de Peça Jurídica</a></li>
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
<form action="Inicio.aspx" role="form">
    <div id="page-wrapper">
        
         <div class="row">
          <div class="col-lg-12">
            <h1>Visão Geral <small>Resultados</small></h1>
            <ol class="breadcrumb">
              <li class="active"><i class="fa fa-dashboard"></i> Estatística</li>
            </ol>            
          </div>
        </div>
        
        <div class="row">
            <div class="col-lg-3">
            <div class="panel panel-success">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-file-text-o fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading"><asp:label ID="lblPecCad" runat="server"></asp:label></p>
                    <p class="announcement-text">Peças Jurídicas Cadastradas</p>
                  </div>
                </div>
              </div>             
            </div>
          </div>

          <div class="col-lg-3">
            <div class="panel panel-success">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-file-text-o fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading"><asp:label ID="lblPecEnv" runat="server"></asp:label></p>
                    <p class="announcement-text">Peças Jurídicas Enviadas</p>
                  </div>
                </div>
              </div>             
            </div>
          </div>

          <div class="col-lg-3">
            <div class="panel panel-success">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-file-text-o fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading"><asp:label ID="lblPecDia" runat="server"></asp:label></p>
                    <p class="announcement-text">No Dia <%= DateTime.Now.Date.ToString("dd/MM/yyyy") %></p>
                  </div>
                </div>
              </div>             
            </div>
          </div>
        </div>

    </div>
</form>
</asp:Content>

