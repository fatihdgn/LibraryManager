using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Caching
{
    public class CredentialCacheFile : CredentialCache
    {
        private static readonly object @lock = new object();
        public string Path { get; set; }
        public CredentialCacheFile(string path)
        {
            Path = path;
            BeforeAccess = OnBeforeAccess;
            AfterAccess = OnAfterAccess;
            FileInfo fi = new FileInfo(Path);
            if (!fi.Directory.Exists)
                fi.Directory.Create();
            OnBeforeAccess();
        }

        public override void Clear()
        {
            base.Clear();
            File.Delete(Path);
        }

        public void OnBeforeAccess()
        {
            lock (@lock)
            {
                Deserialize(File.Exists(Path) ? File.ReadAllBytes(Path) : null);
            }
        }

        public void OnAfterAccess()
        {
            if (HasStateChanged)
            {
                lock (@lock)
                {
                    File.WriteAllBytes(Path, Serialize());
                    HasStateChanged = false;
                }
            }
        }
    }
}
