/*
 * This file is automatically generated; any changes will be lost. 
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Beef.RefData;
using Beef.WebApi;
using Beef.Demo.Common.Entities;
using Beef.Demo.Common.Agents.ServiceAgents;

namespace Beef.Demo.Common.Agents
{
    /// <summary>
    /// Provides the <b>ReferenceData</b> Web API service agent.
    /// </summary>
    public partial class ReferenceDataAgent : WebApiAgentBase, IReferenceDataServiceAgent
    {
        /// <summary>
        /// Gets the <b>magic</b> value to get all reference data values.
        /// </summary>
        public const string GetNamedAllNames = "REF_DATA_ALL";
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceDataAgent"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> (where overridding the default value).</param>
        /// <param name="beforeRequest">The <see cref="Action{HttpRequestMessage}"/> to invoke before the <see cref="HttpRequestMessage">Http Request</see> is made (see <see cref="WebApiServiceAgentBase.BeforeRequest"/>).</param>
        public ReferenceDataAgent(HttpClient httpClient = null, Action<HttpRequestMessage> beforeRequest = null)
        {
            ServiceAgent = Beef.Factory.Create<IReferenceDataServiceAgent>(httpClient, beforeRequest);
        }
        
        /// <summary>
        /// Gets the underlyng <see cref="IReferenceDataServiceAgent"/> instance.
        /// </summary>
        public IReferenceDataServiceAgent ServiceAgent { get; private set; }

        /// <summary>
        /// Gets all of the <see cref="Gender"/> objects that match the filter arguments.
        /// </summary>
        /// <param name="args">The optional <see cref="ReferenceDataFilter"/> arguments.</param>
        /// <param name="requestOptions">The optional <see cref="WebApiRequestOptions"/>.</param>
        /// <returns>A <see cref="WebApiAgentResult"/>.</returns>
        public Task<WebApiAgentResult<GenderCollection>> GenderGetAllAsync(ReferenceDataFilter args = null, WebApiRequestOptions requestOptions = null) => ServiceAgent.GenderGetAllAsync(args, requestOptions);      

        /// <summary>
        /// Gets all of the <see cref="EyeColor"/> objects that match the filter arguments.
        /// </summary>
        /// <param name="args">The optional <see cref="ReferenceDataFilter"/> arguments.</param>
        /// <param name="requestOptions">The optional <see cref="WebApiRequestOptions"/>.</param>
        /// <returns>A <see cref="WebApiAgentResult"/>.</returns>
        public Task<WebApiAgentResult<EyeColorCollection>> EyeColorGetAllAsync(ReferenceDataFilter args = null, WebApiRequestOptions requestOptions = null) => ServiceAgent.EyeColorGetAllAsync(args, requestOptions);      

        /// <summary>
        /// Gets all of the <see cref="PowerSource"/> objects that match the filter arguments.
        /// </summary>
        /// <param name="args">The optional <see cref="ReferenceDataFilter"/> arguments.</param>
        /// <param name="requestOptions">The optional <see cref="WebApiRequestOptions"/>.</param>
        /// <returns>A <see cref="WebApiAgentResult"/>.</returns>
        public Task<WebApiAgentResult<PowerSourceCollection>> PowerSourceGetAllAsync(ReferenceDataFilter args = null, WebApiRequestOptions requestOptions = null) => ServiceAgent.PowerSourceGetAllAsync(args, requestOptions);      

        /// <summary>
        /// Gets all of the <see cref="Company"/> objects that match the filter arguments.
        /// </summary>
        /// <param name="args">The optional <see cref="ReferenceDataFilter"/> arguments.</param>
        /// <param name="requestOptions">The optional <see cref="WebApiRequestOptions"/>.</param>
        /// <returns>A <see cref="WebApiAgentResult"/>.</returns>
        public Task<WebApiAgentResult<CompanyCollection>> CompanyGetAllAsync(ReferenceDataFilter args = null, WebApiRequestOptions requestOptions = null) => ServiceAgent.CompanyGetAllAsync(args, requestOptions);      

        /// <summary>
        /// Gets the named reference data objects.
        /// </summary>
        /// <param name="names">The list of reference data names; to retrieve all pass a single name of <see cref="ReferenceDataAgent.GetNamedAllNames"/>.</param>
        /// <param name="requestOptions">The optional <see cref="WebApiRequestOptions"/>.</param>
        /// <returns>A <see cref="WebApiAgentResult"/>.</returns>
        /// <remarks>The reference data objects will need to be manually extracted from the corresponding response content.</remarks>
        public Task<WebApiAgentResult> GetNamedAsync(string[] names, WebApiRequestOptions requestOptions = null) => ServiceAgent.GetNamedAsync(names, requestOptions);
        
        /// <summary>
        /// Gets the reference data entities for the specified entities and codes from the <see cref="WebApiRequestOptions.UrlQueryString"/>; e.g: entity=codeX,codeY&amp;entity2=codeZ&amp;entity3
        /// </summary>
        /// <param name="requestOptions">The optional <see cref="WebApiRequestOptions"/>.</param>
        /// <returns>A <see cref="WebApiAgentResult"/>.</returns>
        /// <remarks>The reference data objects will need to be manually extracted from the corresponding response content.</remarks>
        public Task<WebApiAgentResult> GetByCodesAsync(WebApiRequestOptions requestOptions = null) => ServiceAgent.GetByCodesAsync(requestOptions);
    }
}