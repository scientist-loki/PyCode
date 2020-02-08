using System;
using System.Diagnostics;

namespace PyCode
{
    public class CmdUtils
    {
        public String shell = "";
        public void sendCmd(wdwPyCode cmdoom)
        {
            Process cmd = null;
            if (cmd == null)
            {
#pragma warning disable IDE0068     // 使用建议的 dispose 模式
                cmd = new Process();                //创建进程对象  
#pragma warning restore IDE0068     // 使用建议的 dispose 模式

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",           // 设定需要执行的命令  
                    Arguments = "",                 // “/C”表示执行完命令后马上退出  
                    UseShellExecute = false,        // 不使用系统外壳程序启动  
                    RedirectStandardInput = true,   // 不重定向输入  
                    RedirectStandardOutput = true,  // 重定向输出  
                    CreateNoWindow = true           // 不创建窗口  
                };

                cmd.StartInfo = startInfo;
            }

            if (cmd.Start())                        //开始进程  
            {
                cmd.StandardOutput.ReadLine().Trim();
                cmd.StandardOutput.ReadLine().Trim();

                while (cmdoom.isRun.IndexOf("start") != -1)
                {
                    if (shell.Length > 0)
                    {
                        cmd.StandardInput.WriteLine(shell);
                        cmd.StandardOutput.ReadLine().Trim();
                        cmd.StandardInput.WriteLine("\n");
                        String log = cmd.StandardOutput.ReadLine().Trim();
                        String path = log.Substring(0, 2).ToUpper();
                        updateLog(cmdoom, log);
                        log = "";

                        do
                        {
                            String logm = cmd.StandardOutput.ReadLine().Trim();

                            if (logm.IndexOf(path) != -1)
                            {
                                cmdoom.message = "完成";
                                break;
                            }

                            updateLog(cmdoom, logm + "");
                            log += logm;
                        } while (true);

                        shell = "";
                    }
                }
                cmd.Close();
                cmd = null;
                return;
            }
            return;
        }
        private delegate void UpdateLog();

        private void updateLog(wdwPyCode cmd, String log)
        {
            UpdateLog set = delegate ()
            {
                cmd.console_append("\n" + log);
                cmd.consoleScrollToEndImplement();
            };
            if (cmd != null) cmd.Invoke(set);
        }
    }
}
