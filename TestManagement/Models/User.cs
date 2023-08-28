using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TestManagement.Models
{
    public class User
    {
        public string StudentId { get; set; }
        public string Contactno { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string TestType { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Ans { get; set; }
        public int Ans1 { get; set; }
        public int Mark1 { get; set; }
        public int Mark2 { get; set; }
        public string BehaviorType { get; set; }
        public DateTime submitted { get; set; }
        public int StatusId { get; set; }
        public int IsDelete { get; set; }
        public int TestPaperId { get; set; }
        public int Qgroup { get; set; }
        public string StudentName { get; set; }
        public string Type { get; set; }
        public string markingSystem { get; set; }
        public List<User> lstQue { get; set; }
        public DateTime TestSubmittedDate { get; set; }
        public List<User> studentlist { get; set; }
    }
}