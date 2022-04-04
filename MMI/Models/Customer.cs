using System;

namespace MMI.Models
{
	/// <summary>
	/// The Model for a customer.
	/// </summary>
	public class Customer
	{
		/// <summary>
		/// The ID of the customer.
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The First Name of the customer.
		/// </summary>
		public string FirstName { get; set; } = string.Empty;

		/// <summary>
		/// The Last Name of the customer.
		/// </summary>
		public string Surname { get; set; } = string.Empty;
		/// <summary>
		/// The Street of the customer's address.
		/// </summary>
		public string Street { get; set; } = string.Empty;
		/// <summary>
		/// The County of the customers address.
		/// </summary>
		public string County { get; set; } = string.Empty;
		/// <summary>
		/// The EirCode of the customer's address.
		/// </summary>
		public string Eircode { get; set; } = string.Empty;
		/// <summary>
		/// The Phone Number of the customer.
		/// </summary>
		public string PhoneNumber { get; set; } = string.Empty;
	}
}