﻿using System;
using System.Diagnostics;
using System.IO;

namespace Sniper.Tests.Integration
{
    public static class Helper
    {
        private static readonly Lazy<Credentials> _credentialsThunk = new Lazy<Credentials>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("Sniper_GITHUBUSERNAME");
            UserName = githubUsername;
            Organization = Environment.GetEnvironmentVariable("Sniper_GITHUBORGANIZATION");

            var githubToken = Environment.GetEnvironmentVariable("Sniper_OAUTHTOKEN");

            if (githubToken != null)
                return new Credentials(githubToken);

            var githubPassword = Environment.GetEnvironmentVariable("Sniper_GITHUBPASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

        private static readonly Lazy<Credentials> _oauthApplicationCredentials = new Lazy<Credentials>(() =>
        {
            var applicationClientId = ClientId;
            var applicationClientSecret = ClientSecret;

            if (applicationClientId == null || applicationClientSecret == null)
                return null;

            return new Credentials(applicationClientId, applicationClientSecret);
        });

        private static readonly Lazy<Credentials> _basicAuthCredentials = new Lazy<Credentials>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("Sniper_GITHUBUSERNAME");
            UserName = githubUsername;
            Organization = Environment.GetEnvironmentVariable("Sniper_GITHUBORGANIZATION");

            var githubPassword = Environment.GetEnvironmentVariable("Sniper_GITHUBPASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

        private static readonly Lazy<Uri> _customUrl = new Lazy<Uri>(() =>
        {
            string uri = Environment.GetEnvironmentVariable("Sniper_CUSTOMURL");

            if (uri != null)
                return new Uri(uri);

            return null;
        });

        static Helper()
        {
            // Force reading of environment variables.
            // This wasn't happening if UserName/Organization were 
            // retrieved before Credentials.
            Debug.WriteIf(Credentials == null, "No credentials specified.");
        }

        public static string UserName { get; private set; }
        public static string Organization { get; private set; }

        /// <summary>
        /// These credentials should be set to a test GitHub account using the powershell script configure-integration-tests.ps1
        /// </summary>
        public static Credentials Credentials { get { return _credentialsThunk.Value; } }

        public static Credentials ApplicationCredentials { get { return _oauthApplicationCredentials.Value; } }

        public static Credentials BasicAuthCredentials { get { return _basicAuthCredentials.Value; } }

        public static Uri CustomUrl { get { return _customUrl.Value; } }

        public static Uri TargetUrl { get { return CustomUrl ?? GitHubClient.GitHubApiUrl; } }

        public static bool IsUsingToken
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("Sniper_OAUTHTOKEN"));
            }
        }

        public static bool IsPaidAccount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("Sniper_PRIVATEREPOSITORIES"));
            }
        }

        public static string ClientId
        {
            get { return Environment.GetEnvironmentVariable("Sniper_CLIENTID"); }
        }

        public static string ClientSecret
        {
            get { return Environment.GetEnvironmentVariable("Sniper_CLIENTSECRET"); }
        }

        public static void DeleteRepo(IConnection connection, Repository repository)
        {
            if (repository != null)
                DeleteRepo(connection, repository.Owner.Login, repository.Name);
        }

        public static void DeleteRepo(IConnection connection, string owner, string name)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.Repository.Delete(owner, name).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static void DeleteTeam(IConnection connection, Team team)
        {
            if (team != null)
                DeleteTeam(connection, team.Id);
        }

        public static void DeleteTeam(IConnection connection, int teamId)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.Organization.Team.Delete(teamId).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static void DeleteKey(IConnection connection, PublicKey key)
        {
            if (key != null)
                DeleteKey(connection, key.Id);
        }

        public static void DeleteKey(IConnection connection, int keyId)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.User.GitSshKey.Delete(keyId).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static void DeleteGpgKey(IConnection connection, GpgKey key)
        {
            if (key != null)
                DeleteGpgKey(connection, key.Id);
        }

        public static void DeleteGpgKey(IConnection connection, int keyId)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.User.GpgKey.Delete(keyId).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static string MakeNameWithTimestamp(string name)
        {
            return string.Concat(name, "-", DateTime.UtcNow.ToString("yyyyMMddhhmmssfff"));
        }

        public static Stream LoadFixture(string fileName)
        {
            var key = "Sniper.Tests.Integration.fixtures." + fileName;
            var stream = typeof(Helper).Assembly.GetManifestResourceStream(key);
            if (stream == null)
            {
                throw new InvalidOperationException(
                    "The file '" + fileName + "' was not found as an embedded resource in the assembly. Failing the test...");
            }
            return stream;
        }

        public static IGitHubClient GetAuthenticatedClient()
        {
            return new GitHubClient(new ProductHeaderValue("SniperTests"), TargetUrl)
            {
                Credentials = Credentials
            };
        }

        public static IGitHubClient GetBasicAuthClient()
        {
            return new GitHubClient(new ProductHeaderValue("SniperTests"), TargetUrl)
            {
                Credentials = BasicAuthCredentials
            };
        }

        public static GitHubClient GetAuthenticatedApplicationClient()
        {
            return new GitHubClient(new ProductHeaderValue("SniperTests"), TargetUrl)
            {
                Credentials = ApplicationCredentials
            };
        }

        public static IGitHubClient GetAnonymousClient()
        {
            return new GitHubClient(new ProductHeaderValue("SniperTests"), TargetUrl);
        }

        public static IGitHubClient GetBadCredentialsClient()
        {
            return new GitHubClient(new ProductHeaderValue("SniperTests"), TargetUrl)
            {
                Credentials = new Credentials(Guid.NewGuid().ToString(), "bad-password")
            };
        }
    }
}
