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
    class MenuCategory
    {
        private int id;
        private string name;
        private string url;
        private string access_token;
        private string message;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Url { get => url; set => url = value; }
        public string Access_token { get => access_token; set => access_token = value; }
        public string Message { get => message; set => message = value; }

        public static string gettoken = Program.token;
        public HttpClient client;

        public DataGridViewRow dgv = null;

        public MenuCategory()
        {
            client = new HttpClient();
            Url = "http://127.0.0.1:5555";
            client.BaseAddress = new Uri(Url);
            Access_token = gettoken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken);
        }

        public MenuCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public async Task createMenuCategory()
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);

                var endpoint = "/api/menucategory";
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

        public async Task GetMenuCategories(DataGridView dataGridView)
        {
            try
            {
                dataGridView.Rows.Clear();

                var endpoint = "/api/menucategories";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<User> userlist = JsonConvert.DeserializeObject<List<User>>(resp);
                    foreach (var item in userlist)
                    {
                        String[] record = { item.Id.ToString(), item.Name.ToString() };
                        dataGridView.Rows.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<List<MenuCategory>> GetMenuCategoriesName(ComboBox comboBox)
        {
            List<MenuCategory> menuCategories = new List<MenuCategory>();
            MenuCategory menuCategory;

            try
            {
                comboBox.Items.Clear();

                var endpoint = "/api/menucategories";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<User> userlist = JsonConvert.DeserializeObject<List<User>>(resp);
                    foreach (var item in userlist)
                    {
                        String menuCategoryName = item.Name.ToString();
                        comboBox.Items.Add(menuCategoryName);

                        menuCategory = new MenuCategory();
                        menuCategory.id = int.Parse(item.Id.ToString());
                        menuCategory.name = item.Name.ToString();
                        menuCategories.Add(menuCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboBox.SelectedIndex = 0;
            return menuCategories;
        }

        public async Task deleteMenuCategory(DataGridView dataGridView)
        {
            try
            {
                dgv = new DataGridViewRow();
                dgv = dataGridView.SelectedRows[0];
                id = int.Parse(dgv.Cells[0].Value.ToString());

                var endpoint = "/api/menucategory/" + id + "";

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

        public async Task updateMenuCategory()
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);

                var endpoint = $"/api/menucategory/{id}";
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

        public async Task<int> getMenuCategoryIdByName(string name)
        {
            int id = 0;

            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", name);

                var endpoint = "/api/menucategory/getId";
                var json = JsonConvert.SerializeObject(payload);

                HttpContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, data);

                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    id = JsonConvert.DeserializeObject<int>(resp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return id;
        }
    }
}
