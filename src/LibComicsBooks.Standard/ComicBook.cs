using System;

using Bau.Libraries.LibComicsBooks.ComicParser;

namespace Bau.Libraries.LibComicsBooks
{
	/// <summary>
	///		Clase con los datos de un c�mic
	/// </summary>
	public class ComicBook
	{ 
		// Enumerados p�blicos
		public enum ComicType
		{
			Unknown,
			CBR,
			CBT,
			CBZ,
			Path,
			Image,
			PDF
		}
		// Delegados p�blicos
		public delegate void ComicActionHandler(object sender, EventComicArgs evnArgs);
		// Eventos p�blicos
		public event ComicActionHandler ComicAction;

		/// <summary>
		///		A�ade el manejador de eventos
		/// </summary>
		private void AddEventsHandler()
		{
			if (Comic != null)
				Comic.ComicAction += new ComicActionHandler(comic_ComicAction);
		}

		/// <summary>
		///		Trata el evento
		/// </summary>
		private void comic_ComicAction(object sender, EventComicArgs evnArgs)
		{
			RaiseEvent(evnArgs);
		}

		/// <summary>
		///		Lanza un evento
		/// </summary>
		private void RaiseEvent(EventComicArgs evnArgs)
		{
			ComicAction?.Invoke(this, evnArgs);
		}

		/// <summary>
		///		Lanza el evento de fin
		/// </summary>
		private void RaiseEventEnd()
		{
			RaiseEvent(new EventComicArgs(EventComicArgs.ActionType.End, 0, 0));
		}

		/// <summary>
		///		Graba un archivo
		/// </summary>
		public void Save(string fileName, ComicPagesCollection pages, Definition.ComicInfo comicInfo)
		{
			ComicBase newComic = ComicBase.GetInstance(fileName);

				// Si no se ha encontrado ning�n objeto apropiado para la extensi�n, lo graba como CBZ
				if (newComic is ComicPath)
					newComic = new ComicCompressed();
				// A�ade el manejador de eventos
				newComic.ComicAction += new ComicActionHandler(comic_ComicAction);
				// Graba el archivo
				newComic.Save(fileName, pages, comicInfo);
				// Lanza el evento de fin
				RaiseEventEnd();
		}

		/// <summary>
		///		Carga un archivo de c�mic
		/// </summary>
		public void Load(string fileName)
		{ 
			// Si ya exist�a un c�mic, lo limpia
			if (Comic != null)
			{
				Comic.ComicAction -= comic_ComicAction;
				Comic.Clear();
			}
			// Guarda el nombre de archivo
			FileName = fileName;
			// Obtiene la instancia del c�mic
			Comic = ComicBase.GetInstance(FileName);
			Comic.ComicAction += new ComicActionHandler(comic_ComicAction);
			// Carga el c�mic
			Comic.Load(FileName);
			Comic.Pages.Sort();
			// Carga la informaci�n del c�mic
			Comic.LoadInfo();
			// Lanza el evento
			RaiseEventEnd();
		}

		/// <summary>
		///		Descomprime un archivo
		/// </summary>
		public void Uncompress(string path, bool atThread)
		{ 
			// Limpia las p�ginas del comic
			Comic.Clear();
			// Descomprime
			if (atThread)
			{
				Helper.ThreadComic thread = new Helper.ThreadComic(Comic, path);

					if (!System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Helper.ThreadComic.Execute),
																	   thread))
						throw new Exception("No se puede a�adir el hilo a la cola de procesos");
			}
			else
				Comic.Uncompress(path);
		}

		/// <summary>
		///		Comprueba si un archivo es un c�mic
		/// </summary>
		public static bool IsComic(string fileName)
		{
			return ComicBase.IsComic(fileName);
		}

		/// <summary>
		///		Obtiene el tipo de c�mic de un archivo
		/// </summary>
		public static ComicType GetComicType(string fileName)
		{
			if (ComicCompressed.IsComic(fileName))
				return ComicType.CBZ;
			else if (ComicImage.IsComic(fileName))
				return ComicType.Image;
			else if (ComicPath.IsComic(fileName))
				return ComicType.Path;
			else
				return ComicType.Unknown;
		}

		public string FileName { get; set; }

		private ComicBase Comic { get; set; }

		public Definition.ComicInfo Info
		{
			get
			{
				if (Comic == null)
					return null;
				else
					return Comic.Info;
			}
		}

		public ComicPagesCollection Pages
		{
			get
			{
				if (Comic != null)
					return Comic.Pages;
				else
					return null;
			}
		}

		public ComicType Type
		{
			get
			{
				if (Comic == null)
					return ComicType.Unknown;
				else
					return Comic.Type;
			}
		}
	}
}
