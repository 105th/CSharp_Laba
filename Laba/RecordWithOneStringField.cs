using System;

namespace Laba
{
	//Класс одной записи с одним полем типа "строка", унаследованный от класса Record
	public class RecordWithOneStringField : Record
	{
		//Строка первого поля
		protected string _FirstField;

		//Конструктор
		public RecordWithOneStringField(int primaryKey = 0, string firstField = null)
		{
			_PrimaryKey = primaryKey;
			_FirstField = firstField;
		}

		//Конструктор без параметров
		public RecordWithOneStringField()
		{
		}

		//Методы задания и выдачи первого поля типа "строка"
		public string FirstField {
			get { 
				return(_FirstField);
			}
			set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Поле не может быть пустым!");

				_FirstField = value; 
			}
		}

		//Переопределение стандартного метода ToString для текстового представления
		public override string ToString()
		{
			return string.Format("PK: {0, 3}, FirstField: {1, -10};", _PrimaryKey, _FirstField);
		}
	}

}

