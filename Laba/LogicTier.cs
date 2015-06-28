using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Laba
{
	/// <summary>
	/// Слой данных.
	/// </summary>
	public class LogicTier
	{
		/// <summary> 
		/// Экземляр слоя данных.
		/// </summary>
		DataTier _table;
		/// <summary>
		/// Есть ли несохраненные изменения в таблице.
		/// </summary>
		bool _tableUnsavedChanges;

		/// <summary>
		/// Конструктор экземляра Юзер логики
		/// </summary>
		public LogicTier()
		{
			_table = new DataTier();
			_tableUnsavedChanges = false;
		}

		/// <summary>
		/// Конструктор экземляра Юзер логики (<see cref="Laba.LogicTier"/>) class.
		/// </summary>
		/// <param name="dataTable">Таблица слоя данных 
		/// (<see cref="Laba.DataTier"/>)</param>
		public LogicTier(DataTier dataTable)
		{
			_table = dataTable;
			_tableUnsavedChanges = false;
		}

		/// <summary>
		/// Возвращает статус несохранённый изменений.
		/// </summary>
		/// <value><c>true</c> Если есть несохранённые изменения; если нет, то
		///  <c>false</c>.</value>
		[XmlIgnoreAttribute]
		public bool Status {
			get {
				return _tableUnsavedChanges;
			}
		}

		/// <summary>
		/// Задает или возращает имя таблицы.
		/// </summary>
		/// <value>Имя таблицы</value>
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

		/// <summary>
		/// Возращает количество записей в таблице.
		/// </summary>
		/// <value>Количество записей в таблице</value>
		[XmlIgnoreAttribute]
		public int Length {
			get {
				return _table.Length;
			}
		}

		/// <summary>
		/// Проверки индекса. 
		/// Вызывает внутренний метод у <see cref="Laba.DataTier"./>
		/// </summary>
		/// <returns><c>true</c>, если индес верный, <c>false</c> и
		///  бросает <c>throw</c> когда индекс неверный.</returns>
		/// <param name="index">Индекс таблицы</param>
		public bool CheckIndex(int index)
		{
			return _table.CheckIndex(index);
		}

		/// <summary>
		/// Метод создания записи.
		/// </summary>
		/// <param name="record">Запись абстрактного класса 
		/// <see cref="Laba.Record"/></param>
		public void Create(Record record)
		{
			_table.Create(record);
			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Метод считывания записи.
		/// </summary>
		/// <param name="id">Номер записи.</param>
		public Record Read(int id)
		{
			CheckIndex(id);

			return _table[id];
		}

		/// <summary>
		/// Читает все записи.
		/// </summary>
		/// <returns>Список всех записей</returns>
		public List<Record> ReadAll()
		{
			if (_table.Length == 0)
				Console.WriteLine("Таблица пуста!");
         
			return _table.Records;
		}

		/// <summary>
		/// Находит в списке записей запись с указанным номером.
		/// </summary>
		/// <param name="PK">Первичный ключ для поиска</param>
		/// <returns> Возвращает найденную запись с заданным ключом </returns>
		public Record Find(int PK)
		{  
			for (int i = 0; i < _table.Length; i++)
			{
				// Вызываем метод проверки наличия номера.
				if (_table[i].HasData(PK))
				{
					// Если запись содержит номер, возвращаем её
					return (_table[i]);
				}
			}
			return null;
		}

		/// <summary>
		/// Метод обновления записи.
		/// </summary>
		/// <param name="id">Номер записи.</param>
		/// <param name="record">Запись абстрактного класса 
		/// <see cref="Laba.Record"/></param>
		public void Update(int id, Record record)
		{
			_table.Update(id, record);
			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Метод удаления записи.
		/// </summary>
		/// <param name="id">Номер записи</param>
		public void Delete(int id)
		{
			_table.Delete(id);
			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Удаляет все записи в таблице.
		/// </summary>
		public void DeleteAll()
		{
			for (int i = _table.Length - 1; i >= 0; i--)
				Delete(i);

			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Сохраняет таблицу на диск в указанный путь.
		/// </summary>
		/// <param name="path">Путь относительно исполняемого файла</param>
		public void Save(string path = null)
		{
			if (string.IsNullOrWhiteSpace(path))
				path = "DataBase/" + _table.Name + ".xml";

			FileDB.Serialize(path, _table);
			_tableUnsavedChanges = false;
		}

		/// <summary>
		/// Загружает таблицу с диска из указанного пути.
		/// </summary>
		/// <param name="path">Путь относительно исполняемого файла</param>
		public void Load(string path = null)
		{
			if (string.IsNullOrWhiteSpace(path))
				path = "DataBase/unnamed.xml";

			_table = FileDB.Deserialize(path);
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
				if (index > _table.Length)
					throw new ArgumentOutOfRangeException("Недопустимое значение" +
					" индекса");

				Update(index, value);
			}
		}


		/// <summary>
		/// Решение поставленной задачи
		/// 19. Класс – уровни доступа.
		/// Даны пользователи, роли и пересечение пользователей и ролей, 
		/// вывести всех пользователей для указанной роли 
		/// и наоборот, все роли для указанного пользователя.
		/// Вызывает абстрактный одноимённый метод в классе <see cref="Laba.Solver"/>
		/// </summary>
		public void Solve()
		{
			// TODO
			// Сделать статический класс, который будет тут вызываться и ему 
			// передавать в качестве параметров три таблицы (пользователи, роли и
			// пересечении их)
		}
	}
}

