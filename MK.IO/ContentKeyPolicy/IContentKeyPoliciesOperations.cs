﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IContentKeyPoliciesOperations
    {
        /// <summary>
        /// List Content Key Policies
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="filter">Filters the set of items returned.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <returns></returns>
        List<ContentKeyPolicySchema> List(string? orderBy = null, string? filter = null, int? top = null);

        /// <summary>
        /// List Content Key Policies
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="filter">Filters the set of items returned.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <returns></returns>
        Task<List<ContentKeyPolicySchema>> ListAsync(string? orderBy = null, string? filter = null, int? top = null);

        /// <summary>
        /// List Content Key Policies using pages.
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="filter">Restricts the set of items returned.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <returns></returns>
        PagedResult<ContentKeyPolicySchema> ListAsPage(string? orderBy = null, string? filter = null, int? top = null);

        /// <summary>
        /// List Content Key Policies using pages.
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="filter">Restricts the set of items returned.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <returns></returns>
        Task<PagedResult<ContentKeyPolicySchema>> ListAsPageAsync(string? orderBy = null, string? filter = null, int? top = null);

        /// <summary>
        /// List Content Key Policies using pages.
        /// </summary>
        /// <param name="nextPageLink">Next page link.</param>
        /// <returns></returns>
        PagedResult<ContentKeyPolicySchema> ListAsPageNext(string? nextPageLink);

        /// <summary>
        /// List Content Key Policies using pages.
        /// </summary>
        /// <param name="nextPageLink">Next page link.</param>
        /// <returns></returns>
        Task<PagedResult<ContentKeyPolicySchema>> ListAsPageNextAsync(string? nextPageLink);

        /// <summary>
        /// Delete a Content Key Policy. If the policy does not exist, this will return a 204.
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        void Delete(string contentKeyPolicyName);

        /// <summary>
        /// Delete a Content Key Policy. If the policy does not exist, this will return a 204.
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        Task DeleteAsync(string contentKeyPolicyName);

        /// <summary>
        /// Get one Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        ContentKeyPolicySchema Get(string contentKeyPolicyName);

        /// <summary>
        /// Get one Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        Task<ContentKeyPolicySchema> GetAsync(string contentKeyPolicyName);

        /// <summary>
        /// Create a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        ContentKeyPolicySchema Create(string contentKeyPolicyName, ContentKeyPolicyProperties properties);

        /// <summary>
        /// Create a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<ContentKeyPolicySchema> CreateAsync(string contentKeyPolicyName, ContentKeyPolicyProperties properties);

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        /// <summary>
        /// Update a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        ContentKeyPolicySchema Update(string contentKeyPolicyName, ContentKeyPolicyProperties properties);

        /// <summary>
        /// Update a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<ContentKeyPolicySchema> UpdateAsync(string contentKeyPolicyName, ContentKeyPolicyProperties properties);
#endif
        /// <summary>
        /// Get the properties of a Content Key Policy including secret values
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        ContentKeyPolicyProperties GetPolicyPropertiesWithSecrets(string contentKeyPolicyName);

        /// <summary>
        /// Get the properties of a Content Key Policy including secret values
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        Task<ContentKeyPolicyProperties> GetPolicyPropertiesWithSecretsAsync(string contentKeyPolicyName);
    }
}