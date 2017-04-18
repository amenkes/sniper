﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Sniper.Http;

namespace Sniper
{
    /// <summary>
    /// Represents a HTTP 401 - Unauthorized response returned from the API.
    /// </summary>

    [Serializable]

    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class AuthorizationException : ApiException
    {
        public AuthorizationException() {}

        /// <summary>
        /// Constructs an instance of AuthorizationException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public AuthorizationException(IResponse response) : this(response, null) {}

        /// <summary>
        /// Constructs an instance of AuthorizationException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public AuthorizationException(IResponse response, Exception innerException) : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.Unauthorized,
                "AuthorizationException created with wrong status code");
        }
       
    }
}
