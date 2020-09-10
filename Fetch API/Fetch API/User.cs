using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fetch_API
{
    class User
    {
        private int id;
        private string public_id;
        private string name;
        private string password;
        private string admin;
        private string address;
        private string url;
        private string access_token;
        private string message;

        public static string gettoken = "";
        public HttpClient client;

        public DataGridViewRow dgv = null;

        public User()
        {
            client = new HttpClient();
            Url = "http://127.0.0.1:5555";
            client.BaseAddress = new Uri(Url);
            Access_token = gettoken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken);
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Admin { get => admin; set => admin = value; }
        public string Address { get => address; set => address = value; }
        public string Url { get => url; set => url = value; }
        public string Access_token { get => access_token; set => access_token = value; }
        public string Message { get => message; set => message = value; }
        public string Public_id { get => public_id; set => public_id = value; }

        public async Task createUser()
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);
                payload.Add("password", Password);
                payload.Add("admin", Admin);
                payload.Add("address", Address);

                var endpoint = "/api/user/";
                var json = JsonConvert.SerializeObject(payload);

                HttpContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, data);

                string message = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Message message2 = JsonConvert.DeserializeObject<Message>(message);
                    MessageBox.Show(message2.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task updateUser()
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);
                payload.Add("admin", Admin);
                payload.Add("address", Address);

                var endpoint = $"/api/user/{Public_id}";
                var json = JsonConvert.SerializeObject(payload);

                HttpContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(endpoint, data);

                string message = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Message message2 = JsonConvert.DeserializeObject<Message>(message);
                    MessageBox.Show(message2.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task deleteUser(DataGridView dataGridView)
        {
            try
            {
                dgv = new DataGridViewRow();
                dgv = dataGridView.SelectedRows[0];
                Public_id = dgv.Cells[1].Value.ToString();

                var endpoint = "/api/user/" + Public_id + "";

                if (MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HttpResponseMessage response = await client.DeleteAsync(endpoint);
                    var message = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Message message2 = JsonConvert.DeserializeObject<Message>(message);
                        MessageBox.Show(message2.message);
                        dataGridView.Rows.Remove(dgv);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        public async Task getUserbyPublicId (string publicId)
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("public_id", publicId);

                var endpoint = "/api/users/";
                var json = JsonConvert.SerializeObject(payload);

                HttpContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, data);

                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<User> userlist = JsonConvert.DeserializeObject<List<User>>(resp);

                    if(userlist.Count > 0)
                    {
                        foreach (var item in userlist)
                        {
                            GetUserByPublicId gg = new GetUserByPublicId(item.Id.ToString(), item.Public_id.ToString(), item.Name.ToString(), "Encrypted", item.Admin.ToString(), item.Address.ToString());
                            gg.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("User is not found!", "404 Not Found!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task GetUsers(DataGridView dataGridView)
        {
            try
            {
                dataGridView.Rows.Clear();

                var endpoint = "/api/users/";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<User> userlist = JsonConvert.DeserializeObject<List<User>>(resp);
                    foreach (var item in userlist)
                    {
                        String[] record = { item.Id.ToString(), item.Public_id.ToString(), item.Name.ToString(),
                            "Encrypted", item.Admin.ToString(), item.Address.ToString() };
                        dataGridView.Rows.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<string> Login(Form form)
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);
                payload.Add("password", Password);

                var endpoint = "/login";
                var json = JsonConvert.SerializeObject(payload);
                Access_token = gettoken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Access_token);
                
                HttpContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, data);
                
                string message = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    List<User> userlist = JsonConvert.DeserializeObject<List<User>>(message);
                    foreach (var item in userlist)
                        gettoken = item.Access_token.ToString();

                    Program.token = gettoken;

                    MainForm mf = new MainForm();
                    mf.Show();
                    form.Hide();
                }
                else
                {
                    List<User> userlist = JsonConvert.DeserializeObject<List<User>>(message);
                    foreach (var item in userlist)
                        MessageBox.Show(item.Message.ToString());
                }

                return gettoken;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }
    }
}
