using System.Xml.Serialization;

namespace Laba
{
	//Абстрактный класс одной записи
	[XmlInclude(typeof(RecordWithTwoIntField))]
	[XmlInclude(typeof(RecordWithOneStringField))]
	public abstract class Record
	{
		//Первичный ключ
		protected int _PrimaryKey;

		//Защищенный конструктор
		protected Record(int primaryKey = 0)
		{
			_PrimaryKey = primaryKey;
		}

		/// <summary>
		/// Определяет, есть ли номер <c>data</c> в указанной записи
		/// </summary>
		/// <returns>Если номер совпадает с первичным ключом, то
		/// возвращает <c>true</c>, иначе <c>false</c>.</returns>
		/// <param name="number_of_row">Номер поля, в котором произвести сравнение.</param>
		/// <param name="data">С чем сравнивать.</param>
		public virtual bool HasData(int data, int number_of_row = 0)
		{
			if (PK == data)
				return true;
			return false;
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

