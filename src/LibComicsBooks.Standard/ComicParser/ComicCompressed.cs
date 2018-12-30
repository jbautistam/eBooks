using System;

using Bau.Libraries.LibCompressor;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Lector de cómic a partir de un archivo CBZ, CBR, CBT...
	/// </summary>
	internal class ComicCompressed : ComicBase
	{ 
		// Variables privadas
		private static string[] FilesTar = new string[] { ".cbt", ".tar" };
		private static string[] FilesRar = new string[] { ".cbr", ".rar" };
		private static string[] FilesZip = new string[] { ".cbz", ".zip" };

		/// <summary>
		///		Limpia las páginas
		/// </summary>
		public override void Clear()
		{ 
			// Elimina los archivos de imagen
			// Pages.Delete();
			// Limpia la colección de imágenes
			Pages.Clear();
		}

		/// <summary>
		///		Comrpueba si un archivo es de este tipo de cómic
		/// </summary>
		internal static bool CheckIsComic(string fileName)
		{
			return new ComicCompressed().GetComicType(fileName) != ComicBook.ComicType.Unknown;
		}

		/// <summary>
		///		Carga el cómic
		/// </summary>
		internal override void Load(string fileName)
		{
			Compressor compressor = new Compressor();
			System.Collections.Generic.List<string> files = new System.Collections.Generic.List<string>();

				// Guarda el nombre de archivo
				FileName = fileName;
				// Obtiene los nombres de archivos comprimidos
				files = compressor.ListFiles(FileName);
				// Añade los archivos a la colección de páginas
				foreach (string file in files)
					if (IsImage(file))
						Pages.Add(file);
		}

		/// <summary>
		///		Obtiene el tipo de cómic
		/// </summary>
		private ComicBook.ComicType GetComicType(string fileName)
		{ 
			// Obtiene el tipo de cómic
			if (!string.IsNullOrEmpty(fileName))
			{
				if (IsFileType(fileName, FilesTar))
					return ComicBook.ComicType.CBT;
				else if (IsFileType(fileName, FilesRar))
					return ComicBook.ComicType.CBR;
				else if (IsFileType(fileName, FilesZip))
					return ComicBook.ComicType.CBZ;
			}
			// Si ha llegado hasta aquí es porque no sabe el tipo de cómic
			return ComicBook.ComicType.Unknown;
		}

		/// <summary>
		///		Obtiene el tipo de compresión
		/// </summary>
		private Compressor.CompressType GetCompressType()
		{
			if (GetComicType(FileName) == ComicBook.ComicType.CBT)
				return Compressor.CompressType.Tar;
			else if (GetComicType(FileName) == ComicBook.ComicType.CBR)
				return Compressor.CompressType.Rar;
			else if (GetComicType(FileName) == ComicBook.ComicType.CBZ)
				return Compressor.CompressType.Zip;
			else
				return Compressor.CompressType.Unknown;
		}

		/// <summary>
		///		Carga la información del cómic
		/// </summary>
		internal override void LoadInfo()
		{
		}

		/// <summary>
		///		Descomprime un archivo
		/// </summary>
		public override void Uncompress(string path)
		{
			Compressor compressor = new Compressor();

				// Asigna el manejador de eventos
				compressor.Progress += (sender, eventProgress) =>
												{ 
													// Añade la imagen a la colección de páginas
													if (IsImage(eventProgress.FileName))
													{
														Pages.Add(eventProgress.FileName);
														Pages[Pages.Count - 1].Uncompressed = true;
													}
													// Lanza el evento
													RaiseEvent(EventComicArgs.ActionType.Uncompress, Pages.Count, eventProgress.Actual + 2);
												};
				// Descomprime el archivo
				try
				{
					compressor.Uncompress(FileName, path);
				}
				catch
				{
					RaiseEvent(EventComicArgs.ActionType.Error, 0, 0);
				}
				// Manda el mensaje de fin
				RaiseEvent(EventComicArgs.ActionType.End, Pages.Count, Pages.Count);
		}

		/// <summary>
		///		Guarda los archivos del cómic en un archivo comprimido
		/// </summary>
		internal override void Save(string fileName, ComicPagesCollection pages, Definition.ComicInfo comicInfo)
		{
			Compressor.CompressType type = GetCompressType();

				// Comprime el archivo
				if (type == Compressor.CompressType.Zip)
				{
					Compressor compressor = new Compressor();

						// Graba el archivo comprimido
						compressor.Compress(fileName, pages.GetFiles(), type);
				}
		}

		/// <summary>
		///		Tipo de cómic
		/// </summary>
		public override ComicBook.ComicType Type
		{
			get { return GetComicType(FileName); }
		}
	}
}
