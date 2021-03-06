﻿using Newtonsoft.Json;
using Sniper.Application;
using Sniper.Contracts.Entities.Common;
using Sniper.Contracts.Entities.History;
using Sniper.History;
using System.Collections.ObjectModel;
using static Sniper.CustomAttributes.CustomAttributes;

namespace Sniper.Common
{
    ///<summary>
    /// A statement of end user requirements in a couple of sentences. User Story can be assigned to Iteration or Release.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://md5.tpondemand.com/api/v1/UserStories/meta">API documentation - UserStory</a>
    /// </remarks>
    [CanCreateReadUpdateDelete]
    public class UserStory : Assignable, IHasInitialEstimate, IHasFeature, IHasBugs, IHasTasks, IHasUserHistory
    {
        #region Required for Create

        [JsonProperty(Required = Required.DisallowNull)]
        [RequiredForCreate(JsonProperties.Id)]
        public override Project Project { get; set; }

        #endregion

        [JsonProperty(Required = Required.Default)]
        public decimal InitialEstimate { get; set; }

        [JsonProperty(Required = Required.Default)]
        public Feature Feature { get; set; }

        [JsonProperty(Required = Required.Default)]
        public Collection<Bug> Bugs { get; internal set; }

        [JsonProperty(Required = Required.Default)]
        public Collection<UserStorySimpleHistory> History { get; internal set; }

        [JsonProperty(Required = Required.Default)]
        public Collection<Task> Tasks { get; internal set; }

        [JsonProperty(Required = Required.Default)]
        public Collection<TestCase> UserStoryTestCases { get; internal set; }

        [JsonProperty(Required = Required.Default)]
        public Collection<TestPlan> UserStoryTestPlans { get; internal set; }
    }
}