using Mes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Mes.Client
{
    static class Client
    {
        private static HttpClient _client;

        public static void Initialize()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:12345/api/")
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #region Users
        public static object RegisterUser(User user)
        {
            var response = _client.PostAsJsonAsync(@"users/register", user).Result.Content;

            try
            {
                user = response.ReadAsAsync<User>().Result;
                return user;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new User();
        }

        public static User LoginUser(User user)
        {
            var response = _client.PostAsJsonAsync(@"users/login", user).Result;

            try
            {
                user = response.Content.ReadAsAsync<User>().Result;
                return user;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new User();
        }

        public static void ChangeName(User user)
        {
            var response = _client.PostAsJsonAsync(@"users/changeName", user).Result;
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception e)
            {
                var s = response.Content.ReadAsStringAsync().Result;

                if (String.IsNullOrWhiteSpace(s))
                    MessageBox.Show(e.Message);

                else
                    MessageBox.Show(s);
            }
        }

        public static void ChangePassword(User user)
        {
            var response = _client.PostAsJsonAsync(@"users/changePassword", user).Result;
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception e)
            {
                var s = response.Content.ReadAsStringAsync().Result;

                if (String.IsNullOrWhiteSpace(s))
                    MessageBox.Show(e.Message);

                else
                    MessageBox.Show(s);
            }
        }

        public static void ChangeAvatar(User user)
        {
            var response = _client.PostAsJsonAsync(@"users/changeAvatar", user).Result;
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception e)
            {
                var s = response.Content.ReadAsStringAsync().Result;

                if (String.IsNullOrWhiteSpace(s))
                    MessageBox.Show(e.Message);

                else
                    MessageBox.Show(s);
            }
        }

        public static void DeleteUser(Guid id)
        {
            var response = _client.GetAsync(@"users/delete/" + id).Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                var s = response.Content.ReadAsStringAsync().Result;

                if (String.IsNullOrWhiteSpace(s))
                    MessageBox.Show(e.Message);

                else
                    MessageBox.Show(s);
            }
        }

        public static IEnumerable<User> SearchUsers(Guid id, string name)
        {
            var response = _client.GetAsync(@"users/searchUsers/" + id + "/" + name).Result;

            try
            {
                return response.Content.ReadAsAsync<List<User>>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new List<User>() ;
        }

        public static User GetUser(Guid id)
        {
            var response = _client.GetAsync(@"users/" + id).Result;
            try
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new User();
        }
        #endregion

        #region Chats 

        public static object CreateChat(Chat chat)
        {
            var response = _client.PostAsJsonAsync(@"chats/create", chat).Result.Content;

            try
            {
                return response.ReadAsAsync<Chat>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return new Chat();
        }

        public static object GetChat(Guid id)
        {
            var response = _client.GetAsync(@"chats/" + id).Result.Content;
            try
            {
                return response.ReadAsAsync<Chat>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new Chat();
        }

        public static void DeleteChat(Guid idChat, Guid idUser)
        {
            var response = _client.DeleteAsync(@"chats/" + idChat + "/" + idUser).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                var s = response.Content.ReadAsStringAsync().Result;

                if (String.IsNullOrWhiteSpace(s))
                    MessageBox.Show(e.Message);

                else
                    MessageBox.Show(s);
            }

        }

        public static object GetUserChats(Guid id)
        {
            var response = _client.GetAsync(@"chats/getUserChats/" + id).Result.Content;
            try
            {
                return response.ReadAsAsync<List<Chat>>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new List<Chat>();
        }

        public static object GetChatMembers(Guid idChat)
        {
            var response = _client.GetAsync(@"chats/getChatMembers/" + idChat).Result.Content;
            try
            {
                return response.ReadAsAsync<List<User>>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new List<User>();

        }
        #endregion

        #region Messages
        public static object GetAmountOfMessages(Guid chatId,int skip,int amount)
        {
            var response = _client.GetAsync(@"messages/getAmount/" + chatId + "/" + skip + "/" + amount).Result.Content;
            try
            {
                return response.ReadAsAsync<List<Model.Message>>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new List<Model.Message> ();
        }

        public static object Send(Model.Message message)
        {
            var response = _client.PostAsJsonAsync(@"messages/send", message).Result.Content;

            try
            {
                return response.ReadAsAsync<Model.Message>().Result;
            }
            catch (UnsupportedMediaTypeException)
            {
                MessageBox.Show(response.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return new Model.Message();
        }
        #endregion
        public static Label CreateLabel(string name, int height, int width, MouseEventHandler clickHandler=null)
        {
            var label =  new Label
            {
                Text = name,
                //AutoSize = false,
                Font = new Font("Segoe UI", 14F, FontStyle.Regular),
                //MinimumSize = new Size(0, height),
                Height = height,
                Width = width,
                Cursor = Cursors.Hand,
               // BackColor = Color.White,
               // Margin = new Padding(40,0,0,0),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                //Location = new Point(0, height*i),
                TextAlign = ContentAlignment.MiddleLeft,
                
            };
            if(clickHandler!=null)
            label.MouseClick += clickHandler;

            return label;
        }

        public static Bitmap FromBytesToBitmap(byte[] bytes)
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(bytes));
        }

        public static byte[] FromFileToBytes(string path)
        {
            Bitmap b = (Bitmap)Bitmap.FromFile(path);
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(b, typeof(byte[]));
        }

        public static PictureBox CreatePictureBox(int width, int hight, string name = "")
        {
            return new PictureBox()
            {
                BackgroundImageLayout = ImageLayout.Zoom,
                Size = new Size(width, hight),
                Name = name
            };
        }
    }

}
