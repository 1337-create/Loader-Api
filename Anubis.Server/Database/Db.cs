using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Server.Database
{
    public class Db
    {
        private static Db m_instance;

        public static Db GetInstance()
        {
            if (m_instance == null)
                m_instance = new Db();

            return m_instance;
        }

        public DatabaseContext CreateContext() 
            => new DatabaseContext();
    }
}
