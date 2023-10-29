using HtmlAgilityPack;

namespace DomainInfo;

internal class CountryDomainPagesWikipedia
{
    private const string BaseUrl = "https://en.wikipedia.org";
    private const string DomainsListingPage = "/wiki/Country_code_top-level_domain";

    private const string ArticleNodeClass = "mw-parser-output";

    public CountryDomainPagesWikipedia() { }




    public async Task<List<DomainInfo>> ExtractDomainInfoAsync()
    {
        var document = await LoadDocumentAsync();
        var domains = new List<DomainInfo>();

        var articleNode = document.DocumentNode.SelectNodes($"//div[@class='{ArticleNodeClass}']").FirstOrDefault();
        var domainsTable = articleNode?.SelectSingleNode("table");
        var rows = domainsTable?.SelectNodes(".//tr[position() > 1]");

        if (rows is null)
        {
            return domains;
        }

        foreach (var row in rows)
        {
            var cells = row.SelectNodes("td");
            if (cells is null || cells.Count < 2)
            {
                continue;
            }

            var domainAElement = cells[0].SelectSingleNode(".//a");
            var countryAElement = cells[1].SelectSingleNode(".//a");
            if (domainAElement is null || countryAElement is null)
            {
                continue;
            }

            var domainInfo = new DomainInfo
            {
                Name = domainAElement.GetAttributeValue("title", string.Empty),
                Url = BaseUrl + domainAElement.GetAttributeValue( "href", string.Empty),
                Description = $"Domain name of {countryAElement.GetAttributeValue("title", "???")}"
            };

            domains.Add(domainInfo);    
        }

        return domains;
    }


    private async Task<HtmlDocument> LoadDocumentAsync()
    {
        using var client = new HttpClient();
        var responseStream = await client.GetStreamAsync(BaseUrl + DomainsListingPage);
        var document = new HtmlDocument();
        document.Load(responseStream);
        return document;
    }

}