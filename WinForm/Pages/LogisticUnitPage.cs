using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Pages
{
    public partial class LogisticUnitPage : Form
    {
        private readonly ApiClient _api = new("https://localhost:7267/");

        public sealed record WorkOrderListItemDto
        {
            public Guid Id { get; set; }
            public string BatchNo { get; set; } = null!;
        }

        public sealed class WorkOrderSnapshotDto
        {
            public WorkOrderDto WorkOrder { get; set; } = null!;
            public ProductDto Product { get; set; } = null!;
            public List<SerialDto> Serials { get; set; } = new();
            public List<LogisticUnitDto> LogisticUnits { get; set; } = new();
            public List<AggregationLinkDto> AggregationLinks { get; set; } = new();
        }

        public sealed class WorkOrderDto
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public string BatchNo { get; set; } = null!;
            public DateTime ExpiryDate { get; set; }
            public int Quantity { get; set; }
            public int Status { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public sealed class ProductDto
        {
            public Guid Id { get; set; }
            public Guid CustomerId { get; set; }
            public string GTIN { get; set; } = null!;
            public string Name { get; set; } = null!;
        }

        public sealed class SerialDto
        {
            public Guid Id { get; set; }
            public Guid WorkOrderId { get; set; }
            public string GTIN { get; set; } = null!;
            public string SerialNo { get; set; } = null!;
            public string? BatchNo { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string? Gs1String { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public sealed class LogisticUnitDto
        {
            public Guid Id { get; set; }
            public Guid WorkOrderId { get; set; }
            public string SSCC { get; set; } = null!;
            public int Type { get; set; }         // 1 package, 2 pallet
            public DateTime CreatedAt { get; set; }
        }

        public sealed class AggregationLinkDto
        {
            public Guid Id { get; set; }
            public Guid ParentLogisticUnitId { get; set; }
            public int ChildType { get; set; }
            public Guid? ChildLogisticUnitId { get; set; }
            public Guid? ChildSerialId { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public sealed record CreateLogisticUnitRequest(int Type);

        private const int LuTypePackage = 1;
        private const int LuTypePallet = 2;

        private const string UrlWorkOrders = "api/workorders";
        private static string UrlSnapshot(Guid woId) => $"api/workorders/{woId}/snapshot";
        private static string UrlCreateLu(Guid woId) => $"api/workorders/{woId}/logistic-units";

        private WorkOrderSnapshotDto? _snapshot;

        public LogisticUnitPage()
        {
            InitializeComponent();

            cmbWorkOrders.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWorkOrders.DisplayMember = nameof(WorkOrderListItemDto.Id);
            cmbWorkOrders.ValueMember = nameof(WorkOrderListItemDto.Id);

            cmbLuType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLuType.Items.Clear();
            cmbLuType.Items.Add(new ComboItem("Package (Tipi 1)", LuTypePackage));
            cmbLuType.Items.Add(new ComboItem("Pallet (Tipi 2)", LuTypePallet));
            cmbLuType.SelectedIndex = 0;

            SetupGrid(dgvPackages);
            SetupGrid(dgvPallets);

            Shown += async (_, __) => await InitAsync();
            cmbWorkOrders.SelectedIndexChanged += async (_, __) => await ReloadSnapshotAndBindAsync();
            btnCreate.Click += async (_, __) => await CreateLogisticUnitAsync();
        }

        private static void SetupGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private Guid? SelectedWorkOrderId
        {
            get
            {
                if (cmbWorkOrders.SelectedItem is WorkOrderListItemDto wo) return wo.Id;
                return null;
            }
        }

        private int SelectedLuType
        {
            get
            {
                if (cmbLuType.SelectedItem is ComboItem item) return item.Value;
                return LuTypePackage;
            }
        }

        private async Task InitAsync()
        {
            try
            {
                await LoadWorkOrdersAsync();
                await ReloadSnapshotAndBindAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Init Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadWorkOrdersAsync()
        {
            var list = await _api.GetAsync<List<WorkOrderListItemDto>>(UrlWorkOrders);

            cmbWorkOrders.DataSource = list;

            if (list.Count > 0)
                cmbWorkOrders.SelectedIndex = 0;
        }

        private async Task ReloadSnapshotAndBindAsync()
        {
            var woId = SelectedWorkOrderId;
            if (woId is null) return;

            try
            {
                _snapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(woId.Value));

                var packages = _snapshot.LogisticUnits
                    .Where(x => x.Type == LuTypePackage)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new
                    {
                        x.Id,
                        x.SSCC,
                        x.Type,
                        x.CreatedAt
                    })
                    .ToList();

                var pallets = _snapshot.LogisticUnits
                    .Where(x => x.Type == LuTypePallet)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new
                    {
                        x.Id,
                        x.SSCC,
                        x.Type,
                        x.CreatedAt
                    })
                    .ToList();

                dgvPackages.DataSource = packages;
                dgvPallets.DataSource = pallets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Snapshot Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async Task CreateLogisticUnitAsync()
        {
            var woId = SelectedWorkOrderId;
            if (woId is null)
            {
                MessageBox.Show("İş emri seçmelisin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var type = SelectedLuType; // 1 package, 2 pallet
            var count = 1;             // istersen UI’dan aldırırız

            var confirm = MessageBox.Show(
                $"{(type == LuTypePackage ? "Package" : "Pallet")} oluşturulsun mu?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnCreate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                // 🔥 type & count query param
                var url = $"api/workorders/{woId.Value}/logistic-units?type={type}&count={count}";

                // Body yok -> boş object gönder
                await _api.PostAsync<object>(url, new { });

                MessageBox.Show("Lojistik birim oluşturuldu.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await ReloadSnapshotAndBindAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnCreate.Enabled = true;
            }
        }
        private sealed class ComboItem
        {
            public string Text { get; }
            public int Value { get; }

            public ComboItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString() => Text;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

        }
    }
}