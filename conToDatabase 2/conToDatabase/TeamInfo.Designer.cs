namespace conToDatabase
{
    partial class TeamInfo
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(47, 109);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 37;
            this.dataGridView1.Size = new System.Drawing.Size(1046, 395);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(47, 593);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 37;
            this.dataGridView2.Size = new System.Drawing.Size(1046, 441);
            this.dataGridView2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(104, 520);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 57);
            this.button1.TabIndex = 14;
            this.button1.Text = "添加其他人员";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.addOthClick);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(93, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(198, 57);
            this.button2.TabIndex = 15;
            this.button2.Text = "添加运动员";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.addAthClick);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(776, 46);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(243, 57);
            this.button3.TabIndex = 17;
            this.button3.Text = "编辑所选运动员";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.editClick);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(427, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(231, 57);
            this.button4.TabIndex = 16;
            this.button4.Text = "删除所选运动员";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.deleteClick);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(442, 520);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(197, 57);
            this.button5.TabIndex = 18;
            this.button5.Text = "删除其他人员";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.deleteOthClick);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("宋体", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Location = new System.Drawing.Point(776, 520);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(269, 57);
            this.button6.TabIndex = 19;
            this.button6.Text = "编辑所选其他人员";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.editOthClick);
            // 
            // TeamInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 1060);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TeamInfo";
            this.Text = "TeamInfo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}