using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helper
{
    public class SecurityHelper
    {
        private static string Key = "tbBquB+78++VOyYeZWsmZg==";

        /// <summary>
        /// Mã hóa
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(Key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Giải mã
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(Key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        //private bool ContainURL(string pageName)
        //{
        //    return string.IsNullOrEmpty(pageName) ? false : Request.Url.AbsolutePath.EndsWith(pageName);
        //}

        public static string GetConnectionString()
        {
            System.Diagnostics.Debug.WriteLine($"con: {Encrypt(ConfigurationManager.ConnectionStrings["EMMEntities"].ConnectionString)}");
            string connectionString = ConfigurationManager.ConnectionStrings[SystemSetting.SqlConnectStringName].ConnectionString;
            return Decrypt(connectionString);

            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            //string server = Encrypt(builder["Server"].ToString());
            //string userID = Encrypt("sa");
            //string password = Encrypt("sqlserver2014");
            //builder.UserID = Decrypt(userID);
            //builder.Password = Decrypt(password);
            //builder["Server"] = Decrypt(builder["Server"].ToString());
            //builder.UserID = Decrypt(builder.UserID);
            //builder.Password = Decrypt(builder.Password);
            //return builder.ConnectionString;
        }

        public static string GetConnectionStringUseEntity()
        {
            System.Diagnostics.Debug.WriteLine($"con: {Encrypt(ConfigurationManager.ConnectionStrings["EMMEntities"].ConnectionString)}");
            string connectionString = ConfigurationManager.ConnectionStrings[SystemSetting.EntityConnectStringName].ConnectionString;
            return Decrypt(connectionString);
        }
    }
}
