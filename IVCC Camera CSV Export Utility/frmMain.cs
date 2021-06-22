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

                Device d = c.Device;
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
                    Home_Site = d.Site.Name
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
                DialogResult proceed = MessageBox.Show("총 " + ivCams.Count.ToString() + " 카메라가 발견되었습니다. 목록을 생성할까요?", "카메라 목록 생성", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

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
                    lblGenerateStatus.Text = "목록 생성 취소";
                    lblGenerateStatus.ForeColor = Color.Gold;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("IV ControlCenter가 실행중이 아닙니다. ControlCenter에 로그인 하신 후 다시 시도해주세요.\n\n오류 내용: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            lblGenerateStatus.Text = progress.ToString() + "/" + totalCamCount.ToString() + "대 카메라 생성 완료";            
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
                    MessageBox.Show("CSV 파일을 " + saveFileDialog.FileName + "에 저장했습니다.", "CSV 내보내기 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("CSV 파일을 " + saveFileDialog.FileName + "에 저장하려는 도중 실패했습니다.\n파일 위치가 올바르거나 파일이 열려있는지 확인 해 주십시오.", "CSV 내보내기 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
