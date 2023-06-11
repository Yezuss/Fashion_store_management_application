
namespace QuanLyCuaHangThoiTrangKD.Forms
{
    partial class FormDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDangNhap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorTentaikhoan = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorMatkhau = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.chkHienthiMK = new Bunifu.UI.WinForms.BunifuCheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTentaikhoan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMatkhau)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 353);
            this.panel1.TabIndex = 19;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::QuanLyCuaHangThoiTrangKD.Properties.Resources.facebook_cover_photo_1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(222, 353);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.LightGray;
            this.btnLogin.Location = new System.Drawing.Point(304, 276);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(218, 42);
            this.btnLogin.TabIndex = 12;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(456, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 33);
            this.label3.TabIndex = 24;
            this.label3.Text = "ĐĂNG NHẬP";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Silver;
            this.txtPassword.Location = new System.Drawing.Point(304, 201);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(449, 36);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            this.txtPassword.Validated += new System.EventHandler(this.txtPassword_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(303, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 29);
            this.label2.TabIndex = 20;
            this.label2.Text = "Mật khẩu";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.Silver;
            this.txtUser.Location = new System.Drawing.Point(304, 118);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(449, 36);
            this.txtUser.TabIndex = 10;
            this.txtUser.Validating += new System.ComponentModel.CancelEventHandler(this.txtUser_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(299, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 29);
            this.label1.TabIndex = 21;
            this.label1.Text = "Tên tài khoản";
            // 
            // errorTentaikhoan
            // 
            this.errorTentaikhoan.ContainerControl = this;
            // 
            // errorMatkhau
            // 
            this.errorMatkhau.ContainerControl = this;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.LightGray;
            this.btnExit.Location = new System.Drawing.Point(608, 276);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(145, 42);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkHienthiMK
            // 
            this.chkHienthiMK.AllowBindingControlAnimation = true;
            this.chkHienthiMK.AllowBindingControlColorChanges = false;
            this.chkHienthiMK.AllowBindingControlLocation = true;
            this.chkHienthiMK.AllowCheckBoxAnimation = false;
            this.chkHienthiMK.AllowCheckmarkAnimation = true;
            this.chkHienthiMK.AllowOnHoverStates = true;
            this.chkHienthiMK.AutoCheck = true;
            this.chkHienthiMK.BackColor = System.Drawing.Color.Transparent;
            this.chkHienthiMK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkHienthiMK.BackgroundImage")));
            this.chkHienthiMK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkHienthiMK.BindingControlPosition = Bunifu.UI.WinForms.BunifuCheckBox.BindingControlPositions.Right;
            this.chkHienthiMK.BorderRadius = 12;
            this.chkHienthiMK.Checked = false;
            this.chkHienthiMK.CheckState = Bunifu.UI.WinForms.BunifuCheckBox.CheckStates.Unchecked;
            this.chkHienthiMK.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkHienthiMK.CustomCheckmarkImage = null;
            this.chkHienthiMK.Location = new System.Drawing.Point(556, 168);
            this.chkHienthiMK.MinimumSize = new System.Drawing.Size(17, 17);
            this.chkHienthiMK.Name = "chkHienthiMK";
            this.chkHienthiMK.OnCheck.BorderColor = System.Drawing.Color.DodgerBlue;
            this.chkHienthiMK.OnCheck.BorderRadius = 12;
            this.chkHienthiMK.OnCheck.BorderThickness = 2;
            this.chkHienthiMK.OnCheck.CheckBoxColor = System.Drawing.Color.DodgerBlue;
            this.chkHienthiMK.OnCheck.CheckmarkColor = System.Drawing.Color.White;
            this.chkHienthiMK.OnCheck.CheckmarkThickness = 2;
            this.chkHienthiMK.OnDisable.BorderColor = System.Drawing.Color.LightGray;
            this.chkHienthiMK.OnDisable.BorderRadius = 12;
            this.chkHienthiMK.OnDisable.BorderThickness = 2;
            this.chkHienthiMK.OnDisable.CheckBoxColor = System.Drawing.Color.Transparent;
            this.chkHienthiMK.OnDisable.CheckmarkColor = System.Drawing.Color.LightGray;
            this.chkHienthiMK.OnDisable.CheckmarkThickness = 2;
            this.chkHienthiMK.OnHoverChecked.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.chkHienthiMK.OnHoverChecked.BorderRadius = 12;
            this.chkHienthiMK.OnHoverChecked.BorderThickness = 2;
            this.chkHienthiMK.OnHoverChecked.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.chkHienthiMK.OnHoverChecked.CheckmarkColor = System.Drawing.Color.White;
            this.chkHienthiMK.OnHoverChecked.CheckmarkThickness = 2;
            this.chkHienthiMK.OnHoverUnchecked.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.chkHienthiMK.OnHoverUnchecked.BorderRadius = 12;
            this.chkHienthiMK.OnHoverUnchecked.BorderThickness = 1;
            this.chkHienthiMK.OnHoverUnchecked.CheckBoxColor = System.Drawing.Color.Transparent;
            this.chkHienthiMK.OnUncheck.BorderColor = System.Drawing.Color.DarkGray;
            this.chkHienthiMK.OnUncheck.BorderRadius = 12;
            this.chkHienthiMK.OnUncheck.BorderThickness = 1;
            this.chkHienthiMK.OnUncheck.CheckBoxColor = System.Drawing.Color.Transparent;
            this.chkHienthiMK.Size = new System.Drawing.Size(25, 25);
            this.chkHienthiMK.Style = Bunifu.UI.WinForms.BunifuCheckBox.CheckBoxStyles.Bunifu;
            this.chkHienthiMK.TabIndex = 28;
            this.chkHienthiMK.ThreeState = false;
            this.chkHienthiMK.ToolTipText = null;
            this.chkHienthiMK.CheckedChanged += new System.EventHandler<Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs>(this.chkHienthiMK_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(587, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "Hiển thị mật khẩu";
            // 
            // FormDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(834, 353);
            this.Controls.Add(this.chkHienthiMK);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormDangNhap";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTentaikhoan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMatkhau)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorTentaikhoan;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider errorMatkhau;
        private Bunifu.UI.WinForms.BunifuCheckBox chkHienthiMK;
        private System.Windows.Forms.Label label4;
    }
}