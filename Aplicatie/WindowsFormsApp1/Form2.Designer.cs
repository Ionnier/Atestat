namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txttip = new System.Windows.Forms.TextBox();
            this.txtpret = new System.Windows.Forms.TextBox();
            this.txtcantitate = new System.Windows.Forms.TextBox();
            this.buttoninsert = new System.Windows.Forms.Button();
            this.buttonupdate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataprodus = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtid = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataprodus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "TIP PRODUS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "PRET";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "CANTITATE";
            // 
            // txttip
            // 
            this.txttip.Location = new System.Drawing.Point(270, 77);
            this.txttip.Name = "txttip";
            this.txttip.Size = new System.Drawing.Size(152, 20);
            this.txttip.TabIndex = 3;
            this.txttip.TextChanged += new System.EventHandler(this.txttip_TextChanged);
            // 
            // txtpret
            // 
            this.txtpret.Location = new System.Drawing.Point(270, 137);
            this.txtpret.Name = "txtpret";
            this.txtpret.Size = new System.Drawing.Size(152, 20);
            this.txtpret.TabIndex = 4;
            // 
            // txtcantitate
            // 
            this.txtcantitate.Location = new System.Drawing.Point(270, 200);
            this.txtcantitate.Name = "txtcantitate";
            this.txtcantitate.Size = new System.Drawing.Size(152, 20);
            this.txtcantitate.TabIndex = 5;
            this.txtcantitate.TextChanged += new System.EventHandler(this.txtcantitate_TextChanged);
            // 
            // buttoninsert
            // 
            this.buttoninsert.Location = new System.Drawing.Point(35, 258);
            this.buttoninsert.Name = "buttoninsert";
            this.buttoninsert.Size = new System.Drawing.Size(127, 23);
            this.buttoninsert.TabIndex = 6;
            this.buttoninsert.Text = "Insert";
            this.buttoninsert.UseVisualStyleBackColor = true;
            this.buttoninsert.Click += new System.EventHandler(this.buttoninsert_Click);
            // 
            // buttonupdate
            // 
            this.buttonupdate.Location = new System.Drawing.Point(202, 257);
            this.buttonupdate.Name = "buttonupdate";
            this.buttonupdate.Size = new System.Drawing.Size(115, 23);
            this.buttonupdate.TabIndex = 7;
            this.buttonupdate.Text = "Update";
            this.buttonupdate.UseVisualStyleBackColor = true;
            this.buttonupdate.Click += new System.EventHandler(this.buttonupdate_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(358, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataprodus
            // 
            this.dataprodus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataprodus.Location = new System.Drawing.Point(582, 12);
            this.dataprodus.Name = "dataprodus";
            this.dataprodus.Size = new System.Drawing.Size(406, 373);
            this.dataprodus.TabIndex = 9;
            this.dataprodus.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataprodus_CellContentClick);
            this.dataprodus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataprodus_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "SEARCH VIA PRODUCT ID";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(311, 364);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(148, 20);
            this.txtid.TabIndex = 12;
            this.txtid.TextChanged += new System.EventHandler(this.txtid_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(910, 404);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Log out";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(790, 409);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Time elapsed: h:mm:ss";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 409);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "label6";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 439);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtid);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataprodus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonupdate);
            this.Controls.Add(this.buttoninsert);
            this.Controls.Add(this.txtcantitate);
            this.Controls.Add(this.txtpret);
            this.Controls.Add(this.txttip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataprodus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttip;
        private System.Windows.Forms.TextBox txtpret;
        private System.Windows.Forms.TextBox txtcantitate;
        private System.Windows.Forms.Button buttoninsert;
        private System.Windows.Forms.Button buttonupdate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataprodus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
    }
}