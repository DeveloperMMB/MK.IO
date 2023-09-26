﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    internal class SubscriptionOperations : ISubscriptionOperations
    {
        //
        // account
        //
        private const string _accountProfileApiUrl = "api/profile";
        private const string _accountStatsApiUrl = "api/ams/{0}/stats";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the SubscriptionOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal SubscriptionOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public AccountStats GetStats()
        {
            Task<AccountStats> task = Task.Run<AccountStats>(async () => await GetStatsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<AccountStats> GetStatsAsync()
        {
            var url = Client.GenerateApiUrl(_accountStatsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return AccountStats.FromJson(responseContent);
        }

        public UserInfo GetUserInfo()
        {
            Task<UserInfo> task = Task.Run<UserInfo>(async () => await GetUserInfoAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<UserInfo> GetUserInfoAsync()
        {
            string responseContent = await Client.GetObjectContentAsync(Client._baseUrl + _accountProfileApiUrl);
            return AccountProfile.FromJson(responseContent).Spec;
        }
    }
}