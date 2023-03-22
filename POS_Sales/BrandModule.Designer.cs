
namespace POS_Sales
{
    partial class BrandModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrandModule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.picclose = new System.Windows.Forms.PictureBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnupdate = new System.Windows.Forms.Button();
            this.btncansel = new System.Windows.Forms.Button();
            this.lblid = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picclose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(170)))));
            this.panel1.Controls.Add(this.picclose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 50);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brand Module";
            // 
            // picclose
            // 
            this.picclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picclose.Image = ((System.Drawing.Image)(resources.GetObject("picclose.Image")));
            this.picclose.Location = new System.Drawing.Point(686, 3);
            this.picclose.Name = "picclose";
            this.picclose.Size = new System.Drawing.Size(27, 24);
            this.picclose.TabIndex = 1;
            this.picclose.TabStop = false;
            this.picclose.Click += new System.EventHandler(this.picclose_Click);
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(135, 88);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(542, 26);
            this.txtBrand.TabIndex = 1;
            // 
            // btnsave
            // 
            this.btnsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(160)))));
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(377, 133);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(95, 34);
            this.btnsave.TabIndex = 2;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Brand Name :";
            // 
            // btnupdate
            // 
            this.btnupdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnupdate.BackColor = System.Drawing.Color.DarkRed;
            this.btnupdate.FlatAppearance.BorderSize = 0;
            this.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnupdate.ForeColor = System.Drawing.Color.White;
            this.btnupdate.Location = new System.Drawing.Point(480, 133);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(95, 34);
            this.btnupdate.TabIndex = 6;
            this.btnupdate.Text = "Update";
            this.btnupdate.UseVisualStyleBackColor = false;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // btncansel
            // 
            this.btncansel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btncansel.BackColor = System.Drawing.Color.Gray;
            this.btncansel.FlatAppearance.BorderSize = 0;
            this.btncansel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncansel.ForeColor = System.Drawing.Color.White;
            this.btncansel.Location = new System.Drawing.Point(582, 133);
            this.btncansel.Name = "btncansel";
            this.btncansel.Size = new System.Drawing.Size(95, 34);
            this.btncansel.TabIndex = 7;
            this.btncansel.Text = "Cansel";
            this.btncansel.UseVisualStyleBackColor = false;
            this.btncansel.Click += new System.EventHandler(this.btncansel_Click);
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Location = new System.Drawing.Point(21, 155);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(22, 20);
            this.lblid.TabIndex = 8;
            this.lblid.Text = "id";
            this.lblid.Visible = false;
            // 
            // BrandModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(716, 184);
            this.Controls.Add(this.lblid);
            this.Controls.Add(this.btncansel);
            this.Controls.Add(this.btnupdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BrandModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BrandModule";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picclose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picclose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtBrand;
        public System.Windows.Forms.Button btnsave;
        public System.Windows.Forms.Button btnupdate;
        public System.Windows.Forms.Button btncansel;
        public System.Windows.Forms.Label lblid;
    }
}