using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DesktopShortcuts
{
   public class DesktopState
    {
        Data m_data = Data.GetInstance();

        public void Record()
        {
            if (m_data.HasSnapshot)
                throw new InvalidOperationException("Snapshot already exists.");

            m_data.CurrentSnapshot = GetDesktopShortcuts();
            m_data.Save();
        }

        public void Restore()
        {
            if (!m_data.HasSnapshot)
                throw new InvalidOperationException("No snapshot to restore.");

            var newShortcuts = GetDesktopShortcuts().Except(m_data.CurrentSnapshot);

            foreach (var shortcut in newShortcuts)
                File.Delete(shortcut);

            Discard();
        }

        public void Discard()
        {
            if (m_data.HasSnapshot)
            {
                m_data.HasSnapshot = false;
                m_data.Save();
            }
        }

        string[] GetDesktopShortcuts()
        {
            var folders     = new[] { Environment.SpecialFolder.CommonDesktopDirectory, Environment.SpecialFolder.DesktopDirectory };
            var directories = folders.Select(x => Environment.GetFolderPath(x));

            return directories.SelectMany(x => Directory.EnumerateFiles(x, "*.lnk")).ToArray();
        }

        public override string ToString()
        {
            if (!m_data.HasSnapshot)
                return "No Snapshot exists.";

            var result = new StringBuilder("Shortcuts stored:");
            foreach (var shortcut in m_data.CurrentSnapshot)
                result.AppendLine("    " + shortcut);

            var newShortcuts = GetDesktopShortcuts().Except(m_data.CurrentSnapshot);

            result.AppendLine("Shortcuts which restore option will delete:");
            foreach (var shortcut in newShortcuts)
                result.AppendLine("    " + shortcut);

            return result.ToString();
        }
    }
}