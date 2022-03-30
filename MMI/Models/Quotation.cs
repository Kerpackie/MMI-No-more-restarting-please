using System;

namespace MMI.Models
{
	public class Quotation
	{
		public int Id { get; set; }
		public string Sex { get; set; } = string.Empty;
		public int Age { get; set; }
		public string County { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public string Emissions { get; set; } = string.Empty;
		public string InsuranceCategory { get; set; } = string.Empty;
		public int TotalCost { get; set; }
		public DateTime ValidUntil { get; set; }
		
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
		
		private int SexCost()
		{
			var cost = Sex switch
			{
				"Male" => 1000,
				"Female" => 800,
				_ => 0
			};

			return cost;
		}
		
		private int AgeCost()
		{
			if (Sex == "Male")
			{
				var cost = Age switch
				{
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
			var cost = Emissions switch
			{
				"Fully Comprehensive" => 200,
				"Third Party Fire and Theft" => -120,
				_ => 0
			};

			return cost;
		}
	}
	
	
}