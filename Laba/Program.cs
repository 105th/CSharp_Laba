using System;
using System.Collections.Generic;

namespace Laba
{
    //Одна запись с одним полем типа "строка"
    public class RecordWithOneStringField
    {
        int r_PrimaryKey; //Первичный ключ
        string r_FirstField; //Строка первого поля

        //Конструктор
        public RecordWithOneStringField(int tmpPrimaryKey = 0, string dataOfFirstField = null)
        {
            r_PrimaryKey = tmpPrimaryKey;
            r_FirstField = dataOfFirstField;
        }

//        public void SetPK (int primaryKey)
//        {
//            r_PrimaryKey = primaryKey;
//        }
//
//        public int GetPK ()
//        {
//            return (r_PrimaryKey);
//        }
//
//        public void SetFirstField (string firstField)
//        {
//            r_FirstField = firstField;
//        }
//
//        public string GetFirstField ()
//        {
//            return(r_FirstField);
//        }

        //Методы задания и выдачи поля Первичного Ключа
        public int PK {
            get 
            {
                return(r_PrimaryKey);
            }
            set
            { 
                r_PrimaryKey = value;
            }
        }

        //Методы задания и выдачи поля Первичного Ключа
        public string FirstField {
            get
            { 
                return(r_FirstField);
            }
            set
            {
                r_FirstField = value; 
            }
        }


        //Переопределение стандартного метода ToString для текстового представления
        public override string ToString()
        {
            return string.Format("PK: {0, 3}, FirstField: {1, -10};", r_PrimaryKey, r_FirstField);
        }
    }


    //Таблица Пользователи
	public class Users
	{
        RecordWithOneStringField[] u_Records; //Массив записей с одним полем типа "строка"
        int u_NumberOfRecords; //Количество записей
        int u_NumberOfFields; //Количество полей
        int u_MaxRecords;

//        //Константный первичный ключ для первой строки таблицы, т.е. для название полей
//        const int PRIMARYKEYFORNAMEOFFIELDS = 0;
//        //Константное название первого поля
//        const string NAMEOFFIRSTFIELD = "Name";
//

        //Конструктор
        public Users(int size)
        {
            u_NumberOfRecords = 0;
            u_NumberOfFields = 1;

//            //Проверка на правильность названия поля
//            if (string.IsNullOrWhiteSpace(nameOfFirstField))
//                throw new ArgumentException("Название первого поля не может быть пустым!");

            //Проверка на правильность размера таблицы
            if (size < 0)
                throw new ArgumentException("Количество записей в таблице не может быть меньше нуля!");

            u_MaxRecords = size;
            
            u_Records = new RecordWithOneStringField[size];
            for (int i = 0; i < u_Records.Length; i++)
                u_Records[i] = new RecordWithOneStringField();
//
//            //Присваивание нулевой записи нулевого первичного ключа и задание имя первого поля
//            u_Records[0].PK = 0;
//            u_Records[0].FirstField = NAMEOFFIRSTFIELD;
        }

        //Возвращает максимальное количество записей в таблице
        public int Length
        {
            get
            {
                return(u_MaxRecords);
            }
        }

        //Свойство для поддержки индексирования
        public RecordWithOneStringField this[int index]
        {
            // Аксессор для получения данных. 
            get 
            {
                // Возврат значения, которое определяет индекс.
                return u_Records[index];
            }
            // Аксессор для установки данных.
            set
            {
                // Установка значения, которое определяет индекс.
                u_Records[index] = value;
            }
        }

        //Вывод таблицы на экран
        public void Display()
        {
            Console.WriteLine("| PK |   Name   |");
            for (int i = 0; i < u_NumberOfRecords; i++)
                Console.WriteLine("| {0, -3}|{1, 10}|", u_Records[i].PK, u_Records[i].FirstField);
        }

        //Переопределение стандартного метода ToString
        public override string ToString()
        {
            return string.Format("[Table Users: Amount of records={0}, Max. size={1}]", u_NumberOfRecords, u_MaxRecords);
        }

        //Добавление записи в таблицу
        public void Add(string dataFirstField = null)
        {
            //Проверка на возможность добавления записи
            if (u_NumberOfRecords == u_MaxRecords)
                throw new ArgumentOutOfRangeException("Таблица заполнена!");

            u_Records[u_NumberOfRecords] = new RecordWithOneStringField();
            u_Records[u_NumberOfRecords].PK = u_NumberOfRecords;
            u_Records[u_NumberOfRecords].FirstField = dataFirstField;
            u_NumberOfRecords++;
        }

      //Удаление записи в таблице по 
      public void Remove(int index)
		{
         //Проверка на границы индекса
         if (index < 0 || index > u_MaxRecords)
            throw new ArgumentOutOfRangeException ("Недопустимое значение индекса: выход за границы таблицы!");

				//Проверка правильности параметра
         if (index >= u_NumberOfRecords)
				    throw new ArgumentOutOfRangeException ("Недопустимое значение индекса: данной записи не существует");


         //Передвигаем все значения в таблице на 1 вперед
         for (int i = index; i < u_NumberOfRecords; i++)
         {
            u_Records[i].FirstField = u_Records[i + 1].FirstField;
            u_Records[i].PK = u_Records[i + 1].PK;
         }

         //Если не передан индекс или передан индекс последней записи, то цикл не выполняется и удаляем последнюю
         u_Records[u_NumberOfRecords].FirstField = null;
         u_Records[u_NumberOfRecords].PK = 0;
         u_NumberOfRecords--; // Обновляем количество записей в таблице
         }
      }

    class MainClass
    {
        public static void Main (string[] args)
        {
            Users u1 = new Users(10);

            u1.Add("Dima");
            u1.Add("Vasya");
            u1.Add("Anton");
            u1.Add("Petr");

            Console.WriteLine();
            u1.Display();

            u1.Remove();
            u1.Remove(2);

            Console.WriteLine();
            u1.Display();

            u1.Add("Vasya");
            u1.Add("Vasya2");
            u1.Add("Vasya3");

            Console.WriteLine();
            u1.Display();
        }
    }
}
