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
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            textFileSelector = new TextBox();
            browseButton = new Button();
            cmbShowResultsAs = new ComboBox();
            label4 = new Label();
            cmbAnalysisType = new ComboBox();
            label5 = new Label();
            txtResult = new TextBox();
            txtFreeText = new TextBox();
            cmbFixedQuestions = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            btnAnalyze = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            splitContainer1.Panel2.Controls.Add(cmbShowResultsAs);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(cmbAnalysisType);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(txtResult);
            splitContainer1.Panel2.Controls.Add(txtFreeText);
            splitContainer1.Panel2.Controls.Add(cmbFixedQuestions);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(btnAnalyze);
            splitContainer1.Size = new Size(859, 677);
            splitContainer1.SplitterDistance = 349;
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
            dataGridView1.Size = new Size(856, 307);
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
            // cmbShowResultsAs
            // 
            cmbShowResultsAs.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShowResultsAs.FormattingEnabled = true;
            cmbShowResultsAs.Location = new Point(107, 41);
            cmbShowResultsAs.Name = "cmbShowResultsAs";
            cmbShowResultsAs.Size = new Size(153, 23);
            cmbShowResultsAs.TabIndex = 13;
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
            // 
            // cmbAnalysisType
            // 
            cmbAnalysisType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAnalysisType.FormattingEnabled = true;
            cmbAnalysisType.Location = new Point(107, 9);
            cmbAnalysisType.Name = "cmbAnalysisType";
            cmbAnalysisType.Size = new Size(153, 23);
            cmbAnalysisType.TabIndex = 11;
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
            txtResult.Location = new Point(3, 200);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.Size = new Size(853, 121);
            txtResult.TabIndex = 9;
            // 
            // txtFreeText
            // 
            txtFreeText.BorderStyle = BorderStyle.FixedSingle;
            txtFreeText.Location = new Point(266, 9);
            txtFreeText.Name = "txtFreeText";
            txtFreeText.Size = new Size(433, 23);
            txtFreeText.TabIndex = 7;
            // 
            // cmbFixedQuestions
            // 
            cmbFixedQuestions.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFixedQuestions.FormattingEnabled = true;
            cmbFixedQuestions.Location = new Point(266, 9);
            cmbFixedQuestions.Name = "cmbFixedQuestions";
            cmbFixedQuestions.Size = new Size(433, 23);
            cmbFixedQuestions.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(7, 12);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 4;
            label3.Text = "Analysis Type";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(9, 182);
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
            panel1.Size = new Size(853, 93);
            panel1.TabIndex = 2;
            // 
            // btnAnalyze
            // 
            btnAnalyze.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnAnalyze.Location = new Point(720, 8);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(125, 23);
            btnAnalyze.TabIndex = 0;
            btnAnalyze.Text = "Analyze Data Now";
            btnAnalyze.UseVisualStyleBackColor = true;
            btnAnalyze.Click += btnAnalyze_Click;
            // 
            // DataLoader_v1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 680);
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

        #endregion
        //private Data textFileSelector;
    }
}