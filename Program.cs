using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Linq;

class Person
{
    private string Surname {  get; set; }
    private string Name { get; set; }
    private int Age { get; set; }
    private string Contacts { get; set; }
    public DateOnly Birthday { get; set; }
    public char Gender { get; set; }

    public Person(string surname, string name, int age, string contacts, DateOnly birthday, char gender)
    {
        Surname = surname;
        Name = name;
        Age = age;
        Contacts = contacts;
        Birthday = birthday;
        Gender = gender;
    }
}
class Prepodavatel : Person
{
    private int Salary { get; set; }
    private string EducationLevel { get; set; }

    public Prepodavatel(string surname, string name, int age, string contacts, DateOnly birthday, char gender, int salary, string educationlevel)
        :base(surname, name, age, contacts, birthday, gender)
    {
        Salary = salary;
        EducationLevel = educationlevel;
    }
}

class Student : Person
{
    public int StudentNumberID { get; set; }
    public string HealthGroup { get; private set; }
    public int CourseNumber { get; private set; }

    public Student(string surname, string name, int age, string contacts, DateOnly birthday, char gender, int studentnumberid, string healthgroup, int coursenumber)
        :base(surname, name, age, contacts, birthday, gender)
    {
        StudentNumberID = studentnumberid;
        HealthGroup = healthgroup;
        CourseNumber = coursenumber;
    }
}