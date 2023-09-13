using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Crescent.WinForms.OpenAI;

namespace Crescent.WinForms
{
    public partial class DataLoader : Form
    {
        public DataLoader()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt|CSV files (*.csv)|*.CSV",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            string csvString = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textFileSelector.Text = openFileDialog1.FileName;
                // Call a method to read and load data from the selected file into the DataGridView
                LoadDataIntoDataGridView(openFileDialog1.FileName);
                csvString = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }

            var openAI = new OpenAI.OpenAI();
            openAI.SetupChat(csvString);
            var generatedQuestions = openAI.GetQuestions(string.IsNullOrWhiteSpace(comboBox1.Text) ? string.Empty : comboBox1.Text).Result;
            foreach ( var question in generatedQuestions )
            {
                textBox1.Text += question.QuestionName + Environment.NewLine;
            }
        }
        private void LoadDataIntoDataGridView(string filePath)
        {
            // Assuming the file contains tabular data, you can use StreamReader to read the data
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Read the data into a DataTable
                DataTable dataTable = new DataTable();
                string[] columns = reader.ReadLine().Split(','); //('\t'); // Adjust the delimiter as needed
                foreach (string column in columns)
                {
                    dataTable.Columns.Add(column);
                }

                while (!reader.EndOfStream)
                {
                    string[] rowData = reader.ReadLine().Split(','); //('\t'); // Adjust the delimiter as needed
                    dataTable.Rows.Add(rowData);
                }

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
