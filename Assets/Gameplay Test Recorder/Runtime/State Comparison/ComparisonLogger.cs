using System;
using UnityEngine;
using System.IO;
using System.Text;

namespace TwoGuyGames.GTR.Core
{
    internal class ComparisonLogger
    {
        private int indent;
        private StringBuilder log;

        public ComparisonLogger()
        {
            log = new StringBuilder();
        }

        public void IndentDown()
        {
            indent = Math.Max(0, indent - 1);
        }

        public void IndentUp()
        {
            indent++;
        }

        public void Log(string s)
        {
            for (int i = 0; i < indent; i++)
            {
                log.Append('\t');
            }
            log.Append(s);
            log.Append('\n');
        }

        public void SaveTo(string path)
        {
            try
            {
                path = Path.GetFullPath(path);
                Debug.Log($"Logging to <b>`{path}`</b>");
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                File.WriteAllText(path, ToString());
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public override string ToString()
        {
            return log.ToString();
        }
    }
}