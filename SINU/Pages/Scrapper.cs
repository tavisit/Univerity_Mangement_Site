using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI.WebControls;

namespace SINU.Pages
{
    public class ScrappedInfo
    {
        public string link { get; set; }
        public string image_url { get; set; }
        public string title { get; set; }

        public ScrappedInfo(string link, string image_url, string title)
        {
            this.link = link;
            this.image_url = image_url;
            this.title = title;
        }
    }

    public class Scrapper
    {

        public static List<ScrappedInfo> NewsScrapper(int howMany)
        {
            string url = "https://www.utcluj.ro/noutati/";

            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url);

            var html_document = new HtmlDocument();

            html_document.LoadHtml(html.Result);

            List<HtmlNode> newsOutlet = html_document.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("row m-b10")).ToList();
            List<ScrappedInfo> scrappedInfo = new List<ScrappedInfo>();
            int currentNumberOfNews = 1;
            foreach (HtmlNode item in newsOutlet)
            {

                List<HtmlNode> currentItems = item.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("col-md-4")).ToList();
                string myText = currentItems[0].InnerHtml;
                myText = myText.Replace("\n", "");
                int index = myText.IndexOf('"') + 1;
                myText = myText.Substring(index);
                string item_url = myText.Substring(0, myText.IndexOf('"'));
                for (int i = 0; i < 4; i++)
                {
                    index = myText.IndexOf('"') + 1;
                    myText = myText.Substring(index);
                }
                string item_img_url = myText.Substring(0, myText.IndexOf('"'));
                for (int i = 0; i < 4; i++)
                {
                    index = myText.IndexOf('"') + 1;
                    myText = myText.Substring(index);
                }
                string item_text_temp = myText.Substring(0, myText.IndexOf('"'));
                string item_text = char.ToUpper(item_text_temp[0]) + item_text_temp.Substring(1);

                ScrappedInfo currentItem = new ScrappedInfo(item_url, item_img_url, item_text);
                scrappedInfo.Add(currentItem);

                if(currentNumberOfNews>=howMany)
                {
                    break;
                }
                else
                {
                    currentNumberOfNews++;
                }
            }
            return scrappedInfo;
        }
        public static List<string> AnnouncementScrapper(int howMany)
        {
            string url = "https://ac.utcluj.ro/anunturi.html";

            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url);

            var html_document = new HtmlDocument();

            html_document.LoadHtml(html.Result);

            List<HtmlNode> annOutlet = html_document.DocumentNode.Descendants("p")
                .Where(node => node.GetAttributeValue("class", "").Equals("info")).ToList();
            List<string> scrappedInfo = new List<string>();
            int currentNumberOfNews = 1;
            foreach (HtmlNode item in annOutlet)
            {
                string addingInformation = item.InnerHtml.Substring(0, item.InnerHtml.IndexOf("<a") + 2) +
                                           " target=\"_blank\" href=\"https://ac.utcluj.ro/" +
                                           item.InnerHtml.Substring(item.InnerHtml.IndexOf("<a") + 9);
                scrappedInfo.Add(addingInformation);
                if (currentNumberOfNews >= howMany)
                {
                    break;
                }
                else
                {
                    currentNumberOfNews++;
                }
            }
            return scrappedInfo;
        }
    }

}