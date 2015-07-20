using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Data;

namespace APB.Mercury.DataObjects.BanPeticoes
{
	[XmlRoot("DataFieldCollection")]
	public class DataFieldCollection : Dictionary<DataField, object>, IXmlSerializable
	{
		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader pReader)
		{
			XmlSerializer lKeySerializer = new XmlSerializer(typeof(DataField));

			XmlSerializer lValueSerializer = new XmlSerializer(typeof(object));

			bool lWasEmpty = pReader.IsEmptyElement;

			pReader.Read();

			if (lWasEmpty)
				return;

			while (pReader.NodeType != System.Xml.XmlNodeType.EndElement)
			{
				pReader.ReadStartElement("item");

				pReader.ReadStartElement("key");

				DataField key = (DataField)lKeySerializer.Deserialize(pReader);

				pReader.ReadEndElement();

				pReader.ReadStartElement("value");

				object value = (object)lValueSerializer.Deserialize(pReader);

				pReader.ReadEndElement();

				this.Add(key, value);

				pReader.ReadEndElement();

				pReader.MoveToContent();

			}

			pReader.ReadEndElement();
		}

		public void WriteXml(System.Xml.XmlWriter pWriter)
		{
			XmlSerializer lKeySerializer = new XmlSerializer(typeof(DataField));

			XmlSerializer lValueSerializer = new XmlSerializer(typeof(object));

			foreach (DataField lKey in this.Keys)
			{
				pWriter.WriteStartElement("item");

				pWriter.WriteStartElement("key");

				lKeySerializer.Serialize(pWriter, lKey);

				pWriter.WriteEndElement();

				pWriter.WriteStartElement("value");

				object value = this[lKey];

				lValueSerializer.Serialize(pWriter, value);

				pWriter.WriteEndElement();

				pWriter.WriteEndElement();

			}

		}

		public DataSet ToDataSet()
		{
			DataSet lReturn = new DataSet();

			DataTable lTable = new DataTable();

			DataRow lRow;

			lTable.Columns.Add("Key", typeof(DataField));
			lTable.Columns.Add("Value", typeof(object));

			foreach (DataField lKey in this.Keys)
			{
				lRow = lTable.NewRow();

				lRow["Key"] = lKey;
				lRow["Value"] = this[lKey];

				lTable.Rows.Add(lRow);
			}

			lReturn.Tables.Add(lTable);

			return lReturn;
		}

		#endregion

	}
}
