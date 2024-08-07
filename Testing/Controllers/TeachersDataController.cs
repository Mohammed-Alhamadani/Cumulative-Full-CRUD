using Cumulative_1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Testing.Models;

namespace Testing.Controllers
{

    public class TeachersDataController : ApiController
    {
    private SchoolDbContext School = new SchoolDbContext();
        [HttpPost]

        //http://localhost:53448/api/TeachersData/addteacher when I used this link on post man everything works fine 
        public IHttpActionResult AddTeacher(Teacher teacher)
        {
            MySqlConnection Conn = School.AccessDatabase();
            MySqlCommand cmd = Conn.CreateCommand();



            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, hiredate, salary) VALUES (?fname, ?lname, ?hiredate, ?salary)";

            cmd.Parameters.Add(new MySqlParameter("fname", teacher.FName));
            cmd.Parameters.Add(new MySqlParameter("lname", teacher.LName));
            cmd.Parameters.Add(new MySqlParameter("hiredate", teacher.Hiredate));
            cmd.Parameters.Add(new MySqlParameter("salary", teacher.Salary));
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
            return Ok();
        }
        //Method to get the list of the teachers through the web api.
        [HttpGet]
        [Route("api/teachersdata/list")]
        public String List()
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * from teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();
            while (ResultSet.Read())
            {
                Teacher teacher = new Teacher
                {
                    TeacherId = (int)ResultSet["teacherId"],
                    FName = (string)ResultSet["teacherfname"],
                    LName = (string)ResultSet["teacherlname"],
                    Hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                    Salary = (Decimal)ResultSet["salary"]
                };
                teachers.Add(teacher);
            }
            Conn.Close();
            return Ok(teachers);
        }



    }



}

