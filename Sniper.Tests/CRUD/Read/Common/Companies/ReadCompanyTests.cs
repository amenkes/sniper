using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Read.Common.Companies
{
    public class ReadCompanyTests
    { 
        [Fact] 
        public void CompanyThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Companies) 
            }; 
            var company = new Company 
            { 
            }; 
        } 
    } 
} 
