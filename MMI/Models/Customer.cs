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
		public string FirstName { get; set; }
		/// <summary>
		/// The Last Name of the customer.
		/// </summary>
		public string Surname { get; set; }
		/// <summary>
		/// The Street of the customer's address.
		/// </summary>
		public string Street { get; set; }
		/// <summary>
		/// The County of the customers address.
		/// </summary>
		public string County { get; set; }
		/// <summary>
		/// The EirCode of the customer's address.
		/// </summary>
		public string Eircode { get; set; }
		/// <summary>
		/// The Phone Number of the customer.
		/// </summary>
		public string PhoneNumber { get; set; }
	}
}