using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorWin
{
    public partial class UserControlTotal : UserControl
    {
        public string labelText
        {
            get { return this.lblShift.Text; }
            set { this.lblShift.Text = value; }
        }

        public DataTable DataGridSource
        {
            get { return (DataTable)this.bindingSource.DataSource; }
            set
            {
                this.bindingSource.DataSource = value;
            }
        }


        public UserControlTotal()
        {
            InitializeComponent();
        }

        private void UserControlTotal_Load(object sender, EventArgs e)
        {
            this.dataGridView.RowsDefaultCellStyle.SelectionBackColor = this.dataGridView.DefaultCellStyle.BackColor;
            this.dataGridView.DefaultCellStyle.SelectionForeColor = this.dataGridView.DefaultCellStyle.ForeColor;
            this.dataGridView.Enabled = false;
        }
    }
}
