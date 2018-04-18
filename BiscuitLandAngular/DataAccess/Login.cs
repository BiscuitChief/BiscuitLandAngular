using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace BiscuitLandAngular.Models
{
    public partial class Login
    {
        #region Constructors

        public Login() { }

        public Login(DBHelper dBHelper) {
            _dBHelper = dBHelper;
        }

        public Login(string username, DBHelper dBHelper)
        {
            _dBHelper = dBHelper;

            this.LoadUser(username);
        }


        #endregion

        #region Public Methods

        public bool ValidateLogin(string username, string password)
        {
            this.LoadUser(username);

            return ValidateLogin(password);
        }

        public bool ValidateLogin(string password)
        {
            bool isvalid = false;

            if (!string.IsNullOrEmpty(this.UserName))
            {
                string encryptedpass = PortalUtility.HashString(this.EncryptionSeed, password);
                if (this.Password == encryptedpass)
                { isvalid = true; }
            }

            return isvalid;
        }

        public void LoadUser(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(_dBHelper.GetDBConn()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Security_Select_User", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pUsername", username);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.UserName = dr["Username"].ToString();
                        this.Password = dr["Password"].ToString();
                        this.EncryptionSeed = dr["EncryptionSeed"].ToString();
                    }
                }
                conn.Close();
            }
        }

        public string AddNewUser()
        {
            string resultmsg = string.Empty;

            if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password))
            {
                this.EncryptionSeed = Guid.NewGuid().ToString();
                this.Password = PortalUtility.HashString(this.EncryptionSeed, this.Password);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(_dBHelper.GetDBConn()))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("Security_Insert_User", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pUsername", this.UserName);
                        cmd.Parameters.AddWithValue("@pPassword", this.Password);
                        cmd.Parameters.AddWithValue("@pEncryptionSeed", this.EncryptionSeed);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        resultmsg = "Success";
                    }
                }
                catch (Exception ex)
                {
                    resultmsg = ex.Message;
                }
            }

            return resultmsg;
        }

        #endregion
    }
}