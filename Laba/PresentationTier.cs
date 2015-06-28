using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Laba
{
	public class PresentationTier
	{
		/// <summary>
		/// База данных - хранение таблиц для работы с ними.
		/// </summary>
		List<LogicTier> _dataBase = new List<LogicTier>();
		/// <summary>
		/// Переменная для номера выбранной таблицы.
		/// </summary>
		int _choiseTable = -1;

		/// <summary>
		/// Вызывает консольное меню.
		/// </summary>
		public void Menu()
		{
			// Переменная для выбора пользователя.
			int choise = -1;

			do
			{
				// Проверяем БД.
				// Если там нет таблиц, выводим ограниченное меню
				if (!(CheckDataBase()))
				{
					Console.WriteLine("Вы не выбрали таблицу для работы, поэтому " +
					"нет вариантов для обработки таблицы.");

				}
            // Если таблица выбрана, тогда выводим полноценное меню
            else
				{
					Console.WriteLine("Меню:\n" +
					"1)Создать запись\n" +
					"2)Удалить запись\n" +
					"3)Отредактировать запись\n" +
					"4)Посмотреть конкретную запись\n" +
					"5)Посмотреть все записи\n" +
					"6)Выбрать таблицу для работы\n" +
					"7)Сохранить БД на диск\n" +
					"8)Решить поставленную задачу\n" +
					"0)Выход\n" +
					"Сейчас вы работаете в таблице: {0}\n" +
					"Ваше решение: ", _dataBase[_choiseTable].Name);

					// Если пользователь ввел не цифру, тогда присваем в переменную
					// выбора "-1", чтобы благополучно попасть в случай default
					if (int.TryParse(Console.ReadLine(), out choise) == false)
						choise = -1;
				}
				// В зависимости от того, что выбрал пользователь, работаем с 
				// указанной таблицей.
				switch (choise)
				{
				// Создание записи.
					case 1:
						{
							Console.WriteLine("Создаем запись.\n");
							int answer = 0;

							// Проверяем, какого типа запись нужно создать.
							// Случай, когда в таблице нет записей  
							// и можно создать запись любого типа.
							if (_dataBase[_choiseTable].Length == 0)
							{
								Console.WriteLine("Какого типа запись вы хотите создать:\n" +
								"1.Запись с одним полем типа string\n" +
								"2.Запись с двумя полями типа int\n" +
								"Ваш выбор: ");

								// Считываем ответ.
								int.TryParse(Console.ReadLine(), out answer);
							}
                     // Случай, когда в таблице есть записи и тип записей содержит 
                     // одно строкое поле.
                     else if (_dataBase[_choiseTable].Read(0).GetType().
                        ToString().Contains("RecordWithOneStringField"))
							{
								answer = 1;
							}
                     // Случай, когда в таблице есть записи и тип записей содержит
                     // два целых поля.
                     else if (_dataBase[_choiseTable].Read(0).GetType().
                        ToString().Contains("RecordWithTwoIntField"))
							{
								answer = 2;
							}

							// Если ввёл не то или возникла ошибка, то повторяем ввод.
							while (answer != 1 && answer != 2 && answer != -1)
							{
								Console.WriteLine("Похоже, вы ввели что-то другое, " +
								"либо в программе возникла ошибка, " +
								"пожалуйста введите 1, 2 или -1 для выхода:");
								int.TryParse(Console.ReadLine(), out answer);
							}

							// Работаем с выбранной опцией
							switch (answer)
							{
							// Выходим в меню
								case -1:
									{
										Console.WriteLine("Выходим обратно в меню");
										break;
									}
							// Работаем с записью с одним полем.
								case 1:
									{
										RecordWithOneStringField tmpRecord = CreateRecordWithOneStringField();

										_dataBase[_choiseTable].Create(tmpRecord);
										Console.WriteLine("Запись успешно создана!");

										break;
									}
							// Работаем с записью с двумя полями.
								case 2:
									{
										RecordWithTwoIntField tmpRecord = CreateRecordWithTwoIntField();

										_dataBase[_choiseTable].Create(tmpRecord);
										Console.WriteLine("Запись успешно создана!");

										break;
									}
								default:
									{
										break;
									}
							}

							break;
						}
				// Удаление записи.
					case 2:
						{
							Console.WriteLine("Удаляем запись в таблице: {0}",
								_dataBase[_choiseTable].Name);
							// Выводим имя таблицы и обрабатываем список записей с 
							// помощью лямбда выражений
							Console.WriteLine("Таблица \"{0}\":", _dataBase[_choiseTable].Name);
							_dataBase[_choiseTable].ReadAll().ForEach(element => Console.WriteLine(element));
							Console.WriteLine("Введите номер записи, которую вы хотите " +
							"удалить или '-1' для отмены операции удаления: ");

							// Считываем номер записи.
							int id;
							int.TryParse(Console.ReadLine(), out id);

							// Если ввёл не то, повторяем ввод.
							while (id != -1 && !(_dataBase[_choiseTable].CheckIndex(id)))
							{
								Console.WriteLine("Похоже, данной записи не существует." +
								"Повторите ввод или введите '-1' для выхода");
								int.TryParse(Console.ReadLine(), out id);
							}

							if (id == -1)
							{
								Console.WriteLine("Выходим обратно в меню");
								break;
							}

							// Удаляем запись.
							_dataBase[_choiseTable].Delete(id);
							Console.WriteLine("Запись в таблице {0} под номером {1} " +
							"успешно удалена!", _dataBase[_choiseTable].Name, id);

							break;
						}
				// Обновление записи
					case 3:
						{
							Console.WriteLine("Редактируем запись в таблице: {0}",
								_dataBase[_choiseTable].Name);
							// Выводим имя таблицы и обрабатываем список записей с 
							// помощью лямбда выражений
							Console.WriteLine("Таблица \"{0}\":", _dataBase[_choiseTable].Name);
							_dataBase[_choiseTable].ReadAll().ForEach(element => Console.WriteLine(element));
							Console.WriteLine("Введите номер записи, которую вы хотите " +
							"обновить или '-1' для отмены операции обновления: ");

							// Считываем номер записи.
							int id;
							int.TryParse(Console.ReadLine(), out id);

							// Если ввёл не то, повторяем ввод.
							while (id != -1 && !(_dataBase[_choiseTable].CheckIndex(id)))
							{
								Console.WriteLine("Похоже, данной записи не существует." +
								"Повторите ввод или введите '-1' для выхода:");
								int.TryParse(Console.ReadLine(), out id);
							}

							if (id == -1)
							{
								Console.WriteLine("Выходим обратно в меню");
								break;
							}

							// Проверяем, какого типа запись нужно обновить
							// Обновляем запись с одним строковым полем
							if (_dataBase[_choiseTable].Read(0).GetType().ToString().Contains(
								                  "RecordWithOneStringField"))
							{
								// Создаем временную запись.
								RecordWithOneStringField tmpRecord = CreateRecordWithOneStringField();

								// Обновляем запись с одним полем и выводим сообщение.
								_dataBase[_choiseTable].Update(id, tmpRecord);

								Console.WriteLine("Запись под номером {0} в таблице {1}" +
								"обновлена!\n", id, _dataBase[_choiseTable].Name);
							} 
                     // Обновляем запись с двумя целыми полями
                     else if (_dataBase[_choiseTable].Read(0).GetType().ToString().Contains(
								                       "RecordWithTwoIntField"))
							{
								RecordWithTwoIntField tmpRecord = CreateRecordWithTwoIntField();

								// Обновляем запись с одним полем и выводим сообщение.
								_dataBase[_choiseTable].Update(id, tmpRecord);

								Console.WriteLine("Запись под номером {0} в таблице {1} " +
								"обновлена!\n", id, _dataBase[_choiseTable].Name);
							} else
							{
								Console.WriteLine("Похоже, программа не смогла " +
								"определить тип обновляемой записи. Попробуйте снова");
							}
							break;
						}
				// Чтение одной записи.
					case 4:
						{
							Console.WriteLine("Просматриваем записи в таблице: {0}",
								_dataBase[_choiseTable].Name);
							Console.WriteLine("Введите номер записи, которую вы хотите " +
							"посмотреть или '-1' для отмены операции изменения:");

							// Считываем номер записи.
							int id;
							int.TryParse(Console.ReadLine(), out id);

							// Если ввёл не то, повторяем ввод.
							while (id != -1 && !(_dataBase[_choiseTable].CheckIndex(id)))
							{
								Console.WriteLine("Похоже, данной записи не существует." +
								"Повторите ввод или введите '-1' для выхода:");
								int.TryParse(Console.ReadLine(), out id);
							}

							if (id == -1)
							{
								Console.WriteLine("Выходим обратно в меню");
								return;
							}

							Console.WriteLine("Таблица: {0},\n" +
							"Запись:{1}\n", _dataBase[_choiseTable].Name, 
								_dataBase[_choiseTable].Read(id));
							break;
						}
				// Чтение всех записи.
					case 5:
						{
							// Выводим имя таблицы и обрабатываем список записей с 
							// помощью лямбда выражений
							Console.WriteLine("Таблица \"{0}\":", _dataBase[_choiseTable].Name);
							_dataBase[_choiseTable].ReadAll().ForEach(element => Console.WriteLine(element));

							break;
						}
				// Выбор таблицы для работы.
					case 6:
						{
							// Выводим информацию
							Console.WriteLine("Сейчас вы работаете в таблице {0}",
								_dataBase[_choiseTable].Name);
							Console.WriteLine("БД содержит следующие таблицы:");
							for (int i = 0; i < _dataBase.Count; i++)
							{
								Console.WriteLine("{0}: Имя: {1}, кол-во записей: {2};",
									i, 
									_dataBase[i].Name,
									_dataBase[i].Length);
							}
							Console.WriteLine("Введите номер таблицы для работы, " +
							"\"-2\" для добавления новой таблицы " +
							"или \"-1\" для выхода обратно в меню:");

							// Считываем ответ.
							int answer;
							int.TryParse(Console.ReadLine(), out answer);

							// Если ввёл не то, повторяем ввод.
							while (answer != -1
							                     && answer < 0
							                     && answer > _dataBase.Count
							                     && answer != -2)
							{
								Console.WriteLine("Похоже, вы ввели что-то другое, " +
								"введите номер существующей таблицы," +
								"-2 для добавления новой таблицы или " +
								"-1 для выхода:");
								int.TryParse(Console.ReadLine(), out answer);
							}

							if (answer == -2)
							{
								AddTableToDB();
							} else if (answer != -1)
							{
								_choiseTable = answer;
							}
                     

							break;
						}
				// Сохранение БД на диск
					case 7:
						{
							int countUnnamed = 0;

							for (int i = 0; i < _dataBase.Count; i++)
							{
								// Исправляем коллизии с именами.
								// Считаем общее количество таких.
								if (_dataBase[i].Name == "unnamed")
									countUnnamed++;
								// Если нашлось больше 1 таблицы с таким именем, заменяем
								// имя на unnamed (конфликт имён *дата*)
								if (countUnnamed > 1)
								{
									_dataBase[i].Name = "unnamed (конфликт имён " +
									DateTime.Today + ")";
									countUnnamed = 1;
								}

								_dataBase[i].Save();
								Console.WriteLine("Таблица под номером {0} сохранена!", i);
							}

							Console.WriteLine("Все таблицы сохранены успешно!");
                     
							break;
						}
				// Решение задачи.
					case 8:
						{
							// Выводим информацию о задаче
							Console.WriteLine("19. Класс – уровни доступа.\n" +
							"Даны пользователи, роли и пересечение пользователей и ролей, " +
							"вывести всех пользователей для указанной роли и наоборот, " +
							"все роли для указанного пользователя.");


							// Проверяем количество таблиц в БД
							if (_dataBase.Count < 3)
							{
								Console.WriteLine("Для решения поставленной задачи" +
								"требуется минимум 3 таблицы!");
								break;
							}

							// Выводим все таблицы
							Console.WriteLine("БД содержит следующие таблицы:");
							for (int i = 0; i < _dataBase.Count; i++)
							{
								Console.WriteLine("{0}: Имя: {1}, кол-во записей: {2};",
									i, 
									_dataBase[i].Name,
									_dataBase[i].Length);
							}

							// TODO
							// Убрать 3 повторяющихся куска кода (либо запрашивать сразу
							// три номер, либо как-нибудь по-другому огранизовать)
							// Переменные для хранения ответов.
							int idUsers, idRoles, idUsersRoles;
							Console.WriteLine("Введите номер таблицы Пользователей:");
							// Считываем ответ.
							int.TryParse(Console.ReadLine(), out idUsers);

							// Если ввёл не то, повторяем ввод.
							while (idUsers < 0 && idUsers >= _dataBase.Count
							                     && idUsers != -1)
							{
								Console.WriteLine("Похоже, вы ввели что-то другое, " +
								"введите номер таблицы Пользователей," +
								"или -1 для выхода:");
								int.TryParse(Console.ReadLine(), out idUsers);
							}

							// Выход из текущей опции меню
							if (idUsers == -1)
								break;

							Console.WriteLine("Введите номер таблицы Ролей:");
							// Считываем ответ.
							int.TryParse(Console.ReadLine(), out idRoles);

							// Если ввёл не то, повторяем ввод.
							while (idRoles < 0 && idRoles >= _dataBase.Count
							                     && idRoles != -1)
							{
								Console.WriteLine("Похоже, вы ввели что-то другое, " +
								"введите номер таблицы Пользователей," +
								"или -1 для выхода:");
								int.TryParse(Console.ReadLine(), out idRoles);
							}

							// Выход из текущей опции меню
							if (idRoles == -1)
								break;

							Console.WriteLine("Введите номер таблицы пересечения:");
							// Считываем ответ.
							int.TryParse(Console.ReadLine(), out idUsersRoles);

							// Если ввёл не то, повторяем ввод.
							while (idUsersRoles < 0 && idUsersRoles >= _dataBase.Count
							                     && idUsersRoles != -1)
							{
								Console.WriteLine("Похоже, вы ввели что-то другое, " +
								"введите номер таблицы Пользователей," +
								"или -1 для выхода:");
								int.TryParse(Console.ReadLine(), out idUsersRoles);
							}

							// Выход из текущей опции меню
							if (idUsersRoles == -1)
								break;

							// Вызываем абстрактный метод Solve для решения задачи.
							Solver.Solve(_dataBase[idUsers],
								_dataBase[idRoles],
								_dataBase[idUsersRoles]);

							break;
						}
				// Выход.
					case 0:
						{
							Console.WriteLine("Выходим.");

							// TODO Доделать проверку на выход с 
							// несохранёнными изменениями.
//							if (_dataBase[_choiseTable].Status)
//							{
//								Console.WriteLine("Сохранить изменения перед выходом?" +
//								"[д]/[н]");
//
//								// Создаем переменную для хранения нажатой клавиши.
//								ConsoleKeyInfo Choise = new ConsoleKeyInfo();
//								Choise = Console.ReadKey();
//
//								// Если пользователь ввёл что-то другое, повторяем ввод.
//								while (Choise.KeyChar != 'н' || Choise.KeyChar != 'д' || Choise.KeyChar != 'Н'
//								                       || Choise.KeyChar != 'Д')
//								{
//									Choise = Console.ReadKey();
//								}
//
//								// Сохраняем изменения.
//								if (Choise.KeyChar == 'д' || Choise.KeyChar == 'Д')
//								{
//                           _dataBase.ForEach(element => element.)
//								}
//							}

							return;
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

		// TODO
		// Дополнить описание метода
		/// <summary>
		/// Проверяет базу данных на наличие таблиц, если их нет, то предлагает
		/// либо создать новую, либо загрузить с диска существующую.
		/// </summary>
		private bool CheckDataBase()
		{
			// Если нет выбранных таблиц.
			if (_dataBase.Count == 0)
			{
				Console.WriteLine("Похоже, у вас не выбранных таблиц " +
				"для работы!");

				return AddTableToDB();
			}

			return true;
		}

		/// <summary>
		/// Создает запись с одним текстовым полем.
		/// </summary>
		/// <returns>Запись с одним текстовым полем 
		/// (<see cref="Laba.RecordWithOneStringField"/>/)</returns>
		private RecordWithOneStringField CreateRecordWithOneStringField()
		{
			RecordWithOneStringField tmpRecord = new RecordWithOneStringField();

			Console.WriteLine("Введите содержание первого поля:");
			tmpRecord.FirstField = Console.ReadLine();

			return tmpRecord;
		}

		/// <summary>
		/// Создает запись с двумя текстовыми полями.
		/// </summary>
		/// <returns>Запись с двумя текстовыми полями.
		/// (<see cref="Laba.RecordWithTwoIntField"/>/)</returns>
		private RecordWithTwoIntField CreateRecordWithTwoIntField()
		{
			RecordWithTwoIntField tmpRecord = new RecordWithTwoIntField();
			int answer = -1;

			Console.WriteLine("!ВАЖНО! При создании записи с двумя целыми типами," +
			"обратите внимание на название:\n" +
			"Если таблица называется \"UsersRoles\" - в первой графе записывается" +
			"первичный ключ пользователя, а во второй ПК роли;\n" +
			"Если таблица называется \"RolesUsers\" - в первой графе записывается" +
			"ПК роли, а во второй - пользователя.");
         
			Console.WriteLine("Введите содержание первого поля:");
			int.TryParse(Console.ReadLine(), out answer);
			tmpRecord.FirstField = answer;

			Console.WriteLine("Введите содержание второго поля:");
			int.TryParse(Console.ReadLine(), out answer);
			tmpRecord.SecondField = answer;

			return tmpRecord;
		}

		// TODO
		// Дополнить описание метода
		/// <summary>
		/// Создает новую таблицу.
		/// </summary>
		private bool CreateTable()
		{
			Console.WriteLine("Введите имя для таблицы:");
			string name = Console.ReadLine();

			DataTier table = new DataTier(name);
			LogicTier logicTable = new LogicTier(table);
			_dataBase.Add(logicTable);
			_choiseTable = _dataBase.Count - 1;

			Console.WriteLine("Таблица успешно создана!");
			return true;
		}

		// TODO
		// Дополнить описание метода
		// Дополнить комментарии
		/// <summary>
		///  Загружает таблицу.
		/// </summary>
		private bool LoadTable()
		{
			// Папка, где лежат все таблицы
			string saveDirectory = "/DataBase/";

			Console.WriteLine("Таблицы, доступные для загрузки" +
			"(директория для загрузки таблиц \"{0}\")", saveDirectory);

			// Выводим файлы с расширением ".xml", доступные для загрузки
			string[] files = Directory.GetFiles(
				                       Directory.GetCurrentDirectory() + saveDirectory,
				                       "*.xml");
			for (int i = 0; i < files.Length; i++)
			{
				Console.WriteLine(files[i].Substring(
					files[i].LastIndexOf(saveDirectory)));
			}

			// TODO
			// Заменить ввод имени на ввод порядкового номера

			Console.WriteLine("Введите имя для загрузки таблицы " +
			"*по умолчанию загрузится таблица unnamed.xml " +
			"(для выбора пути по умолчанию, оставьте поле пустым)");

			// Считываем результат.
			string answer = Console.ReadLine();

			// Если такого файла не существует, просим повторить ввод
			while (!(File.Exists(Directory.GetCurrentDirectory()
			             + saveDirectory
			             + "Roles.xml"))
			             && answer != "-1")
			{
				Console.WriteLine("Данного файла не существует, повторите ввод " +
				"или введите -1 для выхода.");
				answer = Console.ReadLine();
			}

			if (answer == "-1")
			{
				Console.WriteLine("Выходим");
				return false;
			}

			LogicTier logicTable = new LogicTier();
			logicTable.Load(Directory.GetCurrentDirectory() + saveDirectory + answer);
			_dataBase.Add(logicTable);
			_choiseTable = _dataBase.Count - 1;

			Console.WriteLine("Таблица успешно загружена!");
			return true;
		}

		// TODO
		// Дополнить описание метода
		/// <summary>
		/// Добавляет новую таблицу к БД.
		/// </summary>
		private bool AddTableToDB()
		{
			Console.WriteLine("Создать новую таблицу [1] " +
			"или загрузить существующую с диска [2]?");
         
			// Считываем ответ.
			int answer;
			int.TryParse(Console.ReadLine(), out answer);


			// Если ввёл не то, повторяем ввод.
			while (answer != 1 && answer != 2 && answer != -1)
			{
				Console.WriteLine("Похоже, вы ввели что-то другое ({0}), " +
				"введите 1, 2 или -1 для выхода из программы:", answer);
				int.TryParse(Console.ReadLine(), out answer);
			}


			switch (answer)
			{
			//Выход из программы
				case (-1):
					{
						Console.WriteLine("Выходим.");
						return false;
					}
			// Создаем новую таблицу.
				case (1):
					{
						return CreateTable();
					} 
			//Загружаем существующую
				case (2):
					{
						return LoadTable();
					}

				default:
					{
						return false;
					}
			}
		}

	}
}

