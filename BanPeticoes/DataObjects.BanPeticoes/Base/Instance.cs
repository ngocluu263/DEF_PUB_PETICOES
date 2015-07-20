using System;

using APB.Framework.DataBase;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	/// <summary>
	/// Summary description for Instance
	/// </summary>
	public class Instance
	{
		public static APB.Framework.DataBase.DataBase CreateDatabase(ConnectionInfo pInfo)
		{
			//TODO: Fazer direito, a connstring tem que ser criptografada
			return new APB.Framework.DataBase.DataBase((DataBaseType)pInfo.DataBaseType, pInfo.ConnectionString);
		}

	}
}