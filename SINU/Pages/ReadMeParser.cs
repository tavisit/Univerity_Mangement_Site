using Markdig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SINU.Pages
{
    public class ReadMeParser
    {
        public static string Parse(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();
            return Markdown.ToHtml(markdown, pipeline);
        }
        public static String AboutUsFromReadMe()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory;
            location = location.Replace("SINU\\", "README.md");
            string text = File.ReadAllText(location);
            text = Parse(text);
            text = text.Replace("<a", "<a target=\"_blank\"");
            string tempString = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
            tempString += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\"><br><br>";
            tempString += text;
            tempString += "<br><br></div>";
            tempString += "</div>";
            return tempString;
        }

    }
}