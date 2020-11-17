namespace FileDistribution
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.targetLocation = new System.Windows.Forms.TextBox();
            this.sourceLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deployToServ = new System.Windows.Forms.Button();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.DeployToAll = new System.Windows.Forms.Button();
            this.DeployResultView = new System.Windows.Forms.DataGridView();
            this.ResultListTable = new System.Windows.Forms.Label();
            this.NewFileList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DeployResultView)).BeginInit();
            this.SuspendLayout();
            // 
            // targetLocation
            // 
            this.targetLocation.Location = new System.Drawing.Point(489, 38);
            this.targetLocation.Name = "targetLocation";
            this.targetLocation.Size = new System.Drawing.Size(262, 21);
            this.targetLocation.TabIndex = 0;
            // 
            // sourceLocation
            // 
            this.sourceLocation.Location = new System.Drawing.Point(102, 38);
            this.sourceLocation.Name = "sourceLocation";
            this.sourceLocation.Size = new System.Drawing.Size(262, 21);
            this.sourceLocation.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "파일 위치";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "타겟 위치";
            // 
            // deployToServ
            // 
            this.deployToServ.Location = new System.Drawing.Point(490, 91);
            this.deployToServ.Name = "deployToServ";
            this.deployToServ.Size = new System.Drawing.Size(75, 23);
            this.deployToServ.TabIndex = 4;
            this.deployToServ.Text = "배포";
            this.deployToServ.UseVisualStyleBackColor = true;
            this.deployToServ.Click += new System.EventHandler(this.deployToServ_Click);
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Font = new System.Drawing.Font("굴림", 15F);
            this.ResultLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.ResultLabel.Location = new System.Drawing.Point(98, 89);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(0, 20);
            this.ResultLabel.TabIndex = 5;
            // 
            // DeployToAll
            // 
            this.DeployToAll.Location = new System.Drawing.Point(584, 91);
            this.DeployToAll.Name = "DeployToAll";
            this.DeployToAll.Size = new System.Drawing.Size(75, 23);
            this.DeployToAll.TabIndex = 6;
            this.DeployToAll.Text = "전체배포";
            this.DeployToAll.UseVisualStyleBackColor = true;
            this.DeployToAll.Click += new System.EventHandler(this.DeployToAll_Click);
            // 
            // DeployResultView
            // 
            this.DeployResultView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DeployResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeployResultView.Location = new System.Drawing.Point(37, 170);
            this.DeployResultView.Name = "DeployResultView";
            this.DeployResultView.ReadOnly = true;
            this.DeployResultView.RowTemplate.Height = 23;
            this.DeployResultView.Size = new System.Drawing.Size(714, 193);
            this.DeployResultView.TabIndex = 7;
            // 
            // ResultListTable
            // 
            this.ResultListTable.AutoSize = true;
            this.ResultListTable.Font = new System.Drawing.Font("LG Smart UI Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ResultListTable.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.ResultListTable.Location = new System.Drawing.Point(33, 134);
            this.ResultListTable.Name = "ResultListTable";
            this.ResultListTable.Size = new System.Drawing.Size(67, 23);
            this.ResultListTable.TabIndex = 8;
            this.ResultListTable.Text = "결과 뷰";
            // 
            // NewFileList
            // 
            this.NewFileList.Location = new System.Drawing.Point(676, 91);
            this.NewFileList.Name = "NewFileList";
            this.NewFileList.Size = new System.Drawing.Size(75, 23);
            this.NewFileList.TabIndex = 9;
            this.NewFileList.Text = "신규파일";
            this.NewFileList.UseVisualStyleBackColor = true;
            this.NewFileList.Click += new System.EventHandler(this.NewFileList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 375);
            this.Controls.Add(this.NewFileList);
            this.Controls.Add(this.ResultListTable);
            this.Controls.Add(this.DeployResultView);
            this.Controls.Add(this.DeployToAll);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.deployToServ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceLocation);
            this.Controls.Add(this.targetLocation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "파일 복사 프로그램";
            ((System.ComponentModel.ISupportInitialize)(this.DeployResultView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox targetLocation;
        private System.Windows.Forms.TextBox sourceLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deployToServ;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Button DeployToAll;
        private System.Windows.Forms.DataGridView DeployResultView;
        private System.Windows.Forms.Label ResultListTable;
        private System.Windows.Forms.Button NewFileList;
    }
}

