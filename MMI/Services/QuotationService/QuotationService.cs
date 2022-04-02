using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MMI.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MMI.Services.QuotationService
{
	public class QuotationService : IQuotationService
	{
		private readonly ILogger<QuotationService> _logger;

		public QuotationService(ILogger<QuotationService> logger)
		{
			_logger = logger;
		}
		
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