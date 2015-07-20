using System;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	[Serializable]
	public class DataField
	{
		#region Properties

		/// <summary>
		/// Nome do Campo
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Decimal = 0, String = 1, Date = 2, Blob = 3, Null = 4, LongText = 5
		/// </summary>
		public byte DBType { get; set; }

		#endregion

		#region Constructor

		public DataField() { }

		/// <summary>
		/// Construtor passando as propriedades
		/// </summary>
		/// <param name="pName">Nome do Campo</param>
		/// <param name="pDBType">Decimal = 0, String = 1, Date = 2, Blob = 3, Null = 4, LongText = 5</param>
		public DataField(string pName, byte pDBType) 
		{
			this.Name = pName;
			this.DBType = pDBType;
		}

		#endregion
	}
}
