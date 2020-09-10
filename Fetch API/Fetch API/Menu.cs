using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fetch_API
{
    class Menu
    {
        private int id;
        private string name;
        private double price;
        private string categoryname;
        private string menucategory_id;
        private string image;
        private string url;
        private string access_token;
        private string message;

        public Menu(int id, string name, double price, string categoryname, string menucategory_id, string image)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.categoryname = categoryname;
            this.Menucategory_id = menucategory_id;
            this.image = image;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public string Categoryname { get => categoryname; set => categoryname = value; }
        public string Image { get => image; set => image = value; }
        public string Url { get => url; set => url = value; }
        public string Access_token { get => access_token; set => access_token = value; }
        public string Message { get => message; set => message = value; }
        public string Menucategory_id { get => menucategory_id; set => menucategory_id = value; }

        public static string gettoken = Program.token;
        public HttpClient client;

        public DataGridViewRow dgv = null;

        public Menu()
        {
            client = new HttpClient();
            Url = "http://127.0.0.1:5555";
            client.BaseAddress = new Uri(Url);
            Access_token = Program.token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken);
        }

        public async Task createMenu()
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);
                payload.Add("price", Price);
                payload.Add("menucategory_id", Menucategory_id);
                payload.Add("image", Image);

                var endpoint = "/api/menu";
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

        public async Task GetMenus(DataGridView dataGridView, List<MenuCategory> menuCategories)
        {
            try
            {
                dataGridView.Rows.Clear();

                var endpoint = "/api/menus";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Menu> menulist = JsonConvert.DeserializeObject<List<Menu>>(resp);
                    foreach (var item in menulist)
                    {
                        foreach (var category in menuCategories)
                        {
                            if(category.Id == int.Parse(item.menucategory_id))
                            {
                                if(!File.Exists(item.Image))
                                {
                                    dataGridView.Rows.Add(item.Id.ToString(), item.Name, category.Name, item.Price.ToString(), Properties.Resources.http_error_404_not_found);
                                }
                                else
                                {
                                    Bitmap bm = new Bitmap(item.image);
                                    dataGridView.Rows.Add(item.Id.ToString(), item.Name, category.Name, item.Price.ToString(), bm);
                                }
                                
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task updateMenu()
        {
            try
            {
                Dictionary<string, Object> payload = new Dictionary<string, Object>();
                payload.Add("name", Name);
                payload.Add("price", Price);
                payload.Add("menucategory_id", Menucategory_id);
                payload.Add("image", image);

                var endpoint = $"/api/menu/{id}";
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

        public async Task deleteMenu(DataGridView dataGridView)
        {
            try
            {
                dgv = new DataGridViewRow();
                dgv = dataGridView.SelectedRows[0];
                id = int.Parse(dgv.Cells[0].Value.ToString());

                var endpoint = "/api/menu/" + id + "";

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

        DataGridView dgvOrder;
        Label lbTotal;

        public async Task GetMenusGenerateUserControl(FlowLayoutPanel flowLayoutPanel, DataGridView dataGridView, Label lb)
        {
            dgvOrder = dataGridView;
            lbTotal = lb;

            try
            {
                flowLayoutPanel.Controls.Clear();

                var endpoint = "/api/menus";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Menu> menulist = JsonConvert.DeserializeObject<List<Menu>>(resp);
                    foreach (var item in menulist)
                    {
                        if (!File.Exists(item.Image))
                        {
                            MenuUserControl menuUserControl = new MenuUserControl();
                            menuUserControl.lbId.Text = item.id.ToString();
                            menuUserControl.lbName.Text = item.Name;
                            menuUserControl.lbPrice.Text = item.price.ToString();
                            menuUserControl.pictureBoxImage.Image = Properties.Resources.http_error_404_not_found;
                            menuUserControl.pictureBoxImage.Click += pictureBoxImage_Click;
                            menuUserControl.pictureBoxImage.Tag += $"{item.Id}@{item.Name}@{item.Price}";
                            flowLayoutPanel.Controls.Add(menuUserControl);
                        }
                        else
                        {
                            MenuUserControl menuUserControl = new MenuUserControl();
                            menuUserControl.lbId.Text = item.id.ToString();
                            menuUserControl.lbName.Text = item.Name;
                            menuUserControl.lbPrice.Text = item.price.ToString();
                            menuUserControl.pictureBoxImage.Image = new Bitmap(item.image);
                            menuUserControl.pictureBoxImage.Click += pictureBoxImage_Click;
                            menuUserControl.pictureBoxImage.Tag += $"{item.Id}@{item.Name}@{item.Price}";
                            flowLayoutPanel.Controls.Add(menuUserControl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task GetMenusWithCategoryNameGenerateUserControl(FlowLayoutPanel flowLayoutPanel, DataGridView dataGridView, Label lb, string cat_name)
        {
            dgvOrder = dataGridView;
            lbTotal = lb;

            try
            {
                flowLayoutPanel.Controls.Clear();

                var endpoint = "/api/menus/cat_name/" + cat_name;

                HttpResponseMessage response = await client.GetAsync(endpoint);
                var resp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Menu> menulist = JsonConvert.DeserializeObject<List<Menu>>(resp);
                    foreach (var item in menulist)
                    {
                        if (!File.Exists(item.Image))
                        {
                            MenuUserControl menuUserControl = new MenuUserControl();
                            menuUserControl.lbId.Text = item.id.ToString();
                            menuUserControl.lbName.Text = item.Name;
                            menuUserControl.lbPrice.Text = item.price.ToString();
                            menuUserControl.pictureBoxImage.Image = Properties.Resources.http_error_404_not_found;
                            menuUserControl.pictureBoxImage.Click += pictureBoxImage_Click;
                            menuUserControl.pictureBoxImage.Tag += $"{item.Id}@{item.Name}@{item.Price}";
                            flowLayoutPanel.Controls.Add(menuUserControl);
                        }
                        else
                        {
                            MenuUserControl menuUserControl = new MenuUserControl();
                            menuUserControl.lbId.Text = item.id.ToString();
                            menuUserControl.lbName.Text = item.Name;
                            menuUserControl.lbPrice.Text = item.price.ToString();
                            menuUserControl.pictureBoxImage.Image = new Bitmap(item.image);
                            menuUserControl.pictureBoxImage.Click += pictureBoxImage_Click;
                            menuUserControl.pictureBoxImage.Tag += $"{item.Id}@{item.Name}@{item.Price}";
                            flowLayoutPanel.Controls.Add(menuUserControl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            PictureBox picbox = sender as PictureBox;
            string[] data = (picbox.Tag.ToString()).Split('@');
            int qty = getQtyFromDataGridView(data[0]);

            qty++;

            if(qty == 1)
            {
                dgvOrder.Rows.Add(data[0], data[1], data[2], qty);
            }
            else
            {
                for (int index = 0; index < dgvOrder.Rows.Count; index++)
                {
                    if (dgvOrder.Rows[index].Cells[0].Value.ToString() == data[0])
                    {
                        dgvOrder.Rows[index].Cells[3].Value = qty.ToString();
                    }
                }
            }

            getTotalOrder();
        }

        private int getQtyFromDataGridView(string id)
        {
            int qty = 0;

            for(int index = 0; index < dgvOrder.Rows.Count; index++)
            {
                if(dgvOrder.Rows[index].Cells[0].Value.ToString() == id)
                {
                    qty = int.Parse(dgvOrder.Rows[index].Cells[3].Value.ToString());
                }
            }

            return qty;
        }

        private void getTotalOrder()
        {
            double total = 0;

            for (int index = 0; index < dgvOrder.Rows.Count; index++)
            {
                total += double.Parse(dgvOrder.Rows[index].Cells[2].Value.ToString()) * int.Parse(dgvOrder.Rows[index].Cells[3].Value.ToString());
            }

            lbTotal.Text = total.ToString();
        }
    }
}
