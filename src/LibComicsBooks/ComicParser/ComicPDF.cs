using System;
using System.IO;

using Bau.Libraries.LibPDFTools.PDF;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de c�mic a partir de un archivo PDF
	/// </summary>
	internal class ComicPDF : ComicBase
	{
		/// <summary>
		///		Limpia las p�ginas
		/// </summary>
		public override void Clear()
		{ 
			// Elimina los archivos de imagen
			Pages.Delete();
			// Limpia la colecci�n de im�genes
			Pages.Clear();
		}

		/// <summary>
		///		Comrpueba si un archivo es de este tipo de c�mic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return IsFileType(fileName, new string[] { ".pdf" });
		}

		/// <summary>
		///		Carga el c�mic
		/// </summary>
		internal override void Load(string fileName)
		{
			int pageIndex;

				// Limpia las p�ginas
				Clear();
				// Asigna el nombre de archivo
				FileName = fileName;
				// Obtiene el n�mero de p�ginas
				pageIndex = CountPages(fileName);
				// Crea la colecci�n
				for (int index = 0; index < pageIndex; index++)
					Pages.Add($"P�gina {index + 1}");
		}

		/// <summary>
		///		Cuenta las p�ginas de un PDF
		/// </summary>
		private int CountPages(string fileName)
		{
			return new PdfHelper().CountPages(fileName);
		}

		/// <summary>
		///		Carga la informaci�n del c�mic
		/// </summary>
		internal override void LoadInfo()
		{ 
			Info.ComicFileName = FileName;
			Info.Title = Path.GetFileName(FileName);
		}

		/// <summary>
		///		Descomprime un archivo
		/// </summary>
		public override void Uncompress(string path)
		{
			PDFExtractImages pdfExtract = new PDFExtractImages();

				// Limpia las p�ginas
				Clear();
				// Asigna el evento de tratamiento de proceso
				pdfExtract.Progress += (sender, objExtractArgs) =>
												{ 
													// Carga las p�ginas
													Pages.Add(objExtractArgs.FileName);
													Pages[Pages.Count - 1].Uncompressed = true;
													// Lanza el evento de progreso
													RaiseEvent(EventComicArgs.ActionType.Uncompress, objExtractArgs.ActualPage, objExtractArgs.PagesTotal);
												};
				pdfExtract.ProcessError += (sender, objExtractArgs) => RaiseEventError(objExtractArgs.ErrorMessage);
				// Extrae las im�genes
				try
				{
					pdfExtract.Extract(FileName, path);
				}
				catch (Exception exception)
				{
					base.RaiseEventError("Error al descomprimir el archivo " + FileName + Environment.NewLine + exception.Message);
				}
		}

		/// <summary>
		///		Guarda los archivos del c�mic en un archivo comprimido
		/// </summary>
		internal override void Save(string fileName, ComicPagesCollection pages, Definition.ComicInfo comicInfo)
		{
			System.Collections.Generic.List<string> files = new System.Collections.Generic.List<string>();

				// Pasa las p�ginas a la colecci�n de archivos
				foreach (ComicPage page in pages)
					files.Add(page.FileName);
				// Crea el PDF
				PDFFromImages.Create(fileName, files);
		}

		/// <summary>
		///		Tipo de archivo de c�mic
		/// </summary>
		public override ComicBook.ComicType Type
		{
			get { return ComicBook.ComicType.PDF; }
		}
	}
}
