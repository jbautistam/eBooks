using System;
using System.IO;

using Bau.Libraries.LibPDFTools.PDF;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de cómic a partir de un archivo PDF
	/// </summary>
	internal class ComicPDF : ComicBase
	{
		/// <summary>
		///		Limpia las páginas
		/// </summary>
		public override void Clear()
		{ 
			// Elimina los archivos de imagen
			Pages.Delete();
			// Limpia la colección de imágenes
			Pages.Clear();
		}

		/// <summary>
		///		Comrpueba si un archivo es de este tipo de cómic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return IsFileType(fileName, new string[] { ".pdf" });
		}

		/// <summary>
		///		Carga el cómic
		/// </summary>
		internal override void Load(string fileName)
		{
			int pageIndex;

				// Limpia las páginas
				Clear();
				// Asigna el nombre de archivo
				FileName = fileName;
				// Obtiene el número de páginas
				pageIndex = CountPages(fileName);
				// Crea la colección
				for (int index = 0; index < pageIndex; index++)
					Pages.Add($"Página {index + 1}");
		}

		/// <summary>
		///		Cuenta las páginas de un PDF
		/// </summary>
		private int CountPages(string fileName)
		{
			return new PdfHelper().CountPages(fileName);
		}

		/// <summary>
		///		Carga la información del cómic
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

				// Limpia las páginas
				Clear();
				// Asigna el evento de tratamiento de proceso
				pdfExtract.Progress += (sender, objExtractArgs) =>
												{ 
													// Carga las páginas
													Pages.Add(objExtractArgs.FileName);
													Pages[Pages.Count - 1].Uncompressed = true;
													// Lanza el evento de progreso
													RaiseEvent(EventComicArgs.ActionType.Uncompress, objExtractArgs.ActualPage, objExtractArgs.PagesTotal);
												};
				pdfExtract.ProcessError += (sender, objExtractArgs) => RaiseEventError(objExtractArgs.ErrorMessage);
				// Extrae las imágenes
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
		///		Guarda los archivos del cómic en un archivo comprimido
		/// </summary>
		internal override void Save(string fileName, ComicPagesCollection pages, Definition.ComicInfo comicInfo)
		{
			System.Collections.Generic.List<string> files = new System.Collections.Generic.List<string>();

				// Pasa las páginas a la colección de archivos
				foreach (ComicPage page in pages)
					files.Add(page.FileName);
				// Crea el PDF
				PDFFromImages.Create(fileName, files);
		}

		/// <summary>
		///		Tipo de archivo de cómic
		/// </summary>
		public override ComicBook.ComicType Type
		{
			get { return ComicBook.ComicType.PDF; }
		}
	}
}
