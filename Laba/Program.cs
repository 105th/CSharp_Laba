﻿// 19. Класс – уровни доступа.
// Даны пользователи, роли и пересечение пользователей и ролей, вывести всех 
// пользователей для указанной роли и наоборот, все роли для указанного 
// пользователя;
using System;

namespace Laba
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			DataTier table = new DataTier();
			LogicTier logicTable = new LogicTier(table);

//			RecordWithOneStringField record = new RecordWithOneStringField(0, "Дима");
//			logicTable.Create(record);
//			logicTable.Save();

			logicTable.Load();

			Console.WriteLine(logicTable.TypeOfRecords.Contains("RecordWithTwoStringField"));
         
		}
	}
}
