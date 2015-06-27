using System;
using System.Collections.Generic;

namespace Laba
{
	public class DataBase
	{
		// Лист из экземляров типа LogicTier
		// (из оболочек бизнес-логики для таблиц)
		List<LogicTier> _LT;
		// Количество таблиц в БД
		int _DataBaseLength;
		// Имя БД
		string _DataBaseName;

		// Конструктор
		public DataBase()
		{
			_LT = new List<LogicTier>();
			_DataBaseLength = 0;
			_DataBaseName = "unnamed";
		}

		// Конструктор с одним параметром типа LogicTier.
		public DataBase(LogicTier logicTier)
		{
			_LT = new List<LogicTier>();
			_DataBaseLength = 0;
			_DataBaseName = "unnamed";

			_LT.Add(logicTier);
		}

		// Свойство для количества таблиц в БД.
		public int Length {
			get {
				return _DataBaseLength;
			}
			set {
				_DataBaseLength = value;
			}
		}

		// Свойство для имени базы данных.
		public string Name {
			get {
				return _DataBaseName;
			}
			set {
				_DataBaseName = value;
			}
		}

		// Метод добавление таблицы в БД.
		public void AddTable(LogicTier logicTier)
		{
			_LT.Add(logicTier);
			_DataBaseLength++;
		}

		// Метод удаления таблицы из БД (по умолчанию удаляет последнюю таблицу).
		public void DeleteTable(int index)
		{
			_LT.RemoveAt(index);
			_DataBaseLength--;
		}

		// Метод удаления всех таблиц из БД
		public void DeleteAllTables()
		{
			_LT.Clear();
			_DataBaseLength = 0;
		}

		// Cвойство для поддержки индексирования
		public LogicTier this[int index] {
			get {
				return _LT[index];
			}
			set {
				//Проверка индекса на выход за границы
				if (index > _DataBaseLength)
					throw new ArgumentOutOfRangeException("Недопустимое значение" +
					" индекса");
            
				_LT[index] = value;
			}
		}
	}
}

