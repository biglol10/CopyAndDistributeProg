using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



/*
    .css, .cshtml, .js, .dll 파일배포 프로그램
*/

namespace CopyViewFilesProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtSource.Text = @"C:\Users\LG\Desktop\개발\View복사리스트";
            targetSrc.Text = @"C:\Users\LG\Desktop\개발\View복사파일";
            ViewSource.Text = @"C:\Users\LG\Desktop\프로젝트\배포용_LGC_MES\MES_WEB\Views";

            CopyButton.Focus();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            string stringdir = txtSource.Text;
            string targetdir = targetSrc.Text;
            string projectSource = ViewSource.Text;
            string[] txtFiles = Directory.GetFiles(stringdir);
            string aFolder = "";
            string targetdirFolder = "";
            string aFile = "";
            bool result = true;
            string targetPlace = "";


            foreach (var subdirectory in Directory.GetDirectories(@"" + targetSrc.Text))
            {
                var files = System.IO.Directory.GetFiles(subdirectory);
                foreach (var file in files)
                    ReadOnlyRemove(file);              // 복사하는 곳 초기화를 위해 타깃 경로 파일의 읽기전용 속성 체크해제

                Directory.Delete(subdirectory, true);
            }

            foreach (var txtfile in txtFiles)
            {
                string[] txtLines = System.IO.File.ReadAllLines(txtfile);

                for (int i = 0; i < txtLines.Length; i++)
                {
                    projectSource = ViewSource.Text;
                    if (string.IsNullOrWhiteSpace(txtLines[i]))
                    {
                        aFolder = "";
                        targetdirFolder = "";
                        aFile = "";
                    }
                    else if (txtLines[i].Contains("Folder: "))
                    {
                        try
                        {
                            targetdir = targetSrc.Text;
                            aFolder = txtLines[i].Substring(8);
                            targetdirFolder = targetdir + @"\" + aFolder;
                            if (!System.IO.Directory.Exists(targetdirFolder))
                                Directory.CreateDirectory(targetdirFolder);
                            ReadOnlyRemove(targetdirFolder);
                            DirectoryInfo parentDirectoryInfo = new DirectoryInfo(@"" + targetdirFolder);
                            ClearReadOnly(parentDirectoryInfo);
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.Message);
                            result = false;
                            break;
                        }

                    }
                    else if (txtLines[i].Contains(".dll"))
                    {
                        try
                        {
                            targetdir = ViewSource.Text;
                            targetdir = targetdir.Substring(0, targetdir.IndexOf(@"\Views")) + @"\bin";
                            projectSource = targetdir + @"\ABS.MES.dll";
                            System.IO.File.Copy(projectSource, targetSrc.Text + @"\ABS.MES.dll", true);
                            ReadOnlyRemove(targetSrc.Text + @"\ABS.MES.dll");
                        }
                        catch (Exception e2)
                        {
                            MessageBox.Show(e2.Message);
                            result = false;
                            break;
                        }
                    }
                    else if (txtLines[i].Contains(".cshtml"))
                    {
                        try
                        {
                            targetdir = targetSrc.Text;
                            aFile = txtLines[i];
                            projectSource += @"\" + aFolder + @"\" + aFile;
                            targetPlace = targetdirFolder + @"\" + aFile;
                            System.IO.File.Copy(projectSource, targetPlace, true);
                            ReadOnlyRemove(targetPlace);
                        }
                        catch (Exception e3)
                        {
                            MessageBox.Show(e3.Message);
                            result = false;
                            break;
                        }
                    }
                    else if (txtLines[i].Contains("Common Files"))
                    {
                        break;
                    }
                }
            }
            if (result)
                MessageBox.Show("복사를 완료했습니다");
            else
                MessageBox.Show("복사실패");
        }

        private void CopyCommonSource_Click(object sender, EventArgs e)
        {
            string stringdir = txtSource.Text;
            string targetdir = targetSrc.Text;
            string projectSource = ViewSource.Text;
            string[] txtFiles = Directory.GetFiles(stringdir);
            string aFolder = "";
            string targetdirFolder = "";
            string aFile = "";
            bool result = true;

            string[] jsFilePaths = Directory.GetFiles(ViewSource.Text.Substring(0, ViewSource.Text.IndexOf(@"\MES_WEB")), "*.js", SearchOption.AllDirectories);
            string[] viewFilePaths = Directory.GetFiles(ViewSource.Text.Substring(0, ViewSource.Text.IndexOf(@"\MES_WEB")), "*.cshtml", SearchOption.AllDirectories);
            string[] dllFilePaths = Directory.GetFiles(ViewSource.Text.Substring(0, ViewSource.Text.IndexOf(@"\MES_WEB")), "*.dll", SearchOption.AllDirectories);
            string[] cssFilePaths = Directory.GetFiles(ViewSource.Text.Substring(0, ViewSource.Text.IndexOf(@"\MES_WEB")), "*.css", SearchOption.AllDirectories);

            foreach (var txtfile in txtFiles)
            {
                string[] txtLines = System.IO.File.ReadAllLines(txtfile);

                for (int i = 0; i < txtLines.Length; i++)
                {
                    projectSource = ViewSource.Text;
                    if (txtLines[i].Contains("Common Files"))
                    {
                        try
                        {
                            targetdir = targetSrc.Text;
                            aFolder = txtLines[i];
                            targetdirFolder = targetdir + @"\" + aFolder;
                            if (!System.IO.Directory.Exists(targetdirFolder))
                                Directory.CreateDirectory(targetdirFolder);
                            ReadOnlyRemove(targetdirFolder);
                            DirectoryInfo parentDirectoryInfo = new DirectoryInfo(@"" + targetdirFolder);
                            ClearReadOnly(parentDirectoryInfo);

                            int idx = i;

                            while (true)
                            {
                                idx++;
                                try
                                {
                                    result = CallCopyCommonFiles(txtLines[idx], jsFilePaths, viewFilePaths, dllFilePaths, cssFilePaths, targetdirFolder);
                                }
                                catch (Exception e100)  //  작업 다 끝났을 때 여기를 타게, 즉 idx 범위 벗어나고 다음 라인이 공백
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception e0)  // 알 수 없는 오류
                        {
                            MessageBox.Show(e0.Message);
                            result = false;
                            break;
                        }
                        break;  // Common Files 만 보고 로직 종료 (파일 복사는 이미 while 안에 다 넣음)
                    }
                }
            }
            if (result)
                MessageBox.Show("복사를 완료했습니다");
            else
                MessageBox.Show("복사실패");
        }

        public bool CallCopyCommonFiles(string aFile, string[] jsFilePaths, string[] viewFilePaths, string[] dllFilePaths, string[] cssFilePaths, string targetdirFolder)
        {
            string fileExtension = !String.IsNullOrEmpty(aFile) ? Path.GetExtension(aFile) : "NOTHING";
            try
            {
                if (fileExtension == ".js")
                {
                    string filname = aFile;
                    string searchJSFile = jsFilePaths.Where(x => Path.GetFileName(x) == filname).ToList()[0].ToString(); // 같은 명칭으로 된 공통파일이 있을 경우 처음에 찾은 것만 가져옴
                    System.IO.File.Copy(searchJSFile, targetdirFolder + @"\" + Path.GetFileName(searchJSFile), true);
                    ReadOnlyRemove(targetdirFolder + @"\" + Path.GetFileName(searchJSFile));
                }
                else if (fileExtension == ".css")
                {
                    string filname = aFile;
                    string searchCSSFile = cssFilePaths.Where(x => Path.GetFileName(x) == filname).ToList()[0].ToString();
                    System.IO.File.Copy(searchCSSFile, targetdirFolder + @"\" + Path.GetFileName(searchCSSFile), true);
                    ReadOnlyRemove(targetdirFolder + @"\" + Path.GetFileName(searchCSSFile));
                }
                else if (fileExtension == ".cshtml")
                {
                    string filname = aFile;
                    var searchViewFile = viewFilePaths.Where(x => Path.GetFileName(x) == filname).Select(x => x).ToList()[0].ToString();
                    System.IO.File.Copy(searchViewFile, targetdirFolder + @"\" + Path.GetFileName(searchViewFile), true);
                    ReadOnlyRemove(targetdirFolder + @"\" + Path.GetFileName(searchViewFile));
                }
                else if (fileExtension == ".dll")
                {
                    string filname = aFile;
                    string searchDLLFile = dllFilePaths.Where(x => Path.GetFileName(x) == filname).Select(x => x).ToList()[0].ToString();
                    System.IO.File.Copy(searchDLLFile, targetdirFolder + @"\" + Path.GetFileName(searchDLLFile), true);
                    ReadOnlyRemove(targetdirFolder + @"\" + Path.GetFileName(searchDLLFile));
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return true;
        }

        private FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private void ReadOnlyRemove(string targetPlace)
        {
            FileAttributes attributes = File.GetAttributes(targetPlace);
            attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
            File.SetAttributes(targetPlace, attributes);
        }

        private void ClearReadOnly(DirectoryInfo parentDirectory)
        {
            if (parentDirectory != null)
            {
                parentDirectory.Attributes = FileAttributes.Normal;
                foreach (FileInfo fi in parentDirectory.GetFiles())
                {
                    fi.Attributes = FileAttributes.Normal;
                }
                foreach (DirectoryInfo di in parentDirectory.GetDirectories())
                {
                    ClearReadOnly(di);
                }
            }
        }

        private void example1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"C:\Users\biglol\Desktop\개발\파일 옮기기 테스트\복사리스트");
        }

        private void example2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"C:\Users\biglol\Desktop\개발\파일 옮기기 테스트\복사파일");
        }

        private void example3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"C:\Users\biglol\Desktop\프로젝트\[통합](배포용) ABS_MES TFS\LGC_MES\MES_WEB\Views");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
