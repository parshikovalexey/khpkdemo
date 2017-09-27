using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Bot_Application1.Dialogs {
    [Serializable]
    public class RootDialog : IDialog<object> {
        public Task StartAsync(IDialogContext context) {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result) {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            // await context.PostAsync($"Ты написал '{activity.Text}' длино {length} символа(ов)");

            var wikiText = GetDataFromWiki(activity.Text);
            //var solution = CalculateExpression(activity.Text);

            await context.PostAsync(wikiText);

            context.Wait(MessageReceivedAsync);
        }


        private string CalculateExpression(string expression) {
            var uriString = $"http://api.mathjs.org/v1/?expr={HttpUtility.UrlEncode(expression)}";
            return getDataToUrl(uriString);
        }


        private string getDataToUrl(string url) {
            try {
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                // Download home page data. 
                // Open a stream to point to the data stream coming from the Web resource.
                Stream myStream = myWebClient.OpenRead(url);

                StreamReader sr = new StreamReader(myStream);
                var result = sr.ReadToEnd();
                // Close the stream. 
                myStream.Close();
                return result;
            }catch (Exception e) {
                return string.Empty;
            }
        }

        private string PArseWikiResponse(string wikiResponse) {
            var result = "";
            var deser = new JavaScriptSerializer().Deserialize<WikiSearchResponse>(wikiResponse);
            foreach (var item in deser.query.search) {
                result += "\r\n" + item.snippet;
            }
            return result;
        }


        private string GetDataFromWiki(string action) {
            var uriString = $"https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch={action}&utf8=&format=json";
            var response = getDataToUrl(uriString);
            return PArseWikiResponse(response);
        }

    }
}