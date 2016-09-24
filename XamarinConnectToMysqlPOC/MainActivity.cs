using System;
using Android.App;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using Android.Util;
using System.Data;
using System.Collections.Generic;

//Require References
//System.Data
//I18N.West
//MySql.Data.MySqlClient;

namespace XamarinConnectToMysqlPOC
{
    [Activity(Label = "XamarinConnectToMysqlPOC", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly string TAG = "XamarinConnectToMysqlPOC";
        private readonly string MYSQL_HOST = "192.168.1.121";
        private readonly string MYSQL_DB = "sakila";
        private readonly string MYSQL_USER = "?";
        private readonly string MYSQL_PASS = "?";
        private TextView _textViewLabel;
        private int _count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //LinearLayout
            LinearLayout linearLayout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical
            };

            //TextView
            _textViewLabel = new TextView(this);
            _textViewLabel.Text = ApplicationContext.Resources.GetString(Resource.String.ApplicationName);

            //Button1
            Button button1 = new Button(this)
            {
                Text = ApplicationContext.Resources.GetString(Resource.String.button_label_1)
            };
            button1.Click += (sender, e) =>
            {
                _textViewLabel.Text = string.Format("Counter: {0}", _count++);
                GetAccountCountFromMySQL();
                Log.Debug(App.Tag, "button1.Click");
            };

            //Add View to linearLayout
            linearLayout.AddView(button1);
            linearLayout.AddView(_textViewLabel);
            //Add Content View
            SetContentView(linearLayout);
        }

        public void GetAccountCountFromMySQL()
        {
            try
            {
                new I18N.West.CP1250();
                MySqlConnection sqlconn;
                //string connsqlstring = @"Server={MYSQL_HOST};Port=3306;database={MYSQL_DB};User Id={MYSQL_USER};Password={MYSQL_PASSWORD};charset=utf8";
                string connsqlstring = string.Format(@"Server={0};Port=3306;database={1};User Id={2};Password={3};charset=utf8", MYSQL_HOST, MYSQL_DB, MYSQL_USER, MYSQL_PASS);
                sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                string queryString = "select * from actor;";
                MySqlCommand sqlcmd = new MySqlCommand(queryString, sqlconn);
                String result = sqlcmd.ExecuteScalar().ToString();
                Log.Info(TAG, result);
                Console.WriteLine(result);

                List<String> products = new List<String>();
                DataSet tickets = new DataSet();
                queryString = "SELECT actor_id, first_name, last_name, last_update FROM actor;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "table");
                foreach (DataRow row in tickets.Tables["table"].Rows)
                {
                    products.Add(row[0].ToString());
                    Log.Info(TAG, row[0].ToString());
                    Console.WriteLine(row[0].ToString());
                    _textViewLabel.Text += string.Format("{0}{1}:{2}:{3}", @"\r\n", row.ItemArray[0], row.ItemArray[1], row.ItemArray[2]);
                }

                sqlconn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*
        public List<String> LoadAllItemFromMySQL()
        {
            List<String> products = new List<String>();
            try
            {
                string connsqlstring = "Server=your.ip.address;Port=3306;database=YOUR_DATA_BASE;User Id=root;Password=password;charset=utf8";
                MySqlConnection sqlconn = new MySqlConnection(connsqlstring);
                sqlconn.Open();

                DataSet tickets = new DataSet();
                string queryString = "select item.NAME from ITEM as item";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "Item");
                foreach (DataRow row in tickets.Tables["Item"].Rows)
                {
                    products.Add(row[0].ToString());
                }

                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return products;
        }
        */
    }
}
