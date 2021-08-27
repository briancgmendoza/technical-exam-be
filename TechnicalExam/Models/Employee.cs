using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExam.Models
{
    public class Employee
    {
        public int EmployeeMasterID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
