using System;

using iTextSharp.text.pdf;

namespace Bau.Libraries.LibPDFTools.PDF
{
	/// <summary>
	///		Clase de ayuda para manejo de arcivos PDF
	/// </summary>
	public class PdfHelper
	{
		/// <summary>
		///		Cuenta el número de páginas
		/// </summary>
		public int CountPages(string fileName)
		{
			int pages = 0;

				// Cuenta el número de páginas
				try
				{
					PdfReader reader = new PdfReader(fileName);

						// Obtiene el número de páginas
						pages = reader.NumberOfPages;
						// Cierra el PDf
						reader.Close();
				}
				catch { }
				// Devuelve el número de páginas
				return pages;
		}
	}
}
