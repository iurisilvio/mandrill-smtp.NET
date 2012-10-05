using System;
using System.Collections.Specialized;

namespace mandrill.smtp.helpers
{
    public enum ETrack
    {
        opens,
        clicks_all,
        clicks,
        clicks_htmlonly,
        clicks_textonly
    }

    public class MandrillHeader
    {
        private NameValueCollection _headers;

        public MandrillHeader(NameValueCollection headers)
        {
            _headers = headers;
        }

        /// <summary>
        /// Add tags to the outgoing message.
        /// Mandrill limits: 50 characters per tag and 100 tags per account.
        /// </summary>
        private MandrillHeaderData<string> _tags;
        public MandrillHeaderData<string> Tags
        {
            get {
                return _tags ?? (_tags = new MandrillHeaderData<string>("X-MC-Tags", _headers));
            }
        }

        /// <summary>
        /// Enable open or click-tracking for the message.
        /// </summary>
        private MandrillHeaderData<ETrack> _tracks;
        public MandrillHeaderData<ETrack> Tracks
        {
            get {
                return _tracks ?? (_tracks = new MandrillHeaderData<ETrack>("X-MC-Track", _headers));
            }
        }

        /// <summary>
        /// Automatically generate a plain-text version of the email from the HTML content.
        /// </summary>
        public bool? Autotext
        {
            get { return _getBool("X-MC-Autotext"); }
            set { _setBool("X-MC-Autotext", value); }
        }

        /// <summary>
        /// Use an HTML template stored in your Mandrill account.
        /// Format: template_name|block_name
        ///     template_name is the name of the stored template.
        ///     block_name is the name of the mc:edit region where the body of the SMTP generated message will be placed. Optional and defaults to "main".
        /// </summary>
        public string Template
        {
            get { return _headers["X-MC-Template"]; }
            set { _headers["X-MC-Template"] = value; }
        }

        /// <summary>
        /// Add dynamic data to replace mergetags that appear in your message content.
        /// A JSON-formatted object with name/value pairs for the variable name and value, separated by commas.
        /// </summary>
        private MergeVars _mergeVars;
        public MergeVars MergeVars
        {
            get { return _mergeVars ?? (_mergeVars = new MergeVars("X-MC-MergeVars", _headers)); }
        }

        /// <summary>
        /// Add Google Analytics tracking to links in your email for the specified domains.
        /// </summary>
        private MandrillHeaderData<string> _googleAnalytics;
        public MandrillHeaderData<string> GoogleAnalytics
        {
            get { return _googleAnalytics ?? (_googleAnalytics = new MandrillHeaderData<string>("X-MC-GoogleAnalytics", _headers)); }
        }

        /// <summary>
        /// Add an optional value to be used for the utm_campaign parameter in Google Analytics tracked links.
        /// </summary>
        public string GoogleAnalyticsCampaign
        {
            get { return _headers["X-MC-GoogleAnalyticsCampaign"]; }
            set { _headers["X-MC-GoogleAnalyticsCampaign"] = value; }
        }

        /// <summary>
        /// Information about any custom fields or data you want to append to the message.
        /// </summary>
        public string Metadata
        {
            get { return _headers["X-MC-Metadata"]; }
            set { _headers["X-MC-Metadata"] = value; }
        }

        /// <summary>
        /// Whether to strip querystrings from links for reporting.
        /// </summary>
        public bool? URLStripQueryString
        {
            get { return _getBool("X-MC-URLStripQS"); }
            set { _setBool("X-MC-URLStripQS", value); }
        }

        /// <summary>
        /// Whether to show recipients of the email other recipients, such as those in the "cc" field.
        /// </summary>
        public bool? PreserveRecipients
        {
            get { return _getBool("X-MC-PreserveRecipients"); }
            set { _setBool("X-MC-PreserveRecipients", value); }
        }

        private bool? _getBool(string header)
        {
            var v = _headers[header];
            return v != null ? bool.Parse(v) : new Nullable<bool>();
        }

        private void _setBool(string header, bool? value) {
            _headers[header] = value.ToString().ToLower();
        }
    }
}
