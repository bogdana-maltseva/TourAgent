using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace TourAgent
{
    public partial class Form2 : Form
    {
        

        int ind;
        SqlConnection sqlConnection;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string connectionString = @"Data Source=DESKTOP-VVS9FBB\TEST;Initial Catalog=dbTrip;
                                      Persist Security Info=True;User ID=sa;Password=123321";

        public Form2()
        {
            InitializeComponent();
        }

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            Application.Exit();
        }

        private  void Form2_Load(object sender, EventArgs e)
        {
            

        }
        private void LoadData()
        {

        }
        private void worker_Click(object sender, EventArgs e)
        {
            
        }

        private async void worker_Click_1(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Employee]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private async void addTool_Click(object sender, EventArgs e)
        {
            if (label23.Visible)
                label23.Visible=false;
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text)&&
                !string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                !string.IsNullOrEmpty(maskedTextBox2.Text) && !string.IsNullOrWhiteSpace(maskedTextBox2.Text) &&
                !string.IsNullOrEmpty(maskedTextBox3.Text) && !string.IsNullOrWhiteSpace(maskedTextBox3.Text) &&
                !string.IsNullOrEmpty(maskedTextBox4.Text) && !string.IsNullOrWhiteSpace(maskedTextBox4.Text) &&
                !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbTrip].[Trip].[Employee] VALUES(@Surname,@FirstName,@LastName,@PositionId,@BirthDay,@FDayWork,@Adress," +
                    "@Passport,@IdentifCode,@PhoneNumber,@WorkNumber)", sqlConnection);
               
                command.Parameters.AddWithValue("Surname", textBox1.Text);
                command.Parameters.AddWithValue("FirstName", textBox2.Text);
                command.Parameters.AddWithValue("LastName", textBox3.Text);
                switch (comboBox1.SelectedItem)
                {
                    case "Менеджер з продаж": command.Parameters.AddWithValue("PositionId", 1); break;
                    case "Директор": command.Parameters.AddWithValue("PositionId", 2); break;
                    case "Бухгалтер": command.Parameters.AddWithValue("PositionId", 3); break;
                    case "Прибиральниця": command.Parameters.AddWithValue("PositionId", 4); break;
                }
                command.Parameters.AddWithValue("BirthDay", maskedTextBox3.Text);
                command.Parameters.AddWithValue("FDayWork", maskedTextBox4.Text);
                command.Parameters.AddWithValue("Adress", textBox4.Text);
                command.Parameters.AddWithValue("Passport", textBox5.Text);
                command.Parameters.AddWithValue("IdentifCode", textBox6.Text);
                command.Parameters.AddWithValue("PhoneNumber", maskedTextBox1.Text);
                command.Parameters.AddWithValue("WorkNumber", maskedTextBox2.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label23.Visible = true;
                label23.Text = "Усі поля, крім поля 'Робочий номер' мають бути обов'язково заповнені";
            }
        }

        private async void оновитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Employee]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            table.Clear();

            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити дані?", "Видалення", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ind = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows.RemoveAt(ind);
                
                SqlCommand command = new SqlCommand("DELETE FROM [dbTrip].[Trip].[Employee] WHERE EmployeeId="+textBox14.Text, sqlConnection);
                
                 command.ExecuteNonQuery();
            }
        }

        private  void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        
        private  void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             ind = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[ind];
            textBox14.Text = row.Cells[0].Value.ToString();
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            textBox3.Text = row.Cells[3].Value.ToString();
            textBox4.Text = row.Cells[7].Value.ToString();
            textBox5.Text = row.Cells[8].Value.ToString();
            textBox6.Text = row.Cells[9].Value.ToString();
            maskedTextBox1.Text = row.Cells[10].Value.ToString();
            maskedTextBox2.Text = row.Cells[11].Value.ToString();
            //maskedTextBox3.Text = row.Cells[5].Value.ToString();
            //maskedTextBox4.Text = row.Cells[6].Value.ToString();
            maskedTextBox3.Mask = row.Cells[5].FormattedValue.ToString();
            maskedTextBox4.Mask= row.Cells[6].FormattedValue.ToString() ;
            switch (row.Cells[4].Value.ToString())
            {
                case "1":comboBox1.Text="Менеджер з продаж"; break;
                case "2": comboBox1.Text = "Директор"; break;
                case "3": comboBox1.Text = "Бухгалтер"; break;
                case "4": comboBox1.Text = "Прибиральниця"; break;
            }

            //comboBox1.Text = row.Cells[4].Value.ToString();
           
        }
        private async void find_employ_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Employee]  WHERE PositionId= @PositionId" , sqlConnection);
            switch (comboBox1.SelectedItem)
            {

                case "Менеджер з продаж": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 1); break;

                case "Директор": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 2); break;
                case "Бухгалтер": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 3); break;
                case "Прибиральниця": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 4); break;
            }

            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void insertTool_Click(object sender, EventArgs e)
        {
            if (label23.Visible)
                label23.Visible = false;



            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                !string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                !string.IsNullOrEmpty(maskedTextBox2.Text) && !string.IsNullOrWhiteSpace(maskedTextBox2.Text) &&
                !string.IsNullOrEmpty(maskedTextBox3.Text) && !string.IsNullOrWhiteSpace(maskedTextBox3.Text) &&
                !string.IsNullOrEmpty(maskedTextBox4.Text) && !string.IsNullOrWhiteSpace(maskedTextBox4.Text) &&
                !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [dbTrip].[Trip].[Employee] SET [dbTrip].[Trip].[Employee].[Surname]=@Surname," +
                                    "[dbTrip].[Trip].[Employee].[FirstName]=@FirstName,[dbTrip].[Trip].[Employee].[LastName]=@LastName," +
                                    "[dbTrip].[Trip].[Employee].[PositionId]=@PositionId,[dbTrip].[Trip].[Employee].[BirthDay]=@BirthDay," +
                                    "[dbTrip].[Trip].[Employee].[FDayWork]=@FDayWork,[dbTrip].[Trip].[Employee].[Adress]=@Adress," +
                                    "[dbTrip].[Trip].[Employee].[Passport]=@Passport,[dbTrip].[Trip].[Employee].[IdentifCode]=@IdentifCode," +
                                    "[dbTrip].[Trip].[Employee].[PhoneNumber]=@PhoneNumber,[dbTrip].[Trip].[Employee].[WorkPhone]=@WorkPhone " +
                                    "WHERE EmployeeId="+textBox14.Text,sqlConnection);

                command.Parameters.AddWithValue("Surname", textBox1.Text);
                command.Parameters.AddWithValue("FirstName", textBox2.Text);
                command.Parameters.AddWithValue("LastName", textBox3.Text);
                switch (comboBox1.SelectedItem)
                {
                    case "Менеджер з продаж": command.Parameters.AddWithValue("PositionId", 1); break;
                    case "Директор": command.Parameters.AddWithValue("PositionId", 2); break;
                    case "Бухгалтер": command.Parameters.AddWithValue("PositionId", 3); break;
                    case "Прибиральниця": command.Parameters.AddWithValue("PositionId", 4); break;
                }
                command.Parameters.AddWithValue("BirthDay", maskedTextBox3.Text);
                command.Parameters.AddWithValue("FDayWork", maskedTextBox4.Text);
                command.Parameters.AddWithValue("Adress", textBox4.Text);
                command.Parameters.AddWithValue("Passport", textBox5.Text);
                command.Parameters.AddWithValue("IdentifCode", textBox6.Text);
                command.Parameters.AddWithValue("PhoneNumber", maskedTextBox1.Text);
                command.Parameters.AddWithValue("WorkPhone", maskedTextBox2.Text);
                command.ExecuteNonQuery();
            }
            else
            {
                label23.Visible = true;
                label23.Text = "Усі поля, крім поля 'Робочий номер' мають бути обов'язково заповнені";
            }
        }

        private void оновленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table.Clear();

            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private async void додатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label25.Visible)
                label25.Visible = false;
            if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
                !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) &&
                !string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text) &&
                !string.IsNullOrEmpty(maskedTextBox5.Text) && !string.IsNullOrWhiteSpace(maskedTextBox5.Text) &&
                !string.IsNullOrEmpty(maskedTextBox7.Text) && !string.IsNullOrWhiteSpace(maskedTextBox7.Text) &&
                !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbTrip].[Trip].[Clients] VALUES(@Surname,@FirstName,@LastName,@BirthDay," +
                    "@Passport,@IdCity,@PhoneNumber)", sqlConnection);

                command.Parameters.AddWithValue("Surname", textBox7.Text);
                command.Parameters.AddWithValue("FirstName", textBox8.Text);
                command.Parameters.AddWithValue("LastName", textBox9.Text);
                switch (comboBox2.SelectedItem)
                {
                    case "Київ": command.Parameters.AddWithValue("IdCity", 1); break;
                    case "Львів": command.Parameters.AddWithValue("IdCity", 2); break;
                    case "Житомир": command.Parameters.AddWithValue("IdCity", 3); break;
                    case "Харків": command.Parameters.AddWithValue("IdCity", 4); break;
                }
                command.Parameters.AddWithValue("BirthDay", maskedTextBox5.Text);
                command.Parameters.AddWithValue("Passport", textBox10.Text);
                command.Parameters.AddWithValue("PhoneNumber", maskedTextBox7.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label25.Visible = true;
                label25.Text = "Усі поля мають бути обов'язково заповнені";
            }
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити дані?", "Видалення", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ind = dataGridView2.SelectedCells[0].RowIndex;
                dataGridView2.Rows.RemoveAt(ind);

                SqlCommand command = new SqlCommand("DELETE FROM [dbTrip].[Trip].[Clients] WHERE ClientId=" + textBox15.Text, sqlConnection);

                command.ExecuteNonQuery();
            }
        }

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label25.Visible)
                label25.Visible = false;

            if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
                !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) &&
                !string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text) &&
                !string.IsNullOrEmpty(maskedTextBox5.Text) && !string.IsNullOrWhiteSpace(maskedTextBox5.Text) &&
                !string.IsNullOrEmpty(maskedTextBox7.Text) && !string.IsNullOrWhiteSpace(maskedTextBox7.Text) &&
                !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [dbTrip].[Trip].[Clients] SET [dbTrip].[Trip].[Clients].[Surname]=@Surname,[dbTrip].[Trip].[Clients].[FirstName]=@FirstName,[dbTrip].[Trip].[Clients].[LastName]=@LastName," +
                    "[dbTrip].[Trip].[Clients].[BirthDay]=@BirthDay," +
                    "[dbTrip].[Trip].[Clients].[Passport]=@Passport,[dbTrip].[Trip].[Clients].[IdCity]=@IdCity,[dbTrip].[Trip].[Clients].[PhoneNumber]=@PhoneNumber WHERE ClientId=" + textBox15.Text, sqlConnection);
                command.Parameters.AddWithValue("Surname", textBox7.Text);
                command.Parameters.AddWithValue("FirstName", textBox8.Text);
                command.Parameters.AddWithValue("LastName", textBox9.Text);
                command.Parameters.AddWithValue("BirthDay", maskedTextBox5.Text);
                command.Parameters.AddWithValue("Passport", textBox10.Text);
                command.Parameters.AddWithValue("PhoneNumber", maskedTextBox7.Text);
                switch (comboBox2.SelectedItem)
                {
                    case "Київ": command.Parameters.AddWithValue("IdCity", 1); break;
                    case "Львів": command.Parameters.AddWithValue("IdCity", 2); break;
                    case "Житомир": command.Parameters.AddWithValue("IdCity", 3); break;
                    case "Харків": command.Parameters.AddWithValue("IdCity", 4); break;
                }
                command.ExecuteNonQuery();
            }
            else
            {
                label25.Visible = true;
                label25.Text = "Усі поля мають бути обов'язково заповнені";
            }
        }

        private async void оновитиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Clients]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;


            table.Clear();

            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ind = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[ind];
            textBox15.Text = row.Cells[0].Value.ToString();
            textBox7.Text = row.Cells[1].Value.ToString();
            textBox8.Text = row.Cells[2].Value.ToString();
            textBox9.Text = row.Cells[3].Value.ToString();
            textBox10.Text = row.Cells[5].Value.ToString();
           
            maskedTextBox7.Text = row.Cells[7].Value.ToString();
           
            maskedTextBox5.Mask = row.Cells[4].FormattedValue.ToString();
            
            switch (row.Cells[6].Value.ToString())
            {
                case "1": comboBox2.Text = "Київ"; break;
                case "2": comboBox2.Text = "Львів"; break;
                case "3": comboBox2.Text = "Житомир"; break;
                case "4": comboBox2.Text = "Харків"; break;
            }
        }

        private async void tabPage2_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Clients]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }
        private async void пошукПоПрізвищуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Clients] WHERE Surname LIKE '"+textBox7.Text+"%'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ind = e.RowIndex;
            DataGridViewRow row = dataGridView3.Rows[ind];
            textBox12.Text = row.Cells[0].Value.ToString();
            textBox13.Text = row.Cells[1].Value.ToString();
            comboBox3.Text = row.Cells[2].Value.ToString();
            comboBox4.Text = row.Cells[3].Value.ToString();
            switch (row.Cells[4].Value.ToString())
            {
                case "5": comboBox5.Text = "Стамбул"; break;
                case "6": comboBox5.Text = "Анкара"; break;
                case "7": comboBox5.Text = "Аланья"; break;
                case "8": comboBox5.Text = "Бодрум"; break;
                case "9": comboBox5.Text = "Хургада"; break;
                case "10": comboBox5.Text = "Шарм-еш-Шейх"; break;
                case "11": comboBox5.Text = "Салоніки"; break;
                case "12": comboBox5.Text = "Катеріні"; break;
                case "13": comboBox5.Text = "Мадрид"; break;
                case "14": comboBox5.Text = "Барселона"; break;
                case "15": comboBox5.Text = "Аліканде"; break;
                case "16": comboBox5.Text = "Стокгольм"; break;
                case "17": comboBox5.Text = "Осло"; break;
                case "18": comboBox5.Text = "Алта"; break;
                case "19": comboBox5.Text = "Париж"; break;
                case "20": comboBox5.Text = "Марсель"; break;
                case "21": comboBox5.Text = "Ніцца"; break;
                case "22": comboBox5.Text = "Мюнхен"; break;
                case "23": comboBox5.Text = "Бремен"; break;
            }
        }

        private async void add1_Click(object sender, EventArgs e)
        {
            if (label26.Visible)
                label26.Visible = false;
            if (!string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrWhiteSpace(textBox13.Text) &&
                !string.IsNullOrEmpty(comboBox3.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text) &&
                !string.IsNullOrEmpty(comboBox4.Text) && !string.IsNullOrWhiteSpace(comboBox4.Text) &&
                !string.IsNullOrEmpty(comboBox5.Text) && !string.IsNullOrWhiteSpace(comboBox5.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbTrip].[Trip].[Hotels] VALUES(@HotelName,@Stars,@Adress,@IdCity", sqlConnection);

                command.Parameters.AddWithValue("HotelName", textBox13.Text);
                command.Parameters.AddWithValue("Stars", comboBox3.Text);
                command.Parameters.AddWithValue("Adress", comboBox4.Text);
                switch (comboBox5.SelectedItem)
                {
                    case "Стамбул": command.Parameters.AddWithValue("IdCity", 5); break;
                    case "Анкара": command.Parameters.AddWithValue("IdCity", 6); break;
                    case "Аланья": command.Parameters.AddWithValue("IdCity", 7); break;
                    case "Бодрум": command.Parameters.AddWithValue("IdCity", 8); break;
                    case "Хургада": command.Parameters.AddWithValue("IdCity", 9); break;
                    case "Шарм-еш-Шейх": command.Parameters.AddWithValue("IdCity", 10); break;
                    case "Салоніки": command.Parameters.AddWithValue("IdCity", 11); break;
                    case "Катеріні": command.Parameters.AddWithValue("IdCity", 12); break;
                    case "Мадрид": command.Parameters.AddWithValue("IdCity", 13); break;
                    case "Барселона": command.Parameters.AddWithValue("IdCity", 14); break;
                    case "Аліканде": command.Parameters.AddWithValue("IdCity", 15); break;
                    case "Стокгольм": command.Parameters.AddWithValue("IdCity", 16); break;
                    case "Осло": command.Parameters.AddWithValue("IdCity", 17); break;
                    case "Алта": command.Parameters.AddWithValue("IdCity", 18); break;
                    case "Париж": command.Parameters.AddWithValue("IdCity", 19); break;
                    case "Марсель": command.Parameters.AddWithValue("IdCity", 20); break;
                    case "Ніцца": command.Parameters.AddWithValue("IdCity", 21); break;
                    case "Мюнхен": command.Parameters.AddWithValue("IdCity", 22); break;
                    case "Бремен": command.Parameters.AddWithValue("IdCity", 23); break;
                }
               
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label26.Visible = true;
                label26.Text = "Усі поля мають бути обов'язково заповнені";
            }
        }

        private void delete1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити дані?", "Видалення", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ind = dataGridView3.SelectedCells[0].RowIndex;
                dataGridView3.Rows.RemoveAt(ind);

                SqlCommand command = new SqlCommand("DELETE FROM [dbTrip].[Trip].[Hotels] WHERE HotelId=" + textBox12.Text, sqlConnection);

                command.ExecuteNonQuery();
            }
        }

        private void insert1_Click(object sender, EventArgs e)
        {
            if (label26.Visible)
                label26.Visible = false;



            if (!string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrWhiteSpace(textBox13.Text) &&
                !string.IsNullOrEmpty(comboBox3.Text) && !string.IsNullOrWhiteSpace(comboBox3.Text) &&
                !string.IsNullOrEmpty(comboBox4.Text) && !string.IsNullOrWhiteSpace(comboBox4.Text) &&
                !string.IsNullOrEmpty(comboBox5.Text) && !string.IsNullOrWhiteSpace(comboBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [dbTrip].[Trip].[Hotels] SET [dbTrip].[Trip].[Hotels].[HotelName]=@HotelName,[dbTrip].[Trip].[Hotels].[Stars]=@Stars," +
                    "[dbTrip].[Trip].[Hotels].[Adress]=@Adress,[dbTrip].[Trip].[Hotels].[IdCity]=@IdCity WHERE IdCity=" + textBox12.Text, sqlConnection);

                command.Parameters.AddWithValue("HotelName", textBox1.Text);
                command.Parameters.AddWithValue("Stars", comboBox3.Text);
                command.Parameters.AddWithValue("Adress", comboBox4.Text);
                switch (comboBox5.SelectedItem)
                {
                    case "Стамбул": command.Parameters.AddWithValue("IdCity", 5); break;
                    case "Анкара": command.Parameters.AddWithValue("IdCity", 6); break;
                    case "Аланья": command.Parameters.AddWithValue("IdCity", 7); break;
                    case "Бодрум": command.Parameters.AddWithValue("IdCity", 8); break;
                    case "Хургада": command.Parameters.AddWithValue("IdCity", 9); break;
                    case "Шарм-еш-Шейх": command.Parameters.AddWithValue("IdCity", 10); break;
                    case "Салоніки": command.Parameters.AddWithValue("IdCity", 11); break;
                    case "Катеріні": command.Parameters.AddWithValue("IdCity", 12); break;
                    case "Мадрид": command.Parameters.AddWithValue("IdCity", 13); break;
                    case "Барселона": command.Parameters.AddWithValue("IdCity", 14); break;
                    case "Аліканде": command.Parameters.AddWithValue("IdCity", 15); break;
                    case "Стокгольм": command.Parameters.AddWithValue("IdCity", 16); break;
                    case "Осло": command.Parameters.AddWithValue("IdCity", 17); break;
                    case "Алта": command.Parameters.AddWithValue("IdCity", 18); break;
                    case "Париж": command.Parameters.AddWithValue("IdCity", 19); break;
                    case "Марсель": command.Parameters.AddWithValue("IdCity", 20); break;
                    case "Ніцца": command.Parameters.AddWithValue("IdCity", 21); break;
                    case "Мюнхен": command.Parameters.AddWithValue("IdCity", 22); break;
                    case "Бремен": command.Parameters.AddWithValue("IdCity", 23); break;
                }
                
                command.ExecuteNonQuery();
            }
            else
            {
                label26.Visible = true;
                label26.Text = "Усі поля мають бути обов'язково заповнені";
            }
        }

        private async void refresh2_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Hotels]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
            table.Clear();

            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }
        private async void find_city_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Hotels]  WHERE IdCity= @IdCity", sqlConnection);
            switch (comboBox5.SelectedItem)
            {
                case "Стамбул": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 5); break;
                case "Анкара": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 6); break;
                case "Аланья": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 7); break;
                case "Бодрум": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 8); break;
                case "Хургада": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 9); break;
                case "Шарм-еш-Шейх": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 10); break;
                case "Салоніки": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 11); break;
                case "Катеріні": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 12); break;
                case "Мадрид": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 13); break;
                case "Барселона": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 14); break;
                case "Аліканде": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 15); break;
                case "Стокгольм": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 16); break;
                case "Осло": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 17); break;
                case "Алта": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 18); break;
                case "Париж": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 19); break;
                case "Марсель": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 20); break;
                case "Ніцца": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 21); break;
                case "Мюнхен": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 22); break;
                case "Бремен": adapter.SelectCommand.Parameters.AddWithValue("IdCity", 23); break;
            }
            switch (comboBox1.SelectedItem)
            {

                case "Менеджер з продаж": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 1); break;

                case "Директор": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 2); break;
                case "Бухгалтер": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 3); break;
                case "Прибиральниця": adapter.SelectCommand.Parameters.AddWithValue("PositionId", 4); break;
            }

            table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }

        private async void find_adress_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Hotels] WHERE Adress LIKE '" + comboBox4.Text + "'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }

        private async void find_star_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Hotels] WHERE Stars LIKE '" + comboBox3.Text + "'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }

        private async void client_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Clients]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private async void contract_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("SELECT * FROM [dbTrip].[Trip].[Hotels]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }

        private async void pricelist_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select Pricelist.PricelistId,Pricelist.Price,Tour.HotelId,Tour.IdCity,Tour.TourName,Tour.DayNumber,Tour.TourId " +
                "from [dbTrip].[Trip].[Pricelist] FULL OUTER JOIN [dbTrip].[Trip].[Tour] on " +
                "[dbTrip].[Trip].[Pricelist].[TourId]=[dbTrip].[Trip].[Tour].[TourId]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private async void hotel_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select [dbTrip].[Trip].[Contracts].[ContractId],dbTrip.Trip.Employee.Surname as Employee,dbTrip.Trip.Clients.Surname as Client," +
                "dbTrip.Trip.Pricelist.Price,dbTrip.Trip.Pricelist.PricelistId, dbTrip.Trip.Contracts.SalesDate, dbTrip.Trip.Contracts.PaymentDate, " +
                "dbTrip.Trip.Contracts.TourNumber,dbTrip.Trip.Discount.DiscountName, dbTrip.Trip.Discount.DiscountPercent from dbTrip.Trip.Contracts " +
                "Inner Join[dbTrip].[Trip].[Employee] on Contracts.ContractId = Employee.EmployeeId Inner join[dbTrip].[Trip].[Clients] on " +
                "Clients.ClientId = Contracts.ClientId Inner join[dbTrip].[Trip].[Pricelist] on " +
                "Pricelist.PricelistId = Contracts.PricelistId Inner join[dbTrip].[Trip].[Discount] on Discount.DiscountId = Contracts.DiscountId", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView5.DataSource = table;
        }

        private void insert_price_Click(object sender, EventArgs e)
        {
            if (label29.Visible)
                label29.Visible = false;



            if (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text) &&
               !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [dbTrip].[Trip].[Pricelist] SET [dbTrip].[Trip].[PriceList].[Price]=@Price " +
                                                            "WHERE TourId=" + textBox17.Text, sqlConnection);
                command=new SqlCommand("UPDATE [dbTrip].[Trip].[Tour] SET [dbTrip].[Trip].[Tour].[DayNumber]=@DayNumber WHERE TourId=" 
                                                            + textBox17.Text, sqlConnection);
                command.Parameters.AddWithValue("Price", textBox11.Text);
                command.Parameters.AddWithValue("DayNumber", textBox16.Text);

                command.ExecuteNonQuery();
            }
            else
            {
                label29.Visible = true;
                label29.Text = "Усі поля мають бути обов'язково заповнені";
            }
        }

        private void delete_price_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити дані?", "Видалення", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ind = dataGridView4.SelectedCells[0].RowIndex;
                dataGridView4.Rows.RemoveAt(ind);

                SqlCommand command = new SqlCommand("DELETE FROM [dbTrip].[Trip].[PriceList] WHERE PricelistId=" + textBox18.Text, sqlConnection);
               
                command.ExecuteNonQuery();
            }
        }

        private async void refresh_price_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select Pricelist.PricelistId,Pricelist.Price,Tour.HotelId,Tour.IdCity,Tour.TourName,Tour.DayNumber,Tour.TourId " +
                "from [dbTrip].[Trip].[Pricelist] FULL OUTER JOIN [dbTrip].[Trip].[Tour] on " +
                "[dbTrip].[Trip].[Pricelist].[TourId]=[dbTrip].[Trip].[Tour].[TourId]", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView4.DataSource = table;
            table.Clear();

            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ind = e.RowIndex;
            DataGridViewRow row = dataGridView4.Rows[ind];
            textBox17.Text = row.Cells[6].Value.ToString();
            textBox11.Text = row.Cells[1].Value.ToString();
            textBox16.Text = row.Cells[5].Value.ToString();
            textBox18.Text = row.Cells[0].Value.ToString();

        }

        private async void find_tour_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select Pricelist.PricelistId,Pricelist.Price,Tour.HotelId,Tour.IdCity,Tour.TourName,Tour.DayNumber,Tour.TourId " +
                "from [dbTrip].[Trip].[Pricelist] FULL OUTER JOIN [dbTrip].[Trip].[Tour] on " +
                "[dbTrip].[Trip].[Pricelist].[TourId]=[dbTrip].[Trip].[Tour].[TourId] WHERE TourName LIKE '%" + textBox19.Text + "%'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private void find_pricelist_Click(object sender, EventArgs e)
        {
            label30.Visible=true;
            textBox19.Visible = true;
        }

        private async void find_price_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select Pricelist.PricelistId,Pricelist.Price,Tour.HotelId,Tour.IdCity,Tour.TourName," +
                "Tour.DayNumber,Tour.TourId from [dbTrip].[Trip].[Pricelist] FULL OUTER JOIN [dbTrip].[Trip].[Tour] on " +
                "[dbTrip].[Trip].[Pricelist].[TourId]=[dbTrip].[Trip].[Tour].[TourId] WHERE Price " + textBox11.Text, sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private async void find_day_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select Pricelist.PricelistId,Pricelist.Price,Tour.HotelId,Tour.IdCity,Tour.TourName,Tour.DayNumber,Tour.TourId " +
                "from [dbTrip].[Trip].[Pricelist] FULL OUTER JOIN [dbTrip].[Trip].[Tour] on " +
                "[dbTrip].[Trip].[Pricelist].[TourId]=[dbTrip].[Trip].[Tour].[TourId] WHERE DayNumber =" + textBox16.Text , sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private async void empl_find_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select [dbTrip].[Trip].[Contracts].[ContractId],dbTrip.Trip.Employee.Surname as Employee,dbTrip.Trip.Clients.Surname as Client," +
                "dbTrip.Trip.Pricelist.Price,dbTrip.Trip.Pricelist.PricelistId, dbTrip.Trip.Contracts.SalesDate, dbTrip.Trip.Contracts.PaymentDate, " +
                "dbTrip.Trip.Contracts.TourNumber,dbTrip.Trip.Discount.DiscountName, dbTrip.Trip.Discount.DiscountPercent,dbTrip.Trip.Contracts.TourNumber from dbTrip.Trip.Contracts " +
                "Inner Join[dbTrip].[Trip].[Employee] on Contracts.ContractId = Employee.EmployeeId Inner join[dbTrip].[Trip].[Clients] on " +
                "Clients.ClientId = Contracts.ClientId Inner join[dbTrip].[Trip].[Pricelist] on " +
                "Pricelist.PricelistId = Contracts.PricelistId Inner join[dbTrip].[Trip].[Discount] on Discount.DiscountId = Contracts.DiscountId where  dbTrip.Trip.Employee.Surname LIKE'" + textBox20.Text +"%'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView5.DataSource = table;
        }

        private async void clien_find_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select [dbTrip].[Trip].[Contracts].[ContractId],dbTrip.Trip.Employee.Surname as Employee,dbTrip.Trip.Clients.Surname as Client," +
                "dbTrip.Trip.Pricelist.Price,dbTrip.Trip.Pricelist.PricelistId, dbTrip.Trip.Contracts.SalesDate, dbTrip.Trip.Contracts.PaymentDate, " +
                "dbTrip.Trip.Contracts.TourNumber,dbTrip.Trip.Discount.DiscountName, dbTrip.Trip.Discount.DiscountPercent,dbTrip.Trip.Contracts.TourNumber from dbTrip.Trip.Contracts " +
                "Inner Join[dbTrip].[Trip].[Employee] on Contracts.ContractId = Employee.EmployeeId Inner join[dbTrip].[Trip].[Clients] on " +
                "Clients.ClientId = Contracts.ClientId Inner join[dbTrip].[Trip].[Pricelist] on " +
                "Pricelist.PricelistId = Contracts.PricelistId Inner join[dbTrip].[Trip].[Discount] on Discount.DiscountId = Contracts.DiscountId WHERE dbTrip.Trip.Clients.Surname LIKE'" + textBox20.Text+"%'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView5.DataSource = table;
        }

        private async void date_find_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select [dbTrip].[Trip].[Contracts].[ContractId],dbTrip.Trip.Employee.Surname as Employee," +
                                "dbTrip.Trip.Clients.Surname as Client,dbTrip.Trip.Pricelist.Price,dbTrip.Trip.Pricelist.PricelistId, " +
                                "dbTrip.Trip.Contracts.SalesDate, dbTrip.Trip.Contracts.PaymentDate, dbTrip.Trip.Contracts.TourNumber," +
                                "dbTrip.Trip.Discount.DiscountName, dbTrip.Trip.Discount.DiscountPercent,dbTrip.Trip.Contracts.TourNumber " +
                                "from dbTrip.Trip.Contracts Inner Join[dbTrip].[Trip].[Employee] on Contracts.ContractId = Employee.EmployeeId " +
                                "Inner join[dbTrip].[Trip].[Clients] on Clients.ClientId = Contracts.ClientId " +
                                "Inner join[dbTrip].[Trip].[Pricelist] on Pricelist.PricelistId = Contracts.PricelistId " +
                                "Inner join[dbTrip].[Trip].[Discount] on Discount.DiscountId = Contracts.DiscountId " +
                                "WHERE dbTrip.Trip.Contracts.PaymentDate LIKE'%" + textBox20.Text+"%'", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView5.DataSource = table;
        }

        private async void refresh_contract_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            adapter = new SqlDataAdapter("select [dbTrip].[Trip].[Contracts].[ContractId],dbTrip.Trip.Employee.Surname as Employee,dbTrip.Trip.Clients.Surname as Client," +
                "dbTrip.Trip.Pricelist.Price,dbTrip.Trip.Pricelist.PricelistId, dbTrip.Trip.Contracts.SalesDate, dbTrip.Trip.Contracts.PaymentDate, " +
                "dbTrip.Trip.Contracts.TourNumber,dbTrip.Trip.Discount.DiscountName, dbTrip.Trip.Discount.DiscountPercent,dbTrip.Trip.Contracts.TourNumber from dbTrip.Trip.Contracts " +
                "Inner Join[dbTrip].[Trip].[Employee] on Contracts.ContractId = Employee.EmployeeId Inner join[dbTrip].[Trip].[Clients] on " +
                "Clients.ClientId = Contracts.ClientId Inner join[dbTrip].[Trip].[Pricelist] on " +
                "Pricelist.PricelistId = Contracts.PricelistId Inner join[dbTrip].[Trip].[Discount] on Discount.DiscountId = Contracts.DiscountId", sqlConnection);

            table = new DataTable();
            adapter.Fill(table);
            dataGridView5.DataSource = table;
            table.Clear();

            adapter.Fill(table);
            dataGridView5.DataSource = table;
        }

        private void count_price_Click(object sender, EventArgs e)
        {
            
        }
    }
}
