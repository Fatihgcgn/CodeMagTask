using WinForm.Pages;

namespace WinForm
{
    public partial class Form1 : Form
    {
        private Form? _activeForm;

        private void OpenChild(Form child)
        {
            _activeForm?.Close();
            _activeForm = child;

            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;

            panelContent.Controls.Clear();
            panelContent.Controls.Add(child);
            child.Show();
        }

        public Form1()
        {
            InitializeComponent();
            ShowWelcome();
        }

        private void aggregationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoadPage(new CustomerPage());
            OpenChild(new CustomerPage());
        }

        private void workOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void logisticUnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ShowWelcome()
        {
            panelContent.Controls.Clear();
            labelMerhaba.Visible = true;
            panelContent.Controls.Add(labelMerhaba);
            labelMerhaba.BringToFront();
        }


        private void LoadPage(Control page)
        {
            panelContent.Controls.Clear();
            labelMerhaba.Visible = false;

            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
        }
    }
}
