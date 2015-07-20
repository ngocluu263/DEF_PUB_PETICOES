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
            rbOpcoes.SelectedIndex = 0;
            CarregaTipoMateria();
            CarregaTipoPeca();
        }
    }
    protected void btnPesquisarTags_Click(object sender, EventArgs e)
    {
        string pParam = "";

        if (rbOpcoes.SelectedValue == "Tags")
        {
            pParam = limparNome(txtTags.Text.ToUpper());

            if (!cbTipoCons.Checked)
                pParam = CondicaoTag(pParam);

            CarregaDadosGrid(pParam);
        }

        if (rbOpcoes.SelectedValue == "Ementa")
        {
            pParam = limparNome(txtEmenta.Text.ToUpper());
            string pCondicao = "AND PECN_EMENTA LIKE '%" + pParam + "%' ";

            divConsulta.Visible = false;
            CarregaDadosGridPorCondicao(pCondicao);
        }

        if (rbOpcoes.SelectedValue == "NumeroPeticao")
        {
            pParam = limparNome(txtNumeroPet.Text.ToUpper());
            string pCondicao = "AND PECN_NUMERO='" + pParam + "' ";

            divConsulta.Visible = false;
            CarregaDadosGridPorCondicao(pCondicao);
        }

        if (rbOpcoes.SelectedValue == "Conteudo")
        {
            pParam = limparNome(txtConteudo.Text.ToUpper());
            string pCondicao = "AND PECN_TEXTO LIKE '%" + pParam + "%' ";

            divConsulta.Visible = false;
            CarregaDadosGridPorCondicao(pCondicao);
        }
    }

    protected void rbOpcoes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbOpcoes.SelectedIndex == 0)
        {
            divTags.Visible = true;
            divEmenta.Visible = false;
            divNumeroPeticao.Visible = false;
            divConteudo.Visible = false;
            divConsulta.Visible = false;
            gvPeticoes.DataSource = null;
            gvPeticoes.DataBind();
        }

        if (rbOpcoes.SelectedIndex == 1)
        {
            divTags.Visible = false;
            divEmenta.Visible = true;
            divNumeroPeticao.Visible = false;
            divConteudo.Visible = false;
            divConsulta.Visible = false;
            gvPeticoes.DataSource = null;
            gvPeticoes.DataBind();
        }

        if (rbOpcoes.SelectedIndex == 2)
        {
            divTags.Visible = false;
            divEmenta.Visible = false;
            divNumeroPeticao.Visible = true;
            divConteudo.Visible = false;
            divConsulta.Visible = false;
            gvPeticoes.DataSource = null;
            gvPeticoes.DataBind();
        }

        if (rbOpcoes.SelectedIndex == 3)
        {
            divTags.Visible = false;
            divEmenta.Visible = false;
            divNumeroPeticao.Visible = false;
            divConteudo.Visible = true;
            divConsulta.Visible = false;
            gvPeticoes.DataSource = null;
            gvPeticoes.DataBind();
        }
    }

    protected void btnPesquisarTagsAvan_Click(object sender, EventArgs e)
    {
        string pParam = limparNome(txtTags.Text.ToUpper());
        string pCondicao = "";

        if (!cbTipoCons.Checked)
            pParam = " AND PECN.PECN_TAGS LIKE '" + CondicaoTag(pParam) + "' ";
        else
            pParam = " AND PECN.PECN_TAGS LIKE '%" + pParam + "%' ";

        if (txtEmentaAvan.Text != "")
            pCondicao = " AND PECN.PECN_EMENTA LIKE '%" + txtEmentaAvan.Text.ToUpper() + "%' " + pParam;
        
        if (ddlTipoMateria.SelectedValue != "0")
            pCondicao = " AND PECN.TPMN_ID = " + ddlTipoMateria.SelectedValue + " " + pParam;

        if (ddlTipoPeca.SelectedValue != "0")
            pCondicao = " AND PECN.TPEN_ID = " + ddlTipoPeca.SelectedValue + " " + pParam;

        if ((txtEmentaAvan.Text != "") && (ddlTipoMateria.SelectedValue != "0") && (ddlTipoPeca.SelectedValue != "0"))
            pCondicao = " AND PECN.PECN_EMENTA LIKE '%" + txtEmentaAvan.Text.ToUpper() + "%' " + " AND PECN.TPMN_ID = " + ddlTipoMateria.SelectedValue + " " + " AND PECN.TPEN_ID = " + ddlTipoPeca.SelectedValue + " " + pParam;

        CarregaDadosGridTagsAvancado(pCondicao);
    }

    protected void gvPeticoes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        DataTable lTabela = (DataTable)ViewState["Tabela"];

        hfPeticao.Value = "Petição: " + lTabela.Rows[e.NewSelectedIndex]["PECN_NUMERO"].ToString() + " - Tags: " + lTabela.Rows[e.NewSelectedIndex]["PECN_TAGS"].ToString();
        ftbPeticao.Text = lTabela.Rows[e.NewSelectedIndex]["PECN_TEXTO"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "$(function(){$('#myModal').modal('show');});", true);
    }

    protected void gvPeticoes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPeticoes.DataSource = (DataTable)ViewState["Tabela"];
        gvPeticoes.PageIndex = e.NewPageIndex;
        gvPeticoes.DataBind();
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
        ddlTipoMateria.DataSource = TIPOMATERIAXNAAPDo.GetAllTIPOMATERIAXNAAP(LocalInstance.ConnectionInfo);
        ddlTipoMateria.DataTextField = "TPMN_DESCRICAO";
        ddlTipoMateria.DataValueField = "TPMN_ID";
        ddlTipoMateria.DataBind();
       
        ddlTipoMateria.Items.Insert(0, new ListItem("Selecione", "0"));
        ddlTipoMateria.SelectedValue = "0";
    }

    private void CarregaTipoPeca()
    {
        ddlTipoPeca.DataSource = TIPOPECAXNAAPDo.GetAllTIPOPECAXNAAP(LocalInstance.ConnectionInfo);
        ddlTipoPeca.DataTextField = "TPEN_DESCRICAO";
        ddlTipoPeca.DataValueField = "TPEN_ID";
        ddlTipoPeca.DataBind();
     
        ddlTipoPeca.Items.Insert(0, new ListItem("Selecione", "0"));
        ddlTipoPeca.SelectedValue = "0";
    }

    private void CarregaDadosGrid(string pParam)
    {
        DataTable lTabela = new DataTable();
        
        if(!cbTipoCons.Checked)
            lTabela = PECASXNAAPDo.GetConsultaPeticaoporTags(LocalInstance.ConnectionInfo, pParam, 1);
        else
            lTabela = PECASXNAAPDo.GetConsultaPeticaoporTags(LocalInstance.ConnectionInfo, pParam, 0);

        if (lTabela.Rows.Count > 0)
        {
            ViewState["Tabela"] = lTabela;

            gvPeticoes.DataSource = (DataTable)ViewState["Tabela"];
            gvPeticoes.DataBind();

            divConsulta.Visible = true;
        }
        else
            MessageBox1.wuc_ShowMessage("Pesquisa não retornou resultado", 2);
    }

    private void CarregaDadosGridTagsAvancado(string pCondicao)
    {
        DataTable lTabela = PECASXNAAPDo.GetConsultaPeticaoporTagsAvancado(LocalInstance.ConnectionInfo, pCondicao);

        if (lTabela.Rows.Count > 0)
        {
            ViewState["Tabela"] = lTabela;

            gvPeticoes.DataSource = (DataTable)ViewState["Tabela"];
            gvPeticoes.DataBind();
        }
        else
        {
            gvPeticoes.DataSource = null;
            gvPeticoes.DataBind();

            MessageBox1.wuc_ShowMessage("Pesquisa não retornou resultado", 2);
        }
    }

    private void CarregaDadosGridPorCondicao(string pCondicao)
    {
        DataTable lTabela = PECASXNAAPDo.GetConsultaPeticaoPorCondicao(LocalInstance.ConnectionInfo, pCondicao);

        if (lTabela.Rows.Count > 0)
        {
            ViewState["Tabela"] = lTabela;

            gvPeticoes.DataSource = (DataTable)ViewState["Tabela"];
            gvPeticoes.DataBind();
        }
        else
        {
            gvPeticoes.DataSource = null;
            gvPeticoes.DataBind();

            MessageBox1.wuc_ShowMessage("Pesquisa não retornou resultado", 2);
        }
    }

    #endregion
}