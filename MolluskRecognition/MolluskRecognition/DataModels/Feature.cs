using System.Collections.Generic;
using Simple.Data;

namespace MolluskRecognition.DataModels
{
	/// <summary>
	/// Признак
	/// </summary>
	public class Feature
	{
		/// <summary>
		/// Название признака
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Значение признака
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Список подпризнаков
		/// </summary>
		public List<Feature> SubFeatures { get; set; }

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public Feature(){}

		/// <summary>
		/// Конструктор с заполнением названием признака
		/// </summary>
		public Feature(string name)
		{
			this.Name = name;
		}
	}
}
