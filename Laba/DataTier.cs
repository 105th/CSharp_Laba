﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laba
{
	//Класс Слоя Данных
	[Serializable]
	public class DataTier
	{
		//Имя для таблицы
		string _tableName;
		//Лист записей
		List<Record> _tableRecords;
		//Количество записей в таблице БД
		int _tableLength;
		//Свободный ключ
		int _tableFreePK;
		// Тип записей, хранящихся в таблице
		string _typeOfRecords;

		

		//Конструктор таблицы базы данных
		public DataTier()
		{
			_tableRecords = new List<Record>();
			_tableLength = 0;
			_tableFreePK = 0;
			_tableName = "unnamed";
		}

		//Конструктор таблицы базы данных с именем
		public DataTier(string name = "unnamed")
		{
			_tableRecords = new List<Record>();
			_tableLength = 0;
			_tableFreePK = 0;
			_tableName = name;
		}

		//Свойство для хранения имени таблицы базы данных.
		[XmlAttribute("Name")]
		public string Name {
			get {
				return _tableName;
			}
			set {
				_tableName = value;
			}
		}

		//Свойство для хранения размера таблицы базы данных.
		[XmlAttribute("Length")]
		public int Length {
			get {
				return _tableLength;
			}
			set {
				_tableLength = value;
			}
		}

		// Свойство для статуса изменений в данных
		[XmlAttribute("TypeOfRecords")]
		public string TypeOfRecords {
			get {
				return _typeOfRecords;
			}
			set {
				_typeOfRecords = value;
			}
		}

		//Свойство для приватного поля записи (для поддержки сериализации)
		[XmlArray("Records"), XmlArrayItem("Record")]
		public List<Record> Records {
			get {
				return _tableRecords;
			}
		}

		//Проверки индекса
		public bool CheckIndex(int index)
		{
			//Проверка индекса на выход за границы
			if (index > _tableLength || index < 0)
				throw new ArgumentOutOfRangeException("Недопустимое значение " +
				"индекса");

			//Проверка наличия записей в таблице базе данных
			if (_tableLength < 1)
				throw new ArgumentOutOfRangeException("В базе данных нет" +
				" записей!");

			//Проверка наличия записи
			if (_tableRecords[index] == null)
				throw new ArgumentException("Данной записи не существует!");

			return true;
		}

		//Метод создания записи
		public void Create(Record record)
		{
			record.PK = _tableFreePK++;

			_tableRecords.Add(record);
			_tableLength++;
			_typeOfRecords = record.GetType().ToString();
		}

		//Метод считывания записи
		public Record Read(int id)
		{
			CheckIndex(id); //Проверяем индекс

			return _tableRecords[id];
		}

		//Метод обновления записи
		public void Update(int id, Record record)
		{
			CheckIndex(id); //Проверяем индекс

			//Обновление записи
			// Чтобы не обновлять ключ, присваиваем старый.
			record.PK = _tableRecords[id].PK;
			_tableRecords[id] = record;
		}

		//Метод удаления записи
		public void Delete(int id)
		{
			CheckIndex(id); //Проверяем индекс

			//Удаление записи
			_tableRecords.RemoveAt(id);
			_tableLength--;
		}

		//Индексатор целого типа
		public Record this[int index] {
			get {
				return Read(index);
			}
			set {
				//Проверка индекса на выход за границы
				if (index > _tableLength)
					throw new ArgumentOutOfRangeException("Недопустимое значение" +
					" индекса");

				Update(index, value);
			}
		}

		//TODO Индексатор для строки
		//      public int this[string day]
		//      {
		//         get
		//         {
		//            return (GetDay(day));
		//         }
		//      }


		//Переопределение метода ToString
		public override string ToString()
		{
			return string.Format("DataTier: Length = {0}", Length);
		}
	}

}