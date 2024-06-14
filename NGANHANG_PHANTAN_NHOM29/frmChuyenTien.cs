using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGANHANG_PHANTAN_NHOM29
{
    public partial class frmChuyenTien : Form
    {
        String macn = "";
        int vitri = 0;
        public frmChuyenTien()
        {
            InitializeComponent();
        }

        private void khachHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void frmChuyenTien_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;
            this.khachHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khachHangTableAdapter.Fill(this.dS.KhachHang);
            this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
            this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
            macn = Program.mTenChiNhanh;
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChiNhanh;
            pnlCK.Enabled = false;

            if (Program.mGroup == "NganHang")
            {
                cmbChiNhanh.Enabled = true;
                cmsTHEM.Enabled = false;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
            }
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cmbChiNhanh.SelectedValue.ToString();
            if(cmbChiNhanh.SelectedIndex != Program.mChiNhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            else
            {
                dS.EnforceConstraints = false;
                this.khachHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khachHangTableAdapter.Fill(this.dS.KhachHang);
                this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
            }
        }

     /*   private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                this.frmChuyenTien_InfoReceiverTableAdapter.Connection.ConnectionString = Program.connstr;
                this.frmChuyenTien_InfoReceiverTableAdapter.Fill(this.dS.frmChuyenTien_InfoReceiver, txtSoTkNhanTien.Text);
            }
            catch(System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        } */

        private void cmsTHEM_Click(object sender, EventArgs e)
        {
            taiKhoanGridControl.Enabled = khachHangGridControl.Enabled = false;
            pnlCK.Enabled = true;
            txtMaNV.Text = Program.username;
            txtSoTKChuyen.Text = ((DataRowView)bdsTK[bdsTK.Position])["SOTK"].ToString();
            sODUTextBox.Text = ((DataRowView)bdsTK[bdsTK.Position])["SODU"].ToString();
            hOTextBox.Text = ((DataRowView)bdsKH[bdsKH.Position])["HO"].ToString();
            tENTextBox.Text = ((DataRowView)bdsKH[bdsKH.Position])["TEN"].ToString();
            cMNDTextBox1.Text = ((DataRowView)bdsKH[bdsKH.Position])["CMND"].ToString();
            vitri = bdsTK.Position;
        }

        private void cmsTAILAI_Click(object sender, EventArgs e)
        {
            try
            {
                this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void cmsTHOAT_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnChuyenTien_Click(object sender, EventArgs e)
        {
            String dt = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            Program.ExecSqlNonQuery("EXEC frmChuyenTien_ChuyenTien '" + txtSoTKChuyen.Text + "','" + txtSoTKNhan.Text + "','" + nuSoTien.Value + "','" + dt + "','" + Program.username + "'");
            dS.EnforceConstraints = false;
            this.khachHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khachHangTableAdapter.Fill(this.dS.KhachHang);
            this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
            this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
            taiKhoanGridControl.Enabled = khachHangGridControl.Enabled = true;
            hOTENTextBox.Text = cMNDTextBox.Text = txtSoTKNhan.Text = mACNTextBox.Text = "";
            pnlCK.Enabled = false;
            bdsTK.Position = vitri;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            bdsTK.Position = vitri;
            bdsKH.CancelEdit();
            pnlCK.Enabled = false;
            khachHangGridControl.Enabled = taiKhoanGridControl.Enabled = true;
            hOTENTextBox.Text = cMNDTextBox.Text = txtSoTKNhan.Text = mACNTextBox.Text = "";
        }
    }
}
