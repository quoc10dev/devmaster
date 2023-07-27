using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Skysoft
{
    class Program
    {
        const string DATA_HOST = "http://tracking.skysoft.vn/";
        const string PRIVATE_KEY = "Aks182aAB1fjAlkjdosa18w1uaABDK1234dh569QAH";
        const string JSON_PUBLIC = "JsonPublic";

        const string SKYSOFT_USER = "ags";
        const string SKYSOFT_PASS = "123123";

        static string connectionString = @"Data Source = SURFACEPRO4; database = EMMS; uid = sa; pwd = qsv5011";
        //static string connectionString = @"Data Source = 10.130.50.33; database = EMM; uid = sa; pwd = 123@123a";
        static HttpClient client;

        static void Main()
        {
            Console.WriteLine("Skysoft Json Service!\n");

            Init();

            // Login to Skysoft
            Login().GetAwaiter().GetResult();
            //Console.ReadLine();


            // Get all summary data
            dynamic list = Online().GetAwaiter().GetResult();

            /*
            DateTime now = DateTime.Now;
            for (int d = 1; d <= 2; d++)
            {
                DateTime dt = now.AddDays(-d);
                Console.WriteLine("\nDate: " + dt.ToString("yyyy/MM/dd"));

                int no = 0;
                foreach (dynamic dv in list.vehicle)
                {
                    Thread.Sleep(1000); // Delay 1 second
                    Console.WriteLine("\nVehicle " + (++no) + ": " + dv);

                    int id = dv.vehicleID;
                    string reg = dv.registerNo;
                    Summary(id, dt, reg).GetAwaiter().GetResult();
                }
            }
            */
            Console.Write("Enter Date in this Format(YYYY-MM-DD): ");
            string input = Console.ReadLine();
            DateTime dt = Convert.ToDateTime(input);

            int no = 0;
            foreach (dynamic dv in list.vehicle)
            {
                Thread.Sleep(1000); // Delay 1 second
                Console.WriteLine("\nVehicle " + (++no) + ": " + dv);

                int id = dv.vehicleID;
                string reg = dv.registerNo;
                Summary(id, dt, reg).GetAwaiter().GetResult();
            }

            Console.Write("\nFinished!");
            Console.ReadLine();

            /*
            // Get online data
            Online().GetAwaiter().GetResult();
            Console.ReadLine();
            
            // Summary: ĐK-CXR-31202
            DateTime dt = DateTime.Now;
            Summary(20826, dt.AddDays(-1), "ĐK-CXR-31202").GetAwaiter().GetResult();
            Console.ReadLine();

            // History: ĐK-CXR-31202
            DateTime dt = DateTime.Now;
            History(20826,dt.AddDays(-1)).GetAwaiter().GetResult();
            Console.ReadLine();
            */
        }

        static void Init()
        {
            // Create an HttpClientHandler object and set to use default credentials
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            // Create an HttpClient object
            client = new HttpClient(handler);
            client.BaseAddress = new Uri(DATA_HOST);
        }

        static async Task Login()
        {
            Console.WriteLine("Login Skysoft\n");

            string time = Microtime().ToString();
            string encrypt = Encrypt(SKYSOFT_PASS);
            Console.WriteLine("Pass: " + SKYSOFT_PASS + " --> Encrypt: " + encrypt);

            string authen = time + '-' + SKYSOFT_USER + '-' + PRIVATE_KEY + '-' + encrypt;
            string authenKey = MD5Hash(authen);
            Console.WriteLine("Authen: " + authen + " --> Hash: " + authenKey);

            var parameters = new Dictionary<string, string> {
                {"action", "login"},
                {"username", SKYSOFT_USER},
                {"password", authenKey},
                {"time", time}
            };
            var post = new FormUrlEncodedContent(parameters);

            Console.WriteLine("Post: {");
            foreach (KeyValuePair<string, string> kv in parameters)
            {
                Console.WriteLine("  " + kv.Key.ToString() + ": " + kv.Value.ToString());
            }
            Console.WriteLine("}\n");

            try
            {
                HttpResponseMessage response = await client.PostAsync(JSON_PUBLIC, post);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                Console.WriteLine("Response: " + result + "\n");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        static async Task<dynamic> Online()
        {
            Console.WriteLine("Online data\n");

            var parameters = new Dictionary<string, string> {
                {"action", "online"}
            };
            var post = new FormUrlEncodedContent(parameters);

            Console.WriteLine("Post: {");
            foreach (KeyValuePair<string, string> kv in parameters)
            {
                Console.WriteLine("  " + kv.Key.ToString() + ": " + kv.Value.ToString());
            }
            Console.WriteLine("}\n");

            try
            {
                HttpResponseMessage response = await client.PostAsync(JSON_PUBLIC, post);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                Console.WriteLine("Response: " + result + "\n");

                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return false;
        }

        static async Task Summary(int vehicle, DateTime date, string register)
        {
            Console.WriteLine("Summary data\n");

            var parameters = new Dictionary<string, string> {
                {"action", "summary"},
                {"vehicleID", vehicle.ToString()},
                {"summaryDate", date.ToString("yyyyMMdd")}
            };
            var post = new FormUrlEncodedContent(parameters);

            Console.WriteLine("Post: {");
            foreach (KeyValuePair<string, string> kv in parameters)
            {
                Console.WriteLine("  " + kv.Key.ToString() + ": " + kv.Value.ToString());
            }
            Console.WriteLine("}\n");

            try
            {
                HttpResponseMessage response = await client.PostAsync(JSON_PUBLIC, post);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                Console.WriteLine("Response: " + result + "\n");

                double SoPhut = result.summary.runningEngineDuration; // + result.summary.stoppingEngineDuration;
                double SoKm = result.summary.mileage;
                Console.WriteLine("Database: " + SoPhut + " phút, " + SoKm + " Km");
                InsertDatabase(vehicle, register, date, SoPhut, SoKm);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        static async Task History(int vehicle, DateTime date)
        {
            Console.WriteLine("History data\n");

            var parameters = new Dictionary<string, string> {
                {"action", "history"},
                {"vehicleID", vehicle.ToString()},
                {"historyDate", date.ToString("yyyyMMdd")}
            };
            var post = new FormUrlEncodedContent(parameters);

            Console.WriteLine("Post: {");
            foreach (KeyValuePair<string, string> kv in parameters)
            {
                Console.WriteLine("  " + kv.Key.ToString() + ": " + kv.Value.ToString());
            }
            Console.WriteLine("}\n");

            try
            {
                HttpResponseMessage response = await client.PostAsync(JSON_PUBLIC, post);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                Console.WriteLine("Response: " + result + "\n");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static void InsertDatabase(int idTrangThietBi, string bienSo, DateTime ngayHoatDong, double soPhut, double soKm)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SkySoft_GetData", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTrangThietBi", idTrangThietBi);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@NgayHoatDong", ngayHoatDong);
                cmd.Parameters.AddWithValue("@SoPhut", soPhut);
                cmd.Parameters.AddWithValue("@SoKm", soKm);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        static string Encrypt(string pass)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                Byte[] PhraseAsByte = Encoding.UTF8.GetBytes(pass);
                return Convert.ToBase64String(sha1.ComputeHash(PhraseAsByte));
            }
        }

        static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        static long Microtime()
        {
            var dto = new DateTimeOffset(DateTime.Now);
            return dto.ToUnixTimeMilliseconds();
        }
    }
}
