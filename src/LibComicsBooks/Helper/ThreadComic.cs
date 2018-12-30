using System;

namespace Bau.Libraries.LibComicsBooks.Helper
{
	/// <summary>
	///		Clase para descompresión 
	/// </summary>
	internal class ThreadComic
	{
		internal ThreadComic(ComicParser.ComicBase comic, string path)
		{
			Comic = comic;
			Path = path;
		}

		/// <summary>
		///		Ejecuta el hilo de descompresión del archivo
		/// </summary>
		internal static void Execute(object info)
		{
			if (info is ThreadComic)
			{
				ThreadComic comic = info as ThreadComic;

					if (comic != null)
						comic.Comic.Uncompress(comic.Path);
			}
		}

		/// <summary>
		///		Cómic
		/// </summary>
		internal ComicParser.ComicBase Comic { get; }

		/// <summary>
		///		Directorio
		/// </summary>
		internal string Path { get; }
	}
}
