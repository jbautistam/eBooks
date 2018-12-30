using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibComicsBooks.Definition
{
	/// <summary>
	///		Colecci�n de <see cref="ComicInfoProperty"/>
	/// </summary>
	public class ComicInfoPropertiesCollection : List<ComicInfoProperty>
	{
		/// <summary>
		///		A�ade una propiedad a la colecci�n
		/// </summary>
		internal ComicInfoProperty Add(string name, string value)
		{
			ComicInfoProperty property = new ComicInfoProperty(name, value);

				// A�ade la propiedad
				Add(property);
				// ... y la devuelve
				return property;
		}

		/// <summary>
		///		Busca una propiedad entr los valores
		/// </summary>
		private ComicInfoProperty Search(string name)
		{ 
			// Busca una propiedad
			foreach (ComicInfoProperty property in this)
				if (property.Name.Equals(name))
					return property;
			// Si ha llegado hasta aqu� es porque no exist�a as� que la a�ade
			return Add(name, "");
		}

		/// <summary>
		///		Indizador
		/// </summary>
		internal ComicInfoProperty this[string name]
		{
			get { return Search(name); }
			set
			{
				ComicInfoProperty property = Search(name);

					property.Value = value.Value;
			}
		}
	}
}
