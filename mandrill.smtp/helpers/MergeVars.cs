using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Serialization;

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
            var s = new JavaScriptSerializer();
            Collection.Add(Key, s.Serialize(item));
        }
    }
}
