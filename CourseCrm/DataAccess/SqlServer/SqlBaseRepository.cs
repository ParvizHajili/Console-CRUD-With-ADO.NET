using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCrm.DataAccess.SqlServer
{
    public abstract class SqlBaseRepository
    {
        protected readonly string ConnectionString;
        public SqlBaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
