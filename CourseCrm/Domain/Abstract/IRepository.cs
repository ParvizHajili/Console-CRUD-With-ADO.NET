using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCrm.Domain.Abstract
{
    public interface IRepository
    {
        IStudentRepository StudentRepository { get; }
        IPaymentRepository PaymentRepository { get; }
    }
}
