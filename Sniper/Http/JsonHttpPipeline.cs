﻿using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Sniper.Types;

namespace Sniper.Http
{
    /// <summary>
    ///     Responsible for serializing the request and response as JSON and
    ///     adding the proper JSON response header.
    /// </summary>
    public static class JsonHttpPipeline
    {

        public static void SerializeRequest(IRequest request)
        {
            Ensure.ArgumentNotNull(nameof(request), request);

            if (!request.Headers.ContainsKey(HttpKeys.HtmlKeys.HeaderKeys.Accept))
            {
                request.Headers[HttpKeys.HtmlKeys.HeaderKeys.Accept] = AcceptHeaders.RedirectsPreviewThenStableVersionJson;
            }

            if (request.Method == HttpMethod.Get || request.Body == null) return;
            if (request.Body is string || request.Body is Stream || request.Body is HttpContent) return;


            request.Body = JsonConvert.SerializeObject(request.Body);
        }

        public static IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            Ensure.ArgumentNotNull(nameof(response), response);

            if (response.ContentType != null && response.ContentType.Equals(MimeTypes.ApplicationJson, StringComparison.Ordinal))
            {
                var body = response.Body as string;
                // simple json does not support the root node being empty. Will submit a pr but in the mean time....
                if (!string.IsNullOrEmpty(body) && body != "{}")
                {
                    var typeIsDictionary = typeof(IDictionary).IsAssignableFrom(typeof(T));
                    var typeIsEnumerable = typeof(IEnumerable).IsAssignableFrom(typeof(T));
                    var responseIsObject = body.StartsWith("{", StringComparison.Ordinal);

                    // If we're expecting an array, but we get a single object, just wrap it.
                    // This supports an api that dynamically changes the return type based on the content.
                    if (!typeIsDictionary && typeIsEnumerable && responseIsObject)
                    {
                        body = "[" + body + "]";
                    }
                    var json = JsonConvert.DeserializeObject<T>(body);
                    return new ApiResponse<T>(response, json);
                }
            }
            return new ApiResponse<T>(response);
        }
    }
}
