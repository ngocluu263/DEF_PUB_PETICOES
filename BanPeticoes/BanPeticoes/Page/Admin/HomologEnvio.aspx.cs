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

public partial class HomologEnvio : BaseWebUi
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hfUsuario.Value = Session["_SesUsuario"].ToString();

            if ((hfUsuario.Value != "ARLETH.GUIMARAES") && (hfUsuario.Value != "ANTONIO.CARDOSO") && (hfUsuario.Value != "ADMIN.WEB"))
            {
                panAcesso.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "alert('Acesso Negado')", true);
            }
            else
            {
                panAcesso.Visible = true;
                CarregaTipoMateria();
                CarregaTipoPeca();
                CarregaDadosGrid("");
            }
        }
    }

    protected void btnPesquisarTags_Click(object sender, EventArgs e)
    {
        if (txtData.Text != "")
            CarregaDadosGrid(txtData.Text);
        else
            MessageBox1.wuc_ShowMessage("Preencha o Campo Data", 2);
    }

    protected void gvPeticoes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        DataTable lTabela = (DataTable)ViewState["Tabela"];

        hfPeca.Value                    = lTabela.Rows[e.NewSelectedIndex]["PECN_ID"].ToString();
        ddlTipoMateria.SelectedValue    = lTabela.Rows[e.NewSelectedIndex]["TPMN_ID"].ToString();
        ddlTipoPeca.SelectedValue       = lTabela.Rows[e.NewSelectedIndex]["TPEN_ID"].ToString();
        txtEmenta.Text                  = lTabela.Rows[e.NewSelectedIndex]["PECN_EMENTA"].ToString();
        txtTags.Text                    = lTabela.Rows[e.NewSelectedIndex]["PECN_TAGS"].ToString();
        ftbPeticao.Text                 = lTabela.Rows[e.NewSelectedIndex]["PECN_TEXTO"].ToString();

        panHomolog.Visible = true;
        panConsulta.Visible = false;
    }

    protected void gvPeticoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable lTabela   = (DataTable)ViewState["Tabela"];
        hfPeca.Value        = lTabela.Rows[e.RowIndex]["PECN_ID"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "$(function(){$('#myModal').modal('show');});", true);
    }

    protected void btnHomologar_Click(object sender, EventArgs e)
    {
        string pRetorno = ValidaCampos();

        if (pRetorno != "")
            MessageBox1.wuc_ShowMessage(pRetorno, 2);
        else
            HomologarPeca();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        panHomolog.Visible = false;
        panConsulta.Visible = true;
        CarregaDadosGrid("");
    }

    protected void btnSim_Click(object sender, EventArgs e)
    {
        ExcluirPeca();
    }

    protected void btnNao_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "$(function(){$('#myModal').modal('close');});", true);
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
        DataTable lTabela = PECASXNAAPDo.GetPecasEnviadasHomolog(LocalInstance.ConnectionInfo, pParam);

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

    private string ValidaCampos()
    {
        string Mensagem = "";

        if (txtTags.Text == "")
            Mensagem = "Favor Preencha o Campo Tags";

        if ((txtEmenta.Text == "") && (Mensagem == ""))
            Mensagem = "Favor Preencha o Campo Ementa";

        if ((ftbPeticao.Text == "") && (Mensagem == ""))
            Mensagem = "Favor Preencha o Campo da Petição";

        return Mensagem;
    }

    private void HomologarPeca()
    {
        DataFieldCollection lFields = new DataFieldCollection();
        OperationResult lReturn = new OperationResult();

        try
        {
            lFields.Clear();
            lFields.Add(PECASXNAAPQD._PECN_ID,      hfPeca.Value);
            lFields.Add(PECASXNAAPQD._TPMN_ID,      ddlTipoMateria.SelectedValue);
            lFields.Add(PECASXNAAPQD._TPEN_ID,      ddlTipoPeca.SelectedValue);
            lFields.Add(PECASXNAAPQD._PECN_TAGS,    limparNome(txtTags.Text.ToUpper()));
            lFields.Add(PECASXNAAPQD._PECN_EMENTA,  limparNome(txtEmenta.Text.ToUpper()));
            lFields.Add(PECASXNAAPQD._PECN_TEXTO,   ftbPeticao.Text);
            lFields.Add(PECASXNAAPQD._PECN_REGDATE, DateTime.Now);
            lFields.Add(PECASXNAAPQD._PECN_STATUS,  "A");
            lFields.Add(PECASXNAAPQD._PECN_USUARIO, hfUsuario.Value);

            lReturn = PECASXNAAPDo.Update(lFields, LocalInstance.ConnectionInfo, "LOW");

            if (!lReturn.IsValid)
            {
                Exception err = new Exception(lReturn.OperationException.Message.ToString());
                MessageBox1.wuc_ShowMessage(err.ToString(), 3);
            }
            else
            {
                MessageBox1.wuc_ShowMessage("Peça Homologada com Sucesso", 1);
                LimpaCampos();
                panHomolog.Visible = false;
                panConsulta.Visible = true;
                CarregaDadosGrid("");
            }
        }
        catch (Exception err)
        {
            MessageBox1.wuc_ShowMessage(err.ToString(), 3);
        }
    }

    private void ExcluirPeca()
    {
        DataFieldCollection lFields = new DataFieldCollection();
        OperationResult lReturn = new OperationResult();

        try
        {
            lFields.Clear();
            lFields.Add(PECASXNAAPQD._PECN_ID,      hfPeca.Value);
            lFields.Add(PECASXNAAPQD._PECN_REGDATE, DateTime.Now);
            lFields.Add(PECASXNAAPQD._PECN_STATUS,  "I");
            lFields.Add(PECASXNAAPQD._PECN_USUARIO, hfUsuario.Value);

            lReturn = PECASXNAAPDo.Update(lFields, LocalInstance.ConnectionInfo, "LOW");

            if (!lReturn.IsValid)
            {
                Exception err = new Exception(lReturn.OperationException.Message.ToString());
                MessageBox1.wuc_ShowMessage(err.ToString(), 3);
            }
            else
            {
                MessageBox1.wuc_ShowMessage("Peça Exlcuída com Sucesso", 1);
                LimpaCampos();
                panHomolog.Visible = false;
                panConsulta.Visible = true;
                CarregaDadosGrid("");
            }
        }
        catch (Exception err)
        {
            MessageBox1.wuc_ShowMessage(err.ToString(), 3);
        }
    }

    private void LimpaCampos()
    {
        txtTags.Text = "";
        txtEmenta.Text = "";
        ftbPeticao.Text = "";
    }

    #endregion
}