﻿using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Laba
{
	public class FileDB
	{
		//Сериализация
		public static void Serialize(string path = "DataBase.xml", DataTier data = null)
		{
			//Проверяем базу данных на наличие чего-либо
			if (data == null)
				throw new ArgumentNullException("База данных пуста!");

			//Создаем объект типа сериализатор
			XmlSerializer serializer = new XmlSerializer(typeof(DataTier));
			//Открываем файл на чтение (если нет, то создаем)
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
				serializer.Serialize(fs, data);
			}

			Console.WriteLine("База данных сохранена успешно!");
		}

		//Десериализация
		public static DataTier Deserialize(string path = "DataBase.xml")
		{
			//Создаем объект типа сериализатор
			XmlSerializer deserializer = new XmlSerializer(typeof(DataTier));
			//Открывает файл для чтения
			TextReader reader = new StreamReader(path);
			//Во временную переменню записываем прочитанную (десериализуем) базу данных
			object obj = deserializer.Deserialize(reader);
			//Переносим ее в переменную типа DataTier
			DataTier XmlData = (DataTier)obj;
			//Закрываем файл
			reader.Close();

			//Выводим сообщение и возвращаем результат
			Console.WriteLine("База данных загружена успешно!");
			return XmlData;
		}
	}
}
