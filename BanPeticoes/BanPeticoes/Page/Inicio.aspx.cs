using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using APB.Mercury.DataObjects.BanPeticoes;
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;
using APB.Mercury.WebInterface.BanPeticoes.Www.DataAccess;

public partial class Inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblPecCad.Text = PECASXNAAPDo.GetConsultaPeticaoTotal(LocalInstance.ConnectionInfo);
            lblPecEnv.Text = PECASXNAAPDo.GetConsultaPecaEnviada(LocalInstance.ConnectionInfo);
            lblPecDia.Text = PECASXNAAPDo.GetConsultaPeticaoTotalPorDia(LocalInstance.ConnectionInfo, DateTime.Now.ToString("dd/MM/yyyy"));
        }
    }
}