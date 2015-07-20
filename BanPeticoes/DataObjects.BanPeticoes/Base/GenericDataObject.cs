using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	public static class GenericDataObject
	{
		#region Private Methods

		private static void ValidateDecimal(string pFieldName, object pValue, OperationResult pOperationResult)
		{
			decimal lResult;

			if (!Decimal.TryParse(pValue.ToString(), out lResult))
			{
				pOperationResult.InvalidField(pFieldName, "Invalid", pValue);
			}
		}

		private static void ValidateDate(string pFieldName, object pValue, OperationResult pOperationResult)
		{
			DateTime lResult;

			if (pValue.GetType() == typeof(DateTime)) return;
			
			if (!DateTime.TryParse(pValue.ToString(), out lResult))
			{
				pOperationResult.InvalidField(pFieldName, "Invalid", pValue);
			}
		}

  	    #endregion

		#region Public Methods

		public static void ValidateRequired(DataField pField, DataFieldCollection pValues, OperationResult pOperationResult)
		{
			if (!pValues.ContainsKey(pField))
			{
				pOperationResult.InvalidField(pField.Name, "Required", null);
			}
			else
			{
				if (pValues[pField] == null)
				{
					pOperationResult.InvalidField(pField.Name, "Required", null);
				}
				else
				{
					if (pValues[pField].ToString() == "")
						pOperationResult.InvalidField(pField.Name, "Required", null);
				}
			}
		}

		public static void ValidateConversion(DataFieldCollection pValues, OperationResult pOperationResult)
		{
			foreach (DataField lField in pValues.Keys)
			{
				switch (lField.DBType)
				{
					case 0:

						ValidateDecimal(lField.Name, pValues[lField], pOperationResult);

						break;

					case 2:

						ValidateDate(lField.Name, pValues[lField], pOperationResult);

						break;
				}
			}
		}

		#endregion
	}
}
