using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.PopUpForm
{
    public partial class ProduceSerialsForm : Form
    {
        private readonly ApiClient _api;
        private readonly Guid _workOrderId;

        public ProduceSerialsForm(ApiClient api, Guid workOrderId)
        {
            InitializeComponent();

            _api = api;
            _workOrderId = workOrderId;

            Text = "Serial Üret";
            StartPosition = FormStartPosition.CenterParent;

            // önerilen ayarlar
            nudCount.Minimum = 1;
            nudCount.Maximum = 100000;
            nudCount.Value = 1;

            //lblInfo.Text = "";
            txtError.Text = "";

            //lblInfo.AutoSize = true;

            btnProduce.Click -= btnProduce_Click;
            btnProduce.Click += async (_, __) => await ProduceAsync();

            btnCancel.Click -= btnCancel_Click;
            btnCancel.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        private async Task ProduceAsync()
        {
            var count = (int)nudCount.Value;
            if (count <= 0)
            {
                lblInfo.Text = "Miktar 1 veya daha büyük olmalı.";
                return;
            }

            btnProduce.Enabled = false;
            nudCount.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                // Swagger: POST /api/workorders/{id}/serials?count=...
                var url = $"api/workorders/{_workOrderId}/serials?count={count}";

                // endpoint body istemiyor, boş gönder
                await _api.PostAsync<object>(url, new { });

                MessageBox.Show("Üretim başarılı.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // ApiClient: HTTP hatasını body ile birlikte exception'a koyuyor
                txtError.Text = ex.Message;

                // kullanıcı düzeltsin tekrar deneyebilsin
                btnProduce.Enabled = true;
                nudCount.Enabled = true;
            }
            finally
            {
                Cursor = Cursors.Default;
                if (!IsDisposed)
                {
                    // başarılıysa zaten kapanmış olur
                    if (btnProduce.Enabled == false && DialogResult != DialogResult.OK)
                        btnProduce.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e) { }

        private void btnProduce_Click(object sender, EventArgs e)
        {

        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }
    }
}