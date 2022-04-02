using System;

namespace MMI.Models
{
	/// <summary>
	/// The Quotation Model, which is used to hold the Quotation data.
	/// Can also be used to represent a Policy.
	/// </summary>
	public class Quotation
	{
		/// <summary>
		/// The Quotation ID.
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The Sex Criteria
		/// </summary>
		public string Sex { get; set; } = string.Empty;
		/// <summary>
		/// The Age Criteria
		/// </summary>
		public int Age { get; set; }
		/// <summary>
		/// The County Criteria
		/// </summary>
		public string County { get; set; } = string.Empty;
		/// <summary>
		/// The Vehicle Model Criteria
		/// </summary>
		public string Model { get; set; } = string.Empty;
		/// <summary>
		/// The Emissions Class Criteria
		/// </summary>
		public string Emissions { get; set; } = string.Empty;
		/// <summary>
		/// The Insurance Category Criteria
		/// </summary>
		public string InsuranceCategory { get; set; } = string.Empty;
		/// <summary>
		/// The total quotation cost
		/// </summary>
		public int TotalCost { get; set; } = 1000;
		/// <summary>
		/// The date that the quotation is valid until
		/// </summary>
		public DateTime ValidUntil { get; set; }
		/// <summary>
		/// Represents if the quotation is a policy or not
		/// </summary>
		public bool IsPolicy { get; set; } = false;
		/// <summary>
		/// The customer that the quotation is belongs to
		/// </summary>
		public Customer Customer { get; set; }
		
		/// <summary>
		/// Calculates the total cost of the quotation.
		/// </summary>
		public void QuotationTotalCost()
		{
			var totalCost = 1000;
			totalCost += SexCost();
			totalCost += AgeCost();
			totalCost += CountyCost();
			totalCost += ModelCost();
			totalCost += EmissionsCost();
			totalCost += InsuranceCategoryCost();
			
			TotalCost = totalCost;
			
			ValidUntil = DateTime.Today.AddDays(31);
		}
		
		// Private methods that are used to calculate the total cost of the quotation
		// They use the Quotation's properties to calculate the cost.
		private int SexCost()
		{
			return Sex == "Male" ? 1000 : 800;
		}
		
		private int AgeCost()
		{
			if (Sex == "Male")
			{
				var cost = Age switch
				{
					0 => 0,
					< 20 => 400,
					<= 35 => -800,
					< 80 => -1300,
					>= 80 => 999999
				};

				return cost;
			}

			else
			{
				var cost = Age switch
				{
					0 => 0,
					< 20 => 160,
					<= 35 => -320,
					< 80 => -520,
					>= 80 => 999999
				};

				return cost;
			}
		}
		
		private int CountyCost()
		{
			var cost = County switch
			{
				"Cork" => 50,
				"Clare" => 225,
				"Kerry" => 50,
				"Limerick" => -75,
				"Tipperary" => -80,
				"Waterford" => -100,
				_ => 0
			};

			return cost;
		}
		
		private int ModelCost()
		{
			var cost = Model switch
			{
				"Convertible" => 200,
				"Gran Truismo" => 250,
				"X6" => 300,
				"Z4 Roadster" => 175,
				"Corsa" => 50,
				"Astra" => 105,
				"Vectra" => 150,
				"Yaris" => 50,
				"Auris" => 75,
				"Corolla" => 100,
				"Avensis" => 125,
				"Renault" => 100,
				"Megane" => 75,
				"Clio" => 50,
				_ => 0
			};

			return cost;
		}
		
		private int EmissionsCost()
		{
			var cost = Emissions switch
			{
				"High" => 300,
				"Medium" => 150,
				"Low" => -75,
				_ => 0
			};

			return cost;
		}
		
		private int InsuranceCategoryCost()
		{
			var cost = InsuranceCategory switch
			{
				"Fully Comprehensive" => 200,
				"Third Party Fire and Theft" => -120,
				_ => 0
			};

			return cost;
		}
	}
}