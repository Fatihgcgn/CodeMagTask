using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Pages
{
    public partial class SimulationPage : Form
    {
        private readonly ApiClient _api = new("https://localhost:7267/");

        // ===== DTOs =====

        public sealed class WorkOrderListItemDto
        {
            public Guid Id { get; set; }
            public string Code { get; set; } = null!;
        }

        public sealed class WorkOrderSnapshotDto
        {
            public WorkOrderDto WorkOrder { get; set; } = null!;
            public ProductDto Product { get; set; } = null!;
            public List<SerialDto> Serials { get; set; } = new();
        }

        public sealed class WorkOrderDto
        {
            public Guid Id { get; set; }
            public string BatchNo { get; set; } = null!;
            public DateTime ExpiryDate { get; set; }
            public int Quantity { get; set; }
            public int Status { get; set; }
        }

        public sealed class ProductDto
        {
            public string GTIN { get; set; } = null!;
            public string Name { get; set; } = null!;
        }

        public sealed class SerialDto
        {
            public Guid Id { get; set; }
            public string SerialNo { get; set; } = null!;
            public string? Gs1String { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public sealed class PrintJobDto
        {
            public Guid Id { get; set; }
            public Guid WorkOrderId { get; set; }
            public string TargetType { get; set; } = null!; // "Serial"
            public Guid TargetId { get; set; }              // SerialId
            public string Payload { get; set; } = null!;    // GS1
            public string Status { get; set; } = null!;     // Queued/Printed/Failed
            public DateTime CreatedAt { get; set; }
            public DateTime? PrintedAt { get; set; }
            public string? Error { get; set; }
        }

        // ===== API URLS =====
        private const string UrlWorkOrders = "api/workorders";
        private static string UrlSnapshot(Guid woId) => $"api/workorders/{woId}/snapshot";

        private static string UrlPrintJobs(Guid woId) => $"api/printjobs?workOrderId={woId}&targetType=Serial";
        private static string UrlMarkPrinted(Guid jobId) => $"api/printjobs/{jobId}/mark-printed";

        // (Opsiyonel) Serial generate endpointin sende farklı olabilir. Gerekirse değiştir.
        private static string UrlGenerateSerials(Guid woId, int count) => $"api/workorders/{woId}/serials/generate?count={count}";

        // ===== UI state =====
        private readonly System.Windows.Forms.Timer _timer = new();
        private Guid? _selectedWoId;
        private WorkOrderSnapshotDto? _snapshot;

        // Konveyör: sadece son X saniyeyi göster
        private readonly BindingList<ConveyorRow> _conveyor = new();
        private readonly BindingList<SerialDto> _serialGrid = new();

        private const int KeepSecondsOnGrid = 10;

        // Log: son N satır
        private readonly Queue<string> _logLines = new();
        private const int MaxLogLines = 200;

        // Queue cache
        private List<PrintJobDto> _jobs = new();

        public SimulationPage()
        {
            InitializeComponent();

            // Combo
            cmbWorkOrders.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWorkOrders.DisplayMember = nameof(WorkOrderListItemDto.Id);
            cmbWorkOrders.ValueMember = nameof(WorkOrderListItemDto.Id);

            // Grid
            //SetupConveyorGrid();
            SetupSerialGrid();

            // Timer (bant hızı)
            _timer.Interval = 700; // ms
            _timer.Tick += async (_, __) => await TickAsync();

            // Events
            Shown += async (_, __) => await InitAsync();
            cmbWorkOrders.SelectedIndexChanged += async (_, __) => await WorkOrderChangedAsync();

            btnSimStart.Click += (_, __) => StartSim();
            btnSimStop.Click += (_, __) => StopSim();

            lblInfo.Text = "-";
        }

        // ===== Conveyor row model (UI için) =====
        private sealed class ConveyorRow
        {
            public DateTime Time { get; set; }
            public string Event { get; set; } = null!;
            public string Payload { get; set; } = null!;
        }

        private void SetupConveyorGrid()
        {
            dgvSerials.AutoGenerateColumns = false;
            dgvSerials.AllowUserToAddRows = false;
            dgvSerials.ReadOnly = true;
            dgvSerials.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSerials.MultiSelect = false;
            dgvSerials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvSerials.Columns.Clear();
            dgvSerials.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ConveyorRow.Time),
                HeaderText = "Time",
                FillWeight = 15
            });
            dgvSerials.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ConveyorRow.Event),
                HeaderText = "Event",
                FillWeight = 20
            });
            dgvSerials.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ConveyorRow.Payload),
                HeaderText = "GS1 Payload",
                FillWeight = 65
            });

            dgvSerials.DataSource = _conveyor;
        }

        private Guid? SelectedWorkOrderId
            => cmbWorkOrders.SelectedItem is WorkOrderListItemDto wo ? wo.Id : null;

        private void SetupSerialGrid()
        {
            dgvSerials.AutoGenerateColumns = false;
            dgvSerials.AllowUserToAddRows = false;
            dgvSerials.ReadOnly = true;
            dgvSerials.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSerials.MultiSelect = false;
            dgvSerials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvSerials.Columns.Clear();

            dgvSerials.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SerialDto.SerialNo),
                HeaderText = "Serial No",
                FillWeight = 20
            });

            dgvSerials.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SerialDto.CreatedAt),
                HeaderText = "Created",
                FillWeight = 15
            });

            dgvSerials.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SerialDto.Gs1String),
                HeaderText = "GS1 String",
                FillWeight = 65
            });

            dgvSerials.DataSource = _serialGrid;
        }

        private void BindSerialsToGrid()
        {
            _serialGrid.Clear();

            if (_snapshot?.Serials == null) return;

            foreach (var s in _snapshot.Serials.OrderBy(x => x.CreatedAt))
                _serialGrid.Add(s);
        }

        // ===== Init =====


        private async Task InitAsync()
        {
            try
            {
                var list = await _api.GetAsync<List<WorkOrderListItemDto>>(UrlWorkOrders);
                cmbWorkOrders.DataSource = list;

                if (list.Count > 0)
                    cmbWorkOrders.SelectedIndex = 0;

                await WorkOrderChangedAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Init Error");
            }
        }

        private async Task WorkOrderChangedAsync()
        {
            _selectedWoId = SelectedWorkOrderId;
            ClearUi();

            if (_selectedWoId is null) return;

            await RefreshSnapshotAndQueueAsync();
            StartSim(); // WorkOrder seçince otomatik başlasın istiyorsan açık kalsın
        }

        private void ClearUi()
        {
            _conveyor.Clear();
            _jobs.Clear();
            _snapshot = null;
            _logLines.Clear();
            txtLog.Clear();
            lblInfo.Text = "-";
        }

        private void StartSim()
        {
            if (_selectedWoId is null) return;

            AppendLog($"SIM START (WO={_selectedWoId})");
            _timer.Start();
        }

        private void StopSim()
        {
            _timer.Stop();
            AppendLog("SIM STOP");
        }

        // ===== Main loop =====
        private async Task TickAsync()
        {
            if (_selectedWoId is null) return;

            // eski satırları grid’den temizle
            PruneConveyor();

            try
            {
                // 1) Kuyruğu güncelle
                await LoadPrintJobsAsync();

                // 2) Eğer kuyruk boşsa opsiyonel serial üret (istersen)
                if (chkAutoGenerate.Checked)
                {
                    var queued = _jobs.Count(x => x.Status.Equals("Queued", StringComparison.OrdinalIgnoreCase));
                    if (queued == 0)
                    {
                        var count = (int)nudAutoGenerateCount.Value;
                        await TryAutoGenerateSerialsAsync(count);

                        // tekrar queue çek
                        await LoadPrintJobsAsync();
                    }
                }

                // 3) En eski queued job’ı “printed” yap
                var next = _jobs
                    .Where(x => x.Status.Equals("Queued", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.CreatedAt)
                    .FirstOrDefault();

                if (next == null)
                {
                    // bant boş: küçük info
                    lblInfo.Text = $"WO: {_snapshot?.WorkOrder.BatchNo} | Queue: 0";
                    return;
                }

                await _api.PostAsync<object>(UrlMarkPrinted(next.Id), new { });

                // UI: "akıp geçti"
                var gs1 = next.Payload;
                AddConveyorRow("PRINTED", gs1);
                AppendLog($"PRINTED: {Short(gs1)}");

                // küçük header bilgi
                lblInfo.Text = $"WO: {_snapshot?.WorkOrder.BatchNo} | PRINTED: {Short(gs1)}";

                // 4) queue’yu tekrar çek (durumu güncellensin)
                await LoadPrintJobsAsync();
            }
            catch (Exception ex)
            {
                // simülasyonda hata olursa logda görünsün
                AddConveyorRow("ERROR", ex.Message);
                AppendLog($"ERROR: {ex.Message}");
            }
        }

        //private async Task RefreshSnapshotAndQueueAsync()
        //{
        //    if (_selectedWoId is null) return;

        //    _snapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(_selectedWoId.Value));
        //    await LoadPrintJobsAsync();

        //    // initial info
        //    lblInfo.Text = $"WO: {_snapshot.WorkOrder.BatchNo} | Serials: {_snapshot.Serials.Count} | Queue: {_jobs.Count}";
        //    AddConveyorRow("LOADED", $"Serials={_snapshot.Serials.Count}, Jobs={_jobs.Count}");
        //    AppendLog($"LOADED: Serials={_snapshot.Serials.Count}, Jobs={_jobs.Count}");
        //}

        private async Task RefreshSnapshotAndQueueAsync()
        {
            if (_selectedWoId is null) return;

            _snapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(_selectedWoId.Value));
            await LoadPrintJobsAsync();

            BindSerialsToGrid(); // ✅

            lblInfo.Text = $"WO: {_snapshot.WorkOrder.BatchNo} | Serials: {_snapshot.Serials.Count} | Queue: {_jobs.Count}";
            AddConveyorRow("LOADED", $"Serials={_snapshot.Serials.Count}, Jobs={_jobs.Count}");
            AppendLog($"LOADED: Serials={_snapshot.Serials.Count}, Jobs={_jobs.Count}");
        }

        private async Task LoadPrintJobsAsync()
        {
            if (_selectedWoId is null) return;

            var list = await _api.GetAsync<List<PrintJobDto>>(UrlPrintJobs(_selectedWoId.Value));
            _jobs = list ?? new List<PrintJobDto>();
        }

        //private async Task TryAutoGenerateSerialsAsync(int count)
        //{
        //    if (_selectedWoId is null) return;

        //    try
        //    {
        //        // ⚠️ endpoint sende farklıysa burayı düzelt
        //        await _api.PostAsync<object>(UrlGenerateSerials(_selectedWoId.Value, count), new { });

        //        AddConveyorRow("GENERATE", $"Generated {count} serial(s)");
        //        AppendLog($"GENERATE: {count} serial(s)");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Generate endpoint yoksa simülasyon yine çalışır (sadece queued job varsa basar)
        //        AddConveyorRow("GENERATE_FAIL", ex.Message);
        //        AppendLog($"GENERATE_FAIL: {ex.Message}");
        //    }
        //}

        private async Task TryAutoGenerateSerialsAsync(int count)
        {
            if (_selectedWoId is null) return;

            try
            {
                await _api.PostAsync<object>(UrlGenerateSerials(_selectedWoId.Value, count), new { });

                AddConveyorRow("GENERATE", $"Generated {count} serial(s)");
                AppendLog($"GENERATE: {count} serial(s)");

                _snapshot = await _api.GetAsync<WorkOrderSnapshotDto>(UrlSnapshot(_selectedWoId.Value));
                BindSerialsToGrid();
                await LoadPrintJobsAsync();

                lblInfo.Text = $"WO: {_snapshot.WorkOrder.BatchNo} | Serials: {_snapshot.Serials.Count} | Queue: {_jobs.Count}";
            }
            catch (Exception ex)
            {
                AddConveyorRow("GENERATE_FAIL", ex.Message);
                AppendLog($"GENERATE_FAIL: {ex.Message}");
            }
        }



        // ===== UI helpers =====
        private void AddConveyorRow(string evt, string payload)
        {
            _conveyor.Add(new ConveyorRow
            {
                Time = DateTime.Now,
                Event = evt,
                Payload = payload
            });

            // her eklemede en alta kaydır
            if (dgvSerials.Rows.Count > 0)
                dgvSerials.FirstDisplayedScrollingRowIndex = dgvSerials.Rows.Count - 1;
        }

        private void PruneConveyor()
        {
            var cutoff = DateTime.Now.AddSeconds(-KeepSecondsOnGrid);

            // BindingList remove loop
            for (int i = _conveyor.Count - 1; i >= 0; i--)
            {
                if (_conveyor[i].Time < cutoff)
                    _conveyor.RemoveAt(i);
            }
        }

        private void AppendLog(string msg)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {msg}";
            _logLines.Enqueue(line);

            while (_logLines.Count > MaxLogLines)
                _logLines.Dequeue();

            txtLog.Lines = _logLines.ToArray();
            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.ScrollToCaret();
        }

        private static string Short(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "-";
            return s.Length <= 40 ? s : s.Substring(0, 40) + "...";
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}