using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Search;
using MetroFramework.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using PyCodeConfig = PyCode_Configuration;

namespace PyCode
{
    public partial class wOptions : MetroForm
    {
        public wOptions()
        {
            InitializeComponent();
        }

        #region variables
        private string PreferencesPath = Application.StartupPath + @"\Important\Configuration\Preferences.ini";
        private string LogPath = Application.StartupPath + @"\Temp\Log\log.ini";
        #endregion

        private void Options_Load(object sender, EventArgs e)
        {
            try
            {
                #region 载入语法高亮解决方案容器
                getPath();
                eleHighligh.Child = CodeHL;
                SearchPanel.Install(CodeHL.TextArea);
                string name = Assembly.GetExecutingAssembly().GetName().Name + ".Python.xshd";
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream s = assembly.GetManifestResourceStream(name))
                {
                    using (XmlTextReader reader = new XmlTextReader(s))
                    {
                        var xshd = HighlightingLoader.LoadXshd(reader);
                        CodeHL.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                    }
                }
                #endregion


                // tabcontrol 被选择下标为 0
                mtab.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                PyCodeConfig.Config.writeConfiguration(LogPath, DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString(), ex.Message.ToString());
            }
        }

        /// <summary>
        /// 语法高亮解决方案设计与布局和载入
        /// </summary>
        private readonly TextEditor CodeHL = new TextEditor
        {
            HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Hidden,
            Foreground = new SolidColorBrush(Color.FromRgb(170, 153, 187)),
            Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            Padding = new System.Windows.Thickness(8),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            Text = new StreamReader(Application.StartupPath + @"\Important\Xml\Python.xshd").ReadToEnd()
        };

        private void getPath()
        {
            try
            {
                #region Select compilation environment preferences
                PyCode.CommonSymbolSet.CompilationEnvironmentPreferences = int.Parse(PyCodeConfig.Config.readConfiguration(PreferencesPath, "compilation-preferences", "select"));
                mtcbox.SelectedIndex = PyCode.CommonSymbolSet.CompilationEnvironmentPreferences;
                #endregion

                #region PyCode Python Compiler Environment
                PyCode.CommonSymbolSet.PythonPyCodeTargetPath = PyCodeConfig.Config.readConfiguration(PreferencesPath, "path", "python-use-pycode");
                txtPyCodePath.Text = PyCode.CommonSymbolSet.PythonPyCodeTargetPath;
                #endregion

                #region Location Python Compiler Environment
                PyCode.CommonSymbolSet.PythonLocationTargetPath = PyCodeConfig.Config.readConfiguration(PreferencesPath, "path", "python-use-location");
                txtLocationPath.Text = PyCode.CommonSymbolSet.PythonLocationTargetPath;
                #endregion
            }
            catch (Exception ex)
            {
                PyCodeConfig.Config.writeConfiguration(LogPath, DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString(), ex.Message.ToString());
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                PyCodeConfig.Config.writeConfiguration(PreferencesPath, "path", "python-use-pycode", txtPyCodePath.Text);
                PyCodeConfig.Config.writeConfiguration(PreferencesPath, "path", "python-use-location", txtLocationPath.Text);
            }
            catch (Exception ex)
            {
                PyCodeConfig.Config.writeConfiguration(LogPath, DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString(), ex.Message.ToString());
            }
        }

        private void btnUpdateCodeHL_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, this feature is not available at this time", "PyCode", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mtcbox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void mtcbox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // 编译首选项保存到配置文件中
            PyCodeConfig.Config.writeConfiguration(PreferencesPath, "compilation-preferences", "select", mtcbox.SelectedIndex.ToString());
            PyCode.CommonSymbolSet.CompilationEnvironmentPreferences = int.Parse(PyCodeConfig.Config.readConfiguration(PreferencesPath, "compilation-preferences", "select"));
        }
    }
}
