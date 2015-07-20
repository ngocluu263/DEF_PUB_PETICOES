using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using APB.Mercury.DataObjects.BanPeticoes;
using APB.Mercury.WebInterface.BanPeticoes.Www.DataAccess;
using System.Web.Services;

public partial class Login : BaseWebUi 
{
    protected void Page_Load(object sender, EventArgs e) 
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e) 
    {
        DataTable lTabela = SystemUserDo.GetSystemUser(LocalInstance.ConnectionInfo, txtUsuario.Value.ToUpper(), txtSenha.Value);

        if (lTabela.Rows.Count > 0) {
            Session["_SesUsuario"] = txtUsuario.Value.ToUpper();
            Session["_SesPesId"] = lTabela.Rows[0]["PES_ID"].ToString();
            Response.Redirect("~/Page/Inicio.aspx");
        }
        else {
            MessageBox1.wuc_ShowMessage("Dados Inválidos", 1);
        }
    }

	[WebMethod]
    protected void autenticacao(String login, String senha)
    {
		/**
		 * 
		 * http://BanPeticoes/Login.aspx/autenticacao
		 * 
		 */
        DataTable lTabela = SystemUserDo.GetSystemUser(LocalInstance.ConnectionInfo, login.ToUpper(), senha);

        if (lTabela.Rows.Count > 0) {
            Session["_SesUsuario"] = txtUsuario.Value.ToUpper();
            Session["_SesPesId"] = lTabela.Rows[0]["PES_ID"].ToString();
            Response.Redirect("~/Page/Inicio.aspx");
        }
    }
}