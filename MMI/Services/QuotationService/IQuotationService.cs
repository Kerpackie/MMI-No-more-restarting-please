using System.Collections.Generic;
using System.Threading.Tasks;
using MMI.Models;

namespace MMI.Services.QuotationService
{
	public interface IQuotationService
	{
		Task<Quotation> GetQuotationAsync(int id);
		Task<Quotation> GetPolicyAsync(int id);
		Task<Quotation> SaveQuotationAsync(Quotation quotation);
		Task<Quotation> UpdateQuotationAsync(Quotation quotation);
		Task<List<Quotation>> GetExpiringQuotationsAsync();
		Task<List<Quotation>> GetExpiringPoliciesAsync();
	}
}