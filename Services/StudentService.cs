using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using StudentRecords.Models;

namespace StudentRecords.Services
{
    public class StudentService
    {
        private readonly string _xmlPath = "students.xml";

        public List<Student> GetStudents()
        {
            if (!File.Exists(_xmlPath)) return new List<Student>();

            var doc = XDocument.Load(_xmlPath);
            if (doc.Root == null)
                return new List<Student>();

            return doc.Root.Elements("Student")
                .Select(x => new Student
                {
                    StudentId = (string?)x.Element("StudentId") ?? string.Empty,
                    FullName = (string?)x.Element("FullName") ?? string.Empty,
                    Course = (string?)x.Element("Course") ?? string.Empty,
                    YearLevel = (string?)x.Element("YearLevel") ?? string.Empty,
                    Email = (string?)x.Element("Email") ?? string.Empty
                }).ToList();
        }

        public void AddStudent(Student student)
        {
            XDocument doc;
            if (File.Exists(_xmlPath))
            {
                doc = XDocument.Load(_xmlPath);
                if (doc.Root == null)
                {
                    doc.Add(new XElement("Students"));
                }
            }
            else
            {
                doc = new XDocument(new XElement("Students"));
            }

            doc.Root!.Add(new XElement("Student",
                new XElement("StudentId", student.StudentId ?? string.Empty),
                new XElement("FullName", student.FullName ?? string.Empty),
                new XElement("Course", student.Course ?? string.Empty),
                new XElement("YearLevel", student.YearLevel ?? string.Empty),
                new XElement("Email", student.Email ?? string.Empty)
            ));

            doc.Save(_xmlPath);
        }

        public void DeleteStudent(string studentId)
        {
            if (!File.Exists(_xmlPath)) return;

            var doc = XDocument.Load(_xmlPath);
            if (doc.Root == null) return;

            var studentElement = doc.Root.Elements("Student")
                .FirstOrDefault(x => ((string?)x.Element("StudentId") ?? string.Empty) == (studentId ?? string.Empty));

            if (studentElement != null)
            {
                studentElement.Remove();
                doc.Save(_xmlPath);
            }
        }

        public Student? GetStudentById(string id)
        {
            return GetStudents().FirstOrDefault(s => s.StudentId == id);
        }
        }
    }