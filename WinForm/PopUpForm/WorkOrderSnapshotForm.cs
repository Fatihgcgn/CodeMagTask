using WinForm.Dto;

namespace WinForm.PopUpForm
{
    public partial class WorkOrderSnapshotForm : Form
    {
        private readonly WorkOrderSnapshotDto _snapshot;

        public WorkOrderSnapshotForm(WorkOrderSnapshotDto snapshot)
        {
            InitializeComponent();
            _snapshot = snapshot;

            this.Load += WorkOrderSnapshotForm_Load;
        }

        private void WorkOrderSnapshotForm_Load(object? sender, EventArgs e)
        {
            // Özet (label/textbox ne kullandıysan ona göre)
            lblWorkOrder.Text =
                $"WO: {_snapshot.WorkOrder.Id} | Qty: {_snapshot.WorkOrder.Quantity} | Batch: {_snapshot.WorkOrder.BatchNo} | Exp: {_snapshot.WorkOrder.ExpiryDate:yyyy-MM-dd}";

            lblProduct.Text =
                $"Product: {_snapshot.Product.Name} | GTIN: {_snapshot.Product.GTIN}";

            // Serials
            dgvSerials.AutoGenerateColumns = true;
            dgvSerials.DataSource = _snapshot.Serials;

            // Logistic Units
            dgvLogisticUnits.AutoGenerateColumns = true;
            dgvLogisticUnits.DataSource = _snapshot.LogisticUnits;

            // Links (opsiyonel)
            dgvLinks.AutoGenerateColumns = true;
            dgvLinks.DataSource = _snapshot.AggregationLinks;
        }
    }
}