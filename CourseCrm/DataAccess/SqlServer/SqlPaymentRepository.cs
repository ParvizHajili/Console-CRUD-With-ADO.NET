using CourseCrm.Domain.Abstract;
using CourseCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CourseCrm.DataAccess.SqlServer
{
    public class SqlPaymentRepository : SqlBaseRepository, IPaymentRepository
    {
        public SqlPaymentRepository(string connectionString) : base(connectionString)
        {
        }

        public List<Payment> GetPayments()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = @"select p.Id as PaymentId, Amount, s.Id as StudentId, s.Name, 
                                   s.Surname, s.Balance from Accounting.Payments as p 
                                   Inner Join Students as s ON p.StudentId = s.Id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Payment> payments = new List<Payment>();

                    while (reader.Read())
                    {
                        Payment payment = GetFromReader(reader);
                        payments.Add(payment);
                    }

                    return payments;
                }
            }
        }

        public Payment Get(int id)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = @"select p.Id as PaymentId, Amount, s.Id as StudentId, s.Name, 
                                   s.Surname, s.Balance from Accounting.Payments as p 
                                   Inner Join Students as s ON p.StudentId = s.Id where Id = @id";

                using(SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        Payment payment = GetFromReader(reader);
                        return payment;
                    }

                    return null;
                }
            }
        }

        public List<Payment> GetByStudent(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = @"select p.Id as PaymentId, Amount, s.Id as StudentId, s.Name, 
                                   s.Surname, s.Balance from Accounting.Payments as p 
                                   Inner Join Students as s ON p.StudentId = s.Id where s.Id = @studentId";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Payment> payments = new List<Payment>();

                    while (reader.Read())
                    {
                        Payment payment = GetFromReader(reader);
                        payments.Add(payment);
                    }

                    return payments;
                }
            }
        }

        public bool Add(Payment payment)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string cmdText = "Insert into Accounting.Payments values(@StudentId, @Amount)";

                using(SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentId", payment.Student.Id);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        private Payment GetFromReader(SqlDataReader reader)
        {
            Payment payment = new Payment();

            payment.Id = Convert.ToInt32(reader["PaymentId"]);
            payment.Amount = Convert.ToDecimal(reader["Amount"]);
            payment.Student = new Student();
            payment.Student.Id = Convert.ToInt32(reader["StudentId"]);
            payment.Student.Name = Convert.ToString(reader["Name"]);
            payment.Student.Surname = Convert.ToString(reader["Surname"]);
            payment.Student.Balance = Convert.ToDecimal(reader["Balance"]);

            return payment;
        }
    }
}
