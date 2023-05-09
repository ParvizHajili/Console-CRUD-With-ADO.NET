using CourseCrm.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCrm.DataAccess.SqlServer
{
    public class SqlRepository : IRepository
    {
        private readonly string ConnectionString;
        public SqlRepository()
        {
            ConnectionString = CreateConnectionString();
        }

        private string CreateConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "Localhost"; // database address
            builder.InitialCatalog = "MyCourse";
            builder.IntegratedSecurity = true; // WindowsAuthentication ==> true, SqlServerAuthentication ==> false

            string connectionString = builder.ConnectionString;

            return connectionString;
        }

        public IStudentRepository StudentRepository => new SqlStudentRepository(ConnectionString);
        public IPaymentRepository PaymentRepository => new SqlPaymentRepository(ConnectionString);
    }
}
