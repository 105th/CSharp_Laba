using System;
using System.Xml.Serialization;

namespace Laba
{
	public class LogicTier
	{
		// Экземляр слоя данных.
		DataTier _table;
		// Есть ли несохраненные изменения в таблице.
		bool _tableUnsavedChanges;

		//Конструктор экземляра Юзер логики
		public LogicTier()
		{
			_table = new DataTier();
			_tableUnsavedChanges = false;
//			_typeOfRecords = "Laba.Record";
		}

		//Конструктор экземляра Юзер логики
		public LogicTier(DataTier dataTable)
		{
			_table = dataTable;
			_tableUnsavedChanges = false;
//			_typeOfRecords = "Laba.Record";
		}

		// Свойство для статуса изменений в данных
		[XmlIgnoreAttribute]
		public bool Status {
			get {
				return _tableUnsavedChanges;
			}
		}

		// Свойство для имени таблицы
		[XmlIgnoreAttribute]
		public string Name {
			get {
				return _table.Name;
			}
			set {
				// Проверяем имя на пустоту.
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Имя не может быть пустым");

				//Задаем имя таблице
				_table.Name = value;
			}
		}

		public string TypeOfRecords {
			get {
				return _table.TypeOfRecords;  
			}
		}

		// Метод проверки индекса.
		public bool CheckIndex(int index)
		{
			return _table.CheckIndex(index);
		}

		//Метод создания записи
		public void Create(Record record)
		{
			_table.Create(record);
			_tableUnsavedChanges = true;
		}

		//Метод считывания записи
		public Record Read(int id)
		{
			if (_table.Length == 0)
				Console.WriteLine("База данных пуста!");

			return _table[id];
		}

		//Метод считывания всех записей
		public void ReadAll()
		{
			if (_table.Length == 0)
				Console.WriteLine("База данных пуста!");

			for (int i = 0; i < _table.Length; i++)
			{
				Console.WriteLine(string.Format("{0}: {1}", i, _table[i]));
			}
		}

		//Метод обновления записи
		public void Update(int id, Record record)
		{
			_table.Update(id, record);
			_tableUnsavedChanges = true;
		}

		//Метод удаления записи
		public void Delete(int id)
		{
			_table.Delete(id);
			_tableUnsavedChanges = true;
		}

		//Метод удаления всех записей
		public void DeleteAll()
		{
			for (int i = _table.Length - 1; i >= 0; i--)
				Delete(i);

			_tableUnsavedChanges = true;
		}
   
		//Сохранение таблицы базы данных на диск
		public void Save(string path = null)
		{
			if (string.IsNullOrWhiteSpace(path))
				path = "DataBase/" + _table.Name + ".xml";

			FileDB.Serialize(path, _table);
			_tableUnsavedChanges = false;

			Console.WriteLine("Таблица сохранена успешно!");
		}
   
		//Загрузка таблицы базы данных с диска
		public void Load(string path = null)
		{
			if (string.IsNullOrWhiteSpace(path))
				path = "DataBase/unnamed.xml";

			_table = FileDB.Deserialize(path);

			Console.WriteLine("Таблица загружена успешно!");
		}

		// Решение поставленной задачи
		//19. Класс – уровни доступа.
		//Даны пользователи, роли и пересечение пользователей и ролей, вывести всех пользователей для указанной роли и наоборот, все роли для указанного пользователя;
		public void Solve()
		{
			// TODO
			// Сделать статический класс, который будет тут вызываться и ему 
			// передавать в качестве параметров три таблицы (пользователи, роли и
			// пересечении их)
		}
	}
}

