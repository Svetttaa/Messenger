using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using Mes.DataLayer;
using Mes.Model;
using NLog;
using System.Data;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Web.UI.WebControls;

namespace Mes.DataLayer.Sql
{
    public static class Helper
    {
        static Logger logger;

        static Helper()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public static object ExecuteQuery(string connectionString, string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (sqlQuery.Split(' ')[0] == "select")
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                        {
                            DataTable ret = new DataTable();
                            adapter.Fill(ret);
                            return ret;
                        }
                    }
                    else
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sqlQuery;
                            return command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При обращении к БД с запросом {sqlQuery} возникла ошибка");
                throw ex;
            }
        }

        public static void ExecuteNonQueryWithSqlCommand(string connectionString, SqlCommand command)
        {
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                _con.Open();
                command.ExecuteNonQuery();
                _con.Close();
            }
        }

        public static object ExecuteScalar(string connectionString, string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlQuery;
                        return command.ExecuteScalar();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"При обращении к БД с запросом {sqlQuery} возникла ошибка");
                throw ex;
            }
        }

        public static bool CheckUserInChat(string connectionString,Guid idUser, Guid idChat)
        {
            return (int)ExecuteScalar(connectionString, $"select count(*) from ChatMembers where ChatId='{idChat}' and UserId='{idUser}'") != 0; 
        }

        public static HttpResponseException GetHttpException(string message, HttpStatusCode statusCode)
        {
            var ex = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            };

            return new HttpResponseException(ex);
        }

        public static byte[] FromFileToByte(string path)
        {
            Bitmap b = (Bitmap)Bitmap.FromFile(path);
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(b, typeof(byte[]));
        }

        public static void FromByteToBitmap(byte[] avatar, string path)
        {
            Bitmap avatarImage = (Bitmap)((new ImageConverter()).ConvertFrom(avatar));
            avatarImage.Save(path);
        }
    }
}
