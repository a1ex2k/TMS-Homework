using DomainInfo;

var directoryPath = args.Length == 1 ? args[0] : null;

var linksLoader = new CountryDomainPagesWikipedia();
var domainsInfo = await linksLoader.ExtractDomainInfoAsync();

var domainsString = string.Join("\n", domainsInfo.Select(d => $"{d.Name}\n{d.Description}\n{d.Url}\n"));
await File.WriteAllTextAsync(Path.Combine(directoryPath ?? string.Empty, "domainInfo.txt"), domainsString);

var downloader = new HtmlDownloader(domainsInfo, directoryPath);
await downloader.DownloadAsync();

Console.ReadLine();