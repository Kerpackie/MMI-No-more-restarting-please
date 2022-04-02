namespace MMI.Models
{
	/// <summary>
	/// A Generic Wrapper for in-taking information from the API.
	/// </summary>
	/// <typeparam name="T">A Generic representing the object assigned to it.</typeparam>
	public class ServiceResponse<T>
	{
		/// <summary>
		/// A generic for the object assigned to it.
		/// </summary>
		public T? Data { get; set; }
		/// <summary>
		/// The success summary of the request.
		/// </summary>
		public bool Success { get; set; } = true;
		/// <summary>
		/// The message returned from the API.
		/// </summary>
		public string Message { get; set; } = string.Empty;
	}
}