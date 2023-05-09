using CourseCrm.Domain.Abstract;
using CourseCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CourseCrm.DataAccess.SqlServer
{
    public class SqlStudentRepository : SqlBaseRepository, IStudentRepository
    {
        public SqlStudentRepository(string connectionString) : base(connectionString)
        {
        }


        public bool Add(Student student)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = $"Insert into Students(Name, Surname, Balance,BirthDate) values(@name, @surname, @balance, @birthDate)";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@surname", student.Surname);
                    cmd.Parameters.AddWithValue("@balance", student.Balance);
                    cmd.Parameters.AddWithValue("@birthDate", student.BirthDate);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Update(Student student)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = $"update Students set Name = @name, Surname = @surname, Balance = @balance, BirthDate = @birthDate where Id = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("id", student.Id);
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@surname", student.Surname);
                    cmd.Parameters.AddWithValue("@balance", student.Balance);
                    cmd.Parameters.AddWithValue("@birthDate", student.BirthDate);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = $"delete from Students where Id = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public List<Student> Get()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = $"select Id, Name, Surname, BirthDate, Balance from Students";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student();

                        student.Id = Convert.ToInt32(reader["Id"]);
                        student.Name = Convert.ToString(reader["Name"]);
                        student.Surname = Convert.ToString(reader["Surname"]);
                        student.BirthDate = Convert.ToDateTime(reader["BirthDate"]);
                        student.Balance = Convert.ToDecimal(reader["Balance"]);

                        students.Add(student);
                    }
                    return students;
                }
            }
        }

        public Student Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = $"select Id, Name, Surname, BirthDate, Balance from Students where Id = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Student student = new Student();

                        student.Id = Convert.ToInt32(reader["Id"]);
                        student.Name = Convert.ToString(reader["Name"]);
                        student.Surname = Convert.ToString(reader["Surname"]);
                        student.BirthDate = Convert.ToDateTime(reader["BirthDate"]);
                        student.Balance = Convert.ToDecimal(reader["Balance"]);

                        return student;
                    }
                    return null;
                }
            }
        }
    }
}
