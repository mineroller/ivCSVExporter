using ControlCenterLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FileHelpers;
using System.Text;
using System.Linq;
using System.Net;

namespace IVCC_Camera_CSV_Export_Utility
{
    public partial class frmMain : Form
    {
        List<ivCamera> camListObj = new List<ivCamera>();
        int totalCamCount = 0;
        int progress = 0;
        string _onvifusr = "";
        string _onvifpwd = "";


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
            
            string _locationString = "[None]";
            string _macString = "[None]";
            string _serString = "[None]";
            string _fwString = "[None]";
            string _modelString = "[None]";

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

                if (chkGetLocMac.Checked)
                {
                    if (d.Connected)
                    {
                        bool _retry = true;
                        while (_retry)
                        {
                            try
                            {
                                string _HttpUrl = c.AccessUrl;

                                // Since https is not supported, take the AccessURL and change the address to http instead of https.

                                if (c.AccessUrl.Substring(0,5) == "https")
                                {
                                    _HttpUrl = c.AccessUrl.Replace("https://", "http://");
                                    // MessageBox.Show("Old URL: " + c.AccessUrl + "\n" + "New URL: " + _HttpUrl, "URL Changed", MessageBoxButtons.OK);
                                }                               

                                ivOnvifObject _onvifObj = OnvifHelper.CreateONVIFObject(_HttpUrl, _onvifusr, _onvifpwd);

                                _locationString = _onvifObj.Location;
                                _macString = _onvifObj.MAC_Address;
                                _serString = _onvifObj.Serial_Number;
                                _modelString = _onvifObj.Hardware_Model;
                                _fwString = _onvifObj.FW_Version;

                                _retry = false;

                                break;

                            }
                            catch (Exception ex)
                            {
                                DialogResult _onvifError = MessageBox.Show(ex.Message + "\n\nChoose an action: Abort, Retry or Ignore?", "ONVIF Error: Device [" + d.IpAddress + "]", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                
                                if (_onvifError == DialogResult.Retry)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        AskOnvifPassword getOnvifCreds = new AskOnvifPassword(d.IpAddress);
                                        getOnvifCreds.ShowDialog(this);
                                        _onvifusr = getOnvifCreds.UserName;
                                        _onvifpwd = getOnvifCreds.Password;
                                    });
                                    _retry = true;
                                    continue;
                                }
                                else if (_onvifError == DialogResult.Abort)
                                {
                                    btnCancel_Click(sender, new EventArgs());
                                    break;
                                }
                                else
                                {
                                    _locationString = "[AuthError]";
                                    _macString = "[AuthError]";
                                    _serString = "[AuthError]";
                                    _modelString = "[AuthError]";
                                    _fwString = "[AuthError]";
                                    _retry= false;
                                }
                            }                            
                        }
                    }
                    else
                    {
                        _locationString = "[Offline]";
                        _macString = "[Offline]";
                        _serString = "[Offline]";
                        _modelString = "[Offline]";
                        _fwString = "[Offline]";
                    }                        
                }

                ivCamera _ivc = new ivCamera
                {
                    Access_URL = c.AccessUrl,
                    Online = d.Connected,
                    IP_Address = d.IpAddress,
                    Name = d.Name,
                    Location = _locationString,
                    MAC_Address = _macString,
                    Serial_Number = _serString,
                    Hardware_Model = _modelString,
                    FW_Version = _fwString,
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

                if (chkGetLocMac.Checked)
                {
                    AskOnvifPassword getOnvifCreds = new AskOnvifPassword("[Default]");
                    getOnvifCreds.ShowDialog(this);
                    _onvifusr = getOnvifCreds.UserName;
                    _onvifpwd = getOnvifCreds.Password;
                }

                Automation ccAutomation = new Automation();
                Cameras ivCams = ccAutomation.Cameras;
                DialogResult proceed = MessageBox.Show("Total " + ivCams.Count.ToString() + " cameras found. Proceed to create list?", "Generate Camera List", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                
                if (proceed == DialogResult.OK)
                {
                    btnGenerate.Enabled = false;
                    olvCamList.Enabled = true;
                    btnCancel.Enabled = true;
                    btnExit.Enabled = false;
                    chkGetLocMac.Enabled = false;
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
            btnGenerate.Enabled = false;
            MessageBox.Show("Operation Aborted. Please close and restart the application.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            btnGenerate.Enabled = false;
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
