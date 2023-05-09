using CourseCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCrm.Domain.Abstract
{
    public interface IPaymentRepository
    {
        List<Payment> GetPayments();
        Payment Get(int id);
        List<Payment> GetByStudent(int studentId);
        bool Add(Payment payment);
    }
}
