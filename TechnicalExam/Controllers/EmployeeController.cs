using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Importing System.Data.SqlClient
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TechnicalExam.Models;

namespace TechnicalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Get Request
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select EmployeeMasterID, LastName, FirstName, MiddleName, Age, Address, PhoneNumber 
                            from dbo.EmployeeMaster";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        // Get Request for specific ID
        [HttpGet("{id}")]
        public JsonResult GetSpecificID(int id)
        {
            string query = @"
                            select EmployeeMasterID, LastName, FirstName, MiddleName, Age, Address, PhoneNumber 
                            from dbo.EmployeeMaster where EmployeeMasterID = @EmployeeMasterID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeMasterID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        // Post Request
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
                            insert into dbo.EmployeeMaster
                            (LastName, FirstName, MiddleName, Age, Address, PhoneNumber)
                    values (@LastName, @Firstname, @MiddleName, @Age, @Address, @PhoneNumber)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@LastName", emp.LastName);
                    myCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    myCommand.Parameters.AddWithValue("@MiddleName", emp.MiddleName);
                    myCommand.Parameters.AddWithValue("@Age", emp.Age);
                    myCommand.Parameters.AddWithValue("@Address", emp.Address);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        // Update Request
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                           update dbo.EmployeeMaster
                           set LastName = @LastName,
                            FirstName = @FirstName,
                            MiddleName = @MiddleName,
                            Age = @Age,
                            Address = @Address,
                            PhoneNumber = @PhoneNumber
                            where EmployeeMasterID = @EmployeeMasterID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeMasterID", emp.EmployeeMasterID);
                    myCommand.Parameters.AddWithValue("@LastName", emp.LastName);
                    myCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    myCommand.Parameters.AddWithValue("@MiddleName", emp.MiddleName);
                    myCommand.Parameters.AddWithValue("@Age", emp.Age);
                    myCommand.Parameters.AddWithValue("@Address", emp.Address);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        // Delete Request
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.EmployeeMaster
                            where EmployeeMasterID = @EmployeeMasterID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeMasterID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
