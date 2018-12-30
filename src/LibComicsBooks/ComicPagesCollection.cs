using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibComicsBooks
{
	/// <summary>
	///		Colecci�n de <see cref="ComicPage"/>
	/// </summary>
	public class ComicPagesCollection : List<ComicPage>
	{
		/// <summary>
		///		A�ade un elemento a la colecci�n
		/// </summary>
		public void Add(string fileName)
		{
			Add(new ComicPage(fileName));
		}

		/// <summary>
		///		Borra los archivos de las p�ginas
		/// </summary>
		internal void Delete()
		{
			foreach (ComicPage page in this)
				LibCommonHelper.Files.HelperFiles.KillFile(page.FileName);
		}

		/// <summary>
		///		Obtiene el �ndice de una p�gina
		/// </summary>
		internal int GetPageIndex(string fileName)
		{
			for (int index = 0; index < Count; index++)
				if (this[index].FileName.Equals(fileName))
					return index;
			return -1;
		}

		/// <summary>
		///		Obtiene los archivos
		/// </summary>
		internal List<string> GetFiles()
		{
			List<string> files = new List<string>();

				// A�ade los archivos
				foreach (ComicPage page in this)
					if (!page.MarkAsDeleted)
						files.Add(page.FileName);
				// Devuelve la colecci�n de archivos
				return files;
		}
	}
}
