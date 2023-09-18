namespace Crescent.WinForms
{
    partial class DataLoader_v2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            textFileSelector = new TextBox();
            browseButton = new Button();
            btnGetQuestions = new Button();
            label5 = new Label();
            lblResult = new Label();
            panel1 = new Panel();
            chartControl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            txtResult = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartControl).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(3, 2);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(textFileSelector);
            splitContainer1.Panel1.Controls.Add(browseButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnGetQuestions);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(lblResult);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(chartControl);
            splitContainer1.Panel2.Controls.Add(txtResult);
            splitContainer1.Size = new Size(859, 770);
            splitContainer1.SplitterDistance = 254;
            splitContainer1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(8, 13);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 8;
            label1.Text = "Input Data";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 39);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(856, 212);
            dataGridView1.TabIndex = 7;
            // 
            // textFileSelector
            // 
            textFileSelector.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textFileSelector.Location = new Point(80, 10);
            textFileSelector.Name = "textFileSelector";
            textFileSelector.Size = new Size(667, 23);
            textFileSelector.TabIndex = 6;
            // 
            // browseButton
            // 
            browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            browseButton.Location = new Point(753, 10);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 5;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // btnGetQuestions
            // 
            btnGetQuestions.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnGetQuestions.Location = new Point(9, 12);
            btnGetQuestions.Name = "btnGetQuestions";
            btnGetQuestions.Size = new Size(147, 23);
            btnGetQuestions.TabIndex = 15;
            btnGetQuestions.Text = "Analyse Data Now";
            btnGetQuestions.UseVisualStyleBackColor = true;
            btnGetQuestions.Click += btnGetQuestions_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(9, 47);
            label5.Name = "label5";
            label5.Size = new Size(115, 15);
            label5.TabIndex = 10;
            label5.Text = "Insight Suggestions";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblResult.Location = new Point(9, 258);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(42, 15);
            lblResult.TabIndex = 3;
            lblResult.Text = "Result";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(3, 65);
            panel1.Name = "panel1";
            panel1.Size = new Size(853, 190);
            panel1.TabIndex = 2;
            // 
            // chartControl
            // 
            chartControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea2.Name = "ChartArea1";
            chartControl.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartControl.Legends.Add(legend2);
            chartControl.Location = new Point(0, 276);
            chartControl.Name = "chartControl";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartControl.Series.Add(series2);
            chartControl.Size = new Size(856, 236);
            chartControl.TabIndex = 14;
            chartControl.Text = "chart1";
            // 
            // txtResult
            // 
            txtResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResult.BorderStyle = BorderStyle.FixedSingle;
            txtResult.Location = new Point(3, 276);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.Size = new Size(853, 236);
            txtResult.TabIndex = 9;
            // 
            // DataLoader_v2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 773);
            Controls.Add(splitContainer1);
            Name = "DataLoader_v2";
            Text = "Open AI CSV Data Analyzer";
            Load += DataLoader_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartControl).EndInit();
            ResumeLayout(false);
        }

        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private TextBox textFileSelector;
        private Button browseButton;
        private Label label1;
        private Panel panel1;
        private Label lblResult;
        private TextBox txtResult;
        private Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartControl;
        private Button btnGetQuestions;

        #endregion
        //private Data textFileSelector;
    }
}