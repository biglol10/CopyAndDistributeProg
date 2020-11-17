using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 개발자: 변지욱 사원
/// 내용  : 파일 복사 프로그램 (1서버, 전체 서버 배포 포함)
/// </summary>

namespace FileDistribution
{
    public partial class Form1 : Form
    {
        public string dllBackup { get; set; }
        public string WebSvcSrc { get; set; }
        public string ServiceSrc { get; set; }
        public string Plant { get; set; }
        public string TempDirectory { get; set; }
        public List<PlantDeployResult> DeployResultList { get; set; }

        public Form1()
        {
            InitializeComponent();

            //string myString = @"C:\Users\LG\Desktop\개발\LocationTXT.txt";  // for testing
            string myString = @"C:\Users\mes_adm01\Desktop\배포프로그램실행\LocationTXT.txt";    // for production

            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(myString);
            myString = utf8.GetString(utfBytes, 0, utfBytes.Length);

            string text = File.ReadLines(myString).First();

            var aSplit = text.Split(',');
            Plant = aSplit[0].Trim();               // 서버
            sourceLocation.Text = aSplit[1].Trim(); // 파일서버 위치
            TempDirectory = aSplit[2].Trim();       // 서버의 임시 폴더
            targetLocation.Text = aSplit[3].Trim(); // 뷰 폴더 위치 (예: D:\WEB_ROOT\ABS.MES\Views)
            dllBackup = aSplit[4].Trim();           // dll 백업 위치

            WebSvcSrc = aSplit[5].Trim();           // 웹서비스 dll 위치
            ServiceSrc = aSplit[6].Trim();          // BizActor dll 위치

            deployToServ.Select();

            DeployResultList = new List<PlantDeployResult>(); // 안 해주면 에러

            string checkMain = @"C:\Users\mes_adm01\Desktop\배포프로그램실행\MainServ.txt";
            if (!File.Exists(checkMain))
                DeployToAll.Enabled = false;

        }

        public void deployToServ_Click(object sender, EventArgs e)
        {
            ResultLabel.Text = "";
            DeployResultList = new List<PlantDeployResult>(); // 안 해주면 에러
            
            Directory.Delete(TempDirectory, true);
            Directory.CreateDirectory(TempDirectory);
            CloneDirectory(sourceLocation.Text, TempDirectory);
            Deploy(Plant, sourceLocation.Text, targetLocation.Text, dllBackup, WebSvcSrc, ServiceSrc);
            ResultLabel.Text = "배포완료";
            DeployResultView.DataSource = DeployResultList;
        }

        public void DeployToAll_Click(object sender, EventArgs e)
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
                
                #region
                // 이렇게 담아주지 않으면 string 값들이 꼬여서 백업, 배포가 제대로 되지 않음
                // 람다식은 지연현상이 있기 때문에 자기가 알아서 프로세스를 지연/시작함... 따라서 aSplit[0].Trim() 값이 이상하게 바뀌는 현상을 발견할 수 있음
                // http://blog.naver.com/PostView.nhn?blogId=oidoman&logNo=90159800032&redirect=Dlog&widgetTypeCall=true&directAccess=false
                string plant = aSplit[0].Trim();          // 플랜트
                string commftp = aSplit[1].Trim();        // 공통 서버
                string tempfolder = aSplit[2].Trim();     // 서버별 임시폴더 위치
                string targetProj = aSplit[3].Trim();     // 프로젝트 뷰폴더 위치
                string targetdllback = aSplit[4].Trim();  // dll 백업위치
                string targetwebsvc = aSplit[5].Trim();   // 웹서비스 위치
                string targetsa = aSplit[6].Trim();       // SA 위치
                #endregion

                Directory.Delete(tempfolder, true);
                Directory.CreateDirectory(tempfolder);

                CloneDirectory(commftp, tempfolder);  // 임시 폴더에 전부 복사

                //Deploy(plant, tempfolder, targetProj, targetdllback, targetwebsvc, targetsa);  동기로 할 시 이거 씀

                deployTasks.Add(new Task(() =>    // async 방법
                {
                    Deploy(plant, tempfolder, targetProj, targetdllback, targetwebsvc, targetsa);
                    //Deploy(aSplit[0].Trim(), aSplit[2].Trim(), aSplit[3].Trim(), aSplit[4].Trim(), aSplit[5].Trim(), aSplit[6].Trim());  // 경로가 알수없는 이유로 계속 꼬임
                }));
            }

            AsyncDeploy(deployTasks);
        }

        public async void AsyncDeploy(List<Task> deployList)
        {
            Parallel.ForEach(deployList, task => task.Start());
            await Task.WhenAll(deployList);
            ResultLabel.Text = "전체 서버 배포완료";
            DeployResultView.DataSource = DeployResultList;
        }

        public void Deploy(string plant, string sourceLocationTXT, string targetLocationTXT, string dllBackupPath, string webSvcSrcPath, string serviceSrcPath)
        {

            string sourcePath = sourceLocationTXT;
            string targetPath = targetLocationTXT;

            string fileName = "";
            string destFile = "";

            string[] files = new string[] { };

            //string[] files = System.IO.Directory.GetFiles(sourcePath);
            string[] subdirectoryEntries = Directory.GetDirectories(sourcePath);
            string[] subFiles = Directory.GetFiles(sourcePath);
            foreach (var subFile in subFiles)
            {
                try
                {
                    if (System.IO.Path.GetFileName(subFile) == "ABS.MES.dll")
                    {
                        string dllLocation = targetPath.Replace("Views", "") + @"\\bin\\ABS.MES.dll";

                        string dllBackupDir = CreateBackupDirectory(dllBackupPath);

                        System.IO.File.Copy(dllLocation, System.IO.Path.Combine(dllBackupDir, System.IO.Path.GetFileName(subFile)), true);
                        ReadOnlyRemove(dllLocation);
                        System.IO.File.Copy(subFile, dllLocation, true);
                    }
                    else if (System.IO.Path.GetFileName(subFile) == "BizActorWeb.dll" || System.IO.Path.GetFileName(subFile) == "MesServiceAccess.dll")
                    {

                        string dllBackupDir = CreateBackupDirectory(dllBackupPath);

                        string IF_FileName = System.IO.Path.GetFileName(subFile);

                        if (IF_FileName == "BizActorWeb.dll")
                        {
                            System.IO.File.Copy(webSvcSrcPath + @"\" + IF_FileName, dllBackupDir + @"\" + IF_FileName, true);
                            destFile = System.IO.Path.Combine(@"" + webSvcSrcPath, IF_FileName);
                            ReadOnlyRemove(destFile);           // 무조건 파일이 있어야 함. 안 그러면 없는 파일의 속성을 변경하려고 해서 에러발생, SA랑 WebSvc는 무조건 있다고 가정
                            System.IO.File.Copy(subFile, destFile, true);
                            ReadOnlyRemove(destFile);
                        }
                        else if (IF_FileName == "MesServiceAccess.dll")
                        {
                            System.IO.File.Copy(serviceSrcPath + @"\" + IF_FileName, dllBackupDir + @"\" + IF_FileName, true);
                            destFile = System.IO.Path.Combine(@"" + serviceSrcPath, IF_FileName);
                            ReadOnlyRemove(destFile);           // 무조건 파일이 있어야 함. 안 그러면 없는 파일의 속성을 변경하려고 해서 에러발생, SA랑 WebSvc는 무조건 있다고 가정
                            System.IO.File.Copy(subFile, destFile, true);
                            ReadOnlyRemove(destFile);
                        }
                    }
                }
                catch (Exception dllF)
                {
                    RecordResult(plant, "F", dllF.Message + subFile);
                    return;
                }
            }
            foreach (var subdirectory in subdirectoryEntries)
            {
                if (subdirectory.Contains("Common Files"))
                {
                    try
                    {
                        // 프로젝트 내 파일리스트 전부 가져옴
                        string[] jsFilePaths = Directory.GetFiles(targetPath.Substring(0, targetPath.IndexOf(@"\Views")), "*.js", SearchOption.AllDirectories);
                        string[] htmlFilePaths = Directory.GetFiles(targetPath.Substring(0, targetPath.IndexOf(@"\Views")), "*.cshtml", SearchOption.AllDirectories);
                        string[] cssFilePaths = Directory.GetFiles(targetPath.Substring(0, targetPath.IndexOf(@"\Views")), "*.css", SearchOption.AllDirectories);
                        string[] dllFilePaths = Directory.GetFiles(targetPath.Substring(0, targetPath.IndexOf(@"\Views")), "*.dll", SearchOption.AllDirectories);

                        files = System.IO.Directory.GetFiles(subdirectory);
                        foreach (var aFile in files)
                        {
                            fileName = System.IO.Path.GetFileName(aFile);
                            if (System.IO.Path.GetExtension(fileName) == ".js")
                                destFile = jsFilePaths.Where(x => x.Contains(fileName)).ToList()[0].ToString();
                            else if (System.IO.Path.GetExtension(fileName) == ".cshtml")
                                destFile = htmlFilePaths.Where(x => x.Contains(fileName)).ToList()[0].ToString();
                            else if (System.IO.Path.GetExtension(fileName) == ".css")
                                destFile = cssFilePaths.Where(x => x.Contains(fileName)).ToList()[0].ToString();
                            else if (System.IO.Path.GetExtension(fileName) == ".dll")
                                destFile = dllFilePaths.Where(x => x.Contains(fileName)).ToList()[0].ToString();
                            if (File.Exists(destFile))
                                ReadOnlyRemove(destFile);          // 파일이 있을 경우에만 속성 변경
                            System.IO.File.Copy(System.IO.Path.Combine(subdirectory, fileName), destFile, true);
                            ReadOnlyRemove(destFile);
                        }
                    }
                    catch (Exception commonF)
                    {
                        RecordResult(plant, "F", "ABS 프로젝트에 포함되지 않은 공통파일이 있습니다");
                        return;
                    }
                }
                else
                {
                    try
                    {
                        files = System.IO.Directory.GetFiles(subdirectory);

                        string aDirectory = subdirectory.Replace(sourcePath, "");

                        foreach (var aFile in files)
                        {
                            fileName = System.IO.Path.GetFileName(aFile);
                            destFile = System.IO.Path.Combine(targetPath + aDirectory, fileName);
                            if (File.Exists(destFile))
                                ReadOnlyRemove(destFile);          // 파일이 있을 경우에만 속성 변경
                            System.IO.File.Copy(System.IO.Path.Combine(subdirectory, fileName), destFile, true);
                            ReadOnlyRemove(destFile);
                        }
                    }
                    catch (Exception viewF)
                    {
                        RecordResult(plant, "F", "뷰 폴더/파일 명을 확인해주세요, " + viewF);
                        return;
                    }
                }
            }
            RecordResult(plant, "S", "Success");
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

        public void ReadOnlyRemove(string targetPlace)
        {
            FileAttributes attributes = File.GetAttributes(targetPlace);
            attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
            File.SetAttributes(targetPlace, attributes);
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

        public class PlantDeployResult
        {
            public string Plant { get; set; }
            public string Result { get; set; }
            public string Message { get; set; }
        }

        public void NewFileList_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
