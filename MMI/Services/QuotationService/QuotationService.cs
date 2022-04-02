using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MMI.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MMI.Services.QuotationService
{
	/// <summary>
	/// The implementation of <see cref="IQuotationService"/> using the Quotation API.
	/// </summary>
	public class QuotationService : IQuotationService
	{
		private readonly ILogger<QuotationService> _logger;

		/// <summary>
		/// Constructor for QuotationService
		/// </summary>
		/// <param name="logger">The logger DI Container</param>
		public QuotationService(ILogger<QuotationService> logger)
		{
			_logger = logger;
		}
		
		/// <summary>
		/// Gets a quotation from the API
		/// </summary>
		/// <param name="id">The id to be passed to the api</param>
		/// <returns>A Completed task containing the result of a Quotation.</returns>
		public async Task<Quotation> GetQuotationAsync(int id)
		{
			_logger.LogInformation("Getting quotation for id: {id}", id);
			
			var client = new RestClient("https://mmi20220402025319.azurewebsites.net/");
			var request = new RestRequest($"api/quotation/{id}");
			
			_logger.LogInformation("Making Request to: {url}", request.Resource);

			var response = client.ExecuteAsync(request);
			
			_logger.LogInformation("Response received: {response}", response.Result.ResponseStatus);

			if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				_logger.LogError("Error getting quotation for id: {id}", id);
				_logger.LogError("Error: {error}", response.Result.ErrorMessage);
				return null;
			}
			
			var quotation = JsonConvert.DeserializeObject<ServiceResponse<Quotation>>(response.Result.Content);
			if (!quotation.Success)
			{
				return null;
			}

			return quotation.Data;
		}

		/// <summary>
		/// Gets a policy from the API
		/// </summary>
		/// <param name="id">The Id to be passed to the api</param>
		/// <returns>A completed task containing the result of a quotation, which represents the policy</returns>
		public async Task<Quotation> GetPolicyAsync(int id)
		{
			
			_logger.LogInformation("Getting policy for id: {id}", id);
			
			var client = new RestClient("https://mmi20220402025319.azurewebsites.net/");
			var request = new RestRequest($"api/quotation/policy/{id}");
			
			_logger.LogInformation("Making Request to: {url}", request.Resource);
			
			var response = client.ExecuteAsync(request);
			
			_logger.LogInformation("Response received: {response}", response.Result.ResponseStatus);
			
			if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				_logger.LogError("Error getting policy for id: {id}", id);
				_logger.LogError("Error: {error}", response.Result.ErrorMessage);
				return null;
			}
			
			var quotation = JsonConvert.DeserializeObject<ServiceResponse<Quotation>>(response.Result.Content);

			if (!quotation.Success)
			{
				return null;
			}

			return quotation.Data;
		}

		/// <summary>
		/// Saves a quotation to the API - this is a POST request
		/// </summary>
		/// <param name="quotation">The quotation object to be sent to the API</param>
		/// <returns>The saved quotation object from the server</returns>
		public async Task<Quotation> SaveQuotationAsync(Quotation quotation)
		{
			
			_logger.LogInformation("Saving quotation to database");

			var client = new RestClient("https://mmi20220402025319.azurewebsites.net/");
			var request = new RestRequest("api/quotation", Method.Post);
			request.AddJsonBody(quotation);
			
			_logger.LogInformation("Making Request to: {url}", request.Resource);
			
			var response = client.ExecuteAsync(request);
			
			_logger.LogInformation("Response received: {response}", response.Result.ResponseStatus);
			
			if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				_logger.LogError("Error saving quotation for id: {id}", quotation.Id);
				_logger.LogError("Error: {error}", response.Result.ErrorMessage);
				return null;
			}
			
			var returnedQuotation = JsonConvert.DeserializeObject<ServiceResponse<Quotation>>(response.Result.Content);

			return returnedQuotation.Data;
		}

		/// <summary>
		/// Updates a quotation to be a policy - this is a PUT request
		/// </summary>
		/// <param name="quotation">The quotation that should be converted to a policy</param>
		/// <returns>The updated quotation object from the server</returns>
		public async Task<Quotation> UpdateQuotationAsync(Quotation quotation)
		{
			_logger.LogInformation("Updating quotation for id: {id}", quotation.Id);
			
			var client = new RestClient("https://mmi20220402025319.azurewebsites.net/");
			var request = new RestRequest($"api/quotation/updatepolicy/{quotation.Id}", Method.Put);
			
			_logger.LogInformation("Making Request to: {url}", request.Resource);
			
			var response = client.ExecuteAsync(request);
			
			_logger.LogInformation("Response received: {response}", response.Result.ResponseStatus);
			
			if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				_logger.LogError("Error updating quotation for id: {id}", quotation.Id);
				_logger.LogError("Error: {error}", response.Result.ErrorMessage);
				return null;
			}
			
			var returnedQuotation = JsonConvert.DeserializeObject<ServiceResponse<Quotation>>(response.Result.Content);
			
			return returnedQuotation.Data;
		}

		/// <summary>
		/// Gets a list of all the quotations from the API which are expiring within 3 days.
		/// </summary>
		/// <returns>A <see cref="List{T}"/> of expiring <see cref="Quotation"/></returns>
		public async Task<List<Quotation>> GetExpiringQuotationsAsync()
		{ 
			
			_logger.LogInformation("Getting expiring quotations");
			
			var client = new RestClient("https://mmi20220402025319.azurewebsites.net/");
			var request = new RestRequest("api/quotation/expiring/quotations");
			
			_logger.LogInformation("Making Request to: {url}", request.Resource);
			
			var response = client.ExecuteAsync(request);
			
			_logger.LogInformation("Response received: {response}", response.Result.ResponseStatus);
			
			if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				_logger.LogError("Error getting expiring quotations");
				_logger.LogError("Error: {error}", response.Result.ErrorMessage);
				return null;
			}
			
			var quotations = JsonConvert.DeserializeObject<ServiceResponse<List<Quotation>>>(response.Result.Content);
			
			return quotations.Data;
		}

		/// <summary>
		/// Gets a list of all the policies from the API which are expiring within 30 days.
		/// </summary>
		/// <returns>A <see cref="List{T}"/> of expiring <see cref="Quotation"/> representing the policies.</returns>
		public async Task<List<Quotation>> GetExpiringPoliciesAsync()
		{
			
			_logger.LogInformation("Getting expiring policies");
			
			var client = new RestClient("https://mmi20220402025319.azurewebsites.net/");
			var request = new RestRequest("api/quotation/expiring/policies");
			
			_logger.LogInformation("Making Request to: {url}", request.Resource);
			
			var response = client.ExecuteAsync(request);
			
			_logger.LogInformation("Response received: {response}", response.Result.ResponseStatus);
			
			if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				_logger.LogError("Error getting expiring policies");
				_logger.LogError("Error: {error}", response.Result.ErrorMessage);
				return null;
			}
			
			var quotations = JsonConvert.DeserializeObject<ServiceResponse<List<Quotation>>>(response.Result.Content);
			
			return quotations.Data;
		}

	}
}