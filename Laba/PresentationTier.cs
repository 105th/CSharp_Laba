﻿using System;
using System.Collections.Generic;

namespace Laba
{
	public class PresentationTier
	{
		// Хранение таблиц для работы с ними.
		List<LogicTier> _dataBase = new List<LogicTier>();
		// Переменная для номера выбранной таблицы.
		int _choiseTable = -1;

		public void menu()
		{
			// Переменная для выбора пользователя.
			int choise;

			do
			{
				Console.Write("Меню:\n" +
				"1)Создать запись\n" +
				"2)Удалить запись\n" +
				"3)Отредактировать запись\n" +
				"4)Посмотреть конкретную запись\n" +
				"5)Посмотреть все записи\n" +
				"6)Решить задачу\n" +
				"0)Выход\n" +
				"Ваше решение: ");

				// Если пользователь ввел не цифру, тогда присваем в переменную
				// выбора "-1", чтобы благополучно попасть в случай default
				if (int.TryParse(Console.ReadLine(), out choise) == false)
					choise = -1;

				// В зависимости от того, что выбрал пользователь, работаем с 
				// указанной таблицей.
				switch (choise)
				{
					case 1:
						{
							checkDataBase();

							Console.WriteLine("Создаем запись.\n" +
							"Какого типа запись вы хотите создать:\n" +
							"1.Запись с одним полем типа string\n" +
							"2.Запись с двумя полями типа string\n" +
							"Ваш выбор: ");

							// Считываем ответ.
							int answer;
							int.TryParse(Console.ReadLine(), out answer);

							// Если ввёл не то, повторяем ввод.
							while (answer != 1 || answer != 2)
							{
								Console.WriteLine("Похоже, вы ввели что-то другое, " +
								"введите 1 или 2:");
								int.TryParse(Console.ReadLine(), out answer);
							}

							//Работаем с записью с одним полем
							if (answer == 1)
							{
								RecordWithOneStringField tmpRecord = new RecordWithOneStringField();

								Console.WriteLine("Введите содержание первого поля:\n");
								tmpRecord.FirstField = Console.ReadLine();

								_dataBase[_choiseTable].Create(tmpRecord);
								Console.WriteLine("Запись успешно создана!\n");
							}
                     //Работаем с записью с двумя полями
                     else if (answer == 2)
							{
								RecordWithTwoStringField tmpRecord = new RecordWithTwoStringField();

								Console.WriteLine("Введите содержание первого поля:\n");
								tmpRecord.FirstField = Console.ReadLine();

								Console.WriteLine("Введите содержание второго поля:\n");
								tmpRecord.SecondField = Console.ReadLine();

								_dataBase[_choiseTable].Create(tmpRecord);
								Console.WriteLine("Запись успешно создана!\n");
							}

							break;
						}
					case 2:
						{
							checkDataBase();

							Console.WriteLine("Удаляем запись в таблице: {0}",
								_dataBase[_choiseTable].Name);
							Console.WriteLine("Введите номер записи, которую вы хотите " +
							"удалить или '-1' для отмены операции удаления: ");

							// Считываем номер записи.
							int id;
							int.TryParse(Console.ReadLine(), out id);

							// Если ввёл не то, повторяем ввод.
							while (id != -1 || _dataBase[_choiseTable].CheckIndex(id))
							{
								Console.WriteLine("Похоже, данной записи не существует." +
								"Повторите ввод или введите '-1' для выхода");
								int.TryParse(Console.ReadLine(), out id);
							}

							// Удаляем запись.
							_dataBase[_choiseTable].Delete(id);
							Console.WriteLine("Запись в таблице {0} под номером {1} " +
							"успешно удалена!", _dataBase[_choiseTable].Name, id);

							break;
						}
					case 3:
						{
							checkDataBase();

							Console.WriteLine("Редактируем запись в таблице: {0}",
								_dataBase[_choiseTable].Name);
							Console.WriteLine("Введите номер записи, которую вы хотите " +
							"изменить или '-1' для отмены операции изменения: ");

							// Считываем номер записи.
							int id;
							int.TryParse(Console.ReadLine(), out id);

							// Если ввёл не то, повторяем ввод.
							while (id != -1 || _dataBase[_choiseTable].CheckIndex(id))
							{
								Console.WriteLine("Похоже, данной записи не существует." +
								"Повторите ввод или введите '-1' для выхода");
								int.TryParse(Console.ReadLine(), out id);
							}

							if (_dataBase[_choiseTable].TypeOfRecords.Contains(
								                  "RecordWithOneStringField"))
								;

							// Редактируем запись.
//                     _dataBase[_choiseTable].Update(id, )
							Console.WriteLine("Запись в таблице {0} под номером {1} " +
							"успешно удалена!", _dataBase[_choiseTable].Name, id);

							break;
						}
					case 4:
						{
							break;
						}
					case 6:
						{
							break;
						}
					case 0:
						{
							Console.WriteLine("Вы решили выйти");
							break;
						}
					default:
						{
							Console.WriteLine("Кажется, вы нажали что-то другое...");
							break;   
						}	
				}

				Console.Write("Нажмите любую клавишу...");
				Console.ReadLine();
				Console.Clear();
			} while (choise != 0);
		}

		public void checkDataBase()
		{
			// Если нет выбранных таблиц.
			if (_dataBase.Count == 0)
			{
				Console.WriteLine("Похоже, у вас не выбранных таблиц" +
				"для работы!\n");
				Console.WriteLine("Создать новую таблицу [0] " +
				"или загрузить существующую с диска [1]?\n");

				// Считываем ответ.
				int answer;
				int.TryParse(Console.ReadLine(), out answer);

				// Если ввёл не то, повторяем ввод.
				while (answer != 0 || answer != 1)
				{
					Console.WriteLine("Похоже, вы ввели что-то другое, " +
					"введите 0 или 1:");
					int.TryParse(Console.ReadLine(), out answer);
				}

				// Создаем новую таблицу.
				if (answer == 0)
				{
					Console.WriteLine("Введите имя для таблицы:\n");
					string name = Console.ReadLine();

					DataTier table = new DataTier(name);
					LogicTier logicTable = new LogicTier(table);
					_dataBase.Add(logicTable);
					_choiseTable = 0;
				} 
            //Загружаем существующую
            else if (answer == 0)
				{
					Console.WriteLine("Введите путь для загрузки" +
					"существующей таблицы" +
					" *путь по умолчанию - исполняемый_файл/DataBase" +
					" (для выбора пути по умолчанию, оставьте поле пустым)" +
					"\n");

					LogicTier logicTable = new LogicTier();
					logicTable.Load(Console.ReadLine());
					_dataBase.Add(logicTable);
					_choiseTable = 0;
				}
			}
		}
	}
}

