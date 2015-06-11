using System;

namespace Laba
{
	public class LogicTier
	{
		// Экземляр слоя данных
		DataTier _table;
		//Есть ли несохраненные изменения в таблице
		bool _tableUnsavedChanges;

		//Конструктор экземляра Юзер логики
		public LogicTier(DataTier db)
		{
			_table = db;
			_tableUnsavedChanges = false;
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
			return _table[id];  
		}

		//Метод считывания всех записей
		public void ReadAll()
		{
			if (_table.Length == 0)
				Console.WriteLine("База данных пуста!");

			for (int i = 0; i < _table.Length; i++) {
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
		}
   
		//Сохранение таблицы базы данных на диск
		public void Save(string path = "DataBase.xml")
		{
			FileDB.Serialize(path, _table);
			_tableUnsavedChanges = false;
		}
   
		//Загрузка таблицы базы данных с диска
		public void Load(string path = "DataBase.xml")
		{
			_table = FileDB.Deserialize(path);
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

