namespace RobotMappingProject
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.mbedData = new System.IO.Ports.SerialPort(this.components);
            this.portBox = new System.Windows.Forms.ComboBox();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.portButton = new System.Windows.Forms.Button();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.testData = new System.Windows.Forms.Button();
            this.mapImage = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.SuspendLayout();
            // 
            // portBox
            // 
            this.portBox.FormattingEnabled = true;
            this.portBox.Location = new System.Drawing.Point(705, 12);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(121, 24);
            this.portBox.TabIndex = 0;
            this.portBox.MouseEnter += new System.EventHandler(this.portBox_MouseEnter);
            // 
            // dataView
            // 
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Location = new System.Drawing.Point(12, 12);
            this.dataView.Name = "dataView";
            this.dataView.RowTemplate.Height = 24;
            this.dataView.Size = new System.Drawing.Size(687, 172);
            this.dataView.TabIndex = 2;
            // 
            // portButton
            // 
            this.portButton.Location = new System.Drawing.Point(705, 42);
            this.portButton.Name = "portButton";
            this.portButton.Size = new System.Drawing.Size(121, 34);
            this.portButton.TabIndex = 3;
            this.portButton.Text = "Select COM Port";
            this.portButton.UseVisualStyleBackColor = true;
            this.portButton.Click += new System.EventHandler(this.portButton_Click);
            // 
            // errorBox
            // 
            this.errorBox.Location = new System.Drawing.Point(705, 82);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.Size = new System.Drawing.Size(121, 64);
            this.errorBox.TabIndex = 4;
            // 
            // testData
            // 
            this.testData.Location = new System.Drawing.Point(705, 152);
            this.testData.Name = "testData";
            this.testData.Size = new System.Drawing.Size(121, 32);
            this.testData.TabIndex = 5;
            this.testData.Text = "Test Sending Data";
            this.testData.UseVisualStyleBackColor = true;
            this.testData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.testData_MouseClick);
            // 
            // mapImage
            // 
            this.mapImage.Location = new System.Drawing.Point(12, 190);
            this.mapImage.Name = "mapImage";
            this.mapImage.Size = new System.Drawing.Size(885, 643);
            this.mapImage.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 845);
            this.Controls.Add(this.mapImage);
            this.Controls.Add(this.testData);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.portButton);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.portBox);
            this.Name = "MainWindow";
            this.Text = "Mapping Project";
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox portBox;
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.Button portButton;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.Button testData;
        private System.Windows.Forms.Panel mapImage;
        internal System.IO.Ports.SerialPort mbedData;
    }
}

