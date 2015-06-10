using System;
using System.Xml.Serialization;

namespace Laba
{
	//Класс одной записи с двумя полями типа "строка", унаследованный от класса RecordWithOneStringField
	public class RecordWithTwoStringField : RecordWithOneStringField
	{
		string r_SecondField;
		//строка второго поля

		//конструктор
		public RecordWithTwoStringField(int primaryKey = 0, string firstField = null, string secondField = null)
		{
			r_PrimaryKey = primaryKey;
			r_FirstField = firstField;
			r_SecondField = secondField;
		}

		//конструктор
		public RecordWithTwoStringField()
		{
		}

		//Методы задания и выдачи второго поля типа "строка"
		public string SecondField {
			get { 
				return(r_SecondField);
			}
			set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Поле не может быть пустым!");

				r_SecondField = value; 
			}
		}

		//Переопределение стандартного метода ToString для текстового представления
		public override string ToString()
		{
			return string.Format("PK: {0, 3}, FirstField: {1, -10}, SecondField: {2, -10};", r_PrimaryKey, r_FirstField, r_SecondField);
		}
	}
}

