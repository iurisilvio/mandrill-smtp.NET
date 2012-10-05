using System.Collections.Specialized;
using mandrill.smtp.helpers;
using NUnit.Framework;

namespace mandrill.smtp.tests
{
    [TestFixture]
    public class SetHeaderTest
    {
        NameValueCollection collection;
        MandrillHeader mandrillHeader;

        [SetUp]
        public void SetUp()
        {
            collection = new NameValueCollection();
            mandrillHeader = new MandrillHeader(collection);
        }

        [Test]
        public void SetTagsTest()
        {
            mandrillHeader.Tags.Add("tag1");
            mandrillHeader.Tags.Add("tag2");
            Assert.AreEqual("tag1,tag2", collection["X-MC-Tags"]);
        }

        [Test]
        public void SetTrackTest()
        {
            mandrillHeader.Tracks.Add(ETrack.clicks);
            Assert.AreEqual("clicks", collection["X-MC-Track"]);
        }

        [Test]
        public void SetAutotextTest()
        {
            mandrillHeader.Autotext = false;
            Assert.AreEqual("false", collection["X-MC-Autotext"]);
        }

        [Test]
        public void SetTemplateTest()
        {
            mandrillHeader.Template = "some template";
            Assert.AreEqual("some template", collection["X-MC-Template"]);
        }

        [Test]
        public void SetGoogleAnalyticsTest()
        {
            mandrillHeader.GoogleAnalytics.Add("gae");
            Assert.Contains("gae", collection["X-MC-GoogleAnalytics"].Split(','));
        }

        [Test]
        public void SetGoogleAnalyticsCampaignTest()
        {
            mandrillHeader.GoogleAnalyticsCampaign = "campaign";
            Assert.AreEqual("campaign", collection["X-MC-GoogleAnalyticsCampaign"]);
        }

        [Test]
        public void SetMetadataTest()
        {
            mandrillHeader.Metadata = "meh";
            Assert.AreEqual("meh", collection["X-MC-Metadata"]);
        }

        [Test]
        public void SetUrlStripQSTest()
        {
            mandrillHeader.URLStripQueryString = true;
            Assert.AreEqual("true", collection["X-MC-URLStripQS"]);
        }

        [Test]
        public void SetPreserveRecipientsTest()
        {
            mandrillHeader.PreserveRecipients = true;
            Assert.AreEqual("true", collection["X-MC-PreserveRecipients"]);
        }
    }
}
