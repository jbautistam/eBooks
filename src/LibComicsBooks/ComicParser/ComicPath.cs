using System;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de c�mic a partir de un directorio
	/// </summary>
	internal class ComicPath : ComicBase
	{
		/// <summary>
		///		Limpia las p�ginas
		/// </summary>
		public override void Clear()
		{
			Pages.Clear();
		}

		/// <summary>
		///		Comprueba si un archivo es de este tipo de c�mic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return System.IO.Directory.Exists(fileName);
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
			LoadFiles(fileName, false);
		}

		/// <summary>
		///		Carga la informaci�n del c�mic
		/// </summary>
		internal override void LoadInfo()
		{
			base.Info.Title = base.FileName;
			base.Info.ComicFileName = base.FileName;
		}

		/// <summary>
		///		Carga los archivos de un directorio
		/// </summary>
		private void LoadFiles(string path, bool blnRecursive)
		{
			string[] masks = new string[] { ".jpg", ".gif", ".bmp", ".tif", ".png" };

				// Carga los archivos a partir de la m�scara
				foreach (string mask in masks)
				{
					string[] files = System.IO.Directory.GetFiles(path, "*" + mask);

						foreach (string file in files)
						{
							Pages.Add(file);
							Pages[Pages.Count - 1].Uncompressed = true;
						}
				}
		}

		/// <summary>
		///		Descomprime un archivo (no hace nada, simplemente implementa el interface)
		/// </summary>
		public override void Uncompress(string path)
		{
		}

		/// <summary>
		///		Tipo de c�mic
		/// </summary>
		public override ComicBook.ComicType Type
		{
			get { return ComicBook.ComicType.Path; }
		}
	}
}
