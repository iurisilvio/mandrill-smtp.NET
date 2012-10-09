using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;

namespace mandrill.smtp.helpers
{
    public class MergeVars : MandrillHeaderData<ExpandoObject>
    {
        public MergeVars(string key, NameValueCollection collection)
            : base(key, collection)
        {
        
        }

        public virtual void Add(ExpandoObject item)
        {
            var d = ((IDictionary<string, object>)item).ToDictionary(x => x.Key, x => x.Value.ToString());
            var values = string.Join(",", d.Select(x => "\"" + x.Key + "\": \"" + x.Value.Replace("\"", "\\\"") + "\""));
            Collection.Add(Key, "{" + values + "}");
        }
    }
}
