﻿using System;

namespace Laba
{
	//Класс одной записи с одним полем типа "строка", унаследованный от класса Record
	public class RecordWithOneStringField : Record
	{
		protected string r_FirstField;
		//Строка первого поля

		//Конструктор
		public RecordWithOneStringField(int primaryKey = 0, string firstField = null)
		{
			r_PrimaryKey = primaryKey;
			r_FirstField = firstField;
		}

		//Конструктор без параметров
		public RecordWithOneStringField()
		{
		}


		//Методы задания и выдачи первого поля типа "строка"
		public string FirstField {
			get { 
				return(r_FirstField);
			}
			set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Поле не может быть пустым!");

				r_FirstField = value; 
			}
		}

		//Переопределение стандартного метода ToString для текстового представления
		public override string ToString()
		{
			return string.Format("PK: {0, 3}, FirstField: {1, -10};", r_PrimaryKey, r_FirstField);
		}
	}

}
