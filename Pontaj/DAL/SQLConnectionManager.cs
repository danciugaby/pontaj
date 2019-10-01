using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DAL
{
    class SQLConnectionManager : IDisposable
    {
        SQLiteConnection m_dbConnection;

        public SQLiteConnection DbConnection { get => m_dbConnection; }

        public SQLConnectionManager()
        {
            m_dbConnection = new SQLiteConnection("Data Source=identifier.sqlite;Version=3;");
           
        }
        public void Open()
        {
            DbConnection.Open();
        }
        public void Close()
        {
            DbConnection.Close();
        }

        public void Dispose()
        {
            if (m_dbConnection != null)
                Close();
            m_dbConnection = null;
        }
    }
}
