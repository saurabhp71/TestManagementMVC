using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TestManagement.Models;

namespace TestManagement.Models
{
    public class BAL
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-RU5490M;Initial Catalog=TestManagement;Integrated Security=True");

        public SqlDataReader GetID(User obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPTMClient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetID");
            cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public DataTable GetQuestion(User obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPTMClient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetQuestion");
            cmd.Parameters.AddWithValue("@TestPaperId", obj.TestPaperId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataTable dtQue = new DataTable();
            adpt.Fill(dtQue);
            return dtQue;
            con.Close();
        }
        public SqlDataReader getdata(User obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SPTMClient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "getdata");
            cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
            cmd.Parameters.AddWithValue("@QueId", obj.QuestionId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void saveANS(User obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SPTMClient", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Flag", "SaveAns");
            cmd1.Parameters.AddWithValue("@StudentId", obj.StudentId);
            cmd1.Parameters.AddWithValue("@QueId", obj.QuestionId);
            cmd1.Parameters.AddWithValue("@BehaviorType", obj.BehaviorType);
            cmd1.Parameters.AddWithValue("@Ans", obj.Ans1);
            cmd1.Parameters.AddWithValue("@Mark1", obj.Mark1);
            cmd1.Parameters.AddWithValue("@Mark2", obj.Mark2);
            cmd1.Parameters.AddWithValue("@TestSubmittedDate", obj.TestSubmittedDate);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateAans(User obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SPTMClient", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Flag", "UpdateAans");
            cmd1.Parameters.AddWithValue("@StudentId", obj.StudentId);
            cmd1.Parameters.AddWithValue("@QueId", obj.QuestionId);
            cmd1.Parameters.AddWithValue("@Ans", obj.Ans1);
            cmd1.Parameters.AddWithValue("@Mark1", obj.Mark1);
            cmd1.Parameters.AddWithValue("@Mark2", obj.Mark2);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        public DataSet allresultPending()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPTMAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "allresultPending");
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
            con.Close();
        }
        public DataTable Getsum(User obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SPTMAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getsum");
            cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
            con.Close();
        }
        public DataTable Getvyaktitvank(User obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SPTMAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getvyaktitvank");
            cmd.Parameters.AddWithValue("@StudentId", obj.StudentId);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);
            return dt1;
            con.Close();

        }
    }
}