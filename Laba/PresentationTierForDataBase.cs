using System;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography;
using System.Text;

namespace Laba
{
	public class PresentationTierForDataBase
	{
		// Лист из экземляров типа LogicTier или База Данных
		DataBase _DataBase;
		// Выбрана ли БД для работы
		bool _choiseTable;
		// Индекс выбранной таблицы для работы
		int _indexChoiseTable;

		// Конструктор
		public PresentationTierForDataBase()
		{
			_DataBase = new DataBase();
			_choiseTable = false;
			_indexChoiseTable = 0;
		}

		// Метод для меню
		public void menu()
		{
//         if (_choiseTable == false)
            
		}

		// Метод работы с базой данных.
		public void WorkWithDB()
		{
			// Случай, когда у нас еще нет таблиц в базе данных.
			if (_DataBase.Length == 0) {
				Console.WriteLine("В базе данных еще нет таблиц для работы.\n" +
				"Создать таблицу? [Д]\\[н] (по умолчанию будет создана новая " +
				"таблица)\n");
				
				// Создаем переменную для хранения нажатой клавиши.
				ConsoleKeyInfo Choise = new ConsoleKeyInfo();
				Choise = Console.ReadKey();

				// Если нажали н, выходим из метода.
				if (Choise.KeyChar == 'н' || Choise.KeyChar == 'Н')
					return;
            // Если нажали д, создаем новую таблицу.
				else if (Choise.KeyChar == 'д' || Choise.KeyChar == 'Д') {
					// Создаем новую таблицу.
					DataTier Table = new DataTier();
					LogicTier LogicTable = new LogicTier(Table);

					// Добавляем таблицу в нашу БД.
					_DataBase.AddTable(LogicTable);
					_choiseTable = false;
					_indexChoiseTable = 0;
					return;
				}
            // Если нажали что-либо кроме Д или Н.
				else {
					Console.WriteLine("Вы нажали что-то другое");
					return;
				}
			}

			//Вызываем метод работы с одной таблицей
			WorkWithTable();
		}

		// Метод работы с таблицей
		public void WorkWithTable()
		{
			// Если не выбрана таблица базы данных для работы, тогда выводим 
			// сообщение.
			if (_choiseTable == false) {
				Console.WriteLine("Для начала, выберите таблицу для работы!");

			}

			// Выводим название таблицы, с которой работаем.
			Console.WriteLine("Вы работаете с таблицей:{0}", 
				_DataBase[_indexChoiseTable].Name);

			// Создаем переменную для хранения пункта меню и выводим в консоль
			// меню.
			int choise;
			do {
				Console.Write("Меню:\n" +
				"1)Создать запись\n" +
				"2)Удалить запись 2\n" +
				"3)Отредактировать запись\n" +
				"4)Посмотреть конкретную запись\n" +
				"5)Посмотреть все записи\n" +
				"6)Решить задачу\n" +
				"0)Выход" +
				"\nВаше решение: ");

				// Если пользователь ввел не цифру, тогда присваем в переменную
				// выбора "-1", чтобы благополучно попасть в случай default
				if (int.TryParse(Console.ReadLine(), out choise) == false)
					choise = -1;

				// В зависимости от того, что выбрал пользователь, работаем с 
				// указанной таблицей.
				switch (choise) {
					case 1:
						{
                     
//							_DataBase[_indexChoiseTable].Create();
							break;
						}
					case 2:
						Console.WriteLine("2");
						break;
					case 3:
						Console.WriteLine("Вы решили выйти");
						break;
//					case 4:
//						{
//							_DataBase[_indexChoiseTable].Read(index);
//						}
					case 0:
						{
							Console.WriteLine("Вы решили выйти");
							break;
						}
					default:
						Console.WriteLine("Кажется, вы нажали что-то другое...");
						break;
				}
				Console.Write("Нажмите любую клавишу...");
				Console.ReadLine();
				Console.Clear();
			} while (choise != 0);

		}
	}
}

