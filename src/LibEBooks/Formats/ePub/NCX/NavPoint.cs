using System;

namespace Bau.Libraries.LibEBook.Formats.ePub.NCX
{
	/// <summary>
	///		Punto de navegación / página
	/// </summary>
	public class NavPoint : Base.eBookBase
	{
		/// <summary>
		///		Título
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		URL
		/// </summary>
		public string URL { get; set; }

		/// <summary>
		///		Orden
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		///		Páginas
		/// </summary>
		public NavPointsCollection Pages { get; } = new NavPointsCollection();
	}
}
