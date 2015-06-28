using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laba
{
	/// <summary>
	/// Класс Слоя Данных..
	/// </summary>
	[Serializable]
	public class DataTier
	{
		/// <summary>
		/// Имя для таблицы.
		/// </summary>
		string _tableName;
		/// <summary>
		/// Лист записей.
		/// </summary>
		List<Record> _tableRecords;
		/// <summary>
		/// Количество записей в таблице БД.
		/// </summary>
		int _tableLength;
		/// <summary>
		/// Свободный ключ.
		/// </summary>
		int _tableFreePK;


		/// <summary>
		/// Конструктор таблицы базы данных <see cref="Laba.DataTier"/>.
		/// </summary>
		public DataTier()
		{
			_tableRecords = new List<Record>();
			_tableLength = 0;
			_tableFreePK = 0;
			_tableName = "unnamed";
		}

		/// <summary>
		/// Конструктор таблицы базы данных с именем <see cref="Laba.DataTier"/>.
		/// </summary>
		/// <param name="name">Имя таблицы.</param>
		public DataTier(string name = "unnamed")
		{
			_tableRecords = new List<Record>();
			_tableLength = 0;
			_tableFreePK = 0;
			_tableName = name;
		}

		/// <summary>
		/// Конструктор таблицы базы данных из списка записей
		///  <see cref="Laba.DataTier"/>.
		/// </summary>
		/// <param name="records">Список записей.</param>
		public DataTier(List<Record> records, string name = "unnamed")
		{
			_tableRecords = records;
			_tableLength = records.Count;
			// Здесь требуется найти первый свободный первичный ключ, т.е. (самый 
			// максимальный + 1).
			int bestFreePK = 0;

			// Выполняем поиск максимального ключа с помощью лямбда выражения.
			records.ForEach(rec => {
				if (rec.PK > bestFreePK)
					bestFreePK = rec.PK;
			});

			// Присваиваем результат.
			_tableFreePK = bestFreePK + 1;
			_tableName = name;
		}

		/// <summary>
		/// Свойство для хранения имени таблицы базы данных.
		/// </summary>
		/// 
		/// <value>Возвращает\задает имя таблицы</value>
		[XmlAttribute("Name")]
		public string Name {
			get {
				return _tableName;
			}
			set {
				_tableName = value;
			}
		}

		/// <summary>
		/// Свойство для хранения количества записей таблицы базы данных.
		/// </summary>
		/// <value>Возвращает\задает количество записей в таблице</value>
		[XmlAttribute("Length")]
		public int Length {
			get {
				return _tableLength;
			}
			set {
				_tableLength = value;
			}
		}

		/// <summary>
		/// Свойство для хранения записей таблицы.
		/// </summary>
		/// <value>Список записей в таблице.</value>
		[XmlArray("Records"), XmlArrayItem("Record")]
		public List<Record> Records {
			get {
				return _tableRecords;
			}
		}

		/// <summary>
		/// Проверки индекса.
		/// </summary>
		/// <returns><c>true</c>, если индес верный, <c>false</c> и бросает 
		/// <c>throw</c> когда индекс неверный.</returns>
		/// <param name="index">Индекс таблицы</param>
		/// TODO
		/// Нелогично, что при правильной обработке возращает true, 
		/// а иначе выбрасывает исключение.
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

		/// <summary>
		/// Метод создания записи.
		/// </summary>
		/// <param name="record">Запись абстрактного класса 
		/// <see cref="Laba.Record"/></param>
		public void Create(Record record)
		{
			record.PK = _tableFreePK++;

			_tableRecords.Add(record);
			_tableLength++;
		}

		/// <summary>
		/// Метод считывания записи.
		/// </summary>
		/// <param name="id">Номер записи.</param>
		public Record Read(int id)
		{
			CheckIndex(id); //Проверяем индекс

			return _tableRecords[id];
		}

		/// <summary>
		/// Метод обновления записи.
		/// </summary>
		/// <param name="id">Номер записи.</param>
		/// <param name="record">Запись абстрактного класса 
		/// <see cref="Laba.Record"/></param>
		public void Update(int id, Record record)
		{
			CheckIndex(id); //Проверяем индекс

			//Обновление записи
			// Чтобы не обновлять ключ, присваиваем старый.
			record.PK = _tableRecords[id].PK;
			_tableRecords[id] = record;
		}

		/// <summary>
		/// Метод удаления записи.
		/// </summary>
		/// <param name="id">Номер записи</param>
		public void Delete(int id)
		{
			CheckIndex(id); //Проверяем индекс

			//Удаление записи
			_tableRecords.RemoveAt(id);
			_tableLength--;
		}

		/// <summary>
		///  Выдает или записивает <see cref="Laba.DataTier"/> по указанному индексу.
		/// </summary>
		/// <param name="index">Индекс записи</param>
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


		/// <summary>
		/// Переопределение стандартного метода ToString()
		/// </summary>
		/// <returns>Возвращает <see cref="System.String"/> которая представляет 
		/// текстовое представление указанного <see cref="Laba.DataTier"/>.</returns>
		public override string ToString()
		{
			return string.Format("DataTier: Length = {0}", Length);
		}
	}

}