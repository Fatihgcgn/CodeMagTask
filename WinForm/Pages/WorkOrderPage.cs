using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm.Dto;
using WinForm.PopUpForm;

namespace WinForm.Pages
{
    public partial class WorkOrderPage : Form
    {
        private readonly ApiClient _api = new("https://localhost:7267/");

        private List<CustomerDto> _customers = new();
        private List<WorkOrderDto> _workOrders = new();

        private Guid? _selectedCustomerId = null;
        private Guid? _selectedProductId = null;

        public WorkOrderPage()
        {
            InitializeComponent();
        }

        private const string ColProduceName = "colProduce";
        private const string ColDetailName = "colDetail";
        //private void SetupWorkOrdersGrid()
        //{
        //    dgvWorkOrders.AutoGenerateColumns = true;
        //    dgvWorkOrders.AllowUserToAddRows = false;
        //    dgvWorkOrders.ReadOnly = true;
        //    dgvWorkOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dgvWorkOrders.MultiSelect = false;

        //    // Detay butonu daha önce eklendiyse tekrar ekleme
        //    if (!dgvWorkOrders.Columns.Contains(ColDetailName))
        //    {
        //        var btn = new DataGridViewButtonColumn
        //        {
        //            Name = ColDetailName,
        //            HeaderText = "",
        //            Text = "Detay",
        //            UseColumnTextForButtonValue = true,
        //            Width = 70
        //        };

        //        dgvWorkOrders.Columns.Insert(0, btn);
        //    }

        //    dgvWorkOrders.CellContentClick -= dgvWorkOrders_CellContentClick;
        //    dgvWorkOrders.CellContentClick += dgvWorkOrders_CellContentClick;
        //}

        private void SetupWorkOrdersGrid()
        {
            dgvWorkOrders.AutoGenerateColumns = true;
            dgvWorkOrders.AllowUserToAddRows = false;
            dgvWorkOrders.ReadOnly = true;
            dgvWorkOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWorkOrders.MultiSelect = false;

            // Üret butonu
            if (!dgvWorkOrders.Columns.Contains(ColProduceName))
            {
                var btnProduce = new DataGridViewButtonColumn
                {
                    Name = ColProduceName,
                    HeaderText = "",
                    Text = "Üret",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };
                dgvWorkOrders.Columns.Insert(0, btnProduce);
            }

            // Detay butonu
            if (!dgvWorkOrders.Columns.Contains(ColDetailName))
            {
                var btnDetail = new DataGridViewButtonColumn
                {
                    Name = ColDetailName,
                    HeaderText = "",
                    Text = "Detay",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };
                dgvWorkOrders.Columns.Insert(1, btnDetail);
            }

            dgvWorkOrders.CellContentClick -= dgvWorkOrders_CellContentClick;
            dgvWorkOrders.CellContentClick += dgvWorkOrders_CellContentClick;
        }

        private void btnProductRehber_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == null)
            {
                MessageBox.Show(
                    "Önce müşteri seçmelisin.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new ProductRehberForm(
                api: _api,
                customerId: _selectedCustomerId.Value
            );

            var result = frm.ShowDialog(this);

            if (result == DialogResult.OK && frm.SelectedProduct != null)
            {
                var p = frm.SelectedProduct;

                txtProductName.Text = p.Name;

                _selectedProductId = p.Id;
            }
        }

        private async void btnWorkOrderSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCustomerId == null)
                    throw new Exception("Müşteri seçilmemiş.");

                if (_selectedProductId == null)
                    throw new Exception("Ürün seçilmemiş.");

                if (!int.TryParse(txtQuantity.Text, out var qty) || qty <= 0)
                    throw new Exception("Geçerli bir Quantity giriniz.");

                var req = new
                {
                    ProductId = _selectedProductId.Value,
                    BatchNo = txtBatchNo.Text.Trim(),
                    ExpiryDate = dtpExpiry.Value.Date,
                    Quantity = qty,
                    Status = cmbStatus.SelectedIndex,
                    SerialStart = 1
                };

                var url = $"api/products/{_selectedProductId.Value}/workorders";

                await _api.PostAsync<object>(url, req);

                MessageBox.Show("WorkOrder başarıyla oluşturuldu.");

                txtBatchNo.Clear();
                txtQuantity.Clear();
                txtProductName.Clear();
                _selectedProductId = null;

                await LoadWorkOrdersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCustomersAsync()
        {
            _customers = await _api.GetAsync<List<CustomerDto>>("api/customers");

            cmbCustomer.DataSource = null;
            cmbCustomer.DisplayMember = nameof(CustomerDto.Name);
            cmbCustomer.ValueMember = nameof(CustomerDto.Id);
            cmbCustomer.DataSource = _customers;

            cmbCustomer.SelectedIndex = -1;
        }

        private async Task LoadWorkOrdersAsync()
        {
            _workOrders = await _api.GetAsync<List<WorkOrderDto>>("api/workorders");

            dgvWorkOrders.DataSource = null;
            dgvWorkOrders.DataSource = _workOrders;

        }

        private async void WorkOrderPage_Load(object sender, EventArgs e)
        {
            SetupWorkOrdersGrid();
            await LoadCustomersAsync();
            await LoadWorkOrdersAsync();
        }

        //private async void dgvWorkOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;

        //    // Detay butonu mu?
        //    if (dgvWorkOrders.Columns[e.ColumnIndex].Name != ColDetailName)
        //        return;

        //    // Seçili satırın WorkOrderDto'sunu al
        //    var row = dgvWorkOrders.Rows[e.RowIndex];
        //    if (row.DataBoundItem is not WorkOrderDto wo)
        //    {
        //        MessageBox.Show("Satır verisi okunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    try
        //    {
        //        var snapshot = await _api.GetAsync<WorkOrderSnapshotDto>($"api/workorders/{wo.Id}/snapshot");

        //        using var frm = new WorkOrderSnapshotForm(snapshot);
        //        frm.ShowDialog(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Snapshot Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private async void dgvWorkOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgvWorkOrders.Columns[e.ColumnIndex].Name;

            // satırdaki WorkOrderDto
            var row = dgvWorkOrders.Rows[e.RowIndex];
            if (row.DataBoundItem is not WorkOrderDto wo)
            {
                MessageBox.Show("Satır verisi okunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ÜRET
            if (colName == ColProduceName)
            {
                using var frm = new ProduceSerialsForm(_api, wo.Id);
                var result = frm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    // başarılı üretim sonrası listeyi yenile
                    await LoadWorkOrdersAsync();
                }
                return;
            }

            // DETAY
            if (colName == ColDetailName)
            {
                try
                {
                    var snapshot = await _api.GetAsync<WorkOrderSnapshotDto>($"api/workorders/{wo.Id}/snapshot");
                    using var frm = new WorkOrderSnapshotForm(snapshot);
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Snapshot Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private void cmbCustomer_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedItem is CustomerDto c)
            {
                _selectedCustomerId = c.Id;

                _selectedProductId = null;
                txtProductName.Clear();
            }
        }


        

    }
}
