using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WinForm.Pages
{
    public partial class AggregationPage : Form
    {
        // ====== DTO (WorkOrder list) ======

        public sealed record WorkOrderListItemDto
        {
            public Guid Id { get; set; }
            public string BatchNo { get; set; } = null!;
        }

        // ====== Snapshot DTO'ları (WebApi.Dto ile uyumlu) ======
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
            public int Type { get; set; }
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



        public sealed record LinkSerialToPackageRequest(Guid SerialId, Guid PackageId);
        public sealed record LinkPackageToPalletRequest(Guid PackageId, Guid PalletId);

        private readonly ApiClient _api = new("https://localhost:7267/");

        private WorkOrderSnapshotDto? _snapshot;
        private Guid? _childId;
        private Guid? _parentId;
        private WorkOrderSnapshotDto? _treeSnapshot;

        private const int LuTypePackage = 1;
        private const int LuTypePallet = 2;

        private const string UrlWorkOrders = "api/workorders";
        private static string UrlSnapshot(Guid woId) => $"api/workorders/{woId}/snapshot";
        //private static string UrlAggSerialPackage(Guid woId) => $"api/logistic-units/{_parentId}/aggregate/serial/{_childId}";
        //private static string UrlAggPackagePallet(Guid woId) => $"api/logistic-units/{_parentId}/aggregate/package/{_childId}";
        private static string UrlAggSerialPackage(Guid parentId, Guid childId) => $"api/logistic-units/{parentId}/aggregate/serial/{childId}";
        private static string UrlAggPackagePallet(Guid parentId, Guid childId) => $"api/logistic-units/{parentId}/aggregate/package/{childId}";

        public AggregationPage()
        {
            InitializeComponent();

            cmbAggregation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAggregation.Items.Clear();
            cmbAggregation.Items.AddRange(new object[]
            {
                "Serial-Package",
                "Package-Pallet"
            });
            cmbAggregation.SelectedIndex = 0;

            cmbWorkorders.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWorkorders.DisplayMember = "Id";
            cmbWorkorders.ValueMember = "Id";

            cmbWorkTree.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWorkTree.DisplayMember = "Id";
            cmbWorkTree.ValueMember = "Id";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = true;

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.AutoGenerateColumns = true;

            Shown += async (_, __) => await InitAsync();

            cmbWorkorders.SelectedIndexChanged += async (_, __) => await ReloadFromSnapshotAsync();
            cmbAggregation.SelectedIndexChanged += async (_, __) => await ReloadFromSnapshotAsync();
            cmbWorkTree.SelectedIndexChanged += async (_, __) => await ReloadTreeAsync();

            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView2.CellClick += dataGridView2_CellClick;

            dataGridView1.CellFormatting -= Grid_CellFormatting;
            dataGridView1.CellFormatting += Grid_CellFormatting;

            dataGridView2.CellFormatting -= Grid_CellFormatting;
            dataGridView2.CellFormatting += Grid_CellFormatting;

            btnAggregate.Click += async (_, __) => await AggregateAsync();

            if (Controls.Find("btnRefresh", true).FirstOrDefault() is Button btnRefresh)
            {
                btnRefresh.Click += async (_, __) => await ReloadFromSnapshotAsync();
            }
        }

        private Guid? SelectedWorkOrderId
        {
            get
            {
                if (cmbWorkorders.SelectedItem is WorkOrderListItemDto wo) return wo.Id;
                return null;
            }
        }
        private Guid? SelectedTreeWorkOrderId
        {
            get
            {
                if (cmbWorkTree.SelectedItem is WorkOrderListItemDto wo) return wo.Id;
                return null;
            }
        }

        private string SelectedMode => cmbAggregation.SelectedItem?.ToString() ?? "Serial-Package";

        // ====== Init ======
        private async Task InitAsync()
        {
            try
            {
                await LoadWorkOrdersForBothCombosAsync();
                await ReloadFromSnapshotAsync(); // mevcut gridler
                await ReloadTreeAsync();         // tree

                //await LoadWorkOrdersAsync();
                //await ReloadFromSnapshotAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Init Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadWorkOrdersAsync()
        {
            var list = await _api.GetAsync<List<WorkOrderListItemDto>>(UrlWorkOrders);

            cmbWorkorders.DataSource = list;
            if (list.Count > 0)
                cmbWorkorders.SelectedIndex = 0;
        }

        //private async Task ReloadTreeAsync()
        //{
        //    var woId = SelectedTreeWorkOrderId;
        //    if (woId is null) return;

        //    try
        //    {
        //        _treeSnapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(woId.Value));
        //        BuildTreeView_FromLinksOnly(_treeSnapshot);
        //        //BuildTreeViewFromSnapshot_OnlyLinked(_treeSnapshot);
        //        //BuildTreeViewFromSnapshot(_treeSnapshot);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Tree Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private async Task ReloadTreeAsync()
        {
            var woId = SelectedTreeWorkOrderId;
            if (woId is null) return;

            try
            {
                _treeSnapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(woId.Value));

                BuildTreeView_FromLinksOnly(_treeSnapshot);

                UpdateCountsLabel(_treeSnapshot);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tree Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCountsLabel(WorkOrderSnapshotDto snap)
        {
            var totalSerials = snap.Serials.Count;
            var totalPackages = snap.LogisticUnits.Count(x => x.Type == LuTypePackage);
            var totalPallets = snap.LogisticUnits.Count(x => x.Type == LuTypePallet);

            var linkedParentLuIds = new HashSet<Guid>(snap.AggregationLinks.Select(x => x.ParentLogisticUnitId));

            var linkedChildLuIds = new HashSet<Guid>(
                snap.AggregationLinks
                    .Where(x => x.ChildLogisticUnitId.HasValue)
                    .Select(x => x.ChildLogisticUnitId!.Value)
            );

            var linkedLuIds = new HashSet<Guid>(linkedParentLuIds);
            linkedLuIds.UnionWith(linkedChildLuIds);

            var linkedSerialIds = new HashSet<Guid>(
                snap.AggregationLinks
                    .Where(x => x.ChildSerialId.HasValue)
                    .Select(x => x.ChildSerialId!.Value)
            );

            var luById = snap.LogisticUnits.ToDictionary(x => x.Id);

            int linkedPackages = 0, linkedPallets = 0, linkedOtherLu = 0;
            foreach (var id in linkedLuIds)
            {
                if (!luById.TryGetValue(id, out var lu)) continue;

                if (lu.Type == LuTypePackage) linkedPackages++;
                else if (lu.Type == LuTypePallet) linkedPallets++;
                else linkedOtherLu++;
            }

            var linkedSerials = linkedSerialIds.Count;

            lblSummary.Text =
                $"Linked → Pallet: {linkedPallets} | Package: {linkedPackages} | Serial: {linkedSerials}";
        }

        private void BuildTreeView_FromLinksOnly(WorkOrderSnapshotDto snap)
        {
            tvAgg.BeginUpdate();
            tvAgg.Nodes.Clear();

            var luById = snap.LogisticUnits.ToDictionary(x => x.Id, x => x);
            var serialById = snap.Serials.ToDictionary(x => x.Id, x => x);

            var childrenLuMap = new Dictionary<Guid, List<Guid>>();
            var childrenSerialMap = new Dictionary<Guid, List<Guid>>();

            foreach (var link in snap.AggregationLinks)
            {
                var parentId = link.ParentLogisticUnitId;

                if (link.ChildLogisticUnitId.HasValue)
                {
                    if (!childrenLuMap.TryGetValue(parentId, out var list))
                        childrenLuMap[parentId] = list = new List<Guid>();
                    list.Add(link.ChildLogisticUnitId.Value);
                }

                if (link.ChildSerialId.HasValue)
                {
                    if (!childrenSerialMap.TryGetValue(parentId, out var list))
                        childrenSerialMap[parentId] = list = new List<Guid>();
                    list.Add(link.ChildSerialId.Value);
                }
            }

            var rootParentLuIds = new HashSet<Guid>(
                snap.AggregationLinks.Select(x => x.ParentLogisticUnitId)
            );

            var allChildLuIds = new HashSet<Guid>(
                snap.AggregationLinks.Where(x => x.ChildLogisticUnitId.HasValue)
                    .Select(x => x.ChildLogisticUnitId!.Value)
            );

            var roots = rootParentLuIds.Where(id => !allChildLuIds.Contains(id)).ToList();

            if (roots.Count == 0)
                roots = rootParentLuIds.ToList();

            var luNodeCache = new Dictionary<Guid, TreeNode>();

            TreeNode BuildLuNode(Guid luId)
            {
                if (luNodeCache.TryGetValue(luId, out var cached))
                    return CloneNode(cached); // TreeNode aynı anda iki yerde olamaz

                var title = luById.TryGetValue(luId, out var lu)
                    ? $"{(lu.Type == LuTypePallet ? "Pallet" : lu.Type == LuTypePackage ? "Package" : "LU")} : {lu.SSCC}"
                    : $"LU : {luId}";

                var node = new TreeNode(title) { Tag = luId };

                if (childrenLuMap.TryGetValue(luId, out var childLuIds))
                {
                    foreach (var childLuId in childLuIds.Distinct())
                    {
                        node.Nodes.Add(BuildLuNode(childLuId));
                    }
                }

                if (childrenSerialMap.TryGetValue(luId, out var childSerialIds))
                {
                    foreach (var sid in childSerialIds.Distinct())
                    {
                        var snTitle = serialById.TryGetValue(sid, out var s)
                            ? $"Serial : {s.SerialNo}"
                            : $"Serial : {sid}";

                        node.Nodes.Add(new TreeNode(snTitle) { Tag = sid });
                    }
                }

                luNodeCache[luId] = node;
                return CloneNode(node);
            }

            foreach (var rootLuId in roots.Distinct())
            {
                var hasAnyChild = childrenLuMap.ContainsKey(rootLuId) || childrenSerialMap.ContainsKey(rootLuId);
                if (!hasAnyChild) continue;

                tvAgg.Nodes.Add(BuildLuNode(rootLuId));
            }

            if (tvAgg.Nodes.Count == 0)
                tvAgg.Nodes.Add(new TreeNode("No aggregation links found."));

            tvAgg.ExpandAll();
            tvAgg.EndUpdate();
        }

        private static TreeNode CloneNode(TreeNode node)
        {
            var clone = (TreeNode)node.Clone();
            return clone;
        }

        private void BuildTreeViewFromSnapshot_OnlyLinked(WorkOrderSnapshotDto snap)
        {
            tvAgg.BeginUpdate();
            tvAgg.Nodes.Clear();

            var serialById = snap.Serials.ToDictionary(s => s.Id);

            var luById = snap.LogisticUnits.ToDictionary(lu => lu.Id);

            var palletLus = snap.LogisticUnits.Where(x => x.Type == LuTypePallet).ToList();
            var packageLus = snap.LogisticUnits.Where(x => x.Type == LuTypePackage).ToList();

            var palletNodeById = palletLus.ToDictionary(p => p.Id, p => new TreeNode($"Pallet: {p.SSCC}") { Tag = p.Id });
            var packageNodeById = packageLus.ToDictionary(p => p.Id, p => new TreeNode($"Package: {p.SSCC}") { Tag = p.Id });

            const int ChildTypeSerial = 1;
            const int ChildTypeLu = 2;

            foreach (var link in snap.AggregationLinks
                         .Where(l => l.ChildType == ChildTypeSerial && l.ChildSerialId.HasValue))
            {
                var parentId = link.ParentLogisticUnitId;    // package
                var serialId = link.ChildSerialId!.Value;

                if (packageNodeById.TryGetValue(parentId, out var packNode) &&
                    serialById.TryGetValue(serialId, out var s))
                {
                    packNode.Nodes.Add(new TreeNode($"Serial: {s.SerialNo}") { Tag = s.Id });
                }
            }

            // 2) Pallet -> Package bağla
            foreach (var link in snap.AggregationLinks
                         .Where(l => l.ChildType == ChildTypeLu && l.ChildLogisticUnitId.HasValue))
            {
                var parentId = link.ParentLogisticUnitId;          // pallet
                var childPackageId = link.ChildLogisticUnitId!.Value;

                if (palletNodeById.TryGetValue(parentId, out var palNode) &&
                    packageNodeById.TryGetValue(childPackageId, out var packNode))
                {
                    palNode.Nodes.Add(packNode);
                }
            }

            // SADECE içi dolu palletleri göster (istersen)
            foreach (var palNode in palletNodeById.Values)
            {
                // Eğer pallete hiç package bağlanmamışsa göstermeyelim:
                if (palNode.Nodes.Count == 0) continue;

                tvAgg.Nodes.Add(palNode);
            }

            // Hiç pallet altında node yoksa kullanıcıya bilgi
            if (tvAgg.Nodes.Count == 0)
                tvAgg.Nodes.Add(new TreeNode("No linked Pallet → Package → Serial data found."));

            tvAgg.ExpandAll();
            tvAgg.EndUpdate();
        }

        private void BuildTreeViewFromSnapshot(WorkOrderSnapshotDto snap)
        {
            tvAgg.BeginUpdate();
            tvAgg.Nodes.Clear();

            var serialById = snap.Serials.ToDictionary(s => s.Id, s => s);
            var luById = snap.LogisticUnits.ToDictionary(lu => lu.Id, lu => lu);

            var packages = snap.LogisticUnits.Where(x => x.Type == LuTypePackage).ToList();
            var pallets = snap.LogisticUnits.Where(x => x.Type == LuTypePallet).ToList();

            var packageNodeByLuId = new Dictionary<Guid, TreeNode>();
            var palletNodeByLuId = new Dictionary<Guid, TreeNode>();

            foreach (var pal in pallets)
            {
                var n = new TreeNode($"Pallet: {pal.SSCC}") { Tag = pal.Id };
                palletNodeByLuId[pal.Id] = n;
            }

            foreach (var pack in packages)
            {
                var n = new TreeNode($"Package: {pack.SSCC}") { Tag = pack.Id };
                packageNodeByLuId[pack.Id] = n;
            }

            var packageAssignedToPallet = new HashSet<Guid>();
            var serialAssignedToPackage = new HashSet<Guid>();

            foreach (var link in snap.AggregationLinks)
            {

                const int ChildTypeSerial = 1;
                const int ChildTypeLu = 2;

                if (link.ChildType == ChildTypeSerial && link.ChildSerialId.HasValue)
                {
                    var parentLuId = link.ParentLogisticUnitId;
                    var childSerialId = link.ChildSerialId.Value;

                    if (packageNodeByLuId.TryGetValue(parentLuId, out var packNode) &&
                        serialById.TryGetValue(childSerialId, out var s))
                    {
                        var sn = new TreeNode($"Serial: {s.SerialNo}") { Tag = s.Id };
                        packNode.Nodes.Add(sn);
                        serialAssignedToPackage.Add(s.Id);
                    }
                }
                else if (link.ChildType == ChildTypeLu && link.ChildLogisticUnitId.HasValue)
                {
                    var parentLuId = link.ParentLogisticUnitId;
                    var childLuId = link.ChildLogisticUnitId.Value;

                    if (palletNodeByLuId.TryGetValue(parentLuId, out var palNode) &&
                        packageNodeByLuId.TryGetValue(childLuId, out var packNode))
                    {
                        palNode.Nodes.Add(packNode);
                        packageAssignedToPallet.Add(childLuId);
                    }
                }
            }

            foreach (var palNode in palletNodeByLuId.Values)
                tvAgg.Nodes.Add(palNode);

            var unassignedPackages = packages.Where(p => !packageAssignedToPallet.Contains(p.Id)).ToList();
            if (unassignedPackages.Count > 0)
            {
                var unNode = new TreeNode("UNASSIGNED PACKAGES");
                foreach (var p in unassignedPackages)
                    unNode.Nodes.Add(packageNodeByLuId[p.Id]);
                tvAgg.Nodes.Add(unNode);
            }

            var unassignedSerials = snap.Serials.Where(s => !serialAssignedToPackage.Contains(s.Id)).ToList();
            if (unassignedSerials.Count > 0)
            {
                var unNode = new TreeNode("UNASSIGNED SERIALS");
                foreach (var s in unassignedSerials)
                    unNode.Nodes.Add(new TreeNode($"Serial: {s.SerialNo}") { Tag = s.Id });
                tvAgg.Nodes.Add(unNode);
            }

            tvAgg.ExpandAll();
            tvAgg.EndUpdate();
        }

        private async Task LoadWorkOrdersForBothCombosAsync()
        {
            var list = await _api.GetAsync<List<WorkOrderListItemDto>>(UrlWorkOrders);

            cmbWorkorders.DataSource = list.ToList();
            cmbWorkorders.DisplayMember = "Id";
            cmbWorkorders.ValueMember = "Id";

            cmbWorkTree.DataSource = list.ToList();
            cmbWorkTree.DisplayMember = "Id";
            cmbWorkTree.ValueMember = "Id";

            if (list.Count > 0)
            {
                cmbWorkorders.SelectedIndex = 0;
                cmbWorkTree.SelectedIndex = 0;
            }
        }

        private async Task ReloadFromSnapshotAsync()
        {
            var woId = SelectedWorkOrderId;
            if (woId is null) return;

            try
            {
                _snapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(woId.Value));

                var linkedSerialIds = _snapshot.AggregationLinks
                    .Where(x => x.ChildSerialId.HasValue)
                    .Select(x => x.ChildSerialId!.Value)
                    .ToHashSet();

                var linkedPackageIds = _snapshot.AggregationLinks
                    .Where(x => x.ChildLogisticUnitId.HasValue)
                    .Select(x => x.ChildLogisticUnitId!.Value)
                    .ToHashSet();

                _childId = null;
                _parentId = null;
                txChild.Text = "";
                txParent.Text = "";

                // build lists
                var packages = _snapshot.LogisticUnits
                    .Where(x => x.Type == LuTypePackage)
                    .Select(x => new
                    {
                        x.Id,
                        x.SSCC,
                        x.Type,
                        x.CreatedAt,
                        // ✅ sadece Package-Pallet modunda linked renklensin
                        IsLinked = (SelectedMode == "Package-Pallet") && linkedPackageIds.Contains(x.Id)
                    })
                    .ToList();

                var pallets = _snapshot.LogisticUnits
                    .Where(x => x.Type == LuTypePallet)
                    .Select(x => new
                    {
                        x.Id,
                        x.SSCC,
                        x.Type,
                        x.CreatedAt
                    })
                    .ToList();

                if (SelectedMode == "Serial-Package")
                {
                    dataGridView1.DataSource = _snapshot.Serials
                        .Select(s => new
                        {
                            s.Id,
                            s.SerialNo,
                            s.Gs1String,
                            s.GTIN,
                            s.BatchNo,
                            s.ExpiryDate,
                            IsLinked = linkedSerialIds.Contains(s.Id)
                        })
                        .ToList();

                    dataGridView2.DataSource = packages;
                }
                else
                {
                    dataGridView1.DataSource = packages;
                    dataGridView2.DataSource = pallets;
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dataGridView1.Columns.Contains("IsLinked"))
                    dataGridView1.Columns["IsLinked"].Visible = false;

                if (dataGridView2.Columns.Contains("IsLinked"))
                    dataGridView2.Columns["IsLinked"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Snapshot Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void dataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataGridView1.CurrentRow?.Cells["Id"]?.Value is Guid id)
        //    {
        //        _childId = id;
        //        txChild.Text = id.ToString();
        //    }
        //}

        private void dataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Linked ise seçtirme
            if (IsRowLinked(dataGridView1, e.RowIndex))
            {
                MessageBox.Show("Bu kayıt zaten bağlı (linked). Başka bir eşleştirme için kullanılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.Rows[e.RowIndex].Cells["Id"]?.Value is Guid id)
            {
                _childId = id;
                txChild.Text = id.ToString();
            }
        }

        private void dataGridView2_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow?.Cells["Id"]?.Value is Guid id)
            {
                _parentId = id;
                txParent.Text = id.ToString();
            }
        }

        private void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is not DataGridView dgv) return;
            if (e.RowIndex < 0) return;

            var row = dgv.Rows[e.RowIndex];

            // DataSource anon object olduğu için reflection ile okuyoruz
            var item = row.DataBoundItem;
            if (item == null) return;

            var prop = item.GetType().GetProperty("IsLinked");
            if (prop == null) return;

            var isLinked = prop.GetValue(item) as bool? ?? false;

            if (isLinked)
            {
                row.DefaultCellStyle.BackColor = Color.MistyRose;
                row.DefaultCellStyle.ForeColor = Color.DarkRed;
                row.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
                row.DefaultCellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                // normal görünüm (istersen resetle)
                row.DefaultCellStyle.BackColor = dgv.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgv.DefaultCellStyle.ForeColor;
                row.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.SelectionBackColor;
                row.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.SelectionForeColor;
            }
        }

        private bool IsRowLinked(DataGridView dgv, int rowIndex)
        {
            var item = dgv.Rows[rowIndex].DataBoundItem;
            if (item == null) return false;

            var prop = item.GetType().GetProperty("IsLinked");
            if (prop == null) return false;

            return prop.GetValue(item) as bool? ?? false;
        }

        private async Task AggregateAsync()
        {
            var woId = SelectedWorkOrderId;
            if (woId is null)
            {
                MessageBox.Show("WorkOrder seçmelisin.", "Uyarı");
                return;
            }

            if (_childId is null || _parentId is null)
            {
                MessageBox.Show("Sol ve sağ gridde seçim yapmalısın.", "Uyarı");
                return;
            }

            try
            {
                if (SelectedMode == "Serial-Package")
                {
                    var ok = MessageBox.Show(
                        "Seçili Serial, seçili Package içine eklensin mi?",
                        "Onay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (ok != DialogResult.Yes) return;

                    var url = UrlAggSerialPackage(_parentId.Value, _childId.Value);
                    await _api.PostAsync<object>(url, new { });

                    MessageBox.Show("Serial -> Package agregasyon tamam.", "OK");
                }
                else
                {
                    var ok = MessageBox.Show(
                        "Seçili Package, seçili Pallet içine eklensin mi?",
                        "Onay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (ok != DialogResult.Yes) return;

                    var url = UrlAggPackagePallet(_parentId.Value, _childId.Value);
                    await _api.PostAsync<object>(url, new { });

                    MessageBox.Show("Package -> Pallet agregasyon tamam.", "OK");
                }

                await ReloadFromSnapshotAsync();
                await SyncTreeToWorkOrderAndReloadAsync(woId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aggregation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SyncTreeToWorkOrderAndReloadAsync(Guid workOrderId)
        {
            // Tree combobox zaten aynı WO seçiliyse direkt reload
            if (SelectedTreeWorkOrderId == workOrderId)
            {
                await ReloadTreeAsync();
                return;
            }

            // Değilse cmbWorkTree'yi o WO'ya getirip reload et
            try
            {
                // SelectedIndexChanged event’i double call yapmasın
                cmbWorkTree.SelectedIndexChanged -= cmbWorkTree_SelectedIndexChanged;

                // DataSource WorkOrderListItemDto olduğu için SelectedValue çalışır
                cmbWorkTree.SelectedValue = workOrderId;
            }
            finally
            {
                cmbWorkTree.SelectedIndexChanged += cmbWorkTree_SelectedIndexChanged;
            }

            await ReloadTreeAsync();
        }

        private async void cmbWorkTree_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ReloadTreeAsync();
        }

        private void btnAggregate_Click(object sender, EventArgs e)
        {

        }
    }
}

