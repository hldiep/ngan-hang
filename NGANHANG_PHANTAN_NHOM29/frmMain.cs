﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NGANHANG_PHANTAN_NHOM29
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            BC.Visible = NV.Visible = DM.Visible = false;
            Form frm = this.CheckExists(typeof(frmLogin));
            if (frm != null) frm.Activate();
            else
            {
                frmLogin f = new frmLogin();
                f.MdiParent = this;
                f.Show();
            }
        }
        private Form CheckExists( Type ftype)
        {
            foreach( Form f in this.MdiChildren)
            {
                if( f.GetType() == ftype)
                {
                    return f;
                }
            }
            return null;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            HT_buttonDangKy.Enabled = HT_buttonTaoTaiKhoan.Enabled = false;
        }
        public void hienThiTool()
        {
            foreach (Form frm in MdiChildren)
            {
                frm.Close();
            }
            STS_MANV.Text = Program.username;
            STS_HOVATEN.Text = Program.mHoten;
            STS_NHOM.Text = Program.mGroup;
            // Bật theo nhóm quyền của nó
            HT_buttonDangNhap.Enabled = false;
            HT_buttonDangKy.Enabled = true;
            
            if (STS_NHOM.Text == "NGANHANG")
            {
                HT_buttonTaoTaiKhoan.Enabled = true;
                DM.Visible = BC.Visible = true;
            }
            else if (STS_NHOM.Text == "CHINHANH")
            {
                HT_buttonTaoTaiKhoan.Enabled = true;
                BC.Visible = DM.Visible = NV.Visible = true;
            }
            else
            {
                BC.Visible = true;
            }
}

        private void HT_buttonDangKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Xác nhận đăng xuất khỏi tài khoản \n - Mã NV : " + Program.username + "\n - Tên : " + Program.mHoten + "\n - Nhóm : " + Program.mGroup, "Xác Nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    string cmd = "EXEC TerminateSessionsForLogin '" + Program.mlogin + "'";
                    Program.ExecSqlNonQuery(cmd);
                    if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                    {
                        Program.conn.Close();
                        Program.conn.Dispose();
                    }
                    foreach (Form frm in MdiChildren)
                    {
                        frm.Close();
                    }
                    STS_MANV.Text = "MANV";
                    STS_HOVATEN.Text = "HOVATEN";
                    STS_NHOM.Text = "NHOM";
                    BC.Visible = DM.Visible = NV.Visible = false;
                    HT_buttonDangKy.Enabled = HT_buttonTaoTaiKhoan.Enabled = false;
                    HT_buttonDangNhap.Enabled = true;
                }
            }
            catch( Exception ex)
            {
                MessageBox.Show("Không thể đăng xuất!!!", "", MessageBoxButtons.OK);
                return;
            }
        }

        private void HT_buttonDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists( typeof(frmLogin));
            if (frm != null) frm.Activate();
            else
            {
                frmLogin f = new frmLogin();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void DM_buttonKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frmKhachHang = this.CheckExists(typeof(frmKhachHang));
            if (frmKhachHang != null) frmKhachHang.Activate();
            else
            {
                frmKhachHang f = new frmKhachHang();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void DM_buttonNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmNhanVien));
            if (frm != null) frm.Activate();
            else
            {
                frmNhanVien f = new frmNhanVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void HT_buttonTaoTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmTaoTaiKhoan));
            if (f != null) f.Activate();
            else
            {
                frmTaoTaiKhoan frm = new frmTaoTaiKhoan();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void NV_butotnGuiVaRut_ItemDoubleClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void NV_butotnGuiVaRut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmGuiRutTien));
            if (f != null) f.Activate();
            else
            {
                frmGuiRutTien frm = new frmGuiRutTien();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void NV_buttonChuyenKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.CheckExists(typeof(frmChuyenTien));
            if (f != null) f.Activate();
            else
            {
                frmChuyenTien frm = new frmChuyenTien();
                frm.MdiParent = this;
                frm.Show();
            }
        }
    }
}
