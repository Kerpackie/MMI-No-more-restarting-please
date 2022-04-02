using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MMI.Models;
using SelectPdf;

namespace MMI.Services.FileService
{
	public class FileService : IFileService
	{
		private readonly ILogger<FileService> _logger;


		public FileService(ILogger<FileService> logger)
		{
			_logger = logger;
		}
		
		public void CreateCertificate(Quotation quotation)
		{
			_logger.LogInformation("Creating certificate for policy {@quotation}", quotation.Id);
			
			var html = @"<html><body>
            				<h1 align='center'><u>Munster Motors Insurance </u></h1>
            				<font size = 7><p align = 'center'>Insurance Certificate</p><hr color=red></hr><font size = 3>
            				<p align='center'> MMI (Munster Motors Insurance) </p>
            				<p align='center'> THIS IS A PERSONAL CERTIFICATE ISSUED BY MMI(MUNSTER MOTORS INSURANCE) REGARDING THE NECESSARY INFORMATION THAT IS NEEDED.</P>
            				<P align='center'>THIS CERTIFICATION THAT IS ISSUED IS A MATTER OF INFORMATION ONLY-REGARDING YOUR RECENT POLICY WITH US AND CONFERS NO RIGHTS TOO THE CERTIFICATION HOLDER.</p>
            				<p align='center'> THIS CERTIFICATION THAT HAS BEEN ISSUED DOES NOT AFFIRMATIVELY OR NEGATIVELY AMEND, EXTEND OR ALTER THE COVERAGE AFFORDED BY THE POLICIES AUTHORISED BELOW.</p>
            				<p align='center'>THIS ISSUE OF THE INSURANCE CERTIFICATE DOES NOT CONSTITUTE A CONTRACT BETWEEN THE ISSUING INSURERS, AUTHORISED REPRESENTATIVE OR PRODUCER AND THE CERTIFICATE HOLDER.</p>
            				<p align='center'><b> IMPORTANT NOTICE: IF THE CERTIFICATE HOLDER IS AN ADDITIONAL INSURED, THE POLICY MUST BE ENDORSED.</b> </p>
            				<hr color=red></hr>
            				<div align ='center'>
            				<table border='1'>";
            			
            html += "<tr><th>Policy Number:</th><td>" + quotation.Id + "</td></tr>";
            html += "<tr><th>Criteria:</th><td></td></tr>";
            html += "<tr><th>Sex:</th><td>" + quotation.Sex + "</td></tr>";
            html += "<tr><th>Age:</th><td>" + quotation.Age + "</td></tr>";
            html += "<tr><th>County:</th><td>" + quotation.County + "</td></tr>";
            html += "<tr><th>Vehicle:</th><td></td></tr>";
			html += "<tr><th>Model:</th><td>" + quotation.Model + "</td></tr>";
			html += "<tr><th>Emissions Class:</th><td>" + quotation.Emissions + "</td></tr>";
			html += "<tr><th>Insurance Category:</th><td>" + quotation.InsuranceCategory + "</td></tr>";
			html += "<tr><th>Policy Cost:</th><td>€" + quotation.TotalCost + "</td></tr>";
			html += "<tr><th>Policy Start Date:</th><td>" + quotation.ValidUntil.AddYears(-1) + "</td></tr>";
			html += "<tr><th>Policy End Date:</th><td>" + quotation.ValidUntil + "</td></tr>";

			html += "</table></div><hr color=red></hr></body></html>";
                     
            html += @"<p align='center'> Dear " + quotation.Customer.FirstName + " " + quotation.Customer.Surname + $@"</p>
            <p align='center'> 	We are writing too you today too notify you that your INSURANCE policy has been successful.</p>
            <p align='center'> As of now your insurance policy is in IMMEDIATE EFFECT and is valid until {quotation.ValidUntil}.</p>
            <p align='center'> Thank you for choosing use as your provider for your insurance</p>
            <p align='center'> Yours Sincerely </p>
            <p align='center'> MMI Director </p>
			<p></p>
            <p align='center'><b> Signature _______________ </p></b>
            <p align='center'><b> Date:____/______/________ </p></b>";

            var fileName = @$"{Environment.CurrentDirectory}\Policies\Policy#{quotation.Id}.pdf";
            
			GeneratePdf(html, fileName);
		}

		public void CreateExpiringSoonReport(List<Quotation> quotations, string reportType)
		{
			
			_logger.LogInformation("Generating {@reportType} Report for {@quotations} items", reportType, quotations.Count);
			
			
			var html = @"<html><body><div align='center'>";
			
			html += "<h1 align='center'><u>Munster Motors Insurance </u></h1>";
			html += $"<font size = 7><p align = 'center'>{reportType}</p><hr color=red></hr><font size = 3>";

			html += @"<table border='1' width='80%' cellpadding='5' ><thead>
				<tr>
				<td><b>Policy ID</td></b>
				<td><b>Customer</td></b>
				<td><b>Vehicle Model</td></b>
				<td><b>Insurance Category</td></b>
				<td><b>Total Cost</td></b>
				<td><b>Valid Until</td></b>
				<td><b>Phone Number</td></b>
				</tr>
				</thead>";

			foreach (var quotation in quotations)
			{
				html += "<tr><td>" + quotation.Id + "</td>";
				html += "<td>" + quotation.Customer.FirstName + " " + quotation.Customer.Surname + "</td>";
				html += "<td>" + quotation.Model + "</td>";
				html += "<td>" + quotation.InsuranceCategory + "</td>";
				html += "<td>€" + quotation.TotalCost + "</td>";
				html += "<td>" + quotation.ValidUntil + "</td>";
				html += "<td>" + quotation.Customer.PhoneNumber + "</td></tr>";
			}
			
			html += @"</div></body></html>";

			var fileName = @$"{Environment.CurrentDirectory}\Reports\{reportType} - {DateTime.Today:yy-MM-dd}.pdf";
			GeneratePdf(html, fileName);
		}

		public void GeneratePdf(string html, string fileName)
		{
			_logger.LogInformation("Initializing PDF Generation");
			var converter = new HtmlToPdf();
			var doc = converter.ConvertHtmlString(html);
			_logger.LogInformation("PDF Generation Complete");
			doc.Save(fileName);
			_logger.LogDebug("PDF Saved to {@fileName}", fileName);
			doc.Close();
			_logger.LogDebug("PDF Closed");
            
			// Open PDF File with default application

			_logger.LogInformation("Opening PDF File for user");
			var proc = new Process();
			proc.StartInfo.UseShellExecute = true;
			proc.StartInfo.FileName = fileName;
			proc.Start();
			_logger.LogInformation("PDF File Opened");
		}
	}
}