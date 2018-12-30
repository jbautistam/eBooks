using System;

namespace Bau.Libraries.LibComicsBooks.ComicParser
{
	/// <summary>
	///		Clase base para los intérpretes de cómics
	/// </summary>
	internal abstract class ComicBase : IDisposable
	{ 
		// Eventos públicos
		internal event ComicBook.ComicActionHandler ComicAction;

		/// <summary>
		///		Obtiene una instancia del lector de cómics
		/// </summary>
		internal static ComicBase GetInstance(string fileName)
		{
			if (ComicCompressed.CheckIsComic(fileName))
				return new ComicCompressed();
			else if (ComicImage.CheckIsComic(fileName))
				return new ComicImage();
			else if (ComicPath.CheckIsComic(fileName))
				return new ComicPath();
			else if (ComicPDF.CheckIsComic(fileName))
				return new ComicPDF();
			else
				throw new Exception("Tipo desconocido");
		}

		/// <summary>
		///		Carga el cómic
		/// </summary>
		internal abstract void Load(string fileName);

		/// <summary>
		///		Descomprime el archivo
		/// </summary>
		public abstract void Uncompress(string path);

		/// <summary>
		///		Rutina de grabación de archivos
		/// </summary>
		internal virtual void Save(string fileName, ComicPagesCollection pages, Definition.ComicInfo comicInfo)
		{
		}

		/// <summary>
		///		Carga la información del cómic
		/// </summary>
		internal abstract void LoadInfo();

		/// <summary>
		///		Limpia las colecciones
		/// </summary>
		public abstract void Clear();

		/// <summary>
		///		Lanza un evento
		/// </summary>
		protected void RaiseEvent(EventComicArgs.ActionType action, int actual, int total)
		{
			ComicAction?.Invoke(this, new EventComicArgs(action, actual, total));
		}

		/// <summary>
		///		Lanza un evento de error
		/// </summary>
		internal void RaiseEventError(string message)
		{
			ComicAction?.Invoke(this, new EventComicArgs(EventComicArgs.ActionType.Error, 0, 0, message));
		}

		/// <summary>
		///		Comprueba si un archivo corresponde a un tipo de cómic
		/// </summary>
		internal static bool IsComic(string fileName)
		{
			return ComicImage.CheckIsComic(fileName) || ComicCompressed.CheckIsComic(fileName) ||
						   ComicPath.CheckIsComic(fileName) || ComicPDF.CheckIsComic(fileName);
		}

		/// <summary>
		///		Comprueba si un archivo es una imagen (o al menos tiene extensión de imagen)
		/// </summary>
		protected bool IsImage(string fileName)
		{
			return ComicImage.CheckIsComic(fileName);
		}

		/// <summary>
		///		Comprueba si un archivo es de un tipo
		/// </summary>
		internal static bool IsFileType(string fileName, string[] masks)
		{ 
			// Recorre las cadenas comprobando si la extensión del archivo es la de un cómic
			foreach (string mask in masks)
				if (fileName.EndsWith(mask, StringComparison.CurrentCultureIgnoreCase))
					return true;
			// Si ha llegado hasta aquí es porque no es un cómic
			return false;
		}

		/// <summary>
		///		Normaliza un nombre de archivo
		/// </summary>
		protected string NormalizeFileName(string fileName)
		{
			string charsTarget = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz_0123456789-.";
			string fileTarget = "";

				// Cambia los caracteres extraños
				foreach (char letter in fileName)
					if (charsTarget.IndexOf(letter) >= 0)
						fileTarget += letter;
					else
						fileTarget += "_";
				// Devuelve el nombre de archivo
				return fileTarget;
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		protected string FileName { get; set; }

		/// <summary>
		///		Páginas
		/// </summary>
		public ComicPagesCollection Pages { get; } = new ComicPagesCollection();

		/// <summary>
		///		Información del cómic
		/// </summary>
		public Definition.ComicInfo Info { get; } = new Definition.ComicInfo();

		/// <summary>
		///		Tipo de cómic
		/// </summary>
		public abstract ComicBook.ComicType Type { get; }

		/// <summary>
		///		Libera la memoria
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///		Libera la memoria
		/// </summary>
		private void Dispose(bool disposing)
		{
			if (disposing && Pages != null)
				Clear();
		}

		~ComicBase()
		{
			Dispose(false);
		}
	}
}
