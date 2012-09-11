using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Johnson.Process.Core
{
    public class ProcessEmailDataProvider
    {
        static ProcessEmailDataProvider _current;
        public static ProcessEmailDataProvider Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new ProcessEmailDataProvider();
                }
                return _current;
            }
        }

        private ProcessEmailDataProvider()
        {

        }

        private const string SQL_INSERT = "insert wf_processMail (email, subject, [content], status) values (@email, @subject, @content, @status)";

        private const string SQL_UPDATE_STATUS = "update wf_processMail set status = @status where id = @id";

        private const string SQL_SELECT_STATUS_IS_0 = "select *from wf_processMail where status = 0";

        public void Insert(string email, string subject, string content)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("email", email));
            paras.Add(new SqlParameter("subject", subject));
            paras.Add(new SqlParameter("content", content));
            SqlParameter para = new SqlParameter("status", System.Data.SqlDbType.Int);
            para.Value = 0;
            paras.Add(para);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, SQL_INSERT, System.Data.CommandType.Text, paras.ToArray());
            }
        }

        public void UpdateStatusAs1(int id)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("id", id),
                new SqlParameter("status", 1),
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, SQL_UPDATE_STATUS, System.Data.CommandType.Text, paras);
            }
        }

        public List<ProcessEmailEntity> SelectStatusIs0()
        {
            List<ProcessEmailEntity> list = new List<ProcessEmailEntity>();
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(conn, SQL_SELECT_STATUS_IS_0, System.Data.CommandType.Text, null);
                while(reader.Read())
                {
                    list.Add(this.Map(reader));
                }
            }
            return list;
        }

        private ProcessEmailEntity Map(SqlDataReader reader)
        {
            ProcessEmailEntity entity = new ProcessEmailEntity();
            entity.ID = reader.GetInt32(0);
            entity.Email = reader.GetString(1);
            entity.Subject = reader.GetString(2);
            entity.Content = reader.GetString(3);
            entity.Status = reader.GetInt32(4);
            return entity;
        }
    }
}
