using System;
using System.Collections.Generic;
using System.Text;

using APB.Mercury.DataObjects;
using System.Xml.Serialization;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	/// <summary>
	/// Resultado do instanceamento de um objeto
	/// </summary>
	[Serializable]
	public class OperationResult
	{
		#region Properties

		public List<InvalidDataField> InvalidFields { get; set; }

		public bool IsValid
		{
			get 
			{ 
				bool lValid = (this.InvalidFields == null && this.OperationException == null);

				if(lValid)
				{
					foreach (OperationResult lChildResult in this.ChildOperationResults)
					{
						if (!lChildResult.IsValid)
						{
							lValid = false;
							break;
						}
					}
				}

				return lValid; 
			}
		}

		public string InsertingToTableName { get; set; }

		public string InsertingObjectName { get; set; }

        //Chave gerada para inserir tabela.
        public decimal SequenceControl { get; set; }

		public List<OperationResult> ChildOperationResults { get; set; }

		public SerializableException OperationException { get; set; }

		public bool HasError
		{
			get
			{
				bool lHasError = (this.OperationException != null);

				if (this.ChildOperationResults != null)
				{
					foreach (OperationResult lResult in this.ChildOperationResults)
					{
						if (lResult.HasError)
						{
							lHasError = true;

							break;
						}
					}
				}

				return lHasError;
			}
		}

		public List<string> TraceMessages { get; set; }

		#endregion

		#region Constructor

		public OperationResult()
		{
			this.ChildOperationResults = new List<OperationResult>();			
		}

		public OperationResult(string pInsertingToTableName, string pInsertingObjectName)
		{
			this.ChildOperationResults = new List<OperationResult>();

			this.InsertingToTableName = pInsertingToTableName;
			this.InsertingObjectName  = pInsertingObjectName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adiciona um campo inválido ao resultado da validação
		/// </summary>
		/// <param name="pFieldDBName">Nome completo do campo no banco de dados, com underscore no lugar de ponto: Tabela_Campo</param>
		/// <param name="pValidationError">Erro específico que ocorreu no campo, sem underscore; vai concatenar com o nome do campo e procurar no arquivo de resources. Ex.: Users_BirthDate_InFuture</param>
		/// <param name="pInvalidValue">Valor que não passou na validação</param>
		/// <param name="pMessageParams">Parâmetros pra passar pro string.format da mensagem de erro</param>
		public void InvalidField(string pFieldDBName, string pValidationError, object pInvalidValue)
		{
			if (this.InvalidFields == null) this.InvalidFields = new List<InvalidDataField>();
			
			this.InvalidFields.Add(new InvalidDataField(pFieldDBName, pValidationError, pInvalidValue));
		}

		public void Trace(string pMessage)
		{
			if (this.TraceMessages == null) this.TraceMessages = new List<string>();

			DateTime lNow = DateTime.Now;

			this.TraceMessages.Add(string.Format("{0} {1}: {2}", lNow.Ticks, lNow.ToString("yy/MM/dd hh:mm:ss"), pMessage));
		}

		public string GetFullTrace()
		{
			if (this.TraceMessages == null) return "<No Trace Messages>";

			StringBuilder lReturn = new StringBuilder();

			foreach (string lTraceMessage in this.TraceMessages)
			{
				lReturn.AppendLine(lTraceMessage);
			}

			foreach (OperationResult lChildResult in this.ChildOperationResults)
			{
				lReturn.Append(lChildResult.GetFullTrace());
			}

			if (this.OperationException != null)
			{
				lReturn.AppendLine(this.OperationException.Message);
				lReturn.AppendLine(this.OperationException.StackTrace);
			}

			return lReturn.ToString();
		}

		#endregion
	}
}