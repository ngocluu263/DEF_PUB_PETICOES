<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register src="~/Resources/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NAAP - Login</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/FormLogin.css" rel="stylesheet" />
    <link href="Styles/Custom.css" rel="stylesheet" />
    
    <style type="text/css">
        body{padding-top: 3%;}
        .btn-label {position: relative;left: -12px;display: inline-block;padding: 6px 12px;background: rgba(0,0,0,0.15);border-radius: 3px 0 0 3px;}
        .btn-labeled {padding-top: 0;padding-bottom: 0;}
        .btn { margin-bottom:10px; }
    </style>

    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/MSGAguarde.js" type="text/javascript"></script>

</head>
<body>
    <uc1:MessageBox ID="MessageBox1" runat="server" />
    <form action="Login.aspx" runat="server" accept-charset="UTF-8" role="form">
    <div class="container">
        <div class="row">
        <div id="divProcessando"></div>
		<div class="col-md-4 col-md-offset-4 well">
            <div class="modal-header">
                <h3 style="text-align: center;">NAAP - Banco de Peças Jurídicas</h3>
                <img style="margin-left: 30%;" alt="" src="images/1389205608_folder_library.png"/>
            </div>
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title" style="text-align: center;">Favor se logar com usuário do DefNet</h3>
			 	</div>
			  	<div class="panel-body">
                    <fieldset>
                        
			    	  	<div class="form-group">
                            <input type="text" runat="server" id="txtUsuario" name="txtUsuario" class="form-control" placeholder="Usuário" required autofocus>
			    		</div>
			    		<div class="form-group">
			    			<input type="password" runat="server" id="txtSenha" name="txtSenha" class="form-control" placeholder="Senha" required autofocus>
			    		</div>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" 
                            cssClass="btn btn-lg btn-primary btn-block" onclick="btnLogin_Click"/>
			    	</fieldset>
			    </div>
			</div>
		</div>
	    </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </form>
</body>
</html>
