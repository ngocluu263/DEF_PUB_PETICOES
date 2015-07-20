using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

using System.Linq;

using APB.Mercury.DataObjects.BanPeticoes;
using System.Data;

/// <summary>
/// Summary description for Extensions
/// </summary>
public static class Extensions
{
    #region OperationResult Extensions

    private static string GetResourceValue(string pLocalResourcePath, string pMessageKey)
    {
        object lValue = null;

        try
        {
            lValue = HttpContext.GetLocalResourceObject(pLocalResourcePath, pMessageKey);
        }
        catch { }

        if (lValue == null)
        {
            return string.Format("[{0}][{1}]", pLocalResourcePath, pMessageKey);
        }
        else
        {
            return lValue.ToString();
        }
    }

    public static string[] GetErrorMessages(this OperationResult lResult)
    {
        List<string> lMessages = new List<string>();

        string lLocalResourcePath = HttpContext.Current.Request.Path;

        if (lResult.InvalidFields != null)
        {
            foreach (InvalidDataField lField in lResult.InvalidFields)
            {
                lMessages.Add(GetResourceValue(lLocalResourcePath, lField.MessageKey));
            }
        }

        if (lResult.OperationException != null)
        {
            lMessages.Add(lResult.OperationException.Message);
            lMessages.Add(lResult.OperationException.StackTrace);
        }

        foreach (OperationResult lChildResult in lResult.ChildOperationResults)
        {
            lMessages.AddRange(lChildResult.GetErrorMessages());
        }

        while (lMessages.Contains(null)) lMessages.Remove(null);

        return lMessages.ToArray();
    }

    #endregion

    #region List<string> Extensions

    public static string Join(this List<string> pList, char pSeparator)
    {
        string lReturn = "";

        foreach (string lItem in pList)
        {
            if (lItem.Contains(pSeparator)) throw new Exception("No items in the list must contain the separator");

            lReturn += lItem + pSeparator;
        }

        lReturn = lReturn.TrimEnd(pSeparator);

        return lReturn;
    }

    #endregion

    #region Dictionary<T, K> Extensions

    public static string JoinKeys<T, K>(this Dictionary<T, K> pDictionary, char pSeparator, char pQuote)
    {
        StringBuilder lBuilder = new StringBuilder();

        foreach (T lKey in pDictionary.Keys)
        {
            lBuilder.Append(string.Format("{1}{0}{1}{2}", lKey, pQuote, pSeparator));
        }

        return lBuilder.ToString().TrimEnd(pSeparator);
    }

    #endregion

    #region DataTable Extensions

    public static Dictionary<object, string> Collect(this DataTable pTable, string pKeyFieldName, string pFormat, params string[] pStringFormatFieldNames)
    {
        Dictionary<object, string> lReturn = new Dictionary<object, string>();

        object[] lParams;

        foreach (DataRow lRow in pTable.Rows)
        {
            lParams = new object[pStringFormatFieldNames.Length];

            for (int a = 0; a < lParams.Length; a++)
                lParams[a] = lRow[pStringFormatFieldNames[a]];

            lReturn.Add(lRow[pKeyFieldName], string.Format(pFormat, lParams));
        }

        return lReturn;
    }

    #endregion
}
