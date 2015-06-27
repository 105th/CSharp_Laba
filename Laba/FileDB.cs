using System;
using System.IO;
using System.Xml.Serialization;

namespace Laba
{
	public class FileDB
	{
		//Сериализация
		public static void Serialize(string path, DataTier data)
		{
			//Проверяем таблицу баз данных на наличие чего-либо
			if (data == null)
				throw new ArgumentNullException("База данных пуста!");

			//Создаем объект типа сериализатор
			XmlSerializer serializer = new XmlSerializer(typeof(DataTier));
			//Открываем файл на чтение (если нет, то создаем)
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
			{
				serializer.Serialize(fs, data);
			}
		}

		//Десериализация
		public static DataTier Deserialize(string path)
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

			//Возвращаем результат
			return XmlData;
		}
	}
}

