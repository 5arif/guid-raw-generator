using System;
using System.Windows.Forms;

namespace GuidConvert
{
    public partial class FormConvert : Form
    {
        public FormConvert()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {

            try
            {
                txtInput.Text = txtInput.Text?.Trim().ToUpper();
                if (txtInput.Text.Contains("-"))
                {
                    var isValid = Guid.TryParse(txtInput.Text, out Guid guidOutput);
                    if (isValid)
                    {
                        txtResult.Text = BitConverter.ToString(guidOutput.ToByteArray()).Replace("-", string.Empty);
                    }
                }
                else
                {
                    var guid = new Guid(txtInput.Text);
                    var newBytes = new byte[16];
                    var oldBytes = guid.ToByteArray();

                    for (var i = 8; i < 16; i++)
                        newBytes[i] = oldBytes[i];

                    newBytes[3] = oldBytes[0];
                    newBytes[2] = oldBytes[1];
                    newBytes[1] = oldBytes[2];
                    newBytes[0] = oldBytes[3];
                    newBytes[5] = oldBytes[4];
                    newBytes[4] = oldBytes[5];
                    newBytes[6] = oldBytes[7];
                    newBytes[7] = oldBytes[6];

                    txtResult.Text = new Guid(newBytes).ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var guid = Guid.NewGuid();
            txtInput.Text = guid.ToString();

            btnConvert_Click(sender, e);
        }
    }
}
