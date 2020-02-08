using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyCode
{
    class CommonSymbolSet
    {
        // PyCode python compiler environment
        private static string PRIPythonPyCodeTargetPath = null;
        public static string PythonPyCodeTargetPath { get => PRIPythonPyCodeTargetPath; set => PRIPythonPyCodeTargetPath = value; }

        // Location python compiler environment
        private static string PRIPythonLocationTargetPath = null;
        public static string PythonLocationTargetPath { get => PRIPythonLocationTargetPath; set => PRIPythonLocationTargetPath = value; }

        // Compilation environment preferences
        private static int PRICompilationEnvironmentPreferences;
        public static int CompilationEnvironmentPreferences { get => PRICompilationEnvironmentPreferences; set => PRICompilationEnvironmentPreferences = value; }
    }
}
