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
        //private readonly string MYSQL_HOST = "192.168.178.57";
        //private readonly string MYSQL_DB = "logicposdb_20160919_scripts";
        //private readonly string MYSQL_USER = "root";
        //private readonly string MYSQL_PASS = "adminx";
        private readonly string MYSQL_HOST = "koakh.com";
        private readonly string MYSQL_DB = "mail";
        private readonly string MYSQL_USER = "softcontrol";
        private readonly string MYSQL_PASS = "kksc28kk";
        
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {
                button.Text = string.Format("{0} clicks!", count++);
                GetAccountCountFromMySQL();
            };
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

                string queryString = "select * from users;";
                MySqlCommand sqlcmd = new MySqlCommand(queryString, sqlconn);
                String result = sqlcmd.ExecuteScalar().ToString();
                Log.Info(TAG, result);
                Console.WriteLine(result);

                List<String> products = new List<String>();
                DataSet tickets = new DataSet();
                queryString = "select id, name from users;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlconn);
                adapter.Fill(tickets, "id");
                foreach (DataRow row in tickets.Tables["id"].Rows)
                {
                    products.Add(row[0].ToString());
                    Log.Info(TAG, row[0].ToString());
                    Console.WriteLine(row[0].ToString());
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

