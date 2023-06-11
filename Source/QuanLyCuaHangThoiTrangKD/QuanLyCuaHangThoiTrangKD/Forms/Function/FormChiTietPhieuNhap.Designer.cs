namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    partial class FormChiTietPhieuNhap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChiTietPhieuNhap));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.lblThueVAT = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Thanhtien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dongia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMaPN = new System.Windows.Forms.Label();
            this.Soluong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tensp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdDsCTPN = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.lblTongcong = new System.Windows.Forms.Label();
            this.lblTongthanhtien = new System.Windows.Forms.Label();
            this.lblSDTNCC = new System.Windows.Forms.Label();
            this.lblTenNCC = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTenCuahang = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNgaylap = new System.Windows.Forms.Label();
            this.btnQuaylai = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.lblTinhtrangPN = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdDsCTPN)).BeginInit();
            this.SuspendLayout();
            // 
            // lblThueVAT
            // 
            this.lblThueVAT.AutoSize = true;
            this.lblThueVAT.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblThueVAT.ForeColor = System.Drawing.Color.Red;
            this.lblThueVAT.Location = new System.Drawing.Point(300, 808);
            this.lblThueVAT.Name = "lblThueVAT";
            this.lblThueVAT.Size = new System.Drawing.Size(163, 35);
            this.lblThueVAT.TabIndex = 2;
            this.lblThueVAT.Text = "Mã hóa đơn:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(44, 808);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(203, 35);
            this.label12.TabIndex = 18;
            this.label12.Text = "Thuế VAT(10%):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(44, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 35);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mã phiếu nhập:";
            // 
            // Thanhtien
            // 
            this.Thanhtien.HeaderText = "Thành tiền";
            this.Thanhtien.Name = "Thanhtien";
            this.Thanhtien.ReadOnly = true;
            // 
            // Dongia
            // 
            this.Dongia.HeaderText = "Đơn giá";
            this.Dongia.Name = "Dongia";
            this.Dongia.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(381, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(418, 57);
            this.label4.TabIndex = 16;
            this.label4.Text = "PHIẾU NHẬP HÀNG";
            // 
            // lblMaPN
            // 
            this.lblMaPN.AutoSize = true;
            this.lblMaPN.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaPN.Location = new System.Drawing.Point(271, 122);
            this.lblMaPN.Name = "lblMaPN";
            this.lblMaPN.Size = new System.Drawing.Size(151, 35);
            this.lblMaPN.TabIndex = 15;
            this.lblMaPN.Text = "Mã hóa đơn:";
            // 
            // Soluong
            // 
            this.Soluong.HeaderText = "Số lượng";
            this.Soluong.Name = "Soluong";
            this.Soluong.ReadOnly = true;
            // 
            // Tensp
            // 
            this.Tensp.HeaderText = "Tên sản phẩm";
            this.Tensp.Name = "Tensp";
            this.Tensp.ReadOnly = true;
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenNV.Location = new System.Drawing.Point(300, 329);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(155, 35);
            this.lblTenNV.TabIndex = 14;
            this.lblTenNV.Text = "Mã hóa đơn:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(44, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 35);
            this.label7.TabIndex = 13;
            this.label7.Text = "Tên nhân viên:";
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // grdDsCTPN
            // 
            this.grdDsCTPN.AllowCustomTheming = false;
            this.grdDsCTPN.AllowUserToAddRows = false;
            this.grdDsCTPN.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.grdDsCTPN.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdDsCTPN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdDsCTPN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdDsCTPN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdDsCTPN.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDsCTPN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdDsCTPN.ColumnHeadersHeight = 40;
            this.grdDsCTPN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.Tensp,
            this.Soluong,
            this.Dongia,
            this.Thanhtien});
            this.grdDsCTPN.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.grdDsCTPN.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdDsCTPN.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.grdDsCTPN.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.grdDsCTPN.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.White;
            this.grdDsCTPN.CurrentTheme.BackColor = System.Drawing.Color.DarkSlateGray;
            this.grdDsCTPN.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.grdDsCTPN.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DarkSlateGray;
            this.grdDsCTPN.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.grdDsCTPN.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.grdDsCTPN.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.grdDsCTPN.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.grdDsCTPN.CurrentTheme.Name = null;
            this.grdDsCTPN.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.grdDsCTPN.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdDsCTPN.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.grdDsCTPN.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.grdDsCTPN.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDsCTPN.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdDsCTPN.EnableHeadersVisualStyles = false;
            this.grdDsCTPN.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.grdDsCTPN.HeaderBackColor = System.Drawing.Color.DarkSlateGray;
            this.grdDsCTPN.HeaderBgColor = System.Drawing.Color.Empty;
            this.grdDsCTPN.HeaderForeColor = System.Drawing.Color.White;
            this.grdDsCTPN.Location = new System.Drawing.Point(50, 383);
            this.grdDsCTPN.Name = "grdDsCTPN";
            this.grdDsCTPN.ReadOnly = true;
            this.grdDsCTPN.RowHeadersVisible = false;
            this.grdDsCTPN.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grdDsCTPN.RowTemplate.Height = 40;
            this.grdDsCTPN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDsCTPN.Size = new System.Drawing.Size(1046, 357);
            this.grdDsCTPN.TabIndex = 21;
            this.grdDsCTPN.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray;
            // 
            // lblTongcong
            // 
            this.lblTongcong.AutoSize = true;
            this.lblTongcong.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongcong.ForeColor = System.Drawing.Color.Red;
            this.lblTongcong.Location = new System.Drawing.Point(885, 758);
            this.lblTongcong.Name = "lblTongcong";
            this.lblTongcong.Size = new System.Drawing.Size(101, 35);
            this.lblTongcong.TabIndex = 12;
            this.lblTongcong.Text = "Địa chỉ:";
            // 
            // lblTongthanhtien
            // 
            this.lblTongthanhtien.AutoSize = true;
            this.lblTongthanhtien.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongthanhtien.ForeColor = System.Drawing.Color.Red;
            this.lblTongthanhtien.Location = new System.Drawing.Point(300, 758);
            this.lblTongthanhtien.Name = "lblTongthanhtien";
            this.lblTongthanhtien.Size = new System.Drawing.Size(85, 35);
            this.lblTongthanhtien.TabIndex = 11;
            this.lblTongthanhtien.Text = "00000";
            // 
            // lblSDTNCC
            // 
            this.lblSDTNCC.AutoSize = true;
            this.lblSDTNCC.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSDTNCC.Location = new System.Drawing.Point(300, 280);
            this.lblSDTNCC.Name = "lblSDTNCC";
            this.lblSDTNCC.Size = new System.Drawing.Size(96, 35);
            this.lblSDTNCC.TabIndex = 10;
            this.lblSDTNCC.Text = "Địa chỉ:";
            // 
            // lblTenNCC
            // 
            this.lblTenNCC.AutoSize = true;
            this.lblTenNCC.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenNCC.Location = new System.Drawing.Point(300, 231);
            this.lblTenNCC.Name = "lblTenNCC";
            this.lblTenNCC.Size = new System.Drawing.Size(96, 35);
            this.lblTenNCC.TabIndex = 9;
            this.lblTenNCC.Text = "Địa chỉ:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label14.Location = new System.Drawing.Point(703, 758);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(146, 35);
            this.label14.TabIndex = 8;
            this.label14.Text = "Tổng cộng:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(44, 280);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(177, 35);
            this.label9.TabIndex = 7;
            this.label9.Text = "Số điện thoại:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(44, 758);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 35);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tổng thành tiền:";
            // 
            // lblDiachi
            // 
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDiachi.Location = new System.Drawing.Point(271, 73);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(619, 35);
            this.lblDiachi.TabIndex = 5;
            this.lblDiachi.Text = "12 Nguyễn Văn Bảo,  Phường 4, Quận Gò Vấp, TP.HCM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(44, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 35);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên nhà cung cấp:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(44, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Địa chỉ:";
            // 
            // lblTenCuahang
            // 
            this.lblTenCuahang.AutoSize = true;
            this.lblTenCuahang.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenCuahang.Location = new System.Drawing.Point(271, 26);
            this.lblTenCuahang.Name = "lblTenCuahang";
            this.lblTenCuahang.Size = new System.Drawing.Size(126, 35);
            this.lblTenCuahang.TabIndex = 19;
            this.lblTenCuahang.Text = "KD SHOP ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(44, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 35);
            this.label1.TabIndex = 20;
            this.label1.Text = "Tên cửa hàng:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(644, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 35);
            this.label8.TabIndex = 13;
            this.label8.Text = "Ngày lập:";
            // 
            // lblNgaylap
            // 
            this.lblNgaylap.AutoSize = true;
            this.lblNgaylap.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNgaylap.Location = new System.Drawing.Point(858, 280);
            this.lblNgaylap.Name = "lblNgaylap";
            this.lblNgaylap.Size = new System.Drawing.Size(155, 35);
            this.lblNgaylap.TabIndex = 14;
            this.lblNgaylap.Text = "Mã hóa đơn:";
            // 
            // btnQuaylai
            // 
            this.btnQuaylai.AllowAnimations = true;
            this.btnQuaylai.AllowMouseEffects = true;
            this.btnQuaylai.AllowToggling = false;
            this.btnQuaylai.AnimationSpeed = 200;
            this.btnQuaylai.AutoGenerateColors = false;
            this.btnQuaylai.AutoRoundBorders = false;
            this.btnQuaylai.AutoSizeLeftIcon = true;
            this.btnQuaylai.AutoSizeRightIcon = true;
            this.btnQuaylai.BackColor = System.Drawing.Color.Transparent;
            this.btnQuaylai.BackColor1 = System.Drawing.Color.DimGray;
            this.btnQuaylai.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuaylai.BackgroundImage")));
            this.btnQuaylai.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnQuaylai.ButtonText = "Quay lại";
            this.btnQuaylai.ButtonTextMarginLeft = 0;
            this.btnQuaylai.ColorContrastOnClick = 45;
            this.btnQuaylai.ColorContrastOnHover = 45;
            this.btnQuaylai.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnQuaylai.CustomizableEdges = borderEdges1;
            this.btnQuaylai.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuaylai.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnQuaylai.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnQuaylai.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnQuaylai.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnQuaylai.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.btnQuaylai.ForeColor = System.Drawing.Color.White;
            this.btnQuaylai.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuaylai.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnQuaylai.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnQuaylai.IconMarginLeft = 11;
            this.btnQuaylai.IconPadding = 10;
            this.btnQuaylai.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuaylai.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnQuaylai.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnQuaylai.IconSize = 25;
            this.btnQuaylai.IdleBorderColor = System.Drawing.Color.DimGray;
            this.btnQuaylai.IdleBorderRadius = 30;
            this.btnQuaylai.IdleBorderThickness = 1;
            this.btnQuaylai.IdleFillColor = System.Drawing.Color.DimGray;
            this.btnQuaylai.IdleIconLeftImage = null;
            this.btnQuaylai.IdleIconRightImage = null;
            this.btnQuaylai.IndicateFocus = false;
            this.btnQuaylai.Location = new System.Drawing.Point(891, 819);
            this.btnQuaylai.Name = "btnQuaylai";
            this.btnQuaylai.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnQuaylai.OnDisabledState.BorderRadius = 30;
            this.btnQuaylai.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnQuaylai.OnDisabledState.BorderThickness = 1;
            this.btnQuaylai.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnQuaylai.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnQuaylai.OnDisabledState.IconLeftImage = null;
            this.btnQuaylai.OnDisabledState.IconRightImage = null;
            this.btnQuaylai.onHoverState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuaylai.onHoverState.BorderRadius = 30;
            this.btnQuaylai.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnQuaylai.onHoverState.BorderThickness = 1;
            this.btnQuaylai.onHoverState.FillColor = System.Drawing.Color.DarkGray;
            this.btnQuaylai.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnQuaylai.onHoverState.IconLeftImage = null;
            this.btnQuaylai.onHoverState.IconRightImage = null;
            this.btnQuaylai.OnIdleState.BorderColor = System.Drawing.Color.DimGray;
            this.btnQuaylai.OnIdleState.BorderRadius = 30;
            this.btnQuaylai.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnQuaylai.OnIdleState.BorderThickness = 1;
            this.btnQuaylai.OnIdleState.FillColor = System.Drawing.Color.DimGray;
            this.btnQuaylai.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnQuaylai.OnIdleState.IconLeftImage = null;
            this.btnQuaylai.OnIdleState.IconRightImage = null;
            this.btnQuaylai.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnQuaylai.OnPressedState.BorderRadius = 30;
            this.btnQuaylai.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnQuaylai.OnPressedState.BorderThickness = 1;
            this.btnQuaylai.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnQuaylai.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnQuaylai.OnPressedState.IconLeftImage = null;
            this.btnQuaylai.OnPressedState.IconRightImage = null;
            this.btnQuaylai.Size = new System.Drawing.Size(195, 50);
            this.btnQuaylai.TabIndex = 22;
            this.btnQuaylai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnQuaylai.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnQuaylai.TextMarginLeft = 0;
            this.btnQuaylai.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnQuaylai.UseDefaultRadiusAndThickness = true;
            this.btnQuaylai.Click += new System.EventHandler(this.btnQuaylai_Click);
            // 
            // lblTinhtrangPN
            // 
            this.lblTinhtrangPN.AutoSize = true;
            this.lblTinhtrangPN.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTinhtrangPN.Location = new System.Drawing.Point(820, 178);
            this.lblTinhtrangPN.Name = "lblTinhtrangPN";
            this.lblTinhtrangPN.Size = new System.Drawing.Size(126, 35);
            this.lblTinhtrangPN.TabIndex = 13;
            this.lblTinhtrangPN.Text = "Ngày lập:";
            // 
            // FormChiTietPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 939);
            this.Controls.Add(this.btnQuaylai);
            this.Controls.Add(this.lblThueVAT);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMaPN);
            this.Controls.Add(this.lblNgaylap);
            this.Controls.Add(this.lblTenNV);
            this.Controls.Add(this.lblTinhtrangPN);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.grdDsCTPN);
            this.Controls.Add(this.lblTongcong);
            this.Controls.Add(this.lblTongthanhtien);
            this.Controls.Add(this.lblSDTNCC);
            this.Controls.Add(this.lblTenNCC);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDiachi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTenCuahang);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1159, 939);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1159, 939);
            this.Name = "FormChiTietPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormChiTietPhieuNhap";
            ((System.ComponentModel.ISupportInitialize)(this.grdDsCTPN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblThueVAT;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thanhtien;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dongia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMaPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Soluong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tensp;
        private System.Windows.Forms.Label lblTenNV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private Bunifu.UI.WinForms.BunifuDataGridView grdDsCTPN;
        private System.Windows.Forms.Label lblTongcong;
        private System.Windows.Forms.Label lblTongthanhtien;
        private System.Windows.Forms.Label lblSDTNCC;
        private System.Windows.Forms.Label lblTenNCC;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDiachi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTenCuahang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNgaylap;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnQuaylai;
        private System.Windows.Forms.Label lblTinhtrangPN;
    }
}