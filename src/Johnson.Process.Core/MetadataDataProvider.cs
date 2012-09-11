using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Johnson.Process.Core
{
    public class MetadataDataProvider
    {
        static MetadataDataProvider _current;
        public static MetadataDataProvider Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new MetadataDataProvider();
                }
                return _current;
            }
        }

        private MetadataDataProvider()
        {

        }

        private const string SQL_INSERT = "insert wf_metadata values (@meta_name, @meta_value)";
        private const string SQL_UPDATE = "update wf_metadata set meta_value = @meta_value where meta_name = @meta_name";
        private const string SQL_SELECT_BY_NAME = "select *from wf_metadata where meta_name = @meta_name";
        private const string SQL_COUNT_BY_NAME = "select count(1) from wf_metadata where meta_name = @meta_name";

        public void Insert(string name, string value)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("meta_name", name));
            paras.Add(new SqlParameter("meta_value", value));
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, SQL_INSERT, System.Data.CommandType.Text, paras.ToArray());
            }
        }

        public void Update(string name, string value)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("meta_name", name));
            if (string.IsNullOrEmpty(value))
            {
                paras.Add(new SqlParameter("meta_value", DBNull.Value));
            }
            else
            {
                paras.Add(new SqlParameter("meta_value", value));
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, SQL_UPDATE, System.Data.CommandType.Text, paras.ToArray());
            }
        }

        public void Set(string name, string value)
        {
            if (this.Exists(name))
            {
                this.Update(name, value);
            }
            else
            {
                this.Insert(name,value);
            }
        }

        public bool Exists(string name)
        {
            bool exists;
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("meta_name", name));
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                int count = (int)SqlHelper.ExecuteScalar(conn, SQL_COUNT_BY_NAME, System.Data.CommandType.Text, paras.ToArray());
                exists = count > 0;
            }
            return exists;
        }

        public string SelectValue(string name)
        {
            string value = null;
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("meta_name", name));
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(conn, SQL_SELECT_BY_NAME, System.Data.CommandType.Text, paras.ToArray());
                if (reader.Read())
                {
                    if (reader["meta_value"] != DBNull.Value)
                    {
                        value = reader["meta_value"].ToString();
                    }
                }
            }
            return value;
        }
    }
}
