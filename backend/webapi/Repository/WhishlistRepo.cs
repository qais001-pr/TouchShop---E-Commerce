using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Repository
{
    public class WhishlistRepo
    {
        public readonly string connectionString;
        public WhishlistRepo(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public async Task GetWhishlistByUserId(int id)
        {

        }
    }
}