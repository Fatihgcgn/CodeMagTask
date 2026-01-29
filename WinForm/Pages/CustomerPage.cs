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

namespace WinForm.Pages
{
    public partial class CustomerPage : Form
    {

        private readonly ApiClient _api = new("https://localhost:7267/");
        private readonly BindingList<CustomerDto> _customerBinding = new();
        private readonly BindingList<ProductDto> _productBinding = new();

        private List<CustomerDto> _customersCache = new();
        private Guid? _selectedCustomerId;

        public CustomerPage()
        {
            InitializeComponent();

            dgvCustomers.AutoGenerateColumns = true;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AllowUserToAddRows = false;

            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;

            dgvCustomers.DataSource = _customerBinding;
            dgvProducts.DataSource = _productBinding;

            this.Load += CustomerPage_Load;

            btnCustomerSave.Click += btnCustomerSave_Click;
            btnProductSave.Click += btnProductSave_Click;

            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            cmbCustomers.SelectedIndexChanged += cmbCustomers_SelectedIndexChanged;

            // 14 hane + rakam
            txtCustomerGln.MaxLength = 14;
            txtProductGtin.MaxLength = 14;

            txtCustomerGln.KeyPress += DigitsOnly_KeyPress;
            txtProductGtin.KeyPress += DigitsOnly_KeyPress;
        }

        private void musteriRhbr_Click(object sender, EventArgs e)
        {

        }

        private async void CustomerPage_Load(object sender, EventArgs e)
        {
            await LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            var list = await _api.GetAsync<List<CustomerDto>>("api/customers");

            _customersCache = list ?? new List<CustomerDto>();

            // dgvCustomers bind
            _customerBinding.RaiseListChangedEvents = false;
            _customerBinding.Clear();
            foreach (var c in _customersCache) _customerBinding.Add(c);
            _customerBinding.RaiseListChangedEvents = true;
            _customerBinding.ResetBindings();

            // cmbCustomers bind
            cmbCustomers.DataSource = null;
            cmbCustomers.DisplayMember = nameof(CustomerDto.Name);
            cmbCustomers.ValueMember = nameof(CustomerDto.Id);
            cmbCustomers.DataSource = _customersCache;

            // İlk açılışta hiçbir şey seçili olmasın istiyorsan:
            cmbCustomers.SelectedIndex = -1;
            _selectedCustomerId = null;

            // ürün grid temizle
            _productBinding.Clear();
        }

        private async Task LoadProductsAsync(Guid customerId)
        {
            var list = await _api.GetAsync<List<ProductDto>>($"api/customers/{customerId}/products");

            _productBinding.RaiseListChangedEvents = false;
            _productBinding.Clear();
            foreach (var p in list ?? new List<ProductDto>()) _productBinding.Add(p);
            _productBinding.RaiseListChangedEvents = true;
            _productBinding.ResetBindings();
        }

        private async void dgvCustomers_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow?.DataBoundItem is not CustomerDto c) return;

            // gridden seçince combobox da o müşteriye gitsin
            _selectedCustomerId = c.Id;
            cmbCustomers.SelectedValue = c.Id;

            await LoadProductsAsync(c.Id);
        }

        private async void cmbCustomers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbCustomers.SelectedItem is not CustomerDto c) return;

            _selectedCustomerId = c.Id;
            await LoadProductsAsync(c.Id);
        }

        private async void btnCustomerSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var name = txtCustomerName.Text.Trim();
                var gln = txtCustomerGln.Text.Trim();
                var desc = txtCustomerDesc.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Müşteri adı boş olamaz.");

                if (!IsDigitsLenMax(gln, 13))
                    throw new Exception("GLN sadece rakam olmalı ve en fazla 13 hane olmalı.");

                var req = new
                {
                    Name = name,
                    GLN = gln,
                    Description = string.IsNullOrWhiteSpace(desc) ? null : desc
                };

                await _api.PostAsync<CustomerDto>("api/customers", req);

                // yeniden çek ve grid/combobox güncelle
                await LoadCustomersAsync();

                // input temizle
                txtCustomerName.Clear();
                txtCustomerGln.Clear();
                txtCustomerDesc.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnProductSave_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedCustomerId is null)
                    throw new Exception("Ürün eklemek için önce müşteri seçmelisin.");

                var name = txtProductName.Text.Trim();
                var gtin = txtProductGtin.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Ürün adı boş olamaz.");

                if (!IsDigitsLenMax(gtin, 14))
                    throw new Exception("GTIN sadece rakam olmalı ve en fazla 14 hane olmalı.");

                var req = new
                {
                    Name = name,
                    GTIN = gtin
                };

                await _api.PostAsync<ProductDto>($"api/customers/{_selectedCustomerId}/products", req);

                // ürün listesini güncelle
                await LoadProductsAsync(_selectedCustomerId.Value);

                txtProductName.Clear();
                txtProductGtin.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DigitsOnly_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private static bool IsDigitsLenMax(string value, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (value.Length > maxLen) return false;
            return value.All(char.IsDigit);
        }
    }
}
