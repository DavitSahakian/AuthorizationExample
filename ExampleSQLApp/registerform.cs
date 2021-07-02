using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleSQLApp
{
    public partial class registerform : Form
    {
        public registerform()
        {
            InitializeComponent();
            NameField.Text = "Please enter your name";
            NameField.ForeColor = Color.Gray;
            SurnameField.Text = "Please enter your surname";
            SurnameField.ForeColor = Color.Gray;
        }

        private void PasswordField_TextChanged(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastpoint;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint.X = e.X;
            lastpoint.Y = e.Y;
        }

        private void NameField_Enter(object sender, EventArgs e)
        {
            if (NameField.Text == "Please enter your name")
            {
                NameField.ForeColor = Color.Black;
                NameField.Text = "";
            }
        }

        private void NameField_Leave(object sender, EventArgs e)
        {
            if (NameField.Text == "" )
            {
                NameField.Text = "Please enter your name";
                NameField.ForeColor = Color.Gray;

            }
        }

        private void SurnameField_Enter(object sender, EventArgs e)
        {
            if (SurnameField.Text == "Please enter your surname")
            {
                SurnameField.ForeColor = Color.Black;
                SurnameField.Text = "";
            }
        }

        private void SurnameField_Leave(object sender, EventArgs e)
        {
            if (SurnameField.Text == "")
            {
                SurnameField.Text = "Please enter your surname";
                SurnameField.ForeColor = Color.Gray;

            }
        }

        private void buttonreg_Click(object sender, EventArgs e)
        {
            if (NameField.Text=="Please enter your name"||SurnameField.Text == "Please enter your surname" || UserField.Text=="" || PasswordField.Text=="" )
            {
                MessageBox.Show("Enter all datas");
                return;
            }
            if (UniqueCheck() == true)
            {
                return;
            }
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`, `surname`) VALUES (@login, @password, @name, @surname)" ,db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = UserField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PasswordField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = NameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = SurnameField.Text;
            db.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("account created succesfull");
            }
            else
            {
                MessageBox.Show("account creation error");
            }
 
            db.CloseConnection();
        }
        public bool UniqueCheck()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL ", db.GetConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = UserField.Text;
           
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Login already exist");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void isauthorizedlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
