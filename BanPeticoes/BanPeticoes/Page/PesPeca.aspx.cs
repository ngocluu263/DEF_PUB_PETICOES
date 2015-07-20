using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FreeTextBoxControls;

using APB.Mercury.DataObjects.BanPeticoes;
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;
using APB.Mercury.WebInterface.BanPeticoes.Www.DataAccess;

public partial class PesPeticao : BaseWebUi
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CarregaTipoMateria();
            CarregaTipoPeca();
        }
    }

    protected void btnPesquisarGeral_Click(object sender, EventArgs e) {
        string pCondicao = "";

        if (ddlTipoMateriaGer.SelectedValue != "0")
            pCondicao = " AND PECN.TPMN_ID = " + ddlTipoMateriaGer.SelectedValue + " ";
        
        if (ddlTipoPecaGer.SelectedValue != "0")
            pCondicao += " AND PECN.TPEN_ID = " + ddlTipoPecaGer.SelectedValue + " ";
        
        if((ddlTipoMateriaGer.SelectedValue == "0") && (ddlTipoPecaGer.SelectedValue == "0"))
            MessageBox1.wuc_ShowMessage("Favor escolher algumas das opções para pesquisa", 2);

        if (pCondicao != "")
            CarregaDadosGridGeral(pCondicao);
    }

    protected void gvPeca_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {
        DataTable lTabela = (DataTable)ViewState["Tabela_Peca"];

        hfPeticao.Value = "Petição: " + lTabela.Rows[e.NewSelectedIndex]["PECN_NUMERO"].ToString() + " - Tags: " + lTabela.Rows[e.NewSelectedIndex]["PECN_TAGS"].ToString();
        ftbPeticao.Text = lTabela.Rows[e.NewSelectedIndex]["PECN_TEXTO"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "$(function(){$('#myModal').modal('show');});", true);
    }

    protected void gvPeca_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPeca.DataSource = (DataTable)ViewState["Tabela_Peca"];
        gvPeca.PageIndex = e.NewPageIndex;
        gvPeca.DataBind();
    }

    #endregion

    #region Metodos Privados

    private string CondicaoTag(string pTag)
    {
        string Condicao = "";

        string[] words = pTag.Split(' ');
        foreach (string word in words)
        {
            if(Condicao == "")
                Condicao = "%" + word + "%";
            else
                Condicao += "" + word + "%";
        }

        return Condicao;
    }

    private string limparNome(string novoNome)
    {
        string novoTexto = "";

        while (novoNome.IndexOf("  ") >= 0)
            novoNome = novoNome.Replace("  ", " ");

        novoNome = novoNome.Trim();

        for (int pos = 0; pos < novoNome.Length; pos++)
        {
            //Deixe o texto em maiúsculo            
            novoTexto = novoNome.ToUpper();

            //Replace."`","" Ele percorre o texto e substitui os Caracteres            
            novoTexto = novoTexto.Replace("`", "");
            novoTexto = novoTexto.Replace("´", "");
            novoTexto = novoTexto.Replace("À", "A");
            novoTexto = novoTexto.Replace("Á", "A");
            novoTexto = novoTexto.Replace("Ã", "A");
            novoTexto = novoTexto.Replace("Â", "A");
            novoTexto = novoTexto.Replace("È", "E");
            novoTexto = novoTexto.Replace("É", "E");
            novoTexto = novoTexto.Replace("Ê", "E");
            novoTexto = novoTexto.Replace("Í", "I");
            novoTexto = novoTexto.Replace("Î", "I");
            novoTexto = novoTexto.Replace("Ì", "I");
            novoTexto = novoTexto.Replace("Ò", "O");
            novoTexto = novoTexto.Replace("Ó", "O");
            novoTexto = novoTexto.Replace("Õ", "O");
            novoTexto = novoTexto.Replace("Ô", "O");
            novoTexto = novoTexto.Replace("Ù", "U");
            novoTexto = novoTexto.Replace("Ú", "U");
            novoTexto = novoTexto.Replace("Û", "U");
            novoTexto = novoTexto.Replace("Ç", "C");
            novoTexto = novoTexto.Replace(",", "");
            novoTexto = novoTexto.Replace("`", "");
            novoTexto = novoTexto.Replace("´", "");
            novoTexto = novoTexto.Replace(".", "");
            novoTexto = novoTexto.Replace("¨", "");
            novoTexto = novoTexto.Replace("_", "");
            novoTexto = novoTexto.Replace(",", "");
        }
        return novoTexto;
    }

    private void CarregaTipoMateria()
    {
        ddlTipoMateriaGer.DataSource = TIPOMATERIAXNAAPDo.GetAllTIPOMATERIAXNAAP(LocalInstance.ConnectionInfo);
        ddlTipoMateriaGer.DataTextField = "TPMN_DESCRICAO";
        ddlTipoMateriaGer.DataValueField = "TPMN_ID";
        ddlTipoMateriaGer.DataBind();

        ddlTipoMateriaGer.Items.Insert(0, new ListItem("Selecione", "0"));
        ddlTipoMateriaGer.SelectedValue = "0";
    }

    private void CarregaTipoPeca()
    {
        ddlTipoPecaGer.DataSource = TIPOPECAXNAAPDo.GetAllTIPOPECAXNAAP(LocalInstance.ConnectionInfo);
        ddlTipoPecaGer.DataTextField = "TPEN_DESCRICAO";
        ddlTipoPecaGer.DataValueField = "TPEN_ID";
        ddlTipoPecaGer.DataBind();

        ddlTipoPecaGer.Items.Insert(0, new ListItem("Selecione", "0"));
        ddlTipoPecaGer.SelectedValue = "0";
    }

    private void CarregaDadosGridGeral(string pCondicao) {
        DataTable lTabela = PECASXNAAPDo.GetConsultaPecaGeral(LocalInstance.ConnectionInfo, pCondicao);

        if (lTabela.Rows.Count > 0) {
            ViewState["Tabela_Peca"] = lTabela;

            gvPeca.DataSource = (DataTable)ViewState["Tabela_Peca"];
            gvPeca.DataBind();
        }
        else {
            gvPeca.DataSource = null;
            gvPeca.DataBind();
            MessageBox1.wuc_ShowMessage("Pesquisa não retornou resultado", 2);
        }
    }

    #endregion    
}