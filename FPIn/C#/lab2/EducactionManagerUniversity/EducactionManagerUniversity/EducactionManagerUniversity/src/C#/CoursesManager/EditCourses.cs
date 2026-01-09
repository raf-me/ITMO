using System;
using System.Collections.Generic;

namespace EducactionManagerUniversity.data.C_.CoursesManager;

public class EditCourses
{
    private string personName;
    private string courseName;
    private string student;
    private string teacher;
    private string admin;
    private int password;
    private int courseID;
    private int personID;
    
    private Dictionary<int, string> CollectNameDiscipline;
    private Dictionary<int, int> CollectDisciplineID;
    private Dictionary<int, string> CollectDisciplineComment;
    private Dictionary<int, string> CollectDisciplineCategory;
    private Dictionary<int, string> CollectDisciplineMob;
    
    private Dictionary<int, string> CollectAdminName;
    private Dictionary<int, int> CollectAdminID;
    private Dictionary<int, int> CollectAdminPassword;
    
    private Dictionary<int, string> CollectTeacherName;
    private Dictionary<int, int> CollectTeacherID;
    private Dictionary<int, int> CollectTeacherPassword;
    
    private Dictionary<int, List<int>> CollectCourseTeacher;
    private Dictionary<int, List<int>> CollectCourseStudents;

    private List<string> categoryDisc;

    public EditCourses()
    {
        CollectNameDiscipline = new Dictionary<int, string>();
        CollectDisciplineID = new Dictionary<int, int>();
        CollectDisciplineComment = new Dictionary<int, string>();
        CollectDisciplineCategory = new Dictionary<int, string>();
        CollectDisciplineMob = new Dictionary<int, string>();

        CollectAdminName = new Dictionary<int, string>();
        CollectAdminID = new Dictionary<int, int>();
        CollectAdminPassword = new Dictionary<int, int>();
        
        CollectTeacherName = new Dictionary<int, string>();
        CollectTeacherID = new Dictionary<int, int>();
        CollectTeacherPassword = new Dictionary<int, int>();

        CollectCourseTeacher = new Dictionary<int, List<int>>();
        CollectCourseStudents = new Dictionary<int, List<int>>();

        categoryDisc = new List<string>
        {
            "математика", "программирование", "теоретическая информатика",
            "русский язык", "английский язык", "физика",
            "биология", "экономика", "SoftSkils"
        };
    }

    public void 시험()
    {
        CollectAdminID.Add(2300000, 2300000);
        CollectAdminPassword.Add(2300000, 1234567);
        CollectAdminName.Add(2300000, "Ivan");
        
        CollectTeacherID.Add(2200000, 2200000);

        CollectDisciplineID.Add(321000, 321000);
        CollectNameDiscipline.Add(321000, "Oop");
        CollectDisciplineComment.Add(321000, "Hight level programming");
        CollectDisciplineMob.Add(321000, "offline");
        CollectDisciplineCategory.Add(321000, "программирование");
    }

    public void WorkWithCourses()
    {
        Console.WriteLine("Хотите ли вы создать курс (코스를 안딜고 싶습닉가) (да - создание курса; нет - работа с курсами)?");
        string 고나리 = Console.ReadLine();

        if (고나리 == "Yes" || 고나리 == "yes" || 고나리 == "Да" || 고나리 == "да" || 고나리 == "예")
        {
            코스추가();
        }

        else
        {

            Console.WriteLine("Выберите действие: \n" +
                              "1 - Создать курс (코스 안들기) \n" +
                              "2 - Назначить преподавателя на курс (코스예 교사 지성) \n" +
                              "3 - Вывести список студентов на курсе (코스의 학생 옥록 표시) \n" +
                              "4 - Вывести курсы, которые ведёт преподаватель (교사 과정) \n" +
                              "5 - Записать студента на курс (학생 기록) \n" +
                              "0 - Выйти");
            고나리 = Console.ReadLine();

            if (고나리 == "1")
            {
                코스추가();
            }

            if (고나리 == "2")
            {
                AssignTeacherCourse();
            }

            if (고나리 == "3")
            {
                ShowStudentsCourse();
            }

            if (고나리 == "4")
            {
                ShowCoursesTeacher();
            }

            if (고나리 == "5")
            {
                EnrollStudentCourse();
            }

            if (고나리 == "0")
            {
                return;
            }
        }
    }

    public void 코스곤리()
    {
        시험();
        AuthPerson();
        WorkWithCourses();
    }

    public void 코스추가() {
        courseID = CreateIDCourses();
        
        try
        {
            CreateNameForCourses(courseID);
            DisciplinesAdd(courseID);
            CommentAdd(courseID);
            NewCoursesMobile(courseID);

            WorkWithCourses();
        }

        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка:");
            Console.WriteLine(ex.Message);
            RemoveCourses(courseID);

            bool removed1 = CollectDisciplineID.Remove(courseID);
            if (!removed1)
            {
                Console.WriteLine("ID не найден!");
            }
            else
            {
                Console.WriteLine("ID удалён!");
            }
        }
    }

    public void AuthPerson() {
        string Permission;
        Console.WriteLine("Введите свой ID: ");
        personID = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Введите пароль: ");
        password = int.Parse(Console.ReadLine());

        if (CollectAdminID.ContainsKey(personID) && CollectAdminPassword[personID] == password) {
            Console.WriteLine("Редактирует - " + CollectAdminName.ContainsKey(personID));
        }

        else {
            Console.WriteLine("Ошибка ввода!");
            AuthPerson();
        }

        Permission = "Permission_Granted_1";
    }

    public string CreateNameForCourses(int courseID)
    {
        Console.WriteLine("Введите название нового курса: ");
        courseName = Console.ReadLine();
        
        if (courseName == null) {
            Console.WriteLine("Название не должно быть пустым!");
            CreateNameForCourses(courseID);
        }

        CollectNameDiscipline.Add(courseID, courseName); 
        
        //courseID = "Permission_Granted_2";
        return courseName;
    }

    public int CreateIDCourses()
    {
        PersonManager manager = new PersonManager();
        courseID = manager.GenerationID("discipline");
        
        if (courseID == null) { CreateIDCourses();}
        
        CollectDisciplineID.Add(courseID, courseID);
        return courseID;
    }

    public string CommentAdd(int courseID)
    {
        //if (Permission != "Permission_Granted_2") { AuthPerson(); }
        
        string comment;
        Console.WriteLine("Сделайте описание дисциплины: ");
        comment = Console.ReadLine();
        
        if (comment != null || comment != " ") { CollectDisciplineComment.Add(courseID, comment); }
        else { CommentAdd(courseID); }
        
        return comment;
    }

    public void DisciplinesAdd(int courseID)
    {
        Console.WriteLine("Выберите категорию из доступных "); 
        Console.WriteLine("(математика, программирование, теоретическая информатика, русский язык, английский язык, физика, биология, экономика, SoftSkils");
                
        List<string> categoryDisc = [
            "математика", "программирование", "теоретическая информатика", "русский язык",
            "английский язык", "физика", "биология", "экономика", "SoftSkils"
        ]; 
        
        string category = Console.ReadLine();

        if (category != null || categoryDisc.Contains(category)) {
        
            CollectDisciplineCategory.Add(courseID, category);
        }

        else {
            Console.WriteLine("Неправильный ввод данных!");
            DisciplinesAdd(courseID);
        }
    }
    
    public void NewCoursesMobile(int courseID) {
        Console.WriteLine("Выберите формат занятий (offline / online): ");
            
        string Permission = Console.ReadLine();
        
        if (Permission == "offline" || Permission == "online") {
            Console.WriteLine();
            CollectDisciplineMob.Add(courseID, Permission);
        }

        else {
            Console.WriteLine("Неверный ввод данных!");
            NewCoursesMobile(courseID);
        }
    }
    
    public void AssignTeacherCourse()
    {
        Console.WriteLine("Введите ID курса:");
        int courseID = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите ID преподавателя:");
        int teacherID = int.Parse(Console.ReadLine());

        if (!CollectCourseTeacher.ContainsKey(courseID))
        {
            CollectCourseTeacher[courseID] = new List<int>();
        }

        if (!CollectCourseTeacher[courseID].Contains(teacherID))
        {
            CollectCourseTeacher[courseID].Add(teacherID);
            Console.WriteLine("Преподаватель добавлен на курс");
        }
        else
        {
            Console.WriteLine("Этот преподаватель уже ведёт курс");
        }
        WorkWithCourses();
    }
    
    public void EnrollStudentCourse()
    {
        Console.WriteLine("Введите ID курса:");
        int courseID = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите ID студента:");
        int studentID = int.Parse(Console.ReadLine());

        if (!CollectCourseStudents.ContainsKey(courseID))
            CollectCourseStudents[courseID] = new List<int>();

        CollectCourseStudents[courseID].Add(studentID);

        Console.WriteLine("Студент записан на курс");
        WorkWithCourses();
    }
    
    public void ShowStudentsCourse()
    {
        Console.WriteLine("Введите ID курса:");
        int courseID = int.Parse(Console.ReadLine());

        if (!CollectCourseStudents.ContainsKey(courseID))
        {
            Console.WriteLine("На курсе нет студентов");
            return;
        }

        Console.WriteLine("Студенты курса:");
        foreach (var id in CollectCourseStudents[courseID])
        {
            Console.WriteLine("ID студента: " + id);
        }
        WorkWithCourses();
    }
    
    public void ShowCoursesTeacher()
    {
        Console.WriteLine("Введите ID преподавателя:");
        int teacherID = int.Parse(Console.ReadLine());

        bool found = false;

        foreach (var pair in CollectCourseTeacher)
        {
            int courseID = pair.Key;
            List<int> teachers = pair.Value;

            if (teachers.Contains(teacherID))
            {
                Console.WriteLine($"Курс ID: {courseID}, Название: {CollectNameDiscipline[courseID]}");
                found = true;
            }
        }

        if (!found) { Console.WriteLine("У преподавателя нет курсов"); }
        WorkWithCourses();
    }

    public void RemoveCourses(int courseID)
    {
        if (courseID != null || courseID > 0)
        {
            bool removed2 = CollectNameDiscipline.Remove(courseID);
            if (!removed2) { Console.WriteLine("Дисциплина не найдена!"); }
            else {Console.WriteLine("Дисциплина удалёна!");}
            
            bool removed3 = CollectDisciplineComment.Remove(courseID);
            if (!removed3) { Console.WriteLine("Коммент не найден!"); }
            else {Console.WriteLine("Коммент удалён!");}
            
            bool removed4 = CollectDisciplineCategory.Remove(courseID);
            if (!removed4) { Console.WriteLine("Категория не найдена!"); }
            else {Console.WriteLine("Категория удалён!");}
            
            bool removed5 = CollectDisciplineMob.Remove(courseID);
            if (!removed5) { Console.WriteLine("Формат не найден!"); }
            else {Console.WriteLine("Формат удалён!");}
        }
    }
}