using System;

namespace Bau.Libraries.LibEBook.Formats.eBook
{
	/// <summary>
	///		Elemento del índice
	/// </summary>
	public class IndexItem : Base.eBookBase
	{
		public IndexItem() : this(null, null, null) { }

		public IndexItem(string name, string idPage, string url)
		{ 
			Name = name;
			IDPage = idPage;
			URL = url;
		}

		/// <summary>
		///		Identificador del índice
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Identificador de la página
		/// </summary>
		public string IDPage { get; set; }

		/// <summary>
		///		Nombre del archivo con la página
		/// </summary>
		public string URL { get; set; }

		/// <summary>
		///		Número de páginas
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		///		Subíndices
		/// </summary>
		public IndexItemsCollection Items { get; } = new IndexItemsCollection();
	}
}
