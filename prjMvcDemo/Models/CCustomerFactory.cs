using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CCustomerFactory
    {
        public void Update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "Update tCustomer set";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " fName=@K_FNAME, ";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " fPhone=@K_FPhone, ";
                paras.Add(new SqlParameter("K_FPhone", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " fEmail=@K_FEmail, ";
                paras.Add(new SqlParameter("K_FEmail", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " fAddress=@K_FAddress, ";
                paras.Add(new SqlParameter("K_FAddress", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " fPassword=@K_FPassword, ";
                paras.Add(new SqlParameter("K_FPassword", (object)p.fPassword));
            }
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " WHERE fId=@K_FID";
            paras.Add(new SqlParameter("K_FID", (object)p.fId));
            databaseCRUD(sql, paras);

        }

        internal CCustomer queryByEmail(string emil)
        {
            string sql = "select* from tCustomer where fEmail=@K_Email";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@K_Email", (object)emil));
            var list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }

        internal List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "select* from tCustomer where fName like @K_Keyword ";
            sql+= " or fPhone like @K_Keyword ";
            sql += " or fAddress like @K_Keyword ";
            sql += " or fEmail like @K_Keyword ";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_Keyword", "%"+(object)keyword+"%"));
            return queryBySql(sql, paras);      
        }

        public void delete(int id)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "delete from tCustomer where fId=@K_Id";
            paras.Add(new SqlParameter("@K_Id", (object)id));
            databaseCRUD(sql, paras);
        }
        public void insert(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer(";
            if (!string.IsNullOrEmpty(p.fName))
                sql += " fName, ";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += " fPhone, ";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += " fEmail, ";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += " fAddress, ";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += " fPassword, ";
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " )VALUES( ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " @K_FNAME, ";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " @K_FPHONE, ";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " @K_FEMAIL, ";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " @K_FADDRESS, ";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " @K_FPASSWORD, ";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            databaseCRUD(sql, paras);
        }
        public CCustomer queryById(int id)
        {
            string sql= "select* from tCustomer where fId=@K_Id";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@K_Id", (object)id));
            var list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }
        public List<CCustomer> queryAll()
        {
            return queryBySql("select*from tCustomer", null);
        }
        private List<CCustomer> queryBySql(string sql,List<SqlParameter>paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand com = new SqlCommand(sql, con);
            if (paras != null)
                foreach (SqlParameter p in paras)
                    com.Parameters.Add(p);

            SqlDataReader reader= com.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CCustomer()
                {
                    fId=(int)reader["fId"],
                    fName = reader["fName"].ToString(),
                    fAddress = reader["fAddress"].ToString(),
                    fEmail = reader["fEmail"].ToString(),
                    fPassword = reader["fPassword"].ToString(),
                    fPhone = reader["fPhone"].ToString(),
                });
            }
            con.Close();
            return list;           
        }
        private static void databaseCRUD(string sq1 , List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand com = new SqlCommand(sq1, con);
            if (paras != null)
                foreach (SqlParameter p in paras)
                    com.Parameters.Add(p);

            com.ExecuteNonQuery();
            con.Close();
        }
    }
}