using System;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de cómic a partir de una imagen
	/// </summary>
	internal class ComicImage : ComicBase
	{
		/// <summary>
		///		Limpia las páginas
		/// </summary>
		public override void Clear()
		{
		}

		/// <summary>
		///		Comrpueba si un archivo es de este tipo de cómic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return IsFileType(fileName, new string[] { ".jpg", ".png", ".gif", ".bmp", ".tiff", ".tif" });
		}

		/// <summary>
		///		Carga el cómic
		/// </summary>
		internal override void Load(string fileName)
		{ 
			// Limpia la colección de páginas
			Clear();
			// Cambia el nombre de archivo
			FileName = fileName;
			// Carga las páginas
			if (IsImage(fileName))
				Pages.Add(fileName);
		}

		/// <summary>
		///		Carga la información del cómic
		/// </summary>
		internal override void LoadInfo()
		{
			Info.Title = System.IO.Path.GetFileName(FileName);
			Info.ComicFileName = FileName;
		}

		/// <summary>
		///		Descomprime un archivo (no hace nada, simplemente implementa el interface)
		/// </summary>
		public override void Uncompress(string path)
		{
		}

		public override ComicBook.ComicType Type
		{
			get { return ComicBook.ComicType.Path; }
		}
	}
}
