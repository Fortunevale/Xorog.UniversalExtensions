namespace Xorog.UniversalExtensions;

public static class WebTools
{
    /// <summary>
    /// Get the URL a redirect leads to (limited to StatusCodes 301, 303, 307, 308)
    /// </summary>
    /// <param name="url">The shortened URL</param>
    /// <returns>The URL the redirect leads to</returns>
    public static async Task<string> UnshortenUrl(string url, bool UseHeadMethod = true)
    {
        _logger?.LogDebug("Unshortening Url '{Url}', using head method: {UseHeadMethod}", url, UseHeadMethod);

        HttpClient client = new(new HttpClientHandler()
        {
            AllowAutoRedirect = false,
            AutomaticDecompression = DecompressionMethods.GZip,

        });
        client.Timeout = TimeSpan.FromSeconds(60);
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36");
        client.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
        client.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate, br");
        client.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9");
        client.MaxResponseContentBufferSize = 4096;

        HttpRequestMessage requestMessage = new HttpRequestMessage((UseHeadMethod ? HttpMethod.Head : HttpMethod.Get), url);

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var request_task = client.SendAsync(requestMessage, cancellationTokenSource.Token);

        try
        {
            await request_task.WaitAsync(TimeSpan.FromSeconds(3));
        }
        catch (Exception)
        {
            if (UseHeadMethod)
                return await UnshortenUrl(url, false);

            throw;
        }

        if (!request_task.IsCompleted)
            cancellationTokenSource.Cancel();

        if (UseHeadMethod && request_task.IsFaulted && request_task.Exception.InnerException.GetType() == typeof(HttpRequestException))
        {
            _logger?.LogWarning("Unshortening Url '{Url}' failed, falling back to non-head method", url);
            return await UnshortenUrl(url, false);
        }

        var statuscode = request_task.Result.StatusCode;
        var header = request_task.Result.Headers;

        if (UseHeadMethod && statuscode is HttpStatusCode.NotFound or HttpStatusCode.InternalServerError)
        {
            _logger?.LogWarning("Unshortening Url '{Url}' failed, falling back to non-head method", url);
            return await UnshortenUrl(url, false);
        }

        if (statuscode is HttpStatusCode.Found
            or HttpStatusCode.Redirect
            or HttpStatusCode.SeeOther
            or HttpStatusCode.RedirectKeepVerb
            or HttpStatusCode.RedirectMethod
            or HttpStatusCode.PermanentRedirect
            or HttpStatusCode.TemporaryRedirect)
        {
            if (header is not null && header.Location is not null)
                return await UnshortenUrl(header.Location.AbsoluteUri);
            else
                return url;
        }
        else
            return url;
    }
}
