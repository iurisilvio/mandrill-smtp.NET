using System.Collections.Specialized;

namespace mandrill.smtp.helpers
{
    public class MandrillHeaderData<T>
    {
        protected readonly NameValueCollection Collection;
        protected readonly string Key;

        public MandrillHeaderData(string key, NameValueCollection collection)
        {
            Collection = collection;
            Key = key;
        }

        public virtual void Add(T item)
        {
            var v = Collection[Key];
            if (v != null)
            {
                v += ",";
            }
            v += item.ToString();
            Collection[Key] = v;
        }
    }
}
