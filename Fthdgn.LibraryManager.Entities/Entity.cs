using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class Entity
    {
        [DefaultValue("lower(convert(nvarchar(255),newid()))")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [DefaultValue("getutcdate()")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? ChangedAt { get; set; }
        public DateTimeOffset? RemovedAt { get; set; }
        [DefaultValue("1")]
        public bool IsEnabled { get; set; } = true;
        public DictionaryJsonEntity Properties { get; set; } = new DictionaryJsonEntity();
    }
}
