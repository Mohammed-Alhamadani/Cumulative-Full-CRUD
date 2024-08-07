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
    }

}

