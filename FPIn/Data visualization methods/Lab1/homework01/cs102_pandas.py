import pandas as pd
from typing import Tuple


def readFile(filename: str) -> pd.DataFrame:
    return pd.read_csv(filename)


def is_patronymic(value: str) -> bool:
    if pd.isna(value):
        return False

    value = str(value)

    male_endings = ("ович", "евич", "ич")
    female_endings = ("овна", "евна", "ична", "инична")

    return value.endswith(male_endings) or value.endswith(female_endings)


def gender_identification(patronym: str) -> str:
    if pd.isna(patronym):
        return "unknown"

    patronym = str(patronym)

    if patronym.endswith(("ович", "евич", "ич")):
        return "male"

    if patronym.endswith(("овна", "евна", "ична", "инична")):
        return "female"

    return "unknown"


def add_name_columns(data: pd.DataFrame) -> pd.DataFrame:
    data = data.copy()

    parts = data["фио"].str.split()

    data["фамилия"] = parts.str[0]
    data["имя"] = parts.str[1]
    data["отчество"] = parts.str[2]

    return data


# Задача 1
def filter_fsuir_students(data: pd.DataFrame) -> Tuple[int, int, pd.DataFrame]:
    """
    Создает подвыборку студентов факультета систем управления и робототехники (ФСУиР).
    Возвращает количество таких студентов, количество уникальных групп и отфильтрованный датасет.
    """
    data = add_name_columns(data)

    fsuir = data[
        data["факультет"] == "факультет систем управления и робототехники"
    ]

    students_count = len(fsuir)
    groups_count = fsuir["группа"].nunique()

    return students_count, groups_count, fsuir


# Задача 2
def find_homonymous_students(fsuir: pd.DataFrame) -> Tuple[bool, int, pd.Series, str]:

    fsuir = add_name_columns(fsuir)

    surname_counts = fsuir["фамилия"].value_counts()

    repeated_surnames = surname_counts[surname_counts > 1].index

    homonyms = fsuir[fsuir["фамилия"].isin(repeated_surnames)]

    has_homonyms = len(homonyms) > 0

    total_homonyms = len(homonyms)

    homonyms_per_course = homonyms["курс"].value_counts().sort_index()

    if total_homonyms == 0:
        max_group = ""
    else:
        max_group = homonyms["группа"].value_counts().idxmax()

    return has_homonyms, total_homonyms, homonyms_per_course, max_group


# Задача 3
def analyze_patronyms(fsuir: pd.DataFrame) -> Tuple[int, pd.Series]:
    """
    Определяет количество студентов без отчества и распределение студентов по полу на основе отчества.
    Возвращает:
     - количество студентов без отчества
     - серию с распределением студентов по полу
    """

    fsuir = add_name_columns(fsuir)

    without_patronym = fsuir["отчество"].isna().sum()

    students_with_patronym = fsuir[fsuir["отчество"].notna()].copy()

    genders = students_with_patronym["отчество"].apply(gender_identification)

    gender_counts = genders[genders != "unknown"].value_counts()

    return without_patronym, gender_counts


# Задача 4
def faculty_statistics(data: pd.DataFrame) -> Tuple[pd.Series, Tuple[str, int], Tuple[str, int]]:
    """
    Подсчитывает количество студентов на каждом факультете,
    а также определяет факультеты с максимальным и минимальным числом студентов.
    """

    faculty_counts = data["факультет"].value_counts()

    max_faculty = (faculty_counts.idxmax(), int(faculty_counts.max()))
    min_faculty = (faculty_counts.idxmin(), int(faculty_counts.min()))

    return faculty_counts, max_faculty, min_faculty


# Задача 5
def course_statistics(data: pd.DataFrame) -> Tuple[pd.Series, pd.Series]:
    """
    Вычисляет среднее и медианное число студентов на каждом курсе.
    Возвращает две серии с результатами: сначала средние, потом медиана.
    """

    students_by_course_and_faculty = data.groupby(["курс", "факультет"]).size()

    mean_by_course = students_by_course_and_faculty.groupby("курс").mean()
    median_by_course = students_by_course_and_faculty.groupby("курс").median()

    return mean_by_course, median_by_course


# Задача 6
def most_popular_name(data: pd.DataFrame) -> Tuple[str, str, str, str, float]:
    """
    Определяет самое популярное имя, группу с наибольшим количеством студентов с этим именем,
    факультет, курс и долю таких студентов в общем числе.
    Возвращает результат в следующем порядке:
     1. самое частое имя
     2. группа
     3. факультет
     4. курс
     5. доля
    """

    data = add_name_columns(data)

    name_counts = data["имя"].value_counts()

    popular_name = name_counts.idxmax()
    popular_name_count = name_counts.max()

    students_with_name = data[data["имя"] == popular_name]

    group_with_max = students_with_name["группа"].value_counts().idxmax()

    group_rows = students_with_name[
        students_with_name["группа"] == group_with_max
    ]

    faculty = group_rows.iloc[0]["факультет"]
    course = group_rows.iloc[0]["курс"]

    ratio = round(popular_name_count / len(data), 2)

    return popular_name, group_with_max, faculty, course, ratio


# Задача 7
def find_students_with_name_starting_P(data: pd.DataFrame) -> pd.DataFrame:
    """
    Находит студентов, чье имя встречается ровно один раз и начинается на "П". Выводит их ФИО, факультет и курс.
    """

    data = add_name_columns(data)

    name_counts = data["имя"].value_counts()

    unique_names = name_counts[name_counts == 1].index

    result = data[
        data["имя"].isin(unique_names)
        & data["имя"].str.startswith("П")
    ]

    return result[["фио", "факультет", "курс"]]


# Задача 8
def highest_avg_grade_faculty(data: pd.DataFrame) -> Tuple[str, str, int]:
    """
    Находит факультет, на котором средний балл студентов третьего курса самый высокий.
    Определяет пол, средний балл котого выше.
    Сначала возвращает факультет, затем пол, затем балл.
    """

    data = add_name_columns(data)

    third_course = data[data["курс"] == "3-й"].copy()

    faculty_avg = third_course.groupby("факультет")["средний_балл"].mean()

    best_faculty = faculty_avg.idxmax()

    best_faculty_students = third_course[
        third_course["факультет"] == best_faculty
    ].copy()

    best_faculty_students["пол"] = best_faculty_students["отчество"].apply(
        gender_identification
    )

    best_faculty_students = best_faculty_students[
        best_faculty_students["пол"] != "unknown"
    ]

    gender_avg = best_faculty_students.groupby("пол")["средний_балл"].mean()

    best_gender = gender_avg.idxmax()
    best_grade = round(gender_avg.max())

    return best_faculty, best_gender, best_grade


# Задача 9
def find_consecutive_students(data: pd.DataFrame) -> pd.DataFrame:
    """
    Находит первых 5 студентов, которым номера были присвоены подряд.
    Выводит их ФИО, факультет, курс и номер группы.
    """

    sorted_data = data.sort_values("ису").copy()

    for i in range(len(sorted_data) - 4):
        fragment = sorted_data.iloc[i:i + 5]

        isu_numbers = fragment["ису"].tolist()

        if all(isu_numbers[j + 1] - isu_numbers[j] == 1 for j in range(4)):
            return fragment[["фио", "факультет", "курс", "группа", "ису"]]

    return pd.DataFrame(columns=["фио", "факультет", "курс", "группа", "ису"])


if __name__ == "__main__":
    data = readFile("/Users/rafael/PycharmProjects/Lab1VDM/homework01/isu_fake_data.scv")

    print("\n#1")
    num_students, num_groups, fsuir = filter_fsuir_students(data)
    print("Количество студентов ФСУиР:", num_students)
    print("Количество групп ФСУиР:", num_groups)

    print("\n#2")
    has_homonyms, total_homonyms, homonyms_per_course, max_group = find_homonymous_students(fsuir)
    print("Есть однофамильцы:", has_homonyms)
    print("Всего однофамильцев:", total_homonyms)
    print("По курсам:")
    print(homonyms_per_course)
    print("Группа с максимумом однофамильцев:", max_group)

    print("\n#3")
    without_patronym, gender_counts = analyze_patronyms(fsuir)
    print("Студентов без отчества:", without_patronym)
    print("Распределение по полу:")
    print(gender_counts)

    print("\n#4")
    faculty_counts, max_faculty, min_faculty = faculty_statistics(data)
    print(faculty_counts)
    print("Больше всего:", max_faculty)
    print("Меньше всего:", min_faculty)

    print("\n#5")
    mean_students, median_students = course_statistics(data)
    print("Среднее число студентов по курсам:")
    print(mean_students)
    print("Медианное число студентов по курсам:")
    print(median_students)

    print("\n#6")
    popular_name, group, faculty, course, ratio = most_popular_name(data)
    print("Самое популярное имя:", popular_name)
    print("Группа:", group)
    print("Факультет:", faculty)
    print("Курс:", course)
    print("Доля:", ratio)

    print("\n#7")
    result_7 = find_students_with_name_starting_P(data)
    print(result_7)

    print("\n#8")
    best_faculty, best_gender, best_grade = highest_avg_grade_faculty(data)
    print("Факультет:", best_faculty)
    print("Пол:", best_gender)
    print("Средний балл:", best_grade)

    print("\n#9")
    result_9 = find_consecutive_students(data)
    print(result_9)