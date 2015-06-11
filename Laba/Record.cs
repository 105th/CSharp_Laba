using System.Xml.Serialization;

namespace Laba
{
	//Абстрактный класс одной записи
	[XmlInclude(typeof(RecordWithTwoStringField))]
	[XmlInclude(typeof(RecordWithOneStringField))]
	public abstract class Record
	{
		protected int _PrimaryKey;
		//Первичный ключ

		//Защищенный конструктор
		protected Record(int primaryKey = 0)
		{
			_PrimaryKey = primaryKey;
		}

		//Виртуальные методы задания и выдачи Первичного Ключа записи
		//		[XmlAttribute("PK")]
		public virtual int PK {
			get {
				return(_PrimaryKey);
			}
			set { 
				_PrimaryKey = value;
			}
		}

	}
}

