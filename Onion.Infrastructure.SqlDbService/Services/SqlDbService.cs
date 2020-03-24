using Onion.Domain.Entities;
using Onion.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;



namespace Onion.Infrastructure.SqlDbService.Services
{
    public class SqlDbService : IStudentDbService
    {
        private static readonly ICollection<Student> _students = new List<Student>
        {
            new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski"},
            new Student{IdStudent=2, FirstName="Anna", LastName="Malewski"},
            new Student{IdStudent=3, FirstName="Andrzej", LastName="Maciejewski"}
        };

        public IEnumerable<Student> GetStudents()
        {
            MySqlConnection cnn;
            ICollection<Student> students = new List<Student>(); ;

            cnn = new MySqlConnection("Server=localhost;Port=3306;Database=pro_students;Uid=root;Pwd=root");
            try
            {
                cnn.Open();
                string sql = "SELECT * from students";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student student = new Student();

                    student.IdStudent = rdr.GetInt32(0);
                    student.FirstName = rdr.GetString(1);
                    student.LastName = rdr.GetString(1);

                    students.Add(student);
                }
                rdr.Close();
                Console.WriteLine("WORK");
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DOESNT WORK");
            }

            return students;
        }

        
        public bool EnrollStudent(Student newStudent, int semestr)
        {
            
                MySqlConnection cnn;

                cnn = new MySqlConnection("Server=localhost;Port=3306;Database=pro_students;Uid=root;Pwd=root");
                try
                {

                    cnn.Open();

                    string sql = "INSERT INTO students (studentId,firstName,lastName) VALUES ("+newStudent.IdStudent+ "," + newStudent.FirstName + ", " + newStudent.LastName + ")";
                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            
        }

 
        
    }
}
