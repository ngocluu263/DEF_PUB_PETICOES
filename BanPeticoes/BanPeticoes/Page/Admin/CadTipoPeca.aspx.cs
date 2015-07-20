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

public partial class CadTipoPeca : BaseWebUi
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
                CarregaDadosGrid();
            }
        }
    }

    protected void gvPrincipal_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        DataTable lTabela = (DataTable)ViewState["Tabela"];

        hfPeca.Value      = lTabela.Rows[e.NewSelectedIndex]["TPEN_ID"].ToString();
        txtPeca.Text      = lTabela.Rows[e.NewSelectedIndex]["TPEN_DESCRICAO"].ToString();

        panCadastro.Visible = true;
        panConsulta.Visible = false;
    }

    protected void gvPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable lTabela   = (DataTable)ViewState["Tabela"];
        hfPeca.Value        = lTabela.Rows[e.RowIndex]["TPEN_ID"].ToString();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "$(function(){$('#myModal').modal('show');});", true);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        string pRetorno = ValidaCampos();

        if (pRetorno != "")
            MessageBox1.wuc_ShowMessage(pRetorno, 2);
        else
            if (hfPeca.Value == "")
                CadastroPeca();
            else
                ModificarPeca("A");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        panCadastro.Visible = false;
        panConsulta.Visible = true;
        CarregaDadosGrid();
    }

    protected void btnSim_Click(object sender, EventArgs e)
    {
        ModificarPeca("I");
    }

    protected void btnNao_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NomedaJanela", "$(function(){$('#myModal').modal('close');});", true);
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        panCadastro.Visible = true;
        panConsulta.Visible = false;
    }

    #endregion

    #region Metodos Privados

    private void CarregaDadosGrid()
    {
        DataTable lTabela = TIPOPECAXNAAPDo.GetAllTIPOPECAXNAAP(LocalInstance.ConnectionInfo);

        if (lTabela.Rows.Count > 0)
        {
            ViewState["Tabela"] = lTabela;

            gvPrincipal.DataSource = (DataTable)ViewState["Tabela"];
            gvPrincipal.DataBind();
        }
        else
        {
            gvPrincipal.DataSource = null;
            gvPrincipal.DataBind();
            MessageBox1.wuc_ShowMessage("Pesquisa não retornou resultado", 2);
        }
    }

    private string ValidaCampos()
    {
        string Mensagem = "";

        if (txtPeca.Text == "")
            Mensagem = "Favor Preencha o Campo Peça";

        return Mensagem;
    }

    private void CadastroPeca()
    {
        DataFieldCollection lFields = new DataFieldCollection();
        OperationResult lReturn = new OperationResult();

        try
        {
            lFields.Clear();
            lFields.Add(TIPOPECAXNAAPQD._TPEN_DESCRICAO,    limparNome(txtPeca.Text));
            lFields.Add(TIPOPECAXNAAPQD._TPEN_REGDATE,      DateTime.Now);
            lFields.Add(TIPOPECAXNAAPQD._TPEN_STATUS,       "A");
            lFields.Add(TIPOPECAXNAAPQD._TPEN_USUARIO,      hfUsuario.Value);

            lReturn = TIPOPECAXNAAPDo.Insert(lFields, LocalInstance.ConnectionInfo);

            if (!lReturn.IsValid)
            {
                Exception err = new Exception(lReturn.OperationException.Message.ToString());
                MessageBox1.wuc_ShowMessage(err.ToString(), 3);
            }
            else
            {
                MessageBox1.wuc_ShowMessage("Tipo de Peça Cadastrada com Sucesso", 1);
                LimpaCampos();
                panCadastro.Visible = false;
                panConsulta.Visible = true;
                CarregaDadosGrid();
            }
        }
        catch (Exception err)
        {
            MessageBox1.wuc_ShowMessage(err.ToString(), 3);
        }
    }

    private void ModificarPeca(string pCondicao)
    {
        DataFieldCollection lFields = new DataFieldCollection();
        OperationResult lReturn = new OperationResult();

        try
        {
            lFields.Clear();
            lFields.Add(TIPOPECAXNAAPQD._TPEN_ID,        hfPeca.Value);
            if(pCondicao == "A")
                lFields.Add(TIPOPECAXNAAPQD._TPEN_DESCRICAO, limparNome(txtPeca.Text));
            lFields.Add(TIPOPECAXNAAPQD._TPEN_REGDATE,  DateTime.Now);
            lFields.Add(TIPOPECAXNAAPQD._TPEN_STATUS,   pCondicao);
            lFields.Add(TIPOPECAXNAAPQD._TPEN_USUARIO,  hfUsuario.Value);

            lReturn = TIPOPECAXNAAPDo.Update(lFields, LocalInstance.ConnectionInfo);

            if (!lReturn.IsValid)
            {
                Exception err = new Exception(lReturn.OperationException.Message.ToString());
                MessageBox1.wuc_ShowMessage(err.ToString(), 3);
            }
            else
            {
                MessageBox1.wuc_ShowMessage("Operação efetuada com Sucesso", 1);
                LimpaCampos();
                panCadastro.Visible = false;
                panConsulta.Visible = true;
                CarregaDadosGrid();
            }
        }
        catch (Exception err)
        {
            MessageBox1.wuc_ShowMessage(err.ToString(), 3);
        }
    }

    private void LimpaCampos()
    {
        txtPeca.Text = "";
        hfPeca.Value = "";
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

    #endregion
}