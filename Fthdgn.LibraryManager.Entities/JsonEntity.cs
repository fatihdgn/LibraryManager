using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Entities
{
    public class JsonEntity<T>
        where T : new()
    {
        public JsonEntity()
        {

        }
        public JsonEntity(T value)
        {
            Value = value;
        }
        [JsonIgnore]
        public string Serialized
        {
            get
            {
                try
                {
                    return JsonConvert.SerializeObject(Value);

                }
                catch
                {
                    return JsonConvert.SerializeObject(new T());
                }
            }
            set
            {
                try
                {
                    Value = value == null ? new T() : JsonConvert.DeserializeObject<T>(value);
                }
                catch
                {
                    Value = new T();
                }
            }
        }

        [NotMapped]
        public T Value { get; set; } = new T();

        public static implicit operator T(JsonEntity<T> x) => x.Value;
        public static implicit operator JsonEntity<T>(T x) => new JsonEntity<T>(x);
    }

    public class DictionaryJsonEntity<TKey, TValue> : JsonEntity<Dictionary<TKey, TValue>>
    {
        public DictionaryJsonEntity() { }
        public DictionaryJsonEntity(Dictionary<TKey, TValue> value) : base(value) { }

        public static implicit operator Dictionary<TKey, TValue>(DictionaryJsonEntity<TKey, TValue> x) => x.Value;
        public static implicit operator DictionaryJsonEntity<TKey, TValue>(Dictionary<TKey, TValue> x) => new DictionaryJsonEntity<TKey, TValue>(x);
    }

    public class DictionaryJsonEntity : DictionaryJsonEntity<string, object>
    {
        public DictionaryJsonEntity() { }
        public DictionaryJsonEntity(Dictionary<string, object> value) : base(value) { }

        public static implicit operator Dictionary<string, object>(DictionaryJsonEntity x) => x.Value;
        public static implicit operator DictionaryJsonEntity(Dictionary<string, object> x) => new DictionaryJsonEntity(x);
    }

    public class ListJsonEntity<T> : JsonEntity<List<T>>
    {
        public ListJsonEntity() { }
        public ListJsonEntity(List<T> value) : base(value) { }

        public static implicit operator List<T>(ListJsonEntity<T> x) => x.Value;
        public static implicit operator ListJsonEntity<T>(List<T> x) => new ListJsonEntity<T>(x);
    }

    public class ListJsonEntity : ListJsonEntity<object>
    {
        public ListJsonEntity() { }
        public ListJsonEntity(List<object> value) : base(value) { }

        public static implicit operator List<object>(ListJsonEntity x) => x.Value;
        public static implicit operator ListJsonEntity(List<object> x) => new ListJsonEntity(x);
    }

    public class StringListJsonEntity : ListJsonEntity<string>
    {
        public StringListJsonEntity() { }
        public StringListJsonEntity(List<string> value) : base(value) { }

        public static implicit operator List<string>(StringListJsonEntity x) => x.Value;
        public static implicit operator StringListJsonEntity(List<string> x) => new StringListJsonEntity(x);
    }
}
