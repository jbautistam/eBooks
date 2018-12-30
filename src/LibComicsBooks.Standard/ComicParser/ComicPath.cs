using System;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de cómic a partir de un directorio
	/// </summary>
	internal class ComicPath : ComicBase
	{
		/// <summary>
		///		Limpia las páginas
		/// </summary>
		public override void Clear()
		{
			Pages.Clear();
		}

		/// <summary>
		///		Comprueba si un archivo es de este tipo de cómic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return System.IO.Directory.Exists(fileName);
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
			LoadFiles(fileName, false);
		}

		/// <summary>
		///		Carga la información del cómic
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

				// Carga los archivos a partir de la máscara
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
		///		Tipo de cómic
		/// </summary>
		public override ComicBook.ComicType Type
		{
			get { return ComicBook.ComicType.Path; }
		}
	}
}
