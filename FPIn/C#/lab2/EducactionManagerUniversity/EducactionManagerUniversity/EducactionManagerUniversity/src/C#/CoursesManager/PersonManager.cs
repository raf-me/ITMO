using System;
using System.Collections.Generic;

namespace EducactionManagerUniversity.data.C_.CoursesManager;

public class PersonManager
{
    private string personName;
    private string admin;
    private string student;
    private string teacher;
    private int courseID;
    private int personID;
    private int countStudentID;
    private int countTeacherID;
    private int password;
    private int passwordDataBase;
    private int disciplineID;

    private Dictionary<int, string> CollectStudentName;
    private Dictionary<int, int> CollectStudentID;
    private Dictionary<int, int> CollectStudentPassword;
    
    // private Dictionary<int, string> CollectNameDiscipline;
    private Dictionary<int, int> CollectDisciplineID;

    private Dictionary<int, string> CollectAdminName;
    private Dictionary<int, int> CollectAdminID;
    private Dictionary<int, int> CollectAdminPassword;
    
    private Dictionary<int, string> CollectTeacherName;
    private Dictionary<int, int> CollectTeacherID;
    private Dictionary<int, int> CollectTeacherPassword;

    public PersonManager() {
        CollectStudentName = new Dictionary<int, string>();
        CollectStudentID = new Dictionary<int, int>();
        CollectStudentPassword = new Dictionary<int, int>();
        
        CollectAdminName = new Dictionary<int, string>();
        CollectAdminID = new Dictionary<int, int>();
        CollectAdminPassword = new Dictionary<int, int>();
        
        CollectTeacherName = new Dictionary<int, string>();
        CollectTeacherID = new Dictionary<int, int>();
        CollectTeacherPassword = new Dictionary<int, int>();
        
        CollectDisciplineID =  new Dictionary<int, int>();
    }
    
    
    public void 시험()
    {
        CollectAdminID.Add(2300000, 2300000);
        CollectAdminPassword.Add(2300000, 1234567);
        CollectAdminName.Add(2300000, "Ivan");
        CollectTeacherID.Add(2200000, 2200000);
        
        CollectDisciplineID.Add(321000, 321000);
    }
    
    public void 관리자사람()
    {
        시험();
        
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите действие управления: ");
            Console.WriteLine("1 - Регистрация студента ");
            Console.WriteLine("2 - Регистрация администратора ");
            Console.WriteLine("3 - Добавить пользователя (администратор) ");
            Console.WriteLine("0 - Выход ");

            string choice = Console.ReadLine();
            switch (choice) { 
                case "1": 
                    Authorisation(); 
                    break;

                case "2": 
                    Admin(); 
                    break;

                case "3": 
                    AddPerson(); 
                    break;

                case "0": 
                    Console.WriteLine("Выход из системы..."); 
                    return;

                default: 
                    Console.WriteLine("Неверный пункт меню!"); 
                    break;
            }
        }
    }
    
    public int GenerationID(string answer)
    {
        Console.WriteLine("Введите, для кого будет создан ID (student, teacher, discipline): ");

        if (answer == null)
        {
            answer = Console.ReadLine();
        }

        if (answer == "student")
        {
            int count = CollectStudentName.Count;
            personID = 2300000 + count + 1;
        }
        else if (answer == "teacher")
        {
            int count = CollectTeacherName.Count;
            personID = 2200000 + count + 1;
        }
        else if (answer == "discipline")
        {
            int count = CollectDisciplineID.Count;
            personID = 320000 + count + 1;
        }
        else
        {
            throw new Exception("Неизвестный тип ID");
        }

        return personID;
    }

    public string Authorisation()
    {
        Console.WriteLine("Введите ваше имя: ");
        personName = Console.ReadLine();
        student = personName;

        if (student != null)
        {
            Console.WriteLine("Введите пароль: ");
            password = int.Parse(Console.ReadLine());
            
            //string student = personName;
            
            personID = GenerationID("student");
            Console.WriteLine("Ваше имя в системе: " + student + 
                              "; Ваш пароль: " + password + 
                              "; Ваш ID в системе: " + personID);
            Console.WriteLine("НЕ ТЕРЯЙТЕ ЭТИ ДАННЫЕ!");
            
            CollectStudentName.Add(personID, student);
            CollectStudentID.Add(personID, personID);
            CollectStudentPassword.Add(personID, password);
        }

        else
        {
            Console.WriteLine("Имя не должно быть пустым!");
            personName = Authorisation();
        }
        
        return student;
    }
    
    public string Admin() {
        string personName = Console.ReadLine();
        if (personName == null)
        {
            Console.WriteLine("Ошибка: Пустое имя!");
            Admin();
        }
        
        Console.WriteLine("Введите свой ID: ");
        int keyID = int.Parse(Console.ReadLine());
        
        if (keyID == null)
        {
            Console.WriteLine("Ошибка: Пустое ID!");
            Admin();
        }
        
        Console.WriteLine("Введите свой пароль: ");
        int keyPassword = int.Parse(Console.ReadLine());
        
        if (keyPassword == null)
        {
            Console.WriteLine("Ошибка: Пустой пароль!");
            Admin();
        }
        
        admin = personName;
        
        CollectAdminName.Add(keyID, admin);
        CollectAdminID.Add(keyID, keyID);
        CollectAdminPassword.Add(keyID, keyPassword);
        
        return admin;
    }

    public string Teacher(string personName, int personID)
    {
        password = int.Parse(Console.ReadLine());
        
        if (password == passwordDataBase && personID > 0 && personID % 1000 == 15)
        {
            
            teacher = personName;
        }
        
        return teacher; 
    }

    public string Student(string personName, int personID)
    {
        if (personName != null && personID > 0 && personID % 1000 == 41)
        {
            
            student = personName;
        }
        
        return student;
    }

    public void AddPerson()
    {
        if (admin == personName)
        {
            Console.WriteLine("Введите имя нового пользователя: ");
            personName = Console.ReadLine();
            
            Console.WriteLine("Выберите должность: студент/преподаватель");
            string answer = Console.ReadLine();
            
            Console.WriteLine("Введите пароль");
            password = int.Parse(Console.ReadLine());

            if (personName == null && password % 10 * 6 < 1)
            {
                Console.WriteLine("Ошибка ввода данных!");
                AddPerson();
            }
            
            personID = GenerationID(answer);

            if (answer != null && answer == "student")
            {
                CollectStudentName.Add(personID, personName);
                CollectStudentID.Add(personID, personID);
                CollectStudentPassword.Add(personID, password);
            }
            if (answer != null && answer == "teacher") {
                CollectTeacherName.Add(personID, student);
                CollectTeacherID.Add(personID, personID);
                CollectTeacherPassword.Add(personID, password);
            }

            else
            {
                Console.WriteLine("Ошибка входных данных!");
                AddPerson();
            }
            
            Console.WriteLine("Внесены данные о: " + personName + 
                              ", должности " + answer + 
                              ", ID: " + personID + 
                              " и паролем: " + password);
        }

        else
        {
            Console.WriteLine("Ошибка, неверные данные!");
        }
    }
}