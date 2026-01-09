using System.Collections.Generic;

namespace EducactionManagerUniversity.data.C_.CoursesManager;

public class Classification
{
    public Dictionary<int, string> CollectStudentName { get; }
    public Dictionary<int, int> CollectStudentID { get; }
    public Dictionary<int, int> CollectStudentPassword { get; }

    public Dictionary<int, int> CollectDisciplineID { get; }

    public Dictionary<int, string> CollectAdminName { get; }
    public Dictionary<int, int> CollectAdminID { get; }
    public Dictionary<int, int> CollectAdminPassword { get; }

    public Dictionary<int, string> CollectTeacherName { get; }
    public Dictionary<int, int> CollectTeacherID { get; }
    public Dictionary<int, int> CollectTeacherPassword { get; }

    public Dictionary<int, string> CollectNameDiscipline { get; }
    public Dictionary<int, string> CollectDisciplineComment { get; }
    public Dictionary<int, string> CollectDisciplineCategory { get; }
    public Dictionary<int, string> CollectDisciplineMob { get; }

    public Dictionary<int, List<int>> CollectCourseTeacher { get; }
    public Dictionary<int, List<int>> CollectCourseStudents { get; }
    
    public Classification()
    {
        CollectStudentName = new();
        CollectStudentID = new();
        CollectStudentPassword = new();

        CollectDisciplineID = new();

        CollectAdminName = new();
        CollectAdminID = new();
        CollectAdminPassword = new();

        CollectTeacherName = new();
        CollectTeacherID = new();
        CollectTeacherPassword = new();

        CollectNameDiscipline = new();
        CollectDisciplineComment = new();
        CollectDisciplineCategory = new();
        CollectDisciplineMob = new();

        CollectCourseTeacher = new();
        CollectCourseStudents = new();
    }
}