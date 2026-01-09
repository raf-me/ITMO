using System;
using EducactionManagerUniversity.data.C_.CoursesManager;

namespace EducactionManagerUniversity.data.C_;

public class 실현
{
    public void FuncionalMethod()
    {
        Console.WriteLine("Выберите действие: {1 - Создать пользователя (사요자 만들기); 2 - Работа с курсами (커스 작업)}");
        string answer = Console.ReadLine();
        if (answer == "1" || answer == "사요자 만들기")
        {
           PersonManager personManager = new PersonManager();
           personManager.관리자사람();
        }

        if (answer == "2" || answer == "커스 작업")
        {
            EditCourses editCourses = new EditCourses();
            editCourses.코스곤리();
        }
    }
}