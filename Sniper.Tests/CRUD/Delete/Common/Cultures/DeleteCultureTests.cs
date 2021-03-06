using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Delete.Common.Cultures
{
    public class DeleteCultureTests 
     { 
        [Fact] 
        public void CultureThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Cultures) 
            }; 
            var culture = new Culture 
            { 
            }; 
        } 
    } 
} 
