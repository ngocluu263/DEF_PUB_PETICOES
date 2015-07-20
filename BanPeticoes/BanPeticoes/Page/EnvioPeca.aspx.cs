using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using APB.Mercury.DataObjects.BanPeticoes;
using APB.Mercury.DataObjects.BanPeticoes.QueryDictionaries;
using APB.Mercury.WebInterface.BanPeticoes.Www.DataAccess;
using FreeTextBoxControls;

public partial class EnvioPeca : BaseWebUi
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CarregaTipoMateria();
            CarregaTipoPeca();
            hfUsuario.Value = Session["_SesUsuario"].ToString();
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        string pRetorno = ValidaCampos();

        if (pRetorno != "")
            MessageBox1.wuc_ShowMessage(pRetorno, 2);
        else
            SalvarPeticaoNew();
    }

    #endregion

    #region Métodos Privados

    private void CarregaTipoMateria()
    {
        ddlTipoMateria.DataSource = TIPOMATERIAXNAAPDo.GetAllTIPOMATERIAXNAAP(LocalInstance.ConnectionInfo);
        ddlTipoMateria.DataTextField = "TPMN_DESCRICAO";
        ddlTipoMateria.DataValueField = "TPMN_ID";
        ddlTipoMateria.DataBind();
    }

    private void CarregaTipoPeca()
    {
        ddlTipoPeca.DataSource = TIPOPECAXNAAPDo.GetAllTIPOPECAXNAAP(LocalInstance.ConnectionInfo);
        ddlTipoPeca.DataTextField = "TPEN_DESCRICAO";
        ddlTipoPeca.DataValueField = "TPEN_ID";
        ddlTipoPeca.DataBind();
    }

    private void SalvarPeticaoNew()
    {
        DataFieldCollection lFields = new DataFieldCollection();
        OperationResult lReturn = new OperationResult();

        try
        {
            lFields.Clear();
            lFields.Add(PECASXNAAPQD._TPMN_ID,      ddlTipoMateria.SelectedValue);
            lFields.Add(PECASXNAAPQD._TPEN_ID,      ddlTipoPeca.SelectedValue);
            lFields.Add(PECASXNAAPQD._PECN_TAGS,    limparNome(txtTags.Text.ToUpper()));
            lFields.Add(PECASXNAAPQD._PECN_EMENTA,  limparNome(txtEmenta.Text.ToUpper()));
            lFields.Add(PECASXNAAPQD._PECN_TEXTO,   ftbPeticao.Text);
            lFields.Add(PECASXNAAPQD._PECN_REGDATE, DateTime.Now);
            lFields.Add(PECASXNAAPQD._PECN_STATUS,  "E");
            lFields.Add(PECASXNAAPQD._PECN_USUARIO, hfUsuario.Value);

            lReturn = PECASXNAAPDo.Insert(lFields, LocalInstance.ConnectionInfo);

            if (lReturn.IsValid)
            {
                hfPetNumero.Value = lReturn.SequenceControl.ToString();
                string Numero = DateTime.Now.Month + "0" + hfPetNumero.Value;
                string pPETN_NUMERO = "PECN" + Numero.PadLeft(11, '0') + "/" + DateTime.Now.Year.ToString();

                lFields.Clear();
                lFields.Add(PECASXNAAPQD._PECN_ID,      hfPetNumero.Value);
                lFields.Add(PECASXNAAPQD._PECN_NUMERO,  pPETN_NUMERO);
                lFields.Add(PECASXNAAPQD._PECN_REGDATE, DateTime.Now);
                lFields.Add(PECASXNAAPQD._PECN_STATUS,  "E");
                lFields.Add(PECASXNAAPQD._PECN_USUARIO, hfUsuario.Value);

                lReturn = PECASXNAAPDo.Update(lFields, LocalInstance.ConnectionInfo, "UPER");

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    MessageBox1.wuc_ShowMessage(err.ToString(), 3);
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Peça Cadastrada com Sucesso. O número da peça cadastrada é " + pPETN_NUMERO, 1);
                    LimpaCampos();
                }
            }
            else
            {
                Exception err = new Exception(lReturn.OperationException.Message.ToString());
                MessageBox1.wuc_ShowMessage(err.ToString(), 3);
            }
        }
        catch (Exception err)
        {
            MessageBox1.wuc_ShowMessage(err.ToString(), 3);
        }
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

    private void LimpaCampos()
    {
        txtTags.Text = "";
        txtEmenta.Text = "";
        ftbPeticao.Text = "";
    }

    private string ValidaCampos()
    {
        string Mensagem = "";

        if(txtTags.Text == "")
            Mensagem = "Favor Preencha o Campo Tags";

        if((txtEmenta.Text == "") && (Mensagem == ""))
            Mensagem = "Favor Preencha o Campo Ementa";

        if((ftbPeticao.Text == "") && (Mensagem == ""))
            Mensagem = "Favor Preencha o Campo da Petição";

        return Mensagem;
    }

    #endregion
}