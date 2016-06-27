using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TriboschAdmin.Enum;

namespace TriboschAdmin.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        public Enum.Enums.UserRoles Role { get; set; }
        public string About { get; set; }
        public Contact ContactDetail { get; set; }

        public bool IsValid(string _username, string _password)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Tribosch"].ToString()))
            {
                string _sql = @"SELECT [Username] FROM [dbo].[Users] " +
                       @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                .Value =_password; //Helpers.SHA1.Encode(
               cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    //Session["UserID"] = _username;
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
    }
}