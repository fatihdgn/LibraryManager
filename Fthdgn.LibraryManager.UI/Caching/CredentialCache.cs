using Fthdgn.LibraryManager.UI.Models;
using Fthdgn.LibraryManager.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Caching
{
    public class CredentialCache : Cache<Credential>
    {
        public CredentialCache() { }

        public CredentialCache(Credential state) : base(state) { }
    }
}
