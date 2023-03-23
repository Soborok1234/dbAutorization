using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace autoruzation
{
    public partial class login : Form
    {

        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=1111;Database=Taxi;");
        Form1 form;

        int counter = 3;
        int i = 0;
        Timer timer = new Timer();
        
        


        public login(Form1 form1)
        {
            form = form1;
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 20000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            counter--;
            label3.Text = "Осталось попыток: " + counter.ToString();

           
                try
                {
                    string login = "@#$%^&*())(*&^";
                    string password = "#$%^&*()_)(*&^";
                    connection.Open();
                    NpgsqlCommand command1 = new NpgsqlCommand($"SELECT login FROM users WHERE login = '{textBox1.Text}'", connection);
                    NpgsqlCommand command2 = new NpgsqlCommand($"SELECT password FROM users WHERE password = '{textBox2.Text}'", connection);


                    using (var reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            login = reader["login"].ToString();
                        }
                    }

                    using (var reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            password = reader["password"].ToString();
                        }
                    }
                    if (login == textBox1.Text)
                    {
                        if (password == textBox2.Text)
                        {
                            form.Enabled = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верный логин");
                    }
                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("Повторите попытку");
                }


            if (counter == 2)
            {
                MessageBox.Show("Повторите попытку");
                this.OnLoad(e);
            }
            else if (counter == 1)
            {
                MessageBox.Show("Повторите попытку");
            }
            else if (counter == 0)
            {

                //OpenA.Enabled = false;
                timer.Start();
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Open.Enabled = true;
            i = 0;
            timer1.Stop();
        }
    }
}
