using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class Teacher
    {

        public int TeacherId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime Hiredate { get; set; }
        public decimal Salary { get; set; }
    }
}