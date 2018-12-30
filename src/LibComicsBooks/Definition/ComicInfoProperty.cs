using System;

namespace Bau.Libraries.LibComicsBooks.Definition
{
	/// <summary>
	///		Propiedad de información de un cómic
	/// </summary>
	public class ComicInfoProperty
	{
		public ComicInfoProperty(string name, string value)
		{
			Name = name;
			Value = value;
		}

		/// <summary>
		///		Nombre de la propiedad
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Valor de la propiedad
		/// </summary>
		public string Value { get; set; }
	}
}
