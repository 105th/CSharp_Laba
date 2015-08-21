using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq.Expressions;

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
			// Пробуем создать экземпляр типа DataTier.
			try
			{
				_table = new DataTier();
			}
			catch (ArgumentException ex) // Поймали исключение и выводим ошибку.
			{
				Console.WriteLine("Ошибка при создании таблицы:");
				Console.WriteLine(ex.Message);
				Console.WriteLine("Попробуйте заново.");
			}
			finally
			{
				_tableUnsavedChanges = false;
			}
		}

		// TODO Нужно ли здесь делать исключения для каких-либо случаев?
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
		public bool Status
		{
			get
			{
				return _tableUnsavedChanges;
			}
		}

		/// <summary>
		/// Задает или возращает имя таблицы.
		/// </summary>
		/// <value>Имя таблицы</value>
		[XmlIgnoreAttribute]
		public string Name
		{
			get
			{
				return _table.Name;
			}
			set
			{
				try
				{
					//Задаем имя таблице
					_table.Name = value;
				}
				catch (NameTableException ex)
				{
					Console.WriteLine("Вы ввели неверное имя: \"{0}\"!", ex.Message);
					Console.WriteLine("Попробуйте заново.");
				}
			}
		}

		/// <summary>
		/// Возращает количество записей в таблице.
		/// </summary>
		/// <value>Количество записей в таблице</value>
		[XmlIgnoreAttribute]
		public int Length
		{
			get
			{
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
			// Пробуем проверить индекс.
			try
			{
				_table.CheckIndex(index);
			}
			catch (ArgumentException ex) // Если плохой индекс, ловим исключение
			{
				Console.WriteLine(ex.Message);
			}

			// Если всё ок, возвращаем результат проверки.
			return _table.CheckIndex(index);
		}

		/// <summary>
		/// Находит в списке записей запись с указанным номером.
		/// </summary>
		/// <param name="pk">Первичный ключ для поиска</param>
		/// <returns> Возвращает найденную запись с заданным ключом </returns>
		public Record Find(int pk)
		{  
			// Пробуем найти запись с переданным первичным ключом
			try
			{
				_table.Records.FindIndex(element => element.PK == pk);
			}
			catch (Exception ex)
			{
				Console.WriteLine("В процессе поиска записи возникли проблемы.\n" +
					"Попробуйте заново.");
				Console.WriteLine(ex.Message);
			}
         
			return _table[_table.Records.FindIndex(element => element.PK == pk)];
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
		public Record Read(int pk)
		{
			// Пробуем найти и прочитать запись с переданным первичным ключом
			try
			{
				_table.Records.FindIndex(element => element.PK == pk);
			}
			catch (Exception ex)
			{
				Console.WriteLine("В процессе чтения записи возникли проблемы.\n" +
					"Попробуйте заново.");
				Console.WriteLine(ex.Message);
			}

			return _table[_table.Records.FindIndex(element => element.PK == pk)];
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
		/// Метод обновления записи.
		/// </summary>
		/// <param name="pk">Первичный ключ записи</param>
		/// <param name="record">Запись абстрактного класса 
		/// <see cref="Laba.Record"/></param>
		public void Update(int pk, Record record)
		{
			// Пробуем найти запись с первичным ключом и затем обновить её
			try
			{
				int index = _table.Records.FindIndex(element => element.PK == pk);
				_table.Update(index, record);
			}
			catch (Exception ex)
			{
				Console.WriteLine("В процессе обновления записи возникли проблемы.\n" +
					"Попробуйте заново.");
				Console.WriteLine(ex.Message);
			}
			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Метод удаления записи.
		/// </summary>
		/// <param name="id">Номер записи</param>
		public void Delete(int pk)
		{
			// TODO Нормально ли, что я у свойства вызываю метод на удаление?
			try
			{
				int index = _table.Records.FindIndex(element => element.PK == pk);
				_table.Records.RemoveAt(index);
			}
			catch (Exception ex)
			{
				Console.WriteLine("В процессе удаления записи с первичным ключом " +
					"\"{0}\"возникли проблемы.\n" +
					"Попробуйте заново.", pk);
				Console.WriteLine(ex.Message);
			}
			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Удаляет все записи в таблице.
		/// </summary>
		public void DeleteAll()
		{
			// Если нет записей, выводим оповещение и выходим
			if (_table.Length == 0)
			{
				Console.WriteLine("В таблице нет записей для удаления");
				return;
			}

			// Пробуем удалить записи
			try
			{
				for (int i = _table.Length - 1; i >= 0; i--)
					Delete(i);
			}
			catch (Exception ex)
			{
				Console.WriteLine("В процессе удаления записей возникли проблемы.\n" +
					"Попробуйте заново.");
				Console.WriteLine(ex.Message);
			}

			_tableUnsavedChanges = true;
		}

		/// <summary>
		/// Сохраняет таблицу на диск в указанный путь.
		/// </summary>
		/// <param name="path">Путь относительно исполняемого файла</param>
		public void Save(string path = null)
		{
			// Если путь из параметра пуст, тогда задаём туда имя таблицы
			if (string.IsNullOrWhiteSpace(path))
				path = "DataBase/" + _table.Name + ".xml";

			try
			{
				FileDB.Serialize(path, _table);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Что-то пошло не так, попробуйте сохранить ещё раз");
				Console.WriteLine(ex.Message);
			}
			_tableUnsavedChanges = false;
		}

		/// <summary>
		/// Загружает таблицу с диска из указанного пути.
		/// </summary>
		/// <param name="path">Путь относительно исполняемого файла</param>
		public void Load(string path = null)
		{
			// Если путь пуст, загружаем таблицу unnamed.xml из каталога DataBase
			if (string.IsNullOrWhiteSpace(path))
				path = "DataBase/unnamed.xml";

			try
			{
				_table = FileDB.Deserialize(path);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Что-то пошло не так, попробуйте загрузить ещё раз");
				Console.WriteLine(ex.Message);
			}

			// После успешной загрузки обновляет свободный первичный ключ
			_table.UpdateFreePK();
		}

		/// <summary>
		///  Выдает или записывает <see cref="Laba.DataTier"/> по указанному индексу.
		/// </summary>
		/// <param name="index">Первичный ключ</param>
		public Record this [int pk]
		{
			get
			{
				try
				{
					Read(pk);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Индекс неверный!");
					Console.WriteLine(ex.Message);
				}

				return Read(pk);
			}
			set
			{
				try
				{
					Update(pk, value);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Индекс неверный!");
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}

