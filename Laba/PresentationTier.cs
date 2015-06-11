﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Laba
{
	public class PresentationTier
	{
		// Лист из экземляров типа LogicTier или База Данных
		List<LogicTier> _LT;
		// Выбрана ли БД для работы
		bool _choiseDataBase;

		// Конструктор
		public PresentationTier()
		{
			_LT = new List<LogicTier>();
			_choiseDataBase = false;
		}

		// Конструктор с одним параметром типа LogicTier
		public PresentationTier(LogicTier logicTier)
		{
			_LT = new List<LogicTier>();
			_LT.Add(logicTier);
			_choiseDataBase = false;
		}


		// Метод для меню
		public void menu()
		{
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

				//Если не выбрана таблица базы данных для работы, тогда выводим сообщение
				if (_choiseDataBase == false) {
					Console.WriteLine("Для начала, выберите базу данных для работы!");

				}

            //Иначе работаем с конкретной таблицой
            else {
					switch (choise) {
						case 1:
							{
								Console.WriteLine("");
								break;
							}
						case 2:
							Console.WriteLine("2");
							break;
						case 3:
							Console.WriteLine("Вы решили выйти");
							break;
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
				}
			} while (choise != 0);

		}

		// Метод выбора таблицы для работы
		public void ChoiseDB(string path = null)
		{
//         if (path == null)
		}
	}
}
