using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Net.Http;

namespace SINU.Pages
{
    public class ScrappedInfo
    {
        public string link { get; set; }
        public string image_url { get; set; }
        public string title { get; set; }

        public ScrappedInfo(string link,string image_url,string title)
        {
            this.link = link;
            this.image_url = image_url;
            this.title = title;
        }
    }

    public class Scrapper
    {
        
        public static List<ScrappedInfo> MainScrapper()
        {
            string url = "https://www.utcluj.ro/noutati/";

            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url);

            var html_document = new HtmlDocument();

            html_document.LoadHtml(html.Result);

            List<HtmlNode> newsOutlet = html_document.DocumentNode.Descendants("div")
                .Where(node=>node.GetAttributeValue("class","").Equals("row m-b10")).ToList();
            List<ScrappedInfo> scrappedInfo = new List<ScrappedInfo>();
            foreach (HtmlNode item in newsOutlet)
            {
                
                List<HtmlNode> currentItems = item.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("col-md-4")).ToList();
                string myText = currentItems[0].InnerHtml;
                myText = myText.Replace("\n", "");
                int index = myText.IndexOf('"')+1;
                myText = myText.Substring(index);
                string item_url = myText.Substring(0, myText.IndexOf('"'));
                for(int i=0;i<4;i++)
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
            }
            return scrappedInfo;
        }
    }

}