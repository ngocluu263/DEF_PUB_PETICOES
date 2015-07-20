using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	[Serializable]
	public class SerializableException
	{
		#region Properties

		public string Message { get; set; }

		public string StackTrace { get; set; }

		public string Source { get; set; }

		public SerializableException InnerException { get; set; }

		#endregion

		#region Constructors

		public SerializableException()
		{
		}

		public SerializableException(Exception pException)
		{
			this.Message = pException.Message;
			this.StackTrace = pException.StackTrace;
			this.Source = pException.Source;

			if(pException.InnerException != null)
				this.InnerException = new SerializableException(pException.InnerException);
		}

		#endregion
	}
}
