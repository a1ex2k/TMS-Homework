using HtmlAgilityPack;
using System.Net;

namespace DomainInfo;

internal class HtmlDownloader
{
    private readonly List<ILink> _links;
    private readonly string _directoryPath;


    public HtmlDownloader(IEnumerable<ILink> links)
        : this(links, null)
    { }


    public HtmlDownloader(IEnumerable<ILink> links, string? outputDirectory)
    {
        ArgumentNullException.ThrowIfNull(links);

        _directoryPath = string.IsNullOrWhiteSpace(outputDirectory)
            ? Path.Combine(Directory.GetCurrentDirectory(), "Download")
            : _directoryPath;
        _links = links.ToList();
    }


    public async Task DownloadAsync()
    {
        using var client = new HttpClient();
        Directory.CreateDirectory(_directoryPath);

        var tasks = _links.Select(link => Task.Run(async () => { await DownloadLinkAsync(link, client); }));

        await Task.WhenAll(tasks);
    }


    private async Task DownloadLinkAsync(ILink link, HttpClient client)
    {
        var uri = new Uri(link.Url);
        var name = CreateFileName(link);
        using var memoryStream = new MemoryStream();
        var succeed = await SaveToStreamAsync(client, uri, memoryStream);

        if (!succeed)
        {
            Console.WriteLine($"Cannot download {link.Url}");
            return;
        }

        memoryStream.Position = 0;
        Directory.CreateDirectory(_directoryPath);
        var document = new HtmlDocument();
        document.Load(memoryStream);
        await DownloadResourcesAsync(client, uri, document, _directoryPath);
        Console.WriteLine($"{link.Url} downloaded");

        await using var fileStream = new FileStream($"{_directoryPath}\\{name}.html", FileMode.Create);
        document.Save(fileStream);
    }


    private async Task<bool> DownloadAsync(HttpClient client, Uri uri, string filePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        await using var stream = new FileStream(filePath, FileMode.Create);
        Console.WriteLine(uri.AbsoluteUri);
        return await SaveToStreamAsync(client, uri, stream);
    }


    private async Task<bool> SaveToStreamAsync(HttpClient client, Uri uri, Stream stream)
    {
        HttpResponseMessage? response;
        try
        {
            response = await client.GetAsync(uri);
        }
        catch
        {
            return false;
        }


        if (response.StatusCode != HttpStatusCode.OK)
        {
            return false;
        }

        await response.Content.CopyToAsync(stream);
        return true;
    }



    private string CreateFileName(ILink link)
    {
        var normalizedFileName = string.Create(link.Name.Length, link.Name, (span, original) =>
        {
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = Array.IndexOf(Path.GetInvalidFileNameChars(), original[i]) >= 0 ? '_' : original[i];
            }
        });
        return normalizedFileName;
    }


    private async Task DownloadResourcesAsync(HttpClient client, Uri baseUri, HtmlDocument htmlDocument, string baseDirectory)
    {
        var resourceNodes = htmlDocument.DocumentNode.SelectNodes("//*[@src or @href]");

        if (resourceNodes is null)
        {
            return;
        }

        foreach (var resourceElement in resourceNodes)
        {
            await ReplaceResourceAsync(client, resourceElement, baseUri);
        }
    }


    private async Task ReplaceResourceAsync(HttpClient client, HtmlNode element, Uri baseUri)
    {
        var attribute = element.Attributes.FirstOrDefault<HtmlAttribute>(a => a.Name is "src" or "href");

        if (!Uri.TryCreate(baseUri, attribute!.Value, out var fullUri))
        {
            return;
        }

        element.SetAttributeValue(attribute.Name, fullUri.ToString());
    }


}