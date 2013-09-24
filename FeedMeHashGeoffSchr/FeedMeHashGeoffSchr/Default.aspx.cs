using System;
using FeedMeHashGeoffSchr.Classes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FeedMeHashGeoffSchr
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtHashTag.Focus();
        }

        protected void btnGetTweets_Click(object sender, EventArgs e)
        {
            /* First get the tweets via API */
            string twitResults = GetSearchTweets();
            SearchTweetsRslt rslt = ExtensionJson.JsonHelper.JsonDeserializer<SearchTweetsRslt>(twitResults);
            /* Deserialize if the text returned isn't some sort of error. */

            /* If the array has elements, display them by threes into the divs. This requires translating
             * the returned data structure into HTML, and replacing the inner HTML of the appropriate divs
             * as necessary. */
            /* TODO: Sort the returned value array by the value of the drop down list. */

            lblDebug.Text = rslt.ToString();
        }
        private string GetSearchTweets()
        {
            string query;
            string resource_url;
            /* Tried to add a count argument but it just blows up in my face - don't understand why
            string count_str = "count=3";
             * *********/

            query = txtHashTag.Text;
            resource_url = "https://api.twitter.com/1.1/search/tweets.json";

            try
            {
                //UserAccount sua = (UserAccount)Session["SessionUser"];
                // oauth application keys
                var oauth_token = "1891947146-rOK6QRZPsD1QfSb5kQ7PeWqyZOiPTP4kuBKwYuR";
                var oauth_token_secret = "LkKdTHEBdd8TRi4N74GmmqDVdflMLPTk1MNrluw3O0";
                var oauth_consumer_key = "pqB5UnBtXMXkGsO4pWULw";
                var oauth_consumer_secret = "L3zNnNphZFWXut5bMmUj3rwrUihgtOoBMmL7LEoyo";

                // oauth implementation details
                var oauth_version = "1.0";
                var oauth_signature_method = "HMAC-SHA1";

                // unique request details
                var oauth_nonce = Convert.ToBase64String(
                                            new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
                var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();


                // create oauth signature
                var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                                "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}";

                var baseString = string.Format(baseFormat,
                                                oauth_consumer_key,
                                                oauth_nonce,
                                                oauth_signature_method,
                                                oauth_timestamp,
                                                oauth_token,
                                                oauth_version,
                                                Uri.EscapeDataString(query)
                                            );

                baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", 
                                            Uri.EscapeDataString(baseString));

                var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                                "&", Uri.EscapeDataString(oauth_token_secret));

                string oauth_signature;
                using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
                {
                    oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
                }

                // create the request header
                var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                                   "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                                   "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                                   "oauth_version=\"{6}\"";

                var authHeader = string.Format(headerFormat,
                                        Uri.EscapeDataString(oauth_nonce),
                                        Uri.EscapeDataString(oauth_signature_method),
                                        Uri.EscapeDataString(oauth_timestamp),
                                        Uri.EscapeDataString(oauth_consumer_key),
                                        Uri.EscapeDataString(oauth_token),
                                        Uri.EscapeDataString(oauth_signature),
                                        Uri.EscapeDataString(oauth_version)
                                );

                // For some reason, they require this.
                ServicePointManager.Expect100Continue = false;

                // make the request
                var postBody = "q=" + Uri.EscapeDataString(query);//
                resource_url += "?" + postBody;
                // Didn't run: resource_url += "&" + Uri.EscapeDataString(count_str);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
                request.Headers.Add("Authorization", authHeader);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    var resp = (HttpWebResponse)request.GetResponse();
                    var rdr = new StreamReader(resp.GetResponseStream());
                    var objText = rdr.ReadToEnd();
                    return objText;
                }
                catch (Exception e2)
                {
                    return "error performing GET." + Environment.NewLine
                        + e2.Message;
                }

            }
            catch (Exception e1)
            {
                return "Error - check your internet connection." + Environment.NewLine
                    + e1.Message;
            }
        }
    }
}