﻿using System;
using System.Threading.Tasks;
using Xunit;

namespace Sniper.Tests.Integration.Clients.Enterprise
{
    public class EnterpriseOrganizationClientTests
    {
        private readonly IGitHubClient _github;

        public EnterpriseOrganizationClientTests()
        {
            _github = EnterpriseHelper.GetAuthenticatedClient();
        }

        [GitHubEnterpriseTest]
        public async Task CanCreateOrganization()
        {
            string orgLogin = Helper.MakeNameWithTimestamp("MyOrganization");
            string orgName = String.Concat(orgLogin, " Display Name");

            var newOrganization = new NewOrganization(orgLogin, EnterpriseHelper.UserName, orgName);
            var organization = await
                _github.Enterprise.Organization.Create(newOrganization);

            Assert.NotNull(organization);

            // Get organization and check login/name
            var checkOrg = await _github.Organization.Get(orgLogin);
            Assert.Equal(checkOrg.Login, orgLogin);
            Assert.Equal(checkOrg.Name, orgName);
        }
    }
}
