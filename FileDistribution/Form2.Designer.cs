namespace FileDistribution
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.NewFileGrid = new System.Windows.Forms.DataGridView();
            this.Files = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deployToServ = new System.Windows.Forms.Button();
            this.DeployToAll = new System.Windows.Forms.Button();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.DeployResultView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.NewFileGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeployResultView)).BeginInit();
            this.SuspendLayout();
            // 
            // NewFileGrid
            // 
            this.NewFileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewFileGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Files,
            this.Location});
            this.NewFileGrid.Location = new System.Drawing.Point(26, 134);
            this.NewFileGrid.Name = "NewFileGrid";
            this.NewFileGrid.RowTemplate.Height = 23;
            this.NewFileGrid.Size = new System.Drawing.Size(743, 231);
            this.NewFileGrid.TabIndex = 0;
            // 
            // Files
            // 
            this.Files.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Files.HeaderText = "파일";
            this.Files.Name = "Files";
            // 
            // Location
            // 
            this.Location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Location.HeaderText = "위치";
            this.Location.Name = "Location";
            // 
            // deployToServ
            // 
            this.deployToServ.Location = new System.Drawing.Point(593, 23);
            this.deployToServ.Name = "deployToServ";
            this.deployToServ.Size = new System.Drawing.Size(75, 23);
            this.deployToServ.TabIndex = 1;
            this.deployToServ.Text = "배포";
            this.deployToServ.UseVisualStyleBackColor = true;
            this.deployToServ.Click += new System.EventHandler(this.deployToServ_Click);
            // 
            // DeployToAll
            // 
            this.DeployToAll.Location = new System.Drawing.Point(694, 23);
            this.DeployToAll.Name = "DeployToAll";
            this.DeployToAll.Size = new System.Drawing.Size(75, 23);
            this.DeployToAll.TabIndex = 2;
            this.DeployToAll.Text = "전체배포";
            this.DeployToAll.UseVisualStyleBackColor = true;
            this.DeployToAll.Click += new System.EventHandler(this.DeployToAll_Click);
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Font = new System.Drawing.Font("굴림", 15F);
            this.ResultLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.ResultLabel.Location = new System.Drawing.Point(26, 91);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(0, 20);
            this.ResultLabel.TabIndex = 3;
            // 
            // DeployResultView
            // 
            this.DeployResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeployResultView.Location = new System.Drawing.Point(26, 400);
            this.DeployResultView.Name = "DeployResultView";
            this.DeployResultView.RowTemplate.Height = 23;
            this.DeployResultView.Size = new System.Drawing.Size(743, 150);
            this.DeployResultView.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 573);
            this.Controls.Add(this.DeployResultView);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.DeployToAll);
            this.Controls.Add(this.deployToServ);
            this.Controls.Add(this.NewFileGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "신규파일 리스트 (ABS_MES 프로젝트 폴더 한정)";
            ((System.ComponentModel.ISupportInitialize)(this.NewFileGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeployResultView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView NewFileGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Files;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.Button deployToServ;
        private System.Windows.Forms.Button DeployToAll;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.DataGridView DeployResultView;
    }
}