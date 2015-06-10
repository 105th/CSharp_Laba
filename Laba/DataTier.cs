﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Laba
{
	//Класс Слоя Данных
	[Serializable]
	public class DataTier
	{
		//Лист записей
		List<Record> db_Records;
		//Количество записей в БД
		int db_Length;
		//Свободный ключ
		int db_FreePK;
		

		//Конструктор базы данных
		public DataTier()
		{
			db_Records = new List<Record>();
			db_Length = 0;
			db_FreePK = 0;
		}

		//Свойство для хранения размера базы данных
		[XmlAttribute("Length")]
		public int Length {
			get {
				return db_Length;
			}
			set {
				db_Length = value;
			}
		}

		//Свойство для приватного поля записи (для поддержки сериализации)
		[XmlArray("Records"), XmlArrayItem("Record")]
		public List<Record> Records {
			get {
				return db_Records;
			}
		}

		//Проверки индекса
		public bool CheckIndex(int index)
		{
			//Проверка индекса на выход за границы
			if (index > db_Length || index < 0)
				throw new ArgumentOutOfRangeException("Недопустимое значение индекса");

			//Проверка наличия записей в базе данных
			if (db_Length < 1)
				throw new ArgumentOutOfRangeException("В базе данных нет записей!");

			//Проверка наличия записи
			if (db_Records[index] == null)
				throw new ArgumentException("Данной записи не существует!");

			return true;
		}

		//Метод создания записи
		public void Create(Record record)
		{
			record.PK = db_FreePK++;

			db_Records.Add(record);
			db_Length++;
		}

		//Метод считывания записи
		public Record Read(int id)
		{
			CheckIndex(id); //Проверяем индекс

			return db_Records[id];
		}

		//Метод обновления записи
		public void Update(int id, Record record)
		{
			CheckIndex(id); //Проверяем индекс

			//Обновление записи
			record.PK = db_Records[id].PK; //Чтобы не обновлять ключ, присваиваем старый
			db_Records[id] = record;
		}

		//Метод удаления записи
		public void Delete(int id)
		{
			CheckIndex(id); //Проверяем индекс

			//Удаление записи
			db_Records.RemoveAt(id);
			db_Length--;
		}

		//Свойство для индексирования
		public Record this[int index] {
			get {
				return Read(index);
			}
			set {
				//Проверка индекса на выход за границы
				if (index > db_Length)
					throw new ArgumentOutOfRangeException("Недопустимое значение индекса");

				Update(index, value);
			}
		}


		//Переопределение метода ToString
		public override string ToString()
		{
			return string.Format("DataMapper: Length = {0}", Length);
		}

		//		//Сериализация
		//      public void Serialize(string path = "DataBase.xml", DataTier)
		//		{
		//			XmlSerializer serializer = new XmlSerializer(typeof(DataTier));
		//			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
		////				for (int i = 0; i < db_Length; i++)
		////					serializer.Serialize(fs, db_Records[i]);
		//				serializer.Serialize(fs, this);
		//
		//			}
		//		}
         
		//TODO Разобраться с приватными записями.
		//Десериализация
		//		public void Deserialize(string filename = "DataBase.xml")
		//		{
		//			// Create an instance of the XmlSerializer specifying type and namespace.
		//			XmlSerializer serializer = new XmlSerializer(typeof(RecordWithTwoStringField));
		//
		//			// A FileStream is needed to read the XML document.
		//			FileStream fs = new FileStream(filename, FileMode.Open);
		//			XmlReader reader = XmlReader.Create(fs);
		//
		//			// Declare an object variable of the type to be deserialized.
		//			RecordWithTwoStringField item;
		//
		//			// Use the Deserialize method to restore the object's state.
		//			while (fs.CanRead) {
		//				item = (RecordWithTwoStringField)serializer.Deserialize(reader);
		//				serializer.
		//				db_Records.Add(item);
		//			}
		//			fs.Close();
		//		}
	}

}