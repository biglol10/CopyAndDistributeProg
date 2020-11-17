using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 개발자: 변지욱 사원
/// 내용  : 신규 파일 배포
/// </summary>

namespace FileDistribution
{
    public partial class Form2 : Form
    {
        public Dictionary<String, bool> PlantFirstTime { get; set; } = new Dictionary<string, bool>();
        public Dictionary<String, String> PlantBackupDirectory { get; set; } = new Dictionary<string, string>();
        public List<PlantDeployResult> DeployResultList { get; set; }
        public Form1 ParentForm { get; set; }
        public bool isFirstRun { get; set; } = true;
        public Form2()
        {
            ParentForm = new Form1(); // 부모 Form 메소드 쓰기 위해 (여기에서 재정의 필요X)
            
            InitializeComponent();
            this.NewFileGrid.KeyDown += new KeyEventHandler(NewFileGrid_KeyDown);

            deployToServ.Select();

            DeployResultList = new List<PlantDeployResult>(); // 안 해주면 에러

            string checkMain = @"C:\Users\mes_adm01\Desktop\배포프로그램실행\MainServ.txt";
            if (!File.Exists(checkMain))
                DeployToAll.Enabled = false;

        }
        void NewFileGrid_KeyDown(object sender, KeyEventArgs e)  // ctrl+v 메소드
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = NewFileGrid.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                int row = NewFileGrid.CurrentCell.RowIndex;
                int col = NewFileGrid.CurrentCell.ColumnIndex;
                foreach (string line in lines)
                {
                    if (row < NewFileGrid.RowCount && line.Length > 0)
                    {
                        NewFileGrid.Rows.Add();
                        string[] cells = line.Split('\t');
                        for (int i = 0; i < cells.GetLength(0); ++i)
                        {
                            if (col + i < this.NewFileGrid.ColumnCount)
                            {
                                NewFileGrid[col + i, row].Value = Convert.ChangeType(cells[i], NewFileGrid[col + i, row].ValueType);
                            }
                            else
                            {
                                break;
                            }
                        }
                        row++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void deployToServ_Click(object sender, EventArgs e)
        {
            //string myString = @"C:\Users\LG\Desktop\개발\LocationTXT.txt";  // for testing
            string myString = @"C:\Users\mes_adm01\Desktop\배포프로그램실행\LocationTXT.txt";    // for production

            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(myString);
            myString = utf8.GetString(utfBytes, 0, utfBytes.Length);

            string text = File.ReadLines(myString).First();

            var aSplit = text.Split(',');

            string plant = aSplit[0].Trim();
            string sourcelocation = aSplit[1].Trim();
            string tempdirectory = aSplit[2].Trim();
            string targetlocation = aSplit[3].Trim();
            string backupDir = aSplit[4].Trim();

            ResultLabel.Text = "";
            DeployResultList = new List<PlantDeployResult>(); // 안 해주면 에러

            Directory.Delete(tempdirectory, true);
            Directory.CreateDirectory(tempdirectory);
            ParentForm.CloneDirectory(sourcelocation, tempdirectory);

            Deploy(plant, tempdirectory, targetlocation, backupDir);

            isFirstRun = true;

            ResultLabel.Text = "배포완료";
            DeployResultView.DataSource = DeployResultList;
            // DeployResultView.DataSource = DeployResultList;
        }

        private void DeployToAll_Click(object sender, EventArgs e)
        {
            ResultLabel.Text = "";
            DeployResultList = new List<PlantDeployResult>(); // 안 해주면 에러

            //string myString = @"C:\Users\LG\Desktop\개발\LocationTXT.txt";  // for testing
            string myString = @"C:\Users\mes_adm01\Desktop\배포프로그램실행\LocationTXT.txt";    // for production

            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(myString);
            myString = utf8.GetString(utfBytes, 0, utfBytes.Length);

            System.IO.StreamReader file = new System.IO.StreamReader(myString);
            string line = "";
            List<Task> deployTasks = new List<Task>();

            while ((line = file.ReadLine()) != null)
            {
                var aSplit = line.Split(',');
                

                string plant = aSplit[0].Trim();          // 플랜트
                string commftp = aSplit[1].Trim();        // 공통 서버
                string tempfolder = aSplit[2].Trim();     // 서버별 임시폴더 위치
                string targetProj = aSplit[3].Trim();     // 프로젝트 뷰폴더 위치
                string targetdllback = aSplit[4].Trim();  // dll 백업위치

                Directory.Delete(tempfolder, true);
                Directory.CreateDirectory(tempfolder);

                PlantFirstTime.Add(plant, true);
                PlantBackupDirectory.Add(plant, "");

                CloneDirectory(commftp, tempfolder);

                deployTasks.Add(new Task(() =>    // async 방법
                {
                    Deploy(plant, tempfolder, targetProj, targetdllback);
                }));
            }
            AsyncDeploy(deployTasks);
        }

        public void Deploy(string plant, string sourceLocationTXT, string targetLocationTXT, string dllBackupPath)
        {

            string sourcePath = sourceLocationTXT;
            string targetPath = targetLocationTXT;

            string fileName = "";
            string destFile = "";

            string[] files = new string[] { };

            //string[] files = System.IO.Directory.GetFiles(sourcePath);
            string[] subdirectoryEntries = Directory.GetDirectories(sourcePath);
            string[] subFiles = Directory.GetFiles(sourcePath);

            List<FileLocation> locationList = new List<FileLocation>();

            for (int i = 0; i < NewFileGrid.Rows.Count; i++)
            {
                try
                {
                    if(NewFileGrid.Rows[i].Cells[0].Value != null || NewFileGrid.Rows[i].Cells[1].Value != null)
                    {
                        FileLocation fileLocation = new FileLocation
                        {
                            fileName = NewFileGrid.Rows[i].Cells[0].Value.ToString(),
                            fileLocation = NewFileGrid.Rows[i].Cells[1].Value.ToString().Substring(0, NewFileGrid.Rows[i].Cells[1].Value.ToString().Length - 1)  // \r 이 알 수 없는 이유로 붙음
                        };
                        fileLocation.fileLocation = fileLocation.fileLocation.Substring(0, 1) == @"\" ? fileLocation.fileLocation.Substring(1) : fileLocation.fileLocation;
                        locationList.Add(fileLocation);
                    }
                    
                }
                catch(Exception e)
                {
                    continue;
                }
            }

            foreach (var subdirectory in subdirectoryEntries)
            {
                if (subdirectory.Contains("New Files"))
                {
                    try
                    {
                        files = System.IO.Directory.GetFiles(subdirectory);
                        foreach (var aFile in files)
                        {
                            fileName = System.IO.Path.GetFileName(aFile);
                            var filetarget = locationList.Where(x => x.fileName == fileName).ToList();
                            if(filetarget.Count > 0)
                            {
                                
                                string deployFileLocation = targetPath.Replace("Views", "") + filetarget[0].fileLocation + @"\" + fileName;
                                if (File.Exists(deployFileLocation))
                                {
                                    if (PlantFirstTime[plant])
                                    {
                                        PlantBackupDirectory[plant] = CreateBackupDirectory(dllBackupPath);
                                        PlantFirstTime[plant] = false;
                                    }
                                    ReadOnlyRemove(deployFileLocation);
                                    System.IO.File.Copy(deployFileLocation, PlantBackupDirectory[plant] + @"\" + fileName, true);
                                }
                                System.IO.File.Copy(aFile, deployFileLocation, true);
                                ReadOnlyRemove(deployFileLocation);
                            }
                        }
                    }
                    catch (Exception commonF)
                    {
                        RecordResult(plant, "F", commonF.Message);
                        return;
                    }
                    break;
                }
            }
            RecordResult(plant, "S", "Success");
        }

        

        public async void AsyncDeploy(List<Task> deployList)
        {
            Parallel.ForEach(deployList, task => task.Start());
            await Task.WhenAll(deployList);
            ResultLabel.Text = "전체 서버 배포완료";
            DeployResultView.DataSource = DeployResultList;
        }

        public void RecordResult(string plant, string pf, string message)
        {
            PlantDeployResult plantDeployResult = new PlantDeployResult
            {
                Plant = plant,
                Result = pf,
                Message = message
            };
            DeployResultList.Add(plantDeployResult);
        }

        public void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
                ReadOnlyRemove(Path.Combine(dest, Path.GetFileName(file)));
            }
        }
        public void ReadOnlyRemove(string targetPlace)
        {
            FileAttributes attributes = File.GetAttributes(targetPlace);
            attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
            File.SetAttributes(targetPlace, attributes);
        }

        public FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        public string CreateBackupDirectory(string dllBackup)
        {
            string dllBackupCreate = dllBackup + @"\\" + DateTime.Now.ToString("yyyy-MM-dd");
            string dllBackupEnd = "";
            if (!System.IO.Directory.Exists(dllBackupCreate))
            {
                Directory.CreateDirectory(dllBackupCreate);
                return dllBackupCreate;
            }
            else
            {
                var qqq = Directory.GetDirectories(dllBackup).ToList();

                var subdirectoryEntries = Directory.GetDirectories(dllBackup).Where(x => x.IndexOf(DateTime.Now.ToString("yyyy-MM-dd")) > 0).OrderBy(x => x.Length).ToList();
                //Directory.GetDirectories(dllBackup).OrderBy(x => x.Substring(x.IndexOf(DateTime.Now.ToString("yyyy-MM-dd")), 10)).ThenBy(x => x.Length).ToList();

                string lastFolder = subdirectoryEntries[subdirectoryEntries.Count - 1];

                if (lastFolder.Contains("" + DateTime.Now.ToString("yyyy-MM-dd") + "-"))
                {
                    int folderIndex = Convert.ToInt32(lastFolder.Substring(lastFolder.IndexOf("" + DateTime.Now.ToString("yyyy-MM-dd") + "-") + 11));
                    folderIndex++;
                    dllBackupEnd = lastFolder.Substring(0, lastFolder.IndexOf("" + DateTime.Now.ToString("yyyy-MM-dd") + "-")) + DateTime.Now.ToString("yyyy-MM-dd") + "-" + folderIndex;
                    Directory.CreateDirectory(dllBackupEnd);

                }
                else if (lastFolder.Contains("" + DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    dllBackupEnd = lastFolder + "-1";
                    Directory.CreateDirectory(dllBackupEnd);
                }
            }

            return dllBackupEnd;
        }

        public class PlantDeployResult
        {
            public string Plant { get; set; }
            public string Result { get; set; }
            public string Message { get; set; }
        }

        public class FileLocation
        {
            public string fileName { get; set; }
            public string fileLocation { get; set; }
        }

        
    }
}
