using System;

namespace Laba
{
	//Класс одной записи с двумя полями типа "строка", унаследованный от класса RecordWithOneStringField
	public class RecordWithTwoIntField : Record
	{
		// TODO Сделать имя для первого столбца и для второго
		// Первое поле типа int
		int _firstField;
		// Второе поле типа int
		int _secondField;

		// конструктор.
		public RecordWithTwoIntField(int primaryKey = 0, 
		                                 int firstField = 0, 
		                                 int secondField = 0)
		{
			_PrimaryKey = primaryKey;
			_firstField = firstField;
			_secondField = secondField;
		}

		// Пустой конструктор.
		public RecordWithTwoIntField()
		{
		}

		/// <summary>
		/// Проверяет налачие в указанной колонке искомого номера.
		/// </summary>
		/// <returns><c>true</c> Если в колонке под номером <c>number_of_row</c>
		/// содержание совпадает с номером <c>data</c>; если не совпадает,
		/// возвращает <c>false</c>.</returns>
		/// <param name="number_of_row">Номер поля, в котором произвести сравнение.</param>
		/// <param name="data">С чем сравнивать.</param>
		public override bool HasData(int data, int number_of_row)
		{
			if (number_of_row == 1)
			{
				if (_firstField == data)
					return true;
			} else if (number_of_row == 2)
			{
				if (_secondField == data)
					return true;
			}

			return false;
		}

		// Методы задания и выдачи первого поля типа "целое".
		public int FirstField {
			get { 
				return(_firstField);
			}
			set {
				if (value < 0)
					throw new ArgumentNullException("Поле не может быть отрицательным!");

				_firstField = value; 
			}
		}

		// Методы задания и выдачи второго поля типа "целое".
		public int SecondField {
			get { 
				return(_secondField);
			}
			set {
				if (value < 0)
					throw new ArgumentNullException("Поле не может быть отрицательным!");

				_secondField = value; 
			}
		}

		//Переопределение стандартного метода ToString для текстового представления
		public override string ToString()
		{
			return string.Format("PK: {0, 3}, FirstField: {1, -10}, SecondField: {2, -10};", _PrimaryKey, _firstField, _secondField);
		}
	}
}

