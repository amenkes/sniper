using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Delete.Common.Severities
{
    public class DeleteSeverityTests 
     { 
        [Fact] 
        public void SeverityThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Severities) 
            }; 
            var severity = new Severity 
            { 
            }; 
        } 
    } 
} 
