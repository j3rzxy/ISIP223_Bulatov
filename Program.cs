using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Linq;

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
class Teacher : Person
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

class Student : Person
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
class Course
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
    static void ShowMenu()
    {
        Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ УНИВЕРСИТЕТОМ ===");
        Console.WriteLine("Выберите с какой сущностью вы хотите работать:");
        Console.WriteLine("1. Студент");
        Console.WriteLine("2. Преподаватель");
        Console.WriteLine("3. Курс");
        Console.WriteLine("0. Выход");
    }
    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":

                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                    case "0":
                        Console.WriteLine("Выход из программы. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите пункт от 0 до 3.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}