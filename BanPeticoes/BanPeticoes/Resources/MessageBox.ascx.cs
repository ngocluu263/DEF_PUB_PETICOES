using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class WebUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string appPath = HttpContext.Current.Request.ApplicationPath;
        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
        MessageBox msgbox = new MessageBox(physicalPath + "\\Resources\\Config\\msgbox.txt");
        msgboxpanel.InnerHtml = ""; // msgbox.ReturnObject();
        //msgboxpanel.Visible = false;
    }

    public void wuc_ShowMessage(string pInfo)
    {
        string appPath = HttpContext.Current.Request.ApplicationPath;
        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
        MessageBox msgbox = new MessageBox(physicalPath + "\\Resources\\Config\\msgbox.txt");
        msgbox.SetTitle("Informação");
        if (appPath.ToString() != "/")
            msgbox.SetIcon(appPath + "/Resources/Img/information.png");
        else
            msgbox.SetIcon("/Resources/Img/information.png");
        msgbox.SetMessage(pInfo);
        msgboxpanel.Visible = true;
        msgbox.SetOKButton("msg_button_class");
        msgboxpanel.InnerHtml = msgbox.ReturnObject();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pInfo">Mensagem</param>
    /// <param name="type">Tipo de Mensagem. 1 - information; 2 - warning; 3 - erro;</param>
    public void wuc_ShowMessage(string pInfo, int type)
    {
        string appPath = HttpContext.Current.Request.ApplicationPath;
        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
        MessageBox msgbox = new MessageBox(physicalPath + "\\Resources\\Config\\msgbox.txt");
        msgbox.SetMessage(pInfo);
        
        switch (type)
        {
            case 1:
                msgbox.SetTitle("Informação");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/information.png");
                else
                    msgbox.SetIcon("/Resources/Img/information.png");
                break;
            case 2:
                msgbox.SetTitle("Atenção");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/warning.png");
                else
                    msgbox.SetIcon("/Resources/Img/warning.png");
                break;
            case 3:
                msgbox.SetTitle("Error");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/error.png");
                else
                    msgbox.SetIcon("/Resources/Img/error.png");
                break;
        }
        msgboxpanel.Visible = true;
        msgbox.SetOKButton("msg_button_class");
        msgboxpanel.InnerHtml = msgbox.ReturnObject();
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pInfo">Mensagem</param>
    /// <param name="pPageRedirect">Pagina Redirecionada</param>
    /// <param name="type">Tipo de Mensagem. 1 - information; 2 - warning; 3 - erro;</param>
    public void wuc_ShowMessage(string pInfo, string pPageRedirect, int type)
    {
        string appPath = HttpContext.Current.Request.ApplicationPath;
        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
        MessageBox msgbox = new MessageBox(physicalPath + "\\Resources\\Config\\msgbox.txt");
        msgbox.SetMessage(pInfo);

        switch (type)
        {
            case 1:
                msgbox.SetTitle("Informação");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/information.png");
                else
                    msgbox.SetIcon("/Resources/Img/information.png");
                break;
            case 2:
                msgbox.SetTitle("Atenção");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/warning.png");
                else
                    msgbox.SetIcon("/Resources/Img/warning.png");
                break;
            case 3:
                msgbox.SetTitle("Error");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/error.png");
                else
                    msgbox.SetIcon("/Resources/Img/error.png");
                break;
        }
        msgboxpanel.Visible = true;
        msgbox.SetOKButton("msg_button_class", pPageRedirect);
        msgboxpanel.InnerHtml = msgbox.ReturnObject();
    }

    public void wuc_ShowMessage(string pInfo, string pPageRedirect, int type, bool confirm)
    {
        string appPath = HttpContext.Current.Request.ApplicationPath;
        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
        MessageBox msgbox = new MessageBox(physicalPath + "\\Resources\\Config\\msgbox.txt");
        msgbox.SetMessage(pInfo);

        switch (type)
        {
            case 1:
                msgbox.SetTitle("Informação");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/information.png");
                else
                    msgbox.SetIcon("/Resources/Img/information.png");
                break;
            case 2:
                msgbox.SetTitle("Atenção");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/warning.png");
                else
                    msgbox.SetIcon("/Resources/Img/warning.png");
                break;
            case 3:
                msgbox.SetTitle("Error");
                if (appPath.ToString() != "/")
                    msgbox.SetIcon(appPath + "/Resources/Img/error.png");
                else
                    msgbox.SetIcon("/Resources/Img/error.png");
                break;
        }
        msgboxpanel.Visible = true;
        msgbox.SetOKButton("msg_button_class", pPageRedirect, confirm);
        msgboxpanel.InnerHtml = msgbox.ReturnObject();
    }
}
