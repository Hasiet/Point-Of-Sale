using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS
{
    
    public partial class Form1 : Form
    {
        SqlConnection PosDBcn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\POS\POS\PosDB.mdf;Integrated Security=True");
        SqlDataAdapter DataAdapter;
        DataSet DataSet;
        DataTable table = new DataTable();
        public Form1()
        {
            
            InitializeComponent();
            
        }
        public void ShowData()
        {
            PosDBcn.Open();
            SqlCommand cmdLoadDATA = new SqlCommand("select * from PosTable", PosDBcn);
            DataAdapter = new SqlDataAdapter(cmdLoadDATA);
            DataSet = new DataSet();
            DataAdapter.Fill(DataSet, "ViewTable");
            PosDBcn.Close();
            table = DataSet.Tables["ViewTable"];
            int i;
            for (i = 0; i < table.Rows.Count; i++)
            {
                listView.Items.Add(table.Rows[i].ItemArray[0].ToString());
                listView.Items[i].SubItems.Add(table.Rows[i].ItemArray[1].ToString());
            }
            PosDBcn.Close();
            listView.FullRowSelect = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmdInsert = "insert into PosTable(Id, Name) Values ('" + textBox1.Text + "','" + textBox2.Text + "')";
            PosDBcn.Open();
            try
            {
                SqlCommand cmdAdd = new SqlCommand(cmdInsert, PosDBcn);
                cmdAdd.ExecuteNonQuery();
                PosDBcn.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
                listView.Items.Clear();
                ShowData();
            }
            catch
            {
                MessageBox.Show("Already in the list!");
                listView.FullRowSelect = true;
            }
            PosDBcn.Close();
            

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            PosDBcn.Open();
            try 
            {
                
                SqlCommand cmdDelete = new SqlCommand("DELETE PosTable WHERE Id = '" + listView.SelectedItems[0].Text + "'", PosDBcn);
                cmdDelete.ExecuteNonQuery();
                PosDBcn.Close();
                listView.Items.Clear();
                ShowData();
            }
            catch 
            {
                MessageBox.Show("You did't selected the item.");
                this.Controls.Clear();
                this.InitializeComponent();
                PosDBcn.Close();
                ShowData();
                
            }
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowData();

        }
    }
}
