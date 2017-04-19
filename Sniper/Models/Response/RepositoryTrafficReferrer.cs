﻿#if false
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sniper.Response
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class RepositoryTrafficReferrer
    {
        public RepositoryTrafficReferrer() { }

        [SuppressMessage(Categories.Naming, "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficReferrer(string referrer, int count, int uniques)
        {
            Referrer = referrer;
            Count = count;
            Uniques = uniques;
        }

        public string Referrer { get; protected set; }

        public int Count { get; protected set; }

        [SuppressMessage(Categories.Naming, "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Referrer: {0}, Count: {1}", Referrer, Count);
    }
}
#endif