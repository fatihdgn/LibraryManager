using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.UI.Caching
{
    public class Cache<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Action BeforeAccess { get; set; }
        public Action AfterAccess { get; set; }

        T state;
        public T State
        {
            set
            {
                BeforeAccess?.Invoke();
                if (Set(ref state, value))
                    HasStateChanged = true;
                AfterAccess?.Invoke();
            }
        }
        bool hasStateChanged;
        public bool HasStateChanged
        {
            get => hasStateChanged;
            set => Set(ref hasStateChanged, value);
        }

        public Cache() { }
        public Cache(T state)
        {
            State = state;
        }

        public virtual void Clear()
        {
            State = default(T);
        }

        public void WithState(Action<T> act)
        {
            BeforeAccess?.Invoke();
            act?.Invoke(state);
            AfterAccess?.Invoke();
        }

        public byte[] Serialize() => state != null ? Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(state)) : null;
        public void Deserialize(byte[] obj) => state = obj != null ? JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(obj)) : default(T);

        protected bool Set<T>(ref T self, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Object.Equals(self, value))
            {
                self = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class Cache : Cache<object>
    {
        public Cache() { }
        public Cache(object state) : base(state) { }
    }
}
