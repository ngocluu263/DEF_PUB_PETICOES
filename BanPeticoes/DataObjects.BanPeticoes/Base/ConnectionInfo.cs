using System;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	[Serializable]
	public class ConnectionInfo
	{
		#region Properties

		public string ConnectionString { get; set; }

		public int DataBaseType { get; set; }

		public int BridgeMode { get; set; }

		#endregion

		#region Constructores

		public ConnectionInfo() { }

		public ConnectionInfo(string pConnectionString, int pDataBaseType, int pBridgeMode)
		{
			this.ConnectionString = pConnectionString;
			this.DataBaseType = pDataBaseType;
			this.BridgeMode = pBridgeMode;
		}

		#endregion
	}
}
