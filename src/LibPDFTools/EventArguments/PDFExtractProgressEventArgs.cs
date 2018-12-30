using System;

namespace Bau.Libraries.LibPDFTools.EventArguments
{
	/// <summary>
	///		Argumentos del evento de progreso
	/// </summary>
	public class PDFExtractProgressEventArgs : EventArgs
	{
		public PDFExtractProgressEventArgs(int actualPage, int pagesTotal, string fileName)
		{
			ActualPage = actualPage;
			PagesTotal = pagesTotal;
			FileName = fileName;
		}

		/// <summary>
		///		Página actual
		/// </summary>
		public int ActualPage { get; }

		/// <summary>
		///		Total de páginas
		/// </summary>
		public int PagesTotal { get; }

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		public string FileName { get; }
	}
}
