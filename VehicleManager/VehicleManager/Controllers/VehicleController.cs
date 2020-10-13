using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace VehicleManager.Controllers
{
    public class VehicleController : Controller
    {
        private string connectionString;
        public VehicleController (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AmazonRDS");
        }
        public IActionResult Index()
        {
            var model = AllVehicles();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(string SearchReg)
        {
            var model = SearchVehReg(SearchReg);
            if (string.IsNullOrWhiteSpace(SearchReg))
            {
                model = AllVehicles();
            }

            return View(model);
        }
        public IActionResult AddVehicle()
        {
            return View();
        }

        public IActionResult EditVehicle(int id)
        {
            var model = SingleVehicle(id);
            ViewBag.Vehicle = model;
            return View();
        }

        public IActionResult DeleteVehicle(int id)
        {
            PushDeleteVehicle(id);
            return RedirectToAction("Index", "Vehicle");
        }

        private List<Vehicle> AllVehicles()
        {
            List<Vehicle> allVehicles = new List<Vehicle>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Get_All_Vehicles", conn);

            try
            {

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Vehicle vehTemp = new Vehicle();
                    vehTemp.Vehicle_ID = reader.GetInt32(reader.GetOrdinal("Vehicle_ID"));
                    vehTemp.Owner_FName = reader.GetString(reader.GetOrdinal("Owner_FName"));
                    vehTemp.Owner_LName = reader.GetString(reader.GetOrdinal("Owner_LName"));
                    vehTemp.Owner_Phone = reader.GetString(reader.GetOrdinal("Owner_Phone"));
                    vehTemp.Owner_Unit = reader.GetString(reader.GetOrdinal("Owner_Unit"));
                    vehTemp.Owner_Apartment = reader.GetString(reader.GetOrdinal("Owner_Apartment"));
                    vehTemp.Vehicle_Make = reader.GetString(reader.GetOrdinal("Vehicle_Make"));
                    vehTemp.Vehicle_Model = reader.GetString(reader.GetOrdinal("Vehicle_Model"));
                    vehTemp.Vehicle_Color = reader.GetString(reader.GetOrdinal("Vehicle_Color"));
                    vehTemp.Vehicle_Registration = reader.GetString(reader.GetOrdinal("Vehicle_Registration"));
                    vehTemp.Registration_Date = reader.GetDateTime(reader.GetOrdinal("Registration_Date"));
                    allVehicles.Add(vehTemp);
                }
                reader.Close();
                conn.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return allVehicles;
        }

        private List<Vehicle> SearchVehReg(string registration)
        {
            List<Vehicle> allVehicles = new List<Vehicle>();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Search_By_Reg", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Reg_Query", registration);

            try
            {

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehTemp = new Vehicle();
                    vehTemp.Vehicle_ID = reader.GetInt32(reader.GetOrdinal("Vehicle_ID"));
                    vehTemp.Owner_FName = reader.GetString(reader.GetOrdinal("Owner_FName"));
                    vehTemp.Owner_LName = reader.GetString(reader.GetOrdinal("Owner_LName"));
                    vehTemp.Owner_Phone = reader.GetString(reader.GetOrdinal("Owner_Phone"));
                    vehTemp.Owner_Unit = reader.GetString(reader.GetOrdinal("Owner_Unit"));
                    vehTemp.Owner_Apartment = reader.GetString(reader.GetOrdinal("Owner_Apartment"));
                    vehTemp.Vehicle_Make = reader.GetString(reader.GetOrdinal("Vehicle_Make"));
                    vehTemp.Vehicle_Model = reader.GetString(reader.GetOrdinal("Vehicle_Model"));
                    vehTemp.Vehicle_Color = reader.GetString(reader.GetOrdinal("Vehicle_Color"));
                    vehTemp.Vehicle_Registration = reader.GetString(reader.GetOrdinal("Vehicle_Registration"));
                    vehTemp.Registration_Date = reader.GetDateTime(reader.GetOrdinal("Registration_Date"));
                    allVehicles.Add(vehTemp);
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return allVehicles;
        }

        private Vehicle SingleVehicle(int primaryKey)
        {
            Vehicle forReturn = new Vehicle();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Get_Vehicle", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vehicle_ID", primaryKey);

            try
            {

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    forReturn.Vehicle_ID = reader.GetInt32(reader.GetOrdinal("Vehicle_ID"));
                    forReturn.Owner_FName = reader.GetString(reader.GetOrdinal("Owner_FName"));
                    forReturn.Owner_LName = reader.GetString(reader.GetOrdinal("Owner_LName"));
                    forReturn.Owner_Phone = reader.GetString(reader.GetOrdinal("Owner_Phone"));
                    forReturn.Owner_Unit = reader.GetString(reader.GetOrdinal("Owner_Unit"));
                    forReturn.Owner_Apartment = reader.GetString(reader.GetOrdinal("Owner_Apartment"));
                    forReturn.Vehicle_Make = reader.GetString(reader.GetOrdinal("Vehicle_Make"));
                    forReturn.Vehicle_Model = reader.GetString(reader.GetOrdinal("Vehicle_Model"));
                    forReturn.Vehicle_Color = reader.GetString(reader.GetOrdinal("Vehicle_Color"));
                    forReturn.Vehicle_Registration = reader.GetString(reader.GetOrdinal("Vehicle_Registration"));
                    forReturn.Registration_Date = reader.GetDateTime(reader.GetOrdinal("Registration_Date"));
                    
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return forReturn;

        }

        [HttpPost]
        public IActionResult PushVehicle(Vehicle input)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Write_New_Vehicle", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Owner_FName", input.Owner_FName);
            cmd.Parameters.AddWithValue("@Owner_LName", input.Owner_LName);
            cmd.Parameters.AddWithValue("@Owner_Phone", input.Owner_Phone);
            cmd.Parameters.AddWithValue("@Owner_Unit", input.Owner_Unit);
            cmd.Parameters.AddWithValue("@Owner_Apartment", input.Owner_Apartment);
            cmd.Parameters.AddWithValue("@Vehicle_Make", input.Vehicle_Make);
            cmd.Parameters.AddWithValue("@Vehicle_Model", input.Vehicle_Model);
            cmd.Parameters.AddWithValue("@Vehicle_Registration", input.Vehicle_Registration);
            cmd.Parameters.AddWithValue("@Vehicle_Color", input.Vehicle_Color);
            cmd.Parameters.AddWithValue("@Registration_Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Audit_User", "Default");
            cmd.Parameters.AddWithValue("@Audit_Time", DateTime.Now);

            try
            {
                conn.Open();
                Int32 rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    TempData["Message"] = "Error Encountered";
                } else { TempData["Message"] = "Success"; }
                return RedirectToAction("Index", "Vehicle");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                TempData["Message"] = "Error Encountered";
                return RedirectToAction("Index", "Vehicle");
            }

        }

        [HttpPost]
        public IActionResult UpdateVehicle(Vehicle input)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Alter_Vehicle", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vehicle_ID", input.Vehicle_ID);
            cmd.Parameters.AddWithValue("@Owner_FName", input.Owner_FName);
            cmd.Parameters.AddWithValue("@Owner_LName", input.Owner_LName);
            cmd.Parameters.AddWithValue("@Owner_Phone", input.Owner_Phone);
            cmd.Parameters.AddWithValue("@Owner_Unit", input.Owner_Unit);
            cmd.Parameters.AddWithValue("@Owner_Apartment", input.Owner_Apartment);
            cmd.Parameters.AddWithValue("@Vehicle_Make", input.Vehicle_Make);
            cmd.Parameters.AddWithValue("@Vehicle_Model", input.Vehicle_Model);
            cmd.Parameters.AddWithValue("@Vehicle_Color", input.Vehicle_Color);
            cmd.Parameters.AddWithValue("@Vehicle_Registration", input.Vehicle_Registration);
            cmd.Parameters.AddWithValue("@Audit_User", "Default");
            cmd.Parameters.AddWithValue("@Audit_Time", DateTime.Now);

            try
            {
                conn.Open();
                Int32 rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    TempData["Message"] = "Error Encountered";
                }
                else { TempData["Message"] = "Success"; }
                return RedirectToAction("Index", "Vehicle");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                TempData["Message"] = "Error Encountered";
                return RedirectToAction("Index", "Vehicle");
            }

        }

        public void PushDeleteVehicle(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Delete_Vehicle", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vehicle_ID", id);

            try
            {
                conn.Open();
                Int32 rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


        }

    }
}
