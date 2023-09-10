namespace Crescent.WinForms
{
    partial class DataLoader
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
            browseButton = new Button();
            textFileSelector = new TextBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // browseButton
            // 
            browseButton.Location = new Point(653, 55);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 0;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // textFileSelector
            // 
            textFileSelector.Location = new Point(36, 55);
            textFileSelector.Name = "textFileSelector";
            textFileSelector.Size = new Size(593, 23);
            textFileSelector.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(39, 90);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(749, 359);
            dataGridView1.TabIndex = 2;
            // 
            // DataLoader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(textFileSelector);
            Controls.Add(browseButton);
            Name = "DataLoader";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button browseButton;
        private TextBox textFileSelector;
        private DataGridView dataGridView1;
        //private Data textFileSelector;
    }
}