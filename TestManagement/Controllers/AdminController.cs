using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestManagement.Models;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace TestManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        int pos = 0;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User obj)
        {
            BAL bAL = new BAL();
            SqlDataReader dr;
            dr = bAL.GetID(obj);
            if (dr.HasRows)
            {
                Session["STID"] = obj.StudentId;
                return RedirectToAction("StartTest", "Admin");
            }
            else
            {
                ViewBag.Message = "Please Enter correct Input";
                return View("Index");
            }
        }

        [HttpGet]
        public JsonResult GetData(int que)
        {
            pos = que;
            User obj = new User();
            obj.TestPaperId = 1;
            BAL objQuestion = new BAL();
            DataTable dtQue = new DataTable();
            dtQue = objQuestion.GetQuestion(obj);

            obj.markingSystem = dtQue.Rows[pos][4].ToString();
            obj.Question = dtQue.Rows[pos][7].ToString();
            ViewBag.Question = obj.Question;
            obj.QuestionId = Convert.ToInt32(dtQue.Rows[pos][0].ToString());
            obj.Type = dtQue.Rows[pos][5].ToString();
            obj.BehaviorType = dtQue.Rows[pos][3].ToString();

            return Json(new { data = obj.Question,data1 = obj.QuestionId, markingSystem = obj.markingSystem, Type= obj.Type, BehaviorType = obj.BehaviorType }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult StartTest()
        {
            string studid = Session["STID"].ToString();
            User obj = new User();

            obj.StudentId = studid;
            BAL bAL = new BAL();
            SqlDataReader dr;
            dr = bAL.GetID(obj);
            while (dr.Read())
            {
                obj.StudentName = dr["FullName"].ToString();
                ViewBag.StudentName = obj.StudentName;
            }
            dr.Close();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Next(int Qid,string ans,string marksystem,string Type, string BehaviorType)
        {
            pos = Qid;

            TempData["Questionid"] = Qid;
            TempData["Ans"] = ans;
            TempData["MarkingSystem1"] = marksystem;
            TempData["Quetype"] = Type;
            TempData["Behaviour"] = BehaviorType;
            saveans();
            User obj = new User();
            obj.TestPaperId = 1;
            BAL objQuestion = new BAL();
            DataTable dtQue = new DataTable();
            dtQue = objQuestion.GetQuestion(obj);

            obj.markingSystem = dtQue.Rows[pos][4].ToString();
            obj.Question = dtQue.Rows[pos][7].ToString();
            obj.QuestionId = Convert.ToInt32(dtQue.Rows[pos][0].ToString());
            obj.Type = dtQue.Rows[pos][5].ToString();
            obj.BehaviorType = dtQue.Rows[pos][3].ToString();

            return Json(new {status = "true", data = obj.Question, data1 = obj.QuestionId, markingSystem = obj.markingSystem, Type = obj.Type, BehaviorType = obj.BehaviorType });     
        }
        [HttpPost]
        public ActionResult Previous(int Qid, string ans, string marksystem, string Type, string BehaviorType)
        {
            pos = Qid-2;

            TempData["Questionid"] = Qid;
            TempData["Ans"] = ans;
            TempData["MarkingSystem1"] = marksystem;
            TempData["Quetype"] = Type;
            TempData["Behaviour"] = BehaviorType;

            saveans();

            User obj = new User();
            obj.TestPaperId = 1;
            BAL objQuestion = new BAL();
            DataTable dtQue = new DataTable();
            dtQue = objQuestion.GetQuestion(obj);

            obj.markingSystem = dtQue.Rows[pos][4].ToString();
            obj.Question = dtQue.Rows[pos][7].ToString();
            obj.QuestionId = Convert.ToInt32(dtQue.Rows[pos][0].ToString());
            obj.Type = dtQue.Rows[pos][5].ToString();
            obj.BehaviorType = dtQue.Rows[pos][3].ToString();

            return Json(new { status = "true", data = obj.Question, data1 = obj.QuestionId, markingSystem = obj.markingSystem, Type = obj.Type, BehaviorType = obj.BehaviorType });
        }
        public ActionResult Submit(int Qid, string ans, string marksystem, string Type, string BehaviorType)
        {
            pos = Qid - 2;

            TempData["Questionid"] = Qid;
            TempData["Ans"] = ans;
            TempData["MarkingSystem1"] = marksystem;
            TempData["Quetype"] = Type;
            TempData["Behaviour"] = BehaviorType;

            saveans();

            return RedirectToAction("StudentList", "Admin");

        }
        public void saveans()
        {
            int Answer=0;
            User obj = new User();
            obj.TestSubmittedDate = DateTime.Now;
            obj.QuestionId = Convert.ToInt32(TempData["Questionid"].ToString());
            obj.Ans = TempData["Ans"].ToString();
            obj.markingSystem = TempData["MarkingSystem1"].ToString();
            obj.BehaviorType = TempData["Behaviour"].ToString();
            obj.Type = TempData["Quetype"].ToString();
            string studid = Session["STID"].ToString();
            obj.StudentId = studid;
            string[] AnsKey = obj.markingSystem.Split(',');

            if(obj.Ans == "Always")
            {
                Answer = Convert.ToInt32(AnsKey[0]);
            }
            if (obj.Ans == "Frequently")
            {
                Answer = Convert.ToInt32(AnsKey[1]);
            }
            if (obj.Ans == "Often")
            {
                Answer = Convert.ToInt32(AnsKey[2]);
            }
            if (obj.Ans == "Never")
            {
                Answer = Convert.ToInt32(AnsKey[3]);
            }
            if (obj.Type == "Positive")
            {
                obj.Mark1 = obj.Mark2 = Answer;
            }
            else if (obj.Type == "Negative")
            {
                if (Answer == 3)
                {
                    obj.Mark2 = 0;
                    obj.Mark1 = Answer;
                    obj.Ans1 = Answer;
                }
                else if (Answer == 0)
                {
                    obj.Mark2 = 3;
                    obj.Mark1 = Answer;
                    obj.Ans1 = Answer;
                }
                else if (Answer == 1)
                {
                    obj.Mark2 = 2;
                    obj.Mark1 = Answer;
                    obj.Ans1 = Answer;
                }
                else if (Answer == 2)
                {
                    obj.Mark2 = 1;
                    obj.Mark1 = Answer;
                    obj.Ans1 = Answer;
                }
            }
            BAL bAL = new BAL();
            SqlDataReader dr;
            dr = bAL.getdata(obj);
            if (dr.HasRows)
            {
                bAL.UpdateAans(obj);
            }
            else
            {
                bAL.saveANS(obj);
            }

        }
        [HttpGet]
        public ActionResult StudentList()
        {
            User obj = new User();
            BAL bal = new BAL();
            DataSet ds = new DataSet();
            ds = bal.allresultPending();
            List<User> jobgridlst = new List<User>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                User objuser = new User();
                objuser.StudentId = ds.Tables[0].Rows[i]["Studentid"].ToString();
                objuser.StudentName = ds.Tables[0].Rows[i]["FullName"].ToString();
                objuser.Contactno = ds.Tables[0].Rows[i]["Contactno"].ToString();
                objuser.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                objuser.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"].ToString());
                objuser.TestType = ds.Tables[0].Rows[i]["TestType"].ToString();
                jobgridlst.Add(objuser);

            }
            obj.studentlist = jobgridlst;
            //var result = jobgridlst.Where(a => a.Address.ToLower().Contains(searchtext));
            return  View(obj);
        }
        public ActionResult Result(string StudentId)
        {
            User obj = new User();

            obj.StudentId = StudentId;
            BAL bAL = new BAL();
            SqlDataReader dr;
            dr = bAL.GetID(obj);
            while (dr.Read())
            {
                obj.StudentName = dr["FullName"].ToString();
                ViewBag.Gender = dr["Gender"].ToString();
                ViewBag.Age = dr["Age"].ToString();
                ViewBag.StudentName = obj.StudentName;
            }
            dr.Close();

            DataTable dt = new DataTable();

            dt = bAL.Getsum(obj);
            decimal satwik = 0;
            decimal rajas = 0;
            decimal tamas = 0;
            decimal mark2 = 0;
            decimal vyaktitvank = 0;
            int Count = 0;
            int Count1 = 0;
            int Count2 = 0;
            int Count3 = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == "सात्विक")
                {
                    if (Convert.ToInt32(dt.Rows[i][2].ToString()) == 3)
                    {
                        Count += Convert.ToInt32(dt.Rows[i][2].ToString());
                        Count1 += Convert.ToInt32(dt.Rows[i][2].ToString());
                    }
                    else
                    {
                        satwik += Convert.ToInt32(dt.Rows[i][2].ToString());
                    }
                }
                else if (dt.Rows[i][1].ToString() == "राजस")
                {


                    if (Convert.ToInt32(dt.Rows[i][2].ToString()) == 3)
                    {
                        Count += Convert.ToInt32(dt.Rows[i][2].ToString());
                        Count2 += Convert.ToInt32(dt.Rows[i][2].ToString());
                    }
                    else
                    {
                        rajas += Convert.ToInt32(dt.Rows[i][2].ToString());
                    }
                }

                else if (dt.Rows[i][1].ToString() == "तामस")
                {
                    if (Convert.ToInt32(dt.Rows[i][2].ToString()) == 3)
                    {
                        Count += Convert.ToInt32(dt.Rows[i][2].ToString());
                        Count3 += Convert.ToInt32(dt.Rows[i][2].ToString());
                    }
                    else
                    {
                        tamas += Convert.ToInt32(dt.Rows[i][2].ToString());
                    }
                }
            }


            ViewBag.SatwikTotal = satwik.ToString();
            ViewBag.Gadabadank = Count.ToString();
            ViewBag.RajasTotal = rajas.ToString();
            ViewBag.TamasTotal = tamas.ToString();
            ViewBag.SatwikG = Count1.ToString();
            ViewBag.RajasG = Count2.ToString();
            ViewBag.TamasG = Count3.ToString();


            ViewBag.SwatikPraman = ((satwik * 6 / (satwik + rajas + tamas)).ToString("0.00"));
            ViewBag.RajasPraman = ((rajas * 6 / (satwik + rajas + tamas)).ToString("0.00"));
            ViewBag.TamasPraman = ((tamas * 6 / (satwik + rajas + tamas)).ToString("0.00"));

            decimal a = (decimal.Parse(ViewBag.SwatikPraman));
            decimal b = (decimal.Parse(ViewBag.RajasPraman));
            decimal c = decimal.Parse(ViewBag.TamasPraman);
            ViewBag.SatwikPercentage = ((100 * a) / 3).ToString("00.00");
            ViewBag.RajasPercentage = ((100 * b) / 2).ToString("00.00");
            ViewBag.TamasPercentage = ((100 * c) / 1).ToString("00.00");

            DataTable dt1 = new DataTable();

            dt1 = bAL.Getvyaktitvank(obj);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                mark2 += Convert.ToInt32(dt1.Rows[i][2].ToString());

            }
            vyaktitvank = (mark2 - Count);
            ViewBag.vyaktitwank = vyaktitvank.ToString();

            return View();
        }
    }
}