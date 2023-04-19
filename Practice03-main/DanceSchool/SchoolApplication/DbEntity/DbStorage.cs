using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApplication.DbEntity
{
    public static class DbStorage
    {
        public static examEntities DB_s { get; set; } = new examEntities();
    }
}
