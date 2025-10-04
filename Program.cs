using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Linq;

//Абстрактный класс для общих характеристик людей
public abstract class Person
{
    private string name;
    private int age;
    private string contactInfo;

    public string Name
    {
        get => name;
        set => name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Имя не может быть пустым");
    }

    public int Age
    {
        get => age;
        set => age = value >= 0 ? value : throw new ArgumentException("Возраст не может быть отрицательным");
    }

    public string ContactInfo
    {
        get => contactInfo;
        set => contactInfo = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Контактная информация не может быть пустой");
    }

    protected Person(string name, int age, string contactInfo)
    {
        Name = name;
        Age = age;
        ContactInfo = contactInfo;
    }

    public abstract void DisplayInfo();
}
//Класс преподавателя
public class Teacher : Person
{
    private readonly List<Course> courses = new List<Course>();

    public Teacher(string name, int age, string contactInfo)
        : base(name, age, contactInfo) { }

    public void AssignCourse(Course course)
    {
        if (course == null) throw new ArgumentNullException(nameof(course));
        if (!courses.Contains(course))
        {
            courses.Add(course);
            course.SetTeacher(this);
        }
    }

    public IEnumerable<Course> GetCourses() => courses.AsReadOnly();

    public override void DisplayInfo()
    {
        Console.WriteLine($"Преподаватель: {Name}");
        Console.WriteLine($"Возраст: {Age}");
        Console.WriteLine($"Контакт: {ContactInfo}");

        if (GetCourses().Any())
        {
            Console.WriteLine("Ведет курсы:");
            foreach (var course in GetCourses())
            {
                Console.WriteLine($"- {course.Name}");
            }
        }
        else
        {
            Console.WriteLine("Не назначен ни на один курс");
        }
    }
}
//Класс студента
public class Student : Person
{
    private readonly List<Course> courses = new List<Course>();

    public Student(string name, int age, string contactInfo)
        : base(name, age, contactInfo) { }

    public void EnrollCourse(Course course)
    {
        if (course == null) throw new ArgumentNullException(nameof(course));
        if (!courses.Contains(course))
        {
            courses.Add(course);
            course.AddStudent(this);
        }
    }

    public IEnumerable<Course> GetCourses() => courses.AsReadOnly();

    public override void DisplayInfo()
    {
        Console.WriteLine($"Студент: {Name}");
        Console.WriteLine($"Возраст: {Age}");
        Console.WriteLine($"Контакт: {ContactInfo}");

        if (GetCourses().Any())
        {
            Console.WriteLine("Записан на курсы:");
            foreach (var course in GetCourses())
            {
                Console.WriteLine($"- {course.Name}");
            }
        }
        else
        {
            Console.WriteLine("Не записан ни на один курс");
        }
    }
}
//Класс курса
public class Course
{
    private readonly string name;
    private Teacher teacher;
    private readonly List<Student> students = new List<Student>();

    public string Name => name;
    public Teacher Teacher => teacher;
    public IEnumerable<Student> Students => students.AsReadOnly();

    public Course(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название курса не может быть пустым");
        this.name = name;
    }

    public void SetTeacher(Teacher teacher)
    {
        if (teacher == null) throw new ArgumentNullException(nameof(teacher));
        this.teacher = teacher;
    }

    public void AddStudent(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (!students.Contains(student))
        {
            students.Add(student);
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Курс: {Name}");
        Console.WriteLine($"Преподаватель: {(Teacher != null ? Teacher.Name : "Не назначен")}");

        if (Students.Any())
        {
            Console.WriteLine("Студенты:");
            foreach (var student in Students)
            {
                Console.WriteLine($"- {student.Name}");
            }
        }
        else
        {
            Console.WriteLine("Студентов нет");
        }
    }
}
//Центральный класс управления системой
public class University
{
    private readonly List<Student> students = new List<Student>();
    private readonly List<Teacher> teachers = new List<Teacher>();
    private readonly List<Course> courses = new List<Course>();

    public void AddStudent(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        students.Add(student);
    }
    public void AddTeacher(Teacher teacher)
    {
        if (teacher == null) throw new ArgumentNullException(nameof(teacher));
        teachers.Add(teacher);
    }

    public void AddCourse(Course course)
    {
        if (course == null) throw new ArgumentNullException(nameof(course));
        courses.Add(course);
    }

    public IEnumerable<Student> GetStudents() => students.AsReadOnly();
    public IEnumerable<Teacher> GetTeachers() => teachers.AsReadOnly();
    public IEnumerable<Course> GetCourses() => courses.AsReadOnly();

    public Student FindStudentByName(string name)
    {
        return students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public Teacher FindTeacherByName(string name)
    {
        return teachers.FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public Course FindCourseByName(string name)
    {
        return courses.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
class Program
{
    private readonly University university = new University();

    public void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Система управления университетом =====");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Добавить преподавателя");
            Console.WriteLine("3. Создать курс");
            Console.WriteLine("4. Записать студента на курс");
            Console.WriteLine("5. Просмотреть курсы студента");
            Console.WriteLine("6. Просмотреть студентов курса");
            Console.WriteLine("7. Просмотреть всех студентов");
            Console.WriteLine("8. Просмотреть всех преподавателей");
            Console.WriteLine("9. Просмотреть все курсы");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите опцию: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некорректный ввод. Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                continue;
            }
            try
            {
                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        AddTeacher();
                        break;
                    case 3:
                        CreateCourse();
                        break;
                    case 4:
                        EnrollStudentToCourse();
                        break;
                    case 5:
                        ViewStudentCourses();
                        break;
                    case 6:
                        ViewCourseStudents();
                        break;
                    case 7:
                        ViewAllStudents();
                        break;
                    case 8:
                        ViewAllTeachers();
                        break;
                    case 9:
                        ViewAllCourses();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

}