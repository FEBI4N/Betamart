using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaMartProjek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        private void txtCari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtCari.Text))
                    dataGridView.DataSource = betamartBindingSource;
                else
                {
                    var query = from o in this.ukkDataSet.Betamart
                                where o.NamaBarang.Contains(txtCari.Text) || o.ProducttionDate == txtCari.Text || o.Expired == txtCari.Text || o.Jumlah_Barang.Contains(txtCari.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Apakah kamu yakin akan menghapus?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    betamartBindingSource.RemoveCurrent();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ukkDataSet.Betamart' table. You can move, or remove it, as needed.
            this.betamartTableAdapter.Fill(this.ukkDataSet.Betamart);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                betamartBindingSource.EndEdit();
                betamartTableAdapter.Update(this.ukkDataSet.Betamart);
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betamartBindingSource.ResetBindings(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtNamaBarang.Clear();
            txtProducttionDate.Clear();
            txtExpired.Clear();
            txtJumlahBarang.Clear();
            pictureBox.Image = null;

        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    panel1.Enabled = true;
                    txtNamaBarang.Focus();
                    this.ukkDataSet.Betamart.AddBetamartRow(this.ukkDataSet.Betamart.NewBetamartRow());
                    betamartBindingSource.MoveLast();
                    btnCancel.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    betamartBindingSource.ResetBindings(false);
                }
            }
        }

        

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtNamaBarang.Focus();
            btnCancel.Enabled = true;
            btnNew.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void btndelete_Click(object sender, EventArgs e)
        {
            betamartBindingSource.RemoveCurrent();
        }
    }
    }

