using System.Collections.Generic;
using MMI.Models;

namespace MMI.Services.FileService
{
	/// <summary>
	/// The interface for the FileService
	/// </summary>
	public interface IFileService
	{
		void CreateCertificate(Quotation quotation);
		void CreateExpiringSoonReport(List<Quotation> quotations, string reportType);
		void GeneratePdf(string html, string fileName);
	}
}