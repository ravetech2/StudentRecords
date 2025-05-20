using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecords.Models;

namespace StudentRecords.Services
{
    public class StudentService
    {
        // Using an in-memory list to store students. In production,
        // consider thread-safety or using a database.
        private readonly List<Student> students = new();

        // Add a new student
        public Task AddStudentAsync(Student student)
        {
            students.Add(student);
            return Task.CompletedTask;
        }

        // Retrieve all students
        public Task<List<Student>> GetStudentsAsync()
        {
            return Task.FromResult(students);
        }

        // Retrieve a student by their ID
        public Task<Student?> GetStudentByIdAsync(string id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
            return Task.FromResult(student);
        }

        // Update an existing student by matching the StudentId.
        // This method assumes that 'updatedStudent' contains the latest information.
        public Task UpdateStudentAsync(Student updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.StudentId == updatedStudent.StudentId);
            if (student != null)
            {
                // Update the properties. You could add additional logic if needed.
                student.FullName = updatedStudent.FullName;
                student.Course = updatedStudent.Course;
                student.YearLevel = updatedStudent.YearLevel;
                student.Email = updatedStudent.Email;
            }
            return Task.CompletedTask;
        }

        // Delete a student by their ID
        public Task DeleteStudentAsync(string id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
            if (student != null)
            {
                students.Remove(student);
            }
            return Task.CompletedTask;
        }
    }
}