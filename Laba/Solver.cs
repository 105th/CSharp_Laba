﻿using System;
using System.Collections.Generic;

namespace Laba
{
	/// <summary>
	/// Универсальный "решатель" для таблиц
	/// 19. Класс – уровни доступа.
	/// Даны пользователи, роли и пересечение пользователей и ролей,
	///  вывести всех пользователей для указанной роли и наоборот,
	///  все роли для указанного пользователя;
	/// </summary>
	public abstract class Solver
	{
		/// <summary>
		/// Находить пересечение в таблицах.
		/// </summary>
		/// <param name="Users">Пользователи.</param>
		/// <param name="Roles">Роли.</param>
		/// <param name="UsersRoles">Пересечение ролей и пользователей.</param>
		public static void Solve(LogicTier Users, LogicTier Roles, LogicTier UsersRoles)
		{
			Console.WriteLine("Вывести всех пользователей для роли [1] или " +
			"вывести все роли для пользователя?[2]");

			// Считываем ответ.
			int answer;
			int.TryParse(Console.ReadLine(), out answer);

			// Если ввёл не то, повторяем ввод.
			while (answer != 1 && answer != 2 && answer != -1)
			{
				Console.WriteLine("Похоже, вы ввели что-то другое, " +
				"введите 1, 2 или -1 для выхода:");
				int.TryParse(Console.ReadLine(), out answer);
			}

			// TODO Сделать парсинг названия таблицы, чтобы программа сама
			// понимала, в каком порядке записаны ключи
			Console.WriteLine("Укажите, в каком порядке записаны ключи в " +
			"таблице пересечений(исходя из названия таблицы):\n" +
			"[1] 1 - Role, 2 - User;\n" +
			"[2] 1 - User, 2 - Role.\n" +
			"(или введите \"-1\" для выхода.");
			// TODO Убрать значение по умолчанию
			int position_of_user = 1;
			int position_of_role;
			int.TryParse(Console.ReadLine(), out position_of_role);

			// Если ввёл не то, тогда спрашиваем ещё раз.
			while (position_of_role != 1 && position_of_role != 2
			             && position_of_role != -1)
			{
				Console.WriteLine("Похоже, вы ввели что-то другое.");
				Console.WriteLine("Укажите, в каком порядке записаны ключи в " +
				"таблице пересечений(исходя из названия таблицы):\n" +
				"[1] 1 - Role, 2 - User;\n" +
				"[2] 1 - User, 2 - Role.\n" +
				"(или введите \"-1\" для выхода");
				int.TryParse(Console.ReadLine(), out position_of_role);
			}

			// Анализируем ответ и рассталяем порядок полей в таблице.
			if (position_of_role == -1)
				answer = -1;
			else if (position_of_role == 1)
				position_of_user = 2;
			else if (position_of_role == 2)
				position_of_user = 1;

			// Работаем с ответом.
			switch (answer)
			{
			// Выходим из функции.
				case -1:
					{
						Console.WriteLine("Выходим обратно в меню");
						return;
					}
				case 1:
					{
						// Выводим таблицу ролей.
						Console.WriteLine("Таблица ролей:");
						Roles.ReadAll().ForEach(element => Console.WriteLine(element));

						Console.WriteLine("Введите номер роли (начиная с нуля), " +
						"для которой вывести всех пользователей");

						// Считываем ответ.
						int id;
						int.TryParse(Console.ReadLine(), out id);

						// Если ввёл не то, повторяем ввод.
						while (!(Roles.CheckIndex(id)))
						{
							Console.WriteLine("Похоже, вы ввели что-то другое " +
							"либо данной записи не существует " +
							"повторите ввод или введите -1 для выхода:");
							int.TryParse(Console.ReadLine(), out id);
						}

						// Выполняем поиск в таблице связей, ищем пользователей с 
						// указанной ролью
						List<Record> results = new List<Record>();

						for (int i = 0; i < UsersRoles.Length; i++)
						{
							// Если запись содержит id роли
							if (UsersRoles[i].HasData(id, position_of_role))
							{
								// FIXME Исправить это говно с использованием регулярных выражений
								// Из найденной позиции извлекаем (парсим) ключ и
								// добавляем пароль с этим ключом в список результатов.
								string[] parseStrings = UsersRoles[i].ToString().Split(new Char [] {
									' ',
									',',
									'.',
									':'
								});

								// Тут используется некоторое говно с индексом строки,
								// которое скорее всего сломается
								int PK = int.Parse(parseStrings[8]);

								// Найденную запись добавляем в список результатов
								results.Add(Users.Find(PK));
							}
						}


						// Выдаем результат.
						Console.WriteLine("Результаты поиска:");
						results.ForEach(element => Console.WriteLine(element));

						break;
					}
				case 2:
					{
						// Выводим таблицу ролей.
						Console.WriteLine("Таблица пользователей:");
						Users.ReadAll().ForEach(element => Console.WriteLine(element));

						Console.WriteLine("Введите номер пользователя (начиная с нуля), " +
						"для которого вывести все роли");

						// Считываем ответ.
						int id;
						int.TryParse(Console.ReadLine(), out id);

						// Если ввёл не то, повторяем ввод.
						while (!(Users.CheckIndex(id)))
						{
							Console.WriteLine("Похоже, вы ввели что-то другое " +
							"либо данной записи не существует " +
							"повторите ввод или введите -1 для выхода:");
							int.TryParse(Console.ReadLine(), out id);
						}

						// Выполняем поиск в таблице связей, ищем роль с указанным 
						// пользователем
						List<Record> results = new List<Record>();

						for (int i = 0; i < UsersRoles.Length; i++)
						{
							// Если запись содержит id пользователя
							if (UsersRoles[i].HasData(id, position_of_user))
							{
								// FIXME Исправить это говно с использованием регулярных выражений
								// Из найденной позиции извлекаем (парсим) ключ и
								// добавляем роль с этим ключом в список результатов.
								string[] parseStrings = UsersRoles[i].ToString().Split(new Char [] {
									' ',
									',',
									'.',
									':'
								});

								// Тут используется некоторое говно с индексом строки,
								// которое скорее всего сломается
								int PK = int.Parse(parseStrings[21]);

								// Найденную запись добавляем в список результатов
								results.Add(Roles.Find(PK));
							}
						}


						// Выдаем результат.
						Console.WriteLine("Результаты поиска:");
						results.ForEach(element => Console.WriteLine(element));

						break;
					}
				default:
					break;
			}
		}
	}
}

