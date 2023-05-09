using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCrm.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Student Student { get; set; }
        public decimal Amount { get; set; }
    }
}
