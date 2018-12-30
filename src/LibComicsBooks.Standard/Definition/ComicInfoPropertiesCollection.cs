using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibComicsBooks.Definition
{
	/// <summary>
	///		Colección de <see cref="ComicInfoProperty"/>
	/// </summary>
	public class ComicInfoPropertiesCollection : List<ComicInfoProperty>
	{
		/// <summary>
		///		Añade una propiedad a la colección
		/// </summary>
		internal ComicInfoProperty Add(string name, string value)
		{
			ComicInfoProperty property = new ComicInfoProperty(name, value);

				// Añade la propiedad
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
			// Si ha llegado hasta aquí es porque no existía así que la añade
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
