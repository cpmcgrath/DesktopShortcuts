using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DesktopShortcuts
{
    public class Data
    {
        public bool HasSnapshot
        {
            get { return CurrentSnapshot != null; }
            set { CurrentSnapshot = null; }
        }

        public string[] CurrentSnapshot { get; set; }

        public void Save()
        {
            var file = new FileInfo(SavePath);
            file.Directory.Create();

            if (HasSnapshot)
                File.WriteAllLines(SavePath, CurrentSnapshot);
            else
                File.Delete(SavePath);
        }

        public static Data GetInstance()
        {
            var data = new Data();

            if (File.Exists(SavePath))
                data.CurrentSnapshot = File.ReadAllLines(SavePath);

            return data;
        }

        static string SavePath
        {
            get
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(appData, @"DesktopShortcuts\snapshot.dat");
            }
        }
    }
}