using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using mandrill.smtp.helpers;
using NUnit.Framework;

namespace mandrill.smtp.tests
{
    [TestFixture]
    public class HeaderDataTest
    {
        [Test]
        public void AddElementTest()
        {
            var n = new NameValueCollection();
            MandrillHeaderData<string> l = new MandrillHeaderData<string>("a", n);
            l.Add("bb");
            Assert.AreEqual("bb", n["a"]);
        }

        [Test]
        public void RepeatHeaderTest()
        {
            var n = new NameValueCollection();
            MergeVars l = new MergeVars("a", n);

            dynamic o1 = new ExpandoObject();
            o1.bb = 1;
            o1.cc = 2;
            l.Add(o1);

            dynamic o2 = new ExpandoObject();
            o2.bb = 4;
            o2.cc = 5;
            o2._rcpt = "meh@example.com";
            l.Add(o2);

            var v = n.GetValues("a");
            Assert.AreEqual(2, v.Count());
        }

        [Test]
        public void ReplaceHeaderTest()
        {
            var n = new NameValueCollection();
            MandrillHeaderData<string> l = new MandrillHeaderData<string>("a", n);
            l.Add("bb");
            l.Add("cc");
            var v = n.GetValues("a");
            Assert.AreEqual(1, v.Count());
            Assert.AreEqual("bb,cc", v.First());
        }
    }
}
