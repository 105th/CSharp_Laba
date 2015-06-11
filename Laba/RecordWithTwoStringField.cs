using System;
using System.Xml.Serialization;

namespace Laba
{
	//Класс одной записи с двумя полями типа "строка", унаследованный от класса RecordWithOneStringField
	public class RecordWithTwoStringField : RecordWithOneStringField
	{
		string _SecondField;
		//строка второго поля

		//конструктор
		public RecordWithTwoStringField(int primaryKey = 0, string firstField = null, string secondField = null)
		{
			_PrimaryKey = primaryKey;
			_FirstField = firstField;
			_SecondField = secondField;
		}

		//конструктор
		public RecordWithTwoStringField()
		{
		}

		//Методы задания и выдачи второго поля типа "строка"
		public string SecondField {
			get { 
				return(_SecondField);
			}
			set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Поле не может быть пустым!");

				_SecondField = value; 
			}
		}

		//Переопределение стандартного метода ToString для текстового представления
		public override string ToString()
		{
			return string.Format("PK: {0, 3}, FirstField: {1, -10}, SecondField: {2, -10};", _PrimaryKey, _FirstField, _SecondField);
		}
	}
}

