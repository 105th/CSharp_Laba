﻿using System;

namespace Laba
{
	public class LogicTier
	{
		DataTier _DB;
		//экземляр слоя данных

		//Конструктор экземляра Юзер логики
		public LogicTier(DataTier db)
		{
			_DB = db;
		}

		//Метод создания записи
		public void Create(Record record)
		{
			_DB.Create(record);
		}

		//Метод считывания записи
		public Record Read(int id)
		{
			return _DB[id];  
		}

		//Метод считывания всех записей
		public void ReadAll()
		{
			if (_DB.Length == 0)
				Console.WriteLine("База данных пуста!");

			for (int i = 0; i < _DB.Length; i++) {
				Console.WriteLine(string.Format("{0}: {1}", i, _DB[i]));
			}
		}

		//Метод обновления записи
		public void Update(int id, Record record)
		{
			_DB.Update(id, record);
		}

		//Метод удаления записи
		public void Delete(int id)
		{
			_DB.Delete(id);
		}

		//Метод удаления всех записей
		public void DeleteAll()
		{
			for (int i = _DB.Length - 1; i >= 0; i--)
				_DB.Delete(i);
		}
   
		//Сохранение базы данных на диск
		//		public void Save()
		//		{
		//			_DB.Serialize();
		//		}
   
		//Загрузка базы данных с диска
		//		public void Load()
		//		{
		//			_DB.Deserialize();
		//		}
	}
}
