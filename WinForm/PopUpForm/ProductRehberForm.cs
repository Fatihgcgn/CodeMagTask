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

namespace WinForm.PopUpForm
{
    public partial class ProductRehberForm : Form
    {
        private readonly ApiClient _api;
        private readonly Guid? _customerId; // istersen müşteri filtreli aç
        private readonly BindingList<ProductDto> _binding = new();

        public ProductDto? SelectedProduct { get; private set; }

        public ProductRehberForm(ApiClient api, Guid? customerId = null)
        {
            InitializeComponent();

            _api = api;
            _customerId = customerId;

            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;

            dgvProducts.DataSource = _binding;

            this.Load += ProductRehberForm_Load;
            dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
        }

        private async void ProductRehberForm_Load(object? sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            List<ProductDto> list;

            // Endpoint’lerini senin API’ye göre ayarla:
            // A) müşteri filtresi varsa
            if (_customerId.HasValue)
                list = await _api.GetAsync<List<ProductDto>>($"api/customers/{_customerId.Value}/products");
            else
                // B) tüm ürünler endpointin varsa:
                list = await _api.GetAsync<List<ProductDto>>("api/products");

            _binding.RaiseListChangedEvents = false;
            _binding.Clear();
            foreach (var p in list ?? new List<ProductDto>())
                _binding.Add(p);
            _binding.RaiseListChangedEvents = true;
            _binding.ResetBindings();
        }

        private void dgvProducts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // header double click

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is ProductDto p)
            {
                SelectedProduct = p;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
