using CourseCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCrm.Domain.Abstract
{
    public interface IStudentRepository
    {
        List<Student> Get();
        Student Get(int id);
        bool Add(Student student);
        bool Update(Student student);
        bool Delete(int id);
    }
}
