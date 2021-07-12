using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Engine
{
    public class SearchEngine : ISearchEngine
    {
        private readonly ILogger<SearchEngine> _log;
        private readonly DataContext _context;

        public SearchEngine(ILogger<SearchEngine> logger,DataContext context)
        {
            _log = logger;
            _context = context;
        }

        public async Task search()
        {
            try
            {
                _log.LogInformation("buscando.123..");

                Site s = new Site();
                s.Id = Guid.NewGuid();
                s.Url = "msn.com";

                _context.WebSites.Add(s);
                var success = await _context.SaveChangesAsync() > 0;

                _log.LogInformation("grabado",success);


                 var lista = await _context.WebSites.ToListAsync();
                 _log.LogInformation("lista",lista);


                /*

                string url = @"https://www.google.com/search?q=java&source=hp";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var pageContents = await response.Content.ReadAsStringAsync();
                _log.LogInformation("pagina leida");
                //_log.LogInformation(pageContents);
                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);


                var doc = new HtmlDocument();
		        doc.LoadHtml(pageContents);
		        var div = doc.GetElementbyId("result-stats"); 

               // var headlineText = pageDocument.DocumentNode.SelectSingleNode(x).InnerText;
                var node = pageDocument.DocumentNode.Descendants("div");//.Single(x => x.Id == "result-stats");
                var texto = node.Single(x => x.OuterHtml == "cerca");
                
                _log.LogInformation("cabecera {cab}");
                */
                _log.LogInformation("fin");
            }
            catch (Exception e)
            {
                _log.LogError(e.ToString());
            }

        }
    }
}