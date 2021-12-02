using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firebasee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig ///23-26 ket noi toi dia chi database treen firebase
        {
            AuthSecret = "9hGS78oUKbF6J2OBS9ee0Kd3Xm24VcbAGXxob1pu",
            BasePath = "https://csdlnc-3b0e5-default-rtdb.firebaseio.com/",
        };

        private async void Them_Click(object sender, EventArgs e)//// gui du lieu database leen fire base
        {
            if (txtHoTen.Text == "" || txtMaSV.Text == "" || txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập dữ liệu đầy đủ");
            }
            else
            {
                var sinhvien = new DuLieu
                {
                    MaSV = txtMaSV.Text,
                    HoTen = txtHoTen.Text,
                    NgaySinh = dtpNgaySinh.Text,
                    DiaChi = txtDiaChi.Text,
                };
                IFirebaseClient cilent = new FirebaseClient(config);
                FirebaseResponse response = cilent.Set("Sinh viên/" + txtMaSV.Text, sinhvien);
                MessageBox.Show("Thêm thành công");
                txtHoTen.Text = string.Empty;
                txtMaSV.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
                dgvSinhVien.DataSource = null;
                dgvSinhVien.Rows.Clear();
                LoadDuLieu();
            }
        }

        private void Form1_Load(object sender, EventArgs e) ///// kiemtra ket noi toi firebase
        {
            IFirebaseClient cilent = new FirebaseClient(config); ////kiemtra ket
            if (cilent != null)
            {
                MessageBox.Show("Kết nối thành công");
            }
            else
            {
                MessageBox.Show("Kết nối thất bại");

            }
            LoadDuLieu();
        }
        //search
        private void button3_Click(object sender, EventArgs e) ///tim kiem sv
        {
            IFirebaseClient cilent = new FirebaseClient(config);
            FirebaseResponse response = cilent.Get("Sinh viên/" + txtMaSV.Text);
            DuLieu sv = response.ResultAs<DuLieu>();

            if (txtMaSV.Text== sv.MaSV)
            { 
                MessageBox.Show("Có thông tin sinh viên");
                txtHoTen.Text = string.Empty;
                txtMaSV.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
                dgvSinhVien.DataSource = null;
                dgvSinhVien.Rows.Clear();
                dgvSinhVien.Rows.Add(sv.MaSV, sv.HoTen, sv.NgaySinh, sv.DiaChi);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sinh viên");

            }


        }

        private void Up_Click(object sender, EventArgs e)
        {
            var sinhvien = new DuLieu
            {
                MaSV = txtMaSV.Text,
                HoTen = txtHoTen.Text,
                NgaySinh = dtpNgaySinh.Text,
                DiaChi = txtDiaChi.Text,
            };
            IFirebaseClient cilent = new FirebaseClient(config);
            FirebaseResponse response = cilent.Update("Sinh viên/" + txtMaSV.Text, sinhvien);
            MessageBox.Show("Cập nhật thành công");
            LoadDuLieu();
            txtHoTen.Text = string.Empty;
            txtMaSV.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            dgvSinhVien.DataSource = null;
            dgvSinhVien.Rows.Clear();
            LoadDuLieu();
        }

        private void Del_Click(object sender, EventArgs e)
        {
            IFirebaseClient cilent = new FirebaseClient(config);
            FirebaseResponse response = cilent.Delete("Sinh viên/" + txtMaSV.Text);
            MessageBox.Show("Xóa thành công");
            txtHoTen.Text = string.Empty;
            txtMaSV.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            dgvSinhVien.DataSource = null;
            dgvSinhVien.Rows.Clear();
            LoadDuLieu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvSinhVien.DataSource = null;
            dgvSinhVien.Rows.Clear();
            LoadDuLieu();
        }
        public void LoadDuLieu()
        {
            try
            {
                IFirebaseClient cilent = new FirebaseClient(config);
                FirebaseResponse response = cilent.Get("Sinh viên/");
                Dictionary<string, DuLieu> getDuLieu = response.ResultAs<Dictionary<string, DuLieu>>();
                foreach (var get in getDuLieu)
                {
                    dgvSinhVien.Rows.Add(
                        get.Value.MaSV,
                        get.Value.HoTen,
                        get.Value.NgaySinh,
                        get.Value.DiaChi
                    );
                }

            }
            catch
            {
                MessageBox.Show("Không có dữ liệu lưu trữ");
            }
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSV.Text = dgvSinhVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvSinhVien.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvSinhVien.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSV.Text = dgvSinhVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvSinhVien.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvSinhVien.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtHoTen.Text = string.Empty;
            txtMaSV.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            dgvSinhVien.DataSource = null;
            dgvSinhVien.Rows.Clear();
            LoadDuLieu();
        }
    }
}
