using System;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	/// <summary>
	/// Campo que falhou na validação
	/// </summary>
	[Serializable]
	public class InvalidDataField
	{
		#region Properties

		public string DBFieldName { get; set; }

		public string ValidationError { get; set; }

		public string MessageKey { get; set; }

		public object InvalidValue { get; set; }

		#endregion

		#region Constructor

		public InvalidDataField() { }

		/// <summary>
		/// Adiciona um campo inválido ao resultado
		/// </summary>
		/// <param name="pFieldDBName">Nome completo do campo no banco de dados, com underscore no lugar de ponto: Tabela_Campo</param>
		/// <param name="pValidationError">Erro específico que ocorreu no campo, sem underscore; vai concatenar com o nome do campo e procurar no arquivo de resources. Ex.: Users_BirthDate_InFuture</param>
		/// <param name="p"></param>
		public InvalidDataField(string pFieldDBName, string pValidationError, object pInvalidValue)
		{
			this.DBFieldName		= pFieldDBName;
			this.ValidationError	= pValidationError;
			this.InvalidValue		= pInvalidValue;

			if (pFieldDBName.EndsWith("_")) pFieldDBName = pFieldDBName.TrimEnd('_');

			if (pValidationError.StartsWith("_")) pValidationError = pValidationError.TrimStart('_');

			this.MessageKey = pFieldDBName + "_" + pValidationError;

			//_Message = CultureHub.BuldValidationMessage(lMessageKey, pMessageParams);
		}

		#endregion
	}
}
