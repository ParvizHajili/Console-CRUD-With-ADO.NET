using CourseCrm.DataAccess.SqlServer;
using CourseCrm.Domain.Abstract;
using CourseCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CourseCrm
{
    class Program
    {
        private static IRepository Repository => new SqlRepository();

        static void AddNewStudent()
        {
            Student student = new Student();

            try
            {
                Console.WriteLine("Please enter student name:");
                student.Name = Console.ReadLine();

                Console.WriteLine("Please enter student surname:");
                student.Surname = Console.ReadLine();

                Console.WriteLine("Please enter student birth date: (format: dd.MM.yyyy)");
                string birthDate = Console.ReadLine();
                student.BirthDate = DateTime.ParseExact(birthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine("Please enter student balance:");
                student.Balance = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Input is invalid. Please enter correct data!!!");
            }

            try
            {
                Repository.StudentRepository.Add(student);
            }
            catch
            {
                Console.WriteLine("Unknown exception occured. Please try again!");
            }
        }

        static void UpdateStudent()
        {
            Console.WriteLine("Please enter Id of the student which you want to update");
            int id = Convert.ToInt32(Console.ReadLine());

            Student student = Repository.StudentRepository.Get(id);

            if(student == null)
            {
                Console.WriteLine($"There is no any student whose Id is {id}");
            }
            else
            {
                try
                {
                    Console.WriteLine("Please enter student new name:");
                    student.Name = Console.ReadLine();

                    Console.WriteLine("Please enter student new surname:");
                    student.Surname = Console.ReadLine();

                    Console.WriteLine("Please enter student new birth date: (format: dd.MM.yyyy)");
                    string birthDate = Console.ReadLine();
                    student.BirthDate = DateTime.ParseExact(birthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Please enter student new balance:");
                    student.Balance = Convert.ToDecimal(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Input is invalid. Please enter correct data!!!");
                }

                Repository.StudentRepository.Update(student);
            }
        }

        static void DeleteStudent()
        {
            Console.WriteLine("Please enter Id of the student which you want to delete");
            int id = Convert.ToInt32(Console.ReadLine());

            Student student = Repository.StudentRepository.Get(id);

            if (student != null)
            {
                Repository.StudentRepository.Delete(id);
            }
            else
            {
                Console.WriteLine($"There is no any student whose Id is {id}");
            }
        }

        static void OperatePaymentsForStudent()
        {
            Console.WriteLine("Please enter Id of the student whose payments you want to operate");
            int id = Convert.ToInt32(Console.ReadLine());

            Student student = Repository.StudentRepository.Get(id);

            if (student != null)
            {
                while(true)
                {
                    List<Payment> payments = Repository.PaymentRepository.GetByStudent(id);

                    foreach (var payment in payments)
                    {
                        Console.WriteLine($"{payment.Id} {payment.Student.Name} {payment.Student.Surname} {payment.Amount}");
                    }

                    Console.WriteLine($"If you want to add new payment for {student.Name} {student.Surname}, please click 1");
                    Console.WriteLine($"If you want to return previuos menu, please click 2");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                AddPayment(student.Id);
                                break;
                            }
                        case 2:
                            {
                                return;
                            }
                    }

                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"There is no any student whose Id is {id}");
            }
        }

        static void AddPayment(int studentId)
        {
            Payment payment = new Payment()
            {
                Student = new Student() { Id = studentId }
            };

            Console.WriteLine("Please enter payment amount:");
            payment.Amount = Convert.ToDecimal(Console.ReadLine());

            Repository.PaymentRepository.Add(payment);
        }

        static void OperateStudents()
        {
            while(true)
            {
                Console.WriteLine("The list of the Students:");
                List<Student> students = Repository.StudentRepository.Get();

                foreach (var student in students)
                {
                    Console.WriteLine($"{student.Id} {student.Name} {student.Surname} {student.BirthDate.ToString("dd.MM.yyyy")} {student.Balance}");
                }

                Console.WriteLine("To add new student please click 1");
                Console.WriteLine("To update selected student please click 2");
                Console.WriteLine("To delete selected student please click 3");
                Console.WriteLine("To see all the payments for selected student please click 4");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            AddNewStudent();
                            break;
                        }
                    case 2:
                        {
                            UpdateStudent();
                            break;
                        }
                    case 3:
                        {
                            DeleteStudent();
                            break;
                        }
                    case 4:
                        {
                            OperatePaymentsForStudent();
                            break;
                        }
                }

                Console.Clear();
            }
        }

        static void Main(string[] args)
        {
            // Console opening...
            // Show all the students to the user
            // Insert new student click 1
            // Update selected student click 2
            // Delete selected student click 3
            // Show all the payments for selected student click 4
            // Add new payment for selected student click 1

            Console.WriteLine("Welcome!!!");

            OperateStudents();         
        }
    }
}
