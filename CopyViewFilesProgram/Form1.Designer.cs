namespace CopyViewFilesProgram
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
            this.CopyButton = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.targetSrc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ViewSource = new System.Windows.Forms.TextBox();
            this.example1 = new System.Windows.Forms.Button();
            this.example2 = new System.Windows.Forms.Button();
            this.example3 = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CopyCommonSource = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(183, 143);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(79, 26);
            this.CopyButton.TabIndex = 1;
            this.CopyButton.Text = "복사";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(183, 24);
            this.txtSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(195, 21);
            this.txtSource.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "TXT경로";
            // 
            // targetSrc
            // 
            this.targetSrc.Location = new System.Drawing.Point(183, 58);
            this.targetSrc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.targetSrc.Name = "targetSrc";
            this.targetSrc.Size = new System.Drawing.Size(195, 21);
            this.targetSrc.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "복사경로";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "프로젝트 View파일 경로";
            // 
            // ViewSource
            // 
            this.ViewSource.Location = new System.Drawing.Point(183, 94);
            this.ViewSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ViewSource.Name = "ViewSource";
            this.ViewSource.Size = new System.Drawing.Size(195, 21);
            this.ViewSource.TabIndex = 7;
            // 
            // example1
            // 
            this.example1.Location = new System.Drawing.Point(400, 24);
            this.example1.Name = "example1";
            this.example1.Size = new System.Drawing.Size(75, 23);
            this.example1.TabIndex = 8;
            this.example1.Text = "예시";
            this.example1.UseVisualStyleBackColor = true;
            this.example1.Click += new System.EventHandler(this.example1_Click);
            // 
            // example2
            // 
            this.example2.Location = new System.Drawing.Point(400, 58);
            this.example2.Name = "example2";
            this.example2.Size = new System.Drawing.Size(75, 23);
            this.example2.TabIndex = 9;
            this.example2.Text = "예시";
            this.example2.UseVisualStyleBackColor = true;
            this.example2.Click += new System.EventHandler(this.example2_Click);
            // 
            // example3
            // 
            this.example3.Location = new System.Drawing.Point(400, 95);
            this.example3.Name = "example3";
            this.example3.Size = new System.Drawing.Size(75, 23);
            this.example3.TabIndex = 10;
            this.example3.Text = "예시";
            this.example3.UseVisualStyleBackColor = true;
            this.example3.Click += new System.EventHandler(this.example3_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(396, 143);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(79, 26);
            this.CloseButton.TabIndex = 11;
            this.CloseButton.Text = "종료";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // CopyCommonSource
            // 
            this.CopyCommonSource.Location = new System.Drawing.Point(280, 143);
            this.CopyCommonSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CopyCommonSource.Name = "CopyCommonSource";
            this.CopyCommonSource.Size = new System.Drawing.Size(102, 26);
            this.CopyCommonSource.TabIndex = 12;
            this.CopyCommonSource.Text = "공통소스복사";
            this.CopyCommonSource.UseVisualStyleBackColor = true;
            this.CopyCommonSource.Click += new System.EventHandler(this.CopyCommonSource_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 193);
            this.Controls.Add(this.CopyCommonSource);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.example3);
            this.Controls.Add(this.example2);
            this.Controls.Add(this.example1);
            this.Controls.Add(this.ViewSource);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.targetSrc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.CopyButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "뷰 파일 복사 프로그램";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox targetSrc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ViewSource;
        private System.Windows.Forms.Button example1;
        private System.Windows.Forms.Button example2;
        private System.Windows.Forms.Button example3;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button CopyCommonSource;
    }
}

