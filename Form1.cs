using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using PyCodeConfig = PyCode_Configuration;

namespace PyCode
{
    public partial class wdwPyCode : Form
    {
        public wdwPyCode()
        {
            InitializeComponent();
            new Thread(new ThreadStart(init)).Start();
        }

        #region Cmd, And line 21
        public String isRun = "start";
        readonly CmdUtils cmd = new CmdUtils();

        /// <summary>
        /// 向控制台追加字符串
        /// </summary>
        public void console_append(string content)
        {
            console.AppendText(content);
        }

        /// <summary>
        /// 发送消息到状态区
        /// </summary>
        /// <param name="Context"></param>
        public void sendMsgToStatus(string Context)
        {
            labLeBoMsg.Text = " " + Context;
        }

        /// <summary>
        /// 初始化 cmd
        /// </summary>
        private void init()
        {
            cmd.sendCmd(this);
        }
        #endregion

        /// <summary>
        /// 进程延迟
        /// </summary>
        /// <param name="SleepTime"></param>
        private void sleep(int SleepTime)
        {
            Thread t = new Thread(o => Thread.Sleep(SleepTime));
            t.Start(this);
            while (t.IsAlive)
            {
                Application.DoEvents();
            }
        }

        #region variables
        private string LogPath = Application.StartupPath + @"\Temp\Log\log.ini";
        private string OpenFileName = string.Empty;
        private string SaveFileName = string.Empty;

        private string Tagmodel = "0";
        private string startupmodel = string.Empty;

        readonly System.Timers.Timer Timers = new System.Timers.Timer();

        CompletionWindow completionWindow;
        FoldingManager foldingManager;
        XmlFoldingStrategy foldingStrategy;

        public string message = "就绪";
        #endregion

        #region Timing Events
        /// <summary>
        /// 后台事件
        /// </summary>
        private void timingEventEvent()
        {
            Timers.Elapsed += new ElapsedEventHandler(timingEventImplement);
            Timers.Interval = 2000;
            Timers.AutoReset = true;
            Timers.Enabled = true;
        }
        private delegate void DelegateFunction();
        /// <summary>
        /// 实现方法体
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void timingEventImplement(object source, System.Timers.ElapsedEventArgs e)
        {
            DelegateFunction s = delegate ()
            {
                sendMsgToStatus(message);

                switch (Tagmodel)
                {
                    case "0":
                        {
                            writeFile(Application.StartupPath + @"\Cache\pycode-temp.code", codebox.Text);
                        }
                        break;
                    case "as":
                        {
                            writeFile(SaveFileName, codebox.Text);
                        }
                        break;
                    case "read":
                        {
                            writeFile(OpenFileName, codebox.Text);
                        }
                        break;
                }
            };
            this.Invoke(s);
        }
        #endregion

        /// <summary>
        /// 控制台焦点到最后一行
        /// </summary>
        public void consoleScrollToEndImplement()
        {
            DelegateFunction s = delegate ()
            {
                console.ScrollToLine(console.LineCount);
            };
            this.Invoke(s);
        }

        #region TabControl Add Pages
        private static readonly int max = 7;
        private TextEditor[] codeArea = new TextEditor[max];
        private int build = 1;
        private void addPage()
        {
            try
            {
                if (build == max)
                {
                    sendMsgToStatus("已自动拦截超出的载体");
                    return;
                }
                #region start
                // 1 : 1.1
                MetroFramework.Controls.MetroTabPage nPage = new MetroFramework.Controls.MetroTabPage();
                System.Windows.Forms.Integration.ElementHost nHost = new System.Windows.Forms.Integration.ElementHost();

                // 1.2
                //nHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                //| System.Windows.Forms.AnchorStyles.Left)
                //| System.Windows.Forms.AnchorStyles.Right)));
                nHost.Dock = DockStyle.Fill;
                nHost.BackColor = System.Drawing.Color.White;
                nHost.Location = new System.Drawing.Point(0, 0);
                nHost.Name = "eleHost" + build;
                nHost.Size = new System.Drawing.Size(756, 313);
                nHost.TabIndex = 2;
                nHost.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.eleCodeBox_ChildChanged);
                nHost.Child = null;

                // 1.3
                this.mtp.Controls.Add(nPage);

                // 1.4
                nPage.Controls.Add(nHost);
                nPage.HorizontalScrollbarBarColor = true;
                nPage.HorizontalScrollbarHighlightOnWheel = false;
                nPage.HorizontalScrollbarSize = 10;
                nPage.Location = new System.Drawing.Point(4, 38);
                nPage.Name = "nPage" + build;
                nPage.Size = new System.Drawing.Size(756, 313);
                nPage.TabIndex = 0;
                nPage.Text = "AreaCode" + (build+1);
                nPage.UseVisualStyleBackColor = true;
                nPage.VerticalScrollbarBarColor = true;
                nPage.VerticalScrollbarHighlightOnWheel = false;
                nPage.VerticalScrollbarSize = 10;
                // done

                // 2
                this.codeArea[build] = new TextEditor
                {
                    HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Hidden,
                    ShowLineNumbers = true,
                    Padding = new System.Windows.Thickness(15),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 13
                };
                // done


                // 3 : 3.1
                nHost.Child = codeArea[build];
                SearchPanel.Install(codeArea[build].TextArea);

                // 3.2
                string name = Assembly.GetExecutingAssembly().GetName().Name + ".Python.xshd";
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream s = assembly.GetManifestResourceStream(name))
                {
                    using (XmlTextReader reader = new XmlTextReader(s))
                    {
                        var xshd = HighlightingLoader.LoadXshd(reader);
                        codeArea[build].SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                    }
                }

                // 3.3
                codeArea[build].TextArea.TextEntering += codebox_TextArea_TextEntering;
                codeArea[build].TextArea.TextEntered += codebox_TextArea_TextEntered;

                // 3.4
                foldingManager = FoldingManager.Install(codeArea[build].TextArea);
                foldingStrategy.UpdateFoldings(foldingManager, codeArea[build].Document);

                // 3.5
                codeArea[build].KeyUp += new System.Windows.Input.KeyEventHandler(run_KeyUp);
                // done
                #endregion
                ++build;
            }
            catch (Exception ex)
            {
                PyCodeConfig.Config.writeConfiguration(LogPath, DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString(), ex.ToString());
            }
        }
        #endregion

        /// <summary>
        /// Code Box 载体
        /// </summary>
        private readonly TextEditor codebox = new TextEditor
        {
            HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Hidden,
            ShowLineNumbers = true,
            Padding = new System.Windows.Thickness(15),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 13
        };

        /// <summary>
        /// 控制台载体
        /// </summary>
        private readonly TextEditor console = new TextEditor
        {
            HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Hidden,
            Foreground = new SolidColorBrush(Color.FromRgb(204, 204, 204)),
            Background = new SolidColorBrush(Color.FromRgb(1, 36, 86)),
            Padding = new System.Windows.Thickness(8),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            Text = ""
        };

        /// <summary>
        /// 无 BOM 的 UTF-8
        /// </summary>
        /// <param name="address"></param>
        /// <param name="str"></param>
        private void writeFile(string address, string str)
        {
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            using (var sink = new StreamWriter(address, false, utf8WithoutBom))
            {
                sink.Write(str);
                sink.Flush();
            }
        }

        /// <summary>
        /// 窗体载入3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = true;

            eleCodeBox.Child = codebox;
            eleConsole.Child = console;

            // Fast search
            SearchPanel.Install(codebox.TextArea);
            SearchPanel.Install(console.TextArea);

            // Syntax rule
            int i = 1;
            while (i <= 2)
            {
                string name = Assembly.GetExecutingAssembly().GetName().Name + (i == 1 ? ".Python.xshd" : ".Console.xshd");
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream s = assembly.GetManifestResourceStream(name))
                {
                    using (XmlTextReader reader = new XmlTextReader(s))
                    {
                        var xshd = HighlightingLoader.LoadXshd(reader);
                        (i == 1 ? codebox : console).SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                    }
                }
                i++;
            }

            // 在构造函数中
            codebox.TextArea.TextEntering += codebox_TextArea_TextEntering;
            codebox.TextArea.TextEntered += codebox_TextArea_TextEntered;

            foldingManager = FoldingManager.Install(codebox.TextArea);
            foldingStrategy = new XmlFoldingStrategy();
            foldingStrategy.UpdateFoldings(foldingManager, codebox.Document);

            #region 多线程
            Thread ThreadTimingEvent = new Thread(timingEventEvent);
            ThreadTimingEvent.Start();
            ThreadTimingEvent.IsBackground = true;
            #endregion

            codebox.KeyUp += new System.Windows.Input.KeyEventHandler(run_KeyUp);
            console.KeyUp += new System.Windows.Input.KeyEventHandler(run_KeyUp);

            // 获取 python 编译环境地址, 缓存到 CommonSymbolSet 类的 PythonPyCodeTargetPath & PythonLocationTargetPath 变量
            PyCode.CommonSymbolSet.PythonPyCodeTargetPath = PyCodeConfig.Config.readConfiguration(Application.StartupPath + @"\Important\Configuration\Preferences.ini", "path", "python-use-pycode");
            PyCode.CommonSymbolSet.PythonLocationTargetPath = PyCodeConfig.Config.readConfiguration(Application.StartupPath + @"\Important\Configuration\Preferences.ini", "path", "python-use-location");
        }

        private void run_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F5) toolStripMenuItem10.PerformClick(); // 命令提示符运行
            else if (e.Key == Key.F5) qd.PerformButtonClick(); // 默认运行
            else if (e.Key == Key.F1) temprun.PerformClick(); // 缓存运行
        }

        void codebox_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            completionWindow = new CompletionWindow(codebox.TextArea);

            // 用户按下点后打开代码完成
            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

            if (".".Equals(e.Text))
            {
                foreach (string obj in PyCode_IntelliCode.IntelliCode.dotPythonWords)
                {
                    data.Add(new MyCompletionData(obj));
                }

                completionWindow.Show();
                completionWindow.Closed += delegate
                {
                    completionWindow = null;
                };
            }
        }
        void codebox_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (char.IsLetterOrDigit(e.Text[0]))
                {
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFiles(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog open = new OpenFileDialog
                {
                    Filter = "Files|*.py;*.xml;*.xshd",
                })
                {
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        OpenFileName = open.FileName;
                        SaveFileName = string.Empty;
                    }
                    if ("".Equals(OpenFileName)) return;

                    // !!!
                    codebox.Load(OpenFileName);

                    // ###

                    Tagmodel = "read";
                }
                //setMpgsText(Path.GetFileName(OpenFileName));

                execute(OpenFileName.Substring(0, 2));
                sleep(500);
                execute("cd " + Path.GetDirectoryName(OpenFileName));
            }
            catch (Exception)
            {
                // TODO PASS
            }
        }


        /// <summary>
        /// 保存 --- 标记模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFiles(object sender, EventArgs e)
        {
            switch (Tagmodel)
            {
                case "0":
                    {
                        SaveAsFiles(sender, e);
                    }
                    break;
                case "as":
                    {
                        writeFile(SaveFileName, codebox.Text);
                    }
                    break;
                case "read":
                    {
                        writeFile(OpenFileName, codebox.Text);
                    }
                    break;
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsFiles(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog
            {
                DefaultExt = "py",
                Filter = "Python Files|*.py"
            })
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    SaveFileName = save.FileName;
                    OpenFileName = string.Empty;
                }
                if ("".Equals(SaveFileName)) return;
                writeFile(SaveFileName, codebox.Text);
                Tagmodel = "as";
            }

            execute(SaveFileName.Substring(0, 2));
            sleep(500);
            execute("cd " + Path.GetDirectoryName(SaveFileName));
        }


        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Xjwj_ButtonClick(object sender, EventArgs e)
        {

        }

        private void Dkwj_Click(object sender, EventArgs e) { OpenFiles(sender, e); }
        private void Bc_Click(object sender, EventArgs e) { SaveFiles(sender, e); }
        private void Lcw_Click(object sender, EventArgs e) { SaveAsFiles(sender, e); }
        private void Cx_ButtonClick(object sender, EventArgs e) { codebox.Undo(); }
        private void Cz_ButtonClick(object sender, EventArgs e) { codebox.Redo(); }


        /// <summary>
        /// 启动 ≌ 执行代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Qd_ButtonClick(object sender, EventArgs e)
        {
            // 结束 python 进程，防止 web 开发中无法更新服务器
            // 需要判断上下文是否存在非终结符号 "# webdev"
            if (codebox.Text.Contains("# webdev"))
            {
                execute("taskkill /f /t /im python.exe");
                // 等待结束上次服务
                sleep(3000);
            }
            try
            {
                startupmodel = "Default";
                switch (Tagmodel)
                {
                    case "0":
                        {
                            console_append("\n");
                            message = "已启动运行...";
                            SaveAsFiles(sender, e);
                            console_append("Tag model : " + Tagmodel + " | Startup mode : " + startupmodel + " | Target address [" + (Tagmodel == "0" || Tagmodel == "as" ? SaveFileName : OpenFileName) + "]");
                            execute("python " + SaveFileName);
                        }
                        break;
                    case "as":
                        {
                            console_append("\n");
                            message = "已启动运行...";
                            SaveFiles(sender, e);
                            console_append("Tag model : " + Tagmodel + " | Startup mode : " + startupmodel + " | Target address [" + (Tagmodel == "0" || Tagmodel == "as" ? SaveFileName : OpenFileName) + "]");
                            execute("python " + SaveFileName);
                        }
                        break;
                    case "read":
                        {
                            console_append("\n");
                            message = "已启动运行...";
                            SaveFiles(sender, e);
                            console_append("Tag model : " + Tagmodel + " | Startup mode : " + startupmodel + " | Target address [" + (Tagmodel == "0" || Tagmodel == "as" ? SaveFileName : OpenFileName) + "]");
                            execute("python " + OpenFileName);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                labLeBoMsg.Text = ex.Message;
                throw;
            }
        }


        /// <summary>
        /// 执行 cmd 指令
        /// </summary>
        private void execute(string command)
        {
            cmd.shell = command;
        }

        /// <summary>
        /// 关闭前做一些事情，比如释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WdwPyCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timers.Close();
            execute("taskkill /f /t /im PyCode.exe");
        }

        /// <summary>
        /// 发送命令到 cmd 线程中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sendtocmd_Click(object sender, EventArgs e)
        {
            execute(commandstr.Text);
            commandstr.Clear();
        }

        private void Qk_Click(object sender, EventArgs e)
        {
            codebox.Clear();
        }

        private void 优先显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = TopMost ? false : true;
            优先显示ToolStripMenuItem.Checked = TopMost ? true : false;
        }

        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            // 结束 python 进程，防止 web 开发中无法更新服务器
            // 需要判断上下文是否存在非终结符号 "# webdev"
            if (codebox.Text.Contains("# webdev"))
            {
                execute("taskkill /f /t /im python.exe");
                // 等待结束上次服务
                sleep(3000);
            }

            // 获取编译环境
            string CompilationEnvironment = PyCode.CommonSymbolSet.CompilationEnvironmentPreferences == 0 ? PyCode.CommonSymbolSet.PythonLocationTargetPath
                : PyCode.CommonSymbolSet.CompilationEnvironmentPreferences == 1 ? PyCode.CommonSymbolSet.PythonPyCodeTargetPath
                : "Please check the compilation preferences or re-designate the compilation environment";
            CompilationEnvironment = Application.StartupPath + CompilationEnvironment;

            startupmodel = "开发者命令提示";
            try
            {
                MessageBox.Show(SaveFileName);
                if (PyCode.CommonSymbolSet.CompilationEnvironmentPreferences == 0)
                {
                    // 本地编译环境
                    switch (Tagmodel)
                    {
                        case "0":
                            {
                                console_append("\n");
                                message = "已启动运行...";
                                SaveAsFiles(sender, e);
                                console_append("Tag model : " + Tagmodel
                                    + " | Startup mode : " + startupmodel
                                    + " | Target address [" + SaveFileName + "]"
                                );

                                File.WriteAllText(Application.StartupPath.ToString()
                                    + "\\Temp\\Developer-command-prompt.cmd"
                                    , SaveFileName.Substring(0, 2)
                                        + "\ncd "
                                        + Path.GetDirectoryName(SaveFileName)
                                        + "\npython "
                                        + SaveFileName
                                        + "\npause"
                                    , Encoding.ASCII
                                );
                                Process.Start(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd");
                            }
                            break;
                        case "as":
                            {
                                console_append("\n");
                                message = "已启动运行...";
                                SaveFiles(sender, e);
                                console_append("Tag model : " + Tagmodel +
                                    " | Startup mode : " + startupmodel +
                                    " | Target address [" + SaveFileName + "]"
                                );

                                File.WriteAllText(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd"
                                    , SaveFileName.Substring(0, 2) + "\ncd "
                                        + Path.GetDirectoryName(SaveFileName)
                                        + "\npython "
                                        + SaveFileName
                                    + "\npause"
                                    , Encoding.ASCII
                                );
                                Process.Start(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd");
                            }
                            break;
                        case "read":
                            {
                                console_append("\n");
                                message = "已启动运行...";
                                SaveFiles(sender, e);
                                console_append("Tag model : " + Tagmodel
                                    + " | Startup mode : " + startupmodel
                                    + " | Target address [" + OpenFileName + "]"
                                );

                                File.WriteAllText(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd", OpenFileName.Substring(0, 2) + "\ncd " + Path.GetDirectoryName(OpenFileName) + "\npython " + OpenFileName + "\npause", Encoding.ASCII);
                                Process.Start(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd");
                            }
                            break;
                    }
                }
                else if (PyCode.CommonSymbolSet.CompilationEnvironmentPreferences == 1)
                {
                    // PyCode 编译环境
                    switch (Tagmodel)
                    {
                        case "0":
                            {
                                console_append("\n");
                                message = "已启动运行...";
                                SaveAsFiles(sender, e);

                                console_append("Tag model : " + Tagmodel
                                    + " | Startup mode : " + startupmodel
                                    + " | Target address [" + SaveFileName + "]"
                                );

                                File.WriteAllText(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd"
                                    , CompilationEnvironment.Substring(0, 2) + "\ncd "
                                        + Path.GetDirectoryName(CompilationEnvironment)
                                        + "\npy37.exe " + SaveFileName
                                        + "\npause"
                                    , Encoding.ASCII
                                );

                                Process.Start(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd");
                            }
                            break;
                        case "as":
                            {
                                console_append("\n");
                                message = "已启动运行...";
                                SaveFiles(sender, e);

                                console_append("Tag model : " + Tagmodel
                                    + " | Startup mode : " + startupmodel
                                    + " | Target address [" + SaveFileName + "]"
                                );

                                File.WriteAllText(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd"
                                    , CompilationEnvironment.Substring(0, 2) + "\ncd "
                                        + Path.GetDirectoryName(CompilationEnvironment)
                                        + "\npy37.exe " + SaveFileName
                                        + "\npause"
                                    , Encoding.ASCII
                                );

                                Process.Start(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd");
                            }
                            break;
                        case "read":
                            {
                                console_append("\n");
                                message = "已启动运行...";
                                SaveFiles(sender, e);

                                console_append("Tag model : " + Tagmodel
                                    + " | Startup mode : " + startupmodel
                                    + " | Target address [" + OpenFileName + "]"
                                );

                                File.WriteAllText(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd"
                                    , CompilationEnvironment.Substring(0, 2) + "\ncd "
                                        + Path.GetDirectoryName(CompilationEnvironment)
                                        + "\npy37.exe " + OpenFileName
                                        + "\npause"
                                    , Encoding.ASCII
                                );

                                Process.Start(Application.StartupPath.ToString() + "\\Temp\\Developer-command-prompt.cmd");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                labLeBoMsg.Text = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// 改变状态消息前景颜色
        /// </summary>
        /// <param name="color"></param>
        public void changeColor(System.Drawing.Color color)
        {
            labLeBoMsg.ForeColor = color;
        }

        private void Qkkzt_Click(object sender, EventArgs e)
        {
            console.Clear();
        }

        private void Cztagmodel_Click(object sender, EventArgs e)
        {
            Tagmodel = "0";
            SaveFileName = string.Empty;
            OpenFileName = string.Empty;
        }

        private void LabLeBoMsg_TextChanged(object sender, EventArgs e)
        {
            sleep(5001);
            changeColor(System.Drawing.Color.White);
            message = "就绪";
        }

        private void 不保留文件运行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 结束 python 进程，防止 web 开发中无法更新服务器
            // 需要判断上下文是否存在非终结符号 "# webdev"
            if (codebox.Text.Contains("# webdev"))
            {
                execute("taskkill /f /t /im python.exe");
                // 等待结束上次服务
                sleep(3000);
            }
            startupmodel = "缓存";
            console_append("\n");
            message = "已启动缓存运行...";
            console_append("Tag model : " + Tagmodel + " | Startup mode : " + startupmodel + " | Target address [" + (Tagmodel == "0" || Tagmodel == "as" ? SaveFileName : OpenFileName) + "]");
            writeFile(Application.StartupPath.ToString() + "\\Cache\\TemporarilyReserved.py.cache", codebox.Text);
            execute((PyCode.CommonSymbolSet.CompilationEnvironmentPreferences == 0 ? PyCode.CommonSymbolSet.PythonLocationTargetPath
                : PyCode.CommonSymbolSet.CompilationEnvironmentPreferences == 1 ? Application.StartupPath + PyCode.CommonSymbolSet.PythonPyCodeTargetPath
                : "Please check compilation preferences") + " " + Application.StartupPath.ToString() + "\\Cache\\TemporarilyReserved.py.cache");
        }

        private void 开发者命令提示CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe");
        }

        private void 开发者PowerShellPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("powershell");
        }

        private void 创建GUIDCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new wdwGuid()
            {
                TopMost = true,
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterScreen
            }.Show();
        }

        private void wdwPyCode_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void 生成所有依赖文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (Tagmodel)
            {
                case "0":
                    {
                        // Save File Name
                        SaveAsFiles(sender, e);
                        execute("pipreqs ./");
                    }
                    break;
                case "as":
                    {
                        // Save File Name
                        execute("pipreqs ./");
                    }
                    break;
                case "read":
                    {
                        // Open File Name
                        execute("pipreqs ./");
                    }
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            addPage();
        }

        private void 使用PyinstallerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void options_Click(object sender, EventArgs e)
        {
            new wOptions().Show();
        }

        private void eleCodeBox_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void commandstr_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ("close".Equals(commandstr.Text))
                    execute("taskkill /f /t /im PyCode.exe");
                Sendtocmd_Click(sender, e);
            }
        }

        private void qkkzt_Click_1(object sender, EventArgs e)
        {
            console.Clear();
        }
    }
}
