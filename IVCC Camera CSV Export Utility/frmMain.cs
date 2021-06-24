using ControlCenterLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FileHelpers;
using System.Text;

namespace IVCC_Camera_CSV_Export_Utility
{
    public partial class frmMain : Form
    {
        List<ivCamera> camListObj = new List<ivCamera>();
        int totalCamCount = 0;
        int progress = 0;        
        
        public frmMain()
        {
            InitializeComponent();
            btnCancel.Enabled = false;
            btnExport.Enabled = false;
            olvCamList.Enabled = false;
        }

        private void bgwRequestHandler_DoWork(object sender, DoWorkEventArgs e)
        {

            Cameras _cameras = e.Argument as Cameras;
            totalCamCount = _cameras.Count;            

            foreach (Camera c in _cameras)
            {
                if (bgwRequestHandler.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                List<ivRecSchedule> _ivrList = new List<ivRecSchedule>();
                RecordingJobs r = c.RecordingJobs;
                Device d = c.Device;
                
                if (r.Count == 0)
                {
                    ivRecSchedule _ivr = new ivRecSchedule
                    {
                        NvrName = "[Not Recording]",
                        Status = "[Not Recording]",
                        Mode = "[Not Recording]"
                    };
                    _ivrList.Add(_ivr);
                }
                else
                {
                    foreach (RecordingJob j in r)
                    {
                        if (j.Mode == "Primary")
                        {
                            ivRecSchedule _ivr = new ivRecSchedule
                            {
                                NvrName = j.NvrName,
                                Status = j.Status,
                                Mode = j.Mode
                            };
                            _ivrList.Add(_ivr);
                        }
                    }
                }
                

                ivCamera _ivc = new ivCamera
                {
                    Access_URL = c.AccessUrl,
                    Online = d.Connected,
                    IP_Address = d.IpAddress,
                    Name = d.Name,
                    Service_ID = d.Uri,
                    Number = (int)c.LogicalNumber,
                    Primary_NVR_IP = c.PrimaryNvr.Device.IpAddress,
                    Primary_NVR = c.PrimaryNvr.Device.Name,
                    Home_Site = d.Site.Name,
                    Recording_NVR = _ivrList[0].NvrName,                    
                };
              
                camListObj.Add(_ivc);
                progress++;

                olvCamList.SetObjects(camListObj);

                int percent = progress * 100 / totalCamCount;
                bgwRequestHandler.ReportProgress(percent);
            }
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                olvCamList.ClearObjects();
                camListObj.Clear();
                progress = 0;
                progressBar.Value = 0;

                Automation ccAutomation = new Automation();
                Cameras ivCams = ccAutomation.Cameras;
                DialogResult proceed = MessageBox.Show("Total " + ivCams.Count.ToString() + " cameras found. Proceed to create list?", "Generate Camera List", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (proceed == DialogResult.OK)
                {
                    btnGenerate.Enabled = false;
                    olvCamList.Enabled = true;
                    btnCancel.Enabled = true;
                    btnExit.Enabled = false;
                    bgwRequestHandler.RunWorkerAsync(ivCams);
                }
                else
                {
                    lblGenerateStatus.Text = "Operation Cancelled.";
                    lblGenerateStatus.ForeColor = Color.Gold;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("IV ControlCenter is not running. Please run ControlCenter and try again.\n\nException: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bgwRequestHandler.IsBusy == true)
            {
                bgwRequestHandler.CancelAsync();
            }            
        }

        private void bgwRequestHandler_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblGenerateStatus.ForeColor = Color.SpringGreen;
            lblGenerateStatus.Text = progress.ToString() + "/" + totalCamCount.ToString() + " cameras generated";            
        }

        private void bgwRequestHandler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCancel.Enabled = false;
            btnExport.Enabled = true;
            btnGenerate.Enabled = true;
            btnExit.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "Comma-Separated Values (*.csv)|*.csv";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowHelp = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var csvwriter = new FileHelperEngine<ivCamera>(Encoding.UTF8);

                csvwriter.HeaderText = csvwriter.GetFileHeader();
                try
                {
                    csvwriter.WriteFile(saveFileDialog.FileName, camListObj);
                    MessageBox.Show("CSV File saved at " + saveFileDialog.FileName + ".", "CSV Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Failed to save CSV file at " + saveFileDialog.FileName + ".\nPlease verify whether folder is accessible and file is not in use", "CSV Export FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
