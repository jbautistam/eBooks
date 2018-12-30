using System;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de c�mic a partir de una imagen
	/// </summary>
	internal class ComicImage : ComicBase
	{
		/// <summary>
		///		Limpia las p�ginas
		/// </summary>
		public override void Clear()
		{
		}

		/// <summary>
		///		Comrpueba si un archivo es de este tipo de c�mic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return IsFileType(fileName, new string[] { ".jpg", ".png", ".gif", ".bmp", ".tiff", ".tif" });
		}

		/// <summary>
		///		Carga el c�mic
		/// </summary>
		internal override void Load(string fileName)
		{ 
			// Limpia la colecci�n de p�ginas
			Clear();
			// Cambia el nombre de archivo
			FileName = fileName;
			// Carga las p�ginas
			if (IsImage(fileName))
				Pages.Add(fileName);
		}

		/// <summary>
		///		Carga la informaci�n del c�mic
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
