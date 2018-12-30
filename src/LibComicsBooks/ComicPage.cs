using System;

namespace Bau.Libraries.LibComicsBooks
{
	/// <summary>
	///		Clase con los datos de una página
	/// </summary>
	public class ComicPage : IComparable<ComicPage>
	{
		public ComicPage(string fileName)
		{
			FileName = fileName;
			MarkAsDeleted = false;
			Uncompressed = false;
		}

		/// <summary>
		///		Nombre del archivo de la página
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		///		Indica si está marcado como borrado
		/// </summary>
		public bool MarkAsDeleted { get; set; }

		/// <summary>
		///		Indica si está descomprimido 
		/// </summary>
		public bool Uncompressed { get; set; }

		/// <summary>
		///		Implementa la interface IComparable
		/// </summary>
		public int CompareTo(ComicPage other)
		{
			return FileName.CompareTo(other.FileName);
		}
	}
}
