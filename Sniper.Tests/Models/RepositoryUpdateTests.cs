﻿using Xunit;

namespace Sniper.Tests.Models
{
    public class RepositoryUpdateTests
    {
        [Fact]
        public void CanSerialize()
        {
            var expected = "{\"name\":\"Hello-World\"," +
                           "\"description\":\"This is your first repository\"," +
                           "\"homepage\":\"https://github.com\"," +
                           "\"private\":true," +
                           "\"has_issues\":true," +
                           "\"has_wiki\":true," +
                           "\"has_downloads\":true}";

            var update = new RepositoryUpdate("Hello-World")
            {
                Description = "This is your first repository",
                Homepage = "https://github.com",
                Private = true,
                HasIssues = true,
                HasWiki = true,
                HasDownloads = true
            };

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }
    }
}
