using Cumulative_1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Models;

namespace Testing.Controllers
{
    public class TeachersController : Controller
    {

        private SchoolDbContext School = new SchoolDbContext();
        // GET: Teachers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string name)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * from teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Teacher teacher = new Teacher
                {
                    //TeacherId = ResultSet["teacherId"] == DBNull.Value ? 0 : (int)ResultSet["teacherId"],

                    TeacherId = (int)ResultSet["teacherId"],
                    FName = (string)ResultSet["teacherfname"],
                    LName = (string)ResultSet["teacherlname"],
                    Hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                    Salary = (Decimal)ResultSet["salary"]

                };
                teachers.Add(teacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers
            ViewBag.Message = "Hello " + name;
            ViewBag.Teachers = teachers;
            return View();
        }
        [HttpGet]
        public ActionResult AddTeacher()
        {

            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        public ActionResult AddTeacher(Teacher teacher)
        {

            MySqlConnection Conn = School.AccessDatabase();
            MySqlCommand cmd = Conn.CreateCommand();


        
            cmd.CommandText =  "INSERT INTO teachers (teacherfname, teacherlname, hiredate, salary) VALUES (?fname, ?lname, ?hiredate, ?salary)";

            cmd.Parameters.Add(new MySqlParameter("fname", teacher.FName));
            cmd.Parameters.Add(new MySqlParameter("lname", teacher.LName));
            cmd.Parameters.Add(new MySqlParameter("hiredate", teacher.Hiredate));
            cmd.Parameters.Add(new MySqlParameter("salary", teacher.Salary));
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
        
            
            return RedirectToAction("List");



        }

        public ActionResult DeleteTeacher(int? id)
        {
            MySqlConnection Conn = School.AccessDatabase();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
            cmd.Parameters.Add(new MySqlParameter("id", id));
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult UpdateTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
            cmd.Parameters.Add(new MySqlParameter("id", id));
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            Teacher teacher = new Teacher();
            while (ResultSet.Read())
            {
                teacher.TeacherId = (int)ResultSet["teacherid"];
                teacher.FName = (string)ResultSet["teacherfname"];
                teacher.LName = (string)ResultSet["teacherlname"];
                teacher.Hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                teacher.Salary = (Decimal)ResultSet["salary"];
            }
            Conn.Close();
            return View(teacher);
        }

        [HttpPost]
        public ActionResult UpdateTeacher(Teacher teacher)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "UPDATE teachers SET teacherfname = @fname, teacherlname = @lname, hiredate = @hiredate, salary = @salary WHERE teacherid = @id";
            cmd.Parameters.Add(new MySqlParameter("id", teacher.TeacherId));
            cmd.Parameters.Add(new MySqlParameter("fname", teacher.FName));
            cmd.Parameters.Add(new MySqlParameter("lname", teacher.LName));
            cmd.Parameters.Add(new MySqlParameter("hiredate", teacher.Hiredate));
            cmd.Parameters.Add(new MySqlParameter("salary", teacher.Salary));
            cmd.ExecuteNonQuery();
            Conn.Close();
            return RedirectToAction("List");
        }

        public ActionResult Show(int teacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * from teachers WHERE teacherid=?teacherid";
            cmd.Parameters.Add(new MySqlParameter("teacherid", teacherId));
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            Teacher teacherData = new Teacher { };
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                teacherData = new Teacher
                {
                    TeacherId = (int)ResultSet["teacherid"],
                    FName = (string)ResultSet["teacherfname"],
                    LName= (string)ResultSet["teacherlname"],
                    Hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                    Salary = (Decimal)ResultSet["salary"]

                };
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers
            ViewBag.singleTeacher = teacherData;
            return View();
        }










    }
}