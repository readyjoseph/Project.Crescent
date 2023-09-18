namespace Crescent.WinForms
{
    partial class DataLoader_v1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            textFileSelector = new TextBox();
            browseButton = new Button();
            btnGetQuestions = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            cmbShowResultsAs = new ComboBox();
            label4 = new Label();
            cmbAnalysisType = new ComboBox();
            label5 = new Label();
            txtResult = new TextBox();
            cmbFixedQuestions = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            btnAnalyze = new Button();
            txtFreeText = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
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
            splitContainer1.Panel2.Controls.Add(chart1);
            splitContainer1.Panel2.Controls.Add(cmbShowResultsAs);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(cmbAnalysisType);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(txtResult);
            splitContainer1.Panel2.Controls.Add(cmbFixedQuestions);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(btnAnalyze);
            splitContainer1.Panel2.Controls.Add(txtFreeText);
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
            browseButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
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
            btnGetQuestions.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnGetQuestions.Location = new Point(9, 12);
            btnGetQuestions.Name = "btnGetQuestions";
            btnGetQuestions.Size = new Size(147, 23);
            btnGetQuestions.TabIndex = 15;
            btnGetQuestions.Text = "Get Insight Suggestions";
            btnGetQuestions.UseVisualStyleBackColor = true;
            btnGetQuestions.Click += btnGetQuestions_Click;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(0, 276);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(859, 236);
            chart1.TabIndex = 14;
            chart1.Text = "chart1";
            // 
            // cmbShowResultsAs
            // 
            cmbShowResultsAs.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShowResultsAs.FormattingEnabled = true;
            cmbShowResultsAs.Location = new Point(107, 41);
            cmbShowResultsAs.Name = "cmbShowResultsAs";
            cmbShowResultsAs.Size = new Size(153, 23);
            cmbShowResultsAs.TabIndex = 13;
            cmbShowResultsAs.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(9, 44);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 12;
            label4.Text = "Show Results as";
            label4.Visible = false;
            // 
            // cmbAnalysisType
            // 
            cmbAnalysisType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAnalysisType.FormattingEnabled = true;
            cmbAnalysisType.Location = new Point(370, 36);
            cmbAnalysisType.Name = "cmbAnalysisType";
            cmbAnalysisType.Size = new Size(153, 23);
            cmbAnalysisType.TabIndex = 11;
            cmbAnalysisType.Visible = false;
            cmbAnalysisType.SelectedIndexChanged += cmbAnalysisType_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(7, 68);
            label5.Name = "label5";
            label5.Size = new Size(115, 15);
            label5.TabIndex = 10;
            label5.Text = "Insight Suggestions";
            // 
            // txtResult
            // 
            txtResult.Location = new Point(3, 276);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.Size = new Size(853, 236);
            txtResult.TabIndex = 9;
            // 
            // cmbFixedQuestions
            // 
            cmbFixedQuestions.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFixedQuestions.FormattingEnabled = true;
            cmbFixedQuestions.Location = new Point(529, 36);
            cmbFixedQuestions.Name = "cmbFixedQuestions";
            cmbFixedQuestions.Size = new Size(109, 23);
            cmbFixedQuestions.TabIndex = 5;
            cmbFixedQuestions.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(296, 41);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 4;
            label3.Text = "Analysis Type";
            label3.Visible = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(9, 258);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 3;
            label2.Text = "Result";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(3, 86);
            panel1.Name = "panel1";
            panel1.Size = new Size(853, 169);
            panel1.TabIndex = 2;
            // 
            // btnAnalyze
            // 
            btnAnalyze.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnAnalyze.Location = new Point(720, 36);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(125, 23);
            btnAnalyze.TabIndex = 0;
            btnAnalyze.Text = "Analyze Data Now";
            btnAnalyze.UseVisualStyleBackColor = true;
            btnAnalyze.Click += btnAnalyze_Click;
            // 
            // txtFreeText
            // 
            txtFreeText.BorderStyle = BorderStyle.FixedSingle;
            txtFreeText.Location = new Point(529, 36);
            txtFreeText.Name = "txtFreeText";
            txtFreeText.Size = new Size(109, 23);
            txtFreeText.TabIndex = 7;
            txtFreeText.Visible = false;
            // 
            // DataLoader_v1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 773);
            Controls.Add(splitContainer1);
            Name = "DataLoader_v1";
            Text = "Open AI CSV Data Analyzer";
            Load += DataLoader_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
        }

        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private TextBox textFileSelector;
        private Button browseButton;
        private Label label1;
        private Button btnAnalyze;
        private Panel panel1;
        private Label label2;
        private ComboBox cmbFixedQuestions;
        private Label label3;
        private TextBox txtFreeText;
        private TextBox txtResult;
        private Label label5;
        private ComboBox cmbAnalysisType;
        private ComboBox cmbShowResultsAs;
        private Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button btnGetQuestions;

        #endregion
        //private Data textFileSelector;
    }
}