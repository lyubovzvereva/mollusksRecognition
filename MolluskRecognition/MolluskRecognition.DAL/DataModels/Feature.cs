using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MolluskRecognition.DAL.DataModels
{
	/// <summary>
	/// Признак
	/// </summary>
	public class Feature : Entity
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
		public virtual List<Feature> SubFeatures { get; set; }

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
