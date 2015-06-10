using System.Xml.Serialization;

namespace Laba
{
	//Абстрактный класс одной записи
	[XmlInclude(typeof(RecordWithTwoStringField))]
	[XmlInclude(typeof(RecordWithOneStringField))]
	public abstract class Record
	{
		protected int r_PrimaryKey;
		//Первичный ключ

		//Защищенный конструктор
		protected Record(int primaryKey = 0)
		{
			r_PrimaryKey = primaryKey;
		}

		//Виртуальные методы задания и выдачи Первичного Ключа записи
		//		[XmlAttribute("PK")]
		public virtual int PK {
			get {
				return(r_PrimaryKey);
			}
			set { 
				r_PrimaryKey = value;
			}
		}

	}
}

