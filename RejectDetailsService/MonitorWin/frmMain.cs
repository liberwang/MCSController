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
    public partial class frmMain : Form
    {
        private Shift currentShift = Shift.None;
        private bool isRunning = false;
        private object lockObjet = new object();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RefreshCurrent();
            this.timeRefresh.Start();

            this.dgvCurrentTotal.RowsDefaultCellStyle.SelectionBackColor = this.dgvCurrentTotal.DefaultCellStyle.BackColor;
            this.dgvCurrentTotal.DefaultCellStyle.SelectionForeColor = this.dgvCurrentTotal.DefaultCellStyle.ForeColor;
            this.dgvCurrentTotal.Enabled = false;

            this.dgvLastTotal.RowsDefaultCellStyle.SelectionBackColor = this.dgvLastTotal.DefaultCellStyle.BackColor;
            this.dgvLastTotal.DefaultCellStyle.SelectionForeColor = this.dgvLastTotal.DefaultCellStyle.ForeColor;
            this.dgvLastTotal.Enabled = false;

            this.dgvSecond.RowsDefaultCellStyle.SelectionBackColor = this.dgvSecond.DefaultCellStyle.BackColor;
            this.dgvSecond.DefaultCellStyle.SelectionForeColor = this.dgvSecond.DefaultCellStyle.ForeColor;
            this.dgvSecond.Enabled = false;
        }


        private void timeRefresh_Tick(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                try
                {
                    lock (lockObjet)
                    {
                        isRunning = true;
                        this.RefreshCurrent();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    isRunning = false;
                }
            }
        }

        private void RefreshCurrent()
        {
            Shift current = clsShift.GetCurrentShift();
            if (current != currentShift)
            {
                currentShift = current;
                lblCurrentShift.Text = "Current: " + current.ToString();
                RefreshLast();
            }
            (Shift, DateTime, DateTime) currShift = clsShift.GetCurrentShiftCombo();
            DataSet ds = null;
            foreach (List<string> list in clsKeys.DB_LIST)
            {
                string sConn = string.Format(clsDatabase.DB_CONNECT, list[0]);
                string startTime = clsShift.FormatDateTime(currShift.Item2);
                string endTime = clsShift.FormatDateTime(currShift.Item3);

                try
                {
                    DataSet dataSet = clsDatabase.GetShiftSum(sConn, list[1], startTime, endTime);

                    if (ds == null)
                    {
                        ds = dataSet;
                    }
                    else
                    {
                        ds.Merge(dataSet);
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (ds?.Tables.Count > 0)
            {
                this.bindingSourceCurrentTotal.DataSource = ds.Tables[0];
            }
        }

        private void RefreshLast()
        {
            (Shift, DateTime, DateTime) lastShift = clsShift.GetLastShiftCombo();
            this.lblLastShift.Text = "Last: " + lastShift.Item1.ToString();

            DataSet dsLast = null;
            foreach( List<string> list in clsKeys.DB_LIST)
            {
                string sConn = string.Format(clsDatabase.DB_CONNECT, list[0]);
                string startTime = clsShift.FormatDateTime(lastShift.Item2);
                string endTime = clsShift.FormatDateTime(lastShift.Item3);

                try
                {
                    DataSet dataSet = clsDatabase.GetShiftSum(sConn, list[1], startTime, endTime);

                    if (dsLast == null)
                        dsLast = dataSet;
                    else
                        dsLast.Merge(dataSet);
                } catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            if (dsLast?.Tables.Count > 0)
            {
                this.bindingSourceLast.DataSource = dsLast.Tables[0];
            }

            (Shift, DateTime, DateTime) secondShift = clsShift.GetSecondLastShiftCombo();
            this.lblSecond.Text = "Last Two: " + secondShift.Item1.ToString(); 

            DataSet dsSecond = new DataSet();
            foreach (List<string> list in clsKeys.DB_LIST)
            {
                string sConn = string.Format(clsDatabase.DB_CONNECT, list[0]);
                string startTime = clsShift.FormatDateTime(secondShift.Item2);
                string endTime = clsShift.FormatDateTime(secondShift.Item3);

                try
                {
                    DataSet dataSet = clsDatabase.GetShiftSum(sConn, list[1], startTime, endTime);

                    dsSecond.Merge(dataSet);
                } catch (Exception ex) { Console.WriteLine(ex.Message ); }

            }
            if (dsSecond?.Tables.Count > 0)
            {
                this.bindingSourceSecondLast.DataSource = dsSecond.Tables[0];
            }
        }
    }
}
