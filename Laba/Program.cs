// 19. Класс – уровни доступа.
// Даны пользователи, роли и пересечение пользователей и ролей, вывести всех 
// пользователей для указанной роли и наоборот, все роли для указанного 
// пользователя;

namespace Laba
{
	class MainClass
	{
		public static void Main(string[] args)
		{
//			RecordWithOneStringField r = new RecordWithOneStringField();
//			r.FirstField = "Dima";
//
////			Console.WriteLine(r);
//
//			RecordWithTwoStringField r2 = new RecordWithTwoStringField();
//			r2.FirstField = "Dima";
//			r2.SecondField = "User";
//
////			Console.WriteLine(r2);
//
//			RecordWithTwoStringField r3 = new RecordWithTwoStringField();
//			r3.FirstField = "Dima";
//			r3.SecondField = "Admin";
//
////			Console.WriteLine(r3);
//
//			RecordWithTwoStringField r4 = new RecordWithTwoStringField();
//			r4.FirstField = "Petya";
//			r4.SecondField = "Moderator";
//
////			Console.WriteLine(r4);
//
//			DataTier DB = new DataTier();
//			LogicTier LT = new LogicTier(DB);
//
//			LT.Create(r2);
//			LT.Create(r3);
//			LT.Create(r4);
//
////			LT.ReadAll();
//
//			LT.Delete(1);
//
////			LT.ReadAll();
//
//			LT.Create(r3);
//
////			LT.ReadAll();
//
//			RecordWithTwoStringField r5 = new RecordWithTwoStringField();
//			r5.FirstField = "Dima";
//			r5.SecondField = "Moderator";
//
//			LT.Update(1, r5);
//
//			LT.ReadAll();
//
//			LT.Save();
//
//			DataTier DB2 = new DataTier();
//			LogicTier LT2 = new LogicTier(DB2);
//
//			LT2.Load();
//			LT2.ReadAll();


			DataTier DB = new DataTier();
			LogicTier LT = new LogicTier(DB);
			PresentationTier UI = new PresentationTier(LT);

			UI.menu();


		}
	}
}
