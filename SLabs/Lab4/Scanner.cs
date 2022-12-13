using System.Net;
using System.Text.RegularExpressions;

namespace SLabs.Lab4;

public class Scanner
{
    private readonly WebClient _webClient = new();
    private readonly HashSet<Uri> _oldLinks = new();

    public delegate void ReferenceFound(Reference reference);

    public event ReferenceFound? OnReferenceFound;

    private void Scan(Uri uri, int count)
    {
        if (count <= 0) return;
        
        if (_oldLinks.Contains(uri)) return;

        _oldLinks.Add(uri);

        var page = "";

        try
        {
            page = _webClient.DownloadString(uri);
        }
        catch (Exception e)
        {
            Console.WriteLine($"При загрузке {uri} произошла ошибка");
            return;
        }

        var allRefs = Regex.Matches(page, @"<a href=""([\w\d./-:\#]+)"".*>(.*)</a>");
        var localRefs = allRefs
            .Where(match => match.Value.Contains(uri.Host) || match.Groups[1].Value.StartsWith('/'));

        foreach (var match in allRefs.Where(match =>
                     !match.Value.Contains(uri.Host) && !match.Groups[1].Value.StartsWith('/')))
        {
            var reference = new Reference
            {
                Home = uri.ToString(),
                Name = match.Groups[2].Value,
                Ref = match.Groups[1].Value,
                Lvl = count
            };
            
            OnReferenceFound?.Invoke(reference);
        }

        var start = $"{uri.Scheme}://{uri.Authority}";
        foreach (var match in localRefs)
        {
            var link = match.Groups[1].Value;
            var nextPageUri = link.StartsWith('/') ? new Uri(start + link + '/') : new Uri(link + '/');
            Scan(nextPageUri, --count);
        }
    }

    public void Process(string url, int count)
    {
        _oldLinks.Clear();
        Scan(new Uri(url), count);
    }
}