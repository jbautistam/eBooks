using System;

namespace Bau.Libraries.LibComicsBooks
{
	/// <summary>
	///		Definición de eventos
	/// </summary>
	public class EventComicArgs
	{ 
		// Enumerados públicos
		public enum ActionType
		{
			Unknown,
			Read,
			Compress,
			Error,
			Uncompress,
			End
		}

		public EventComicArgs(ActionType action, int actual, int total) : this(action, actual, total, "") { }

		public EventComicArgs(ActionType action, int actual, int total, string message)
		{
			Action = action;
			Actual = actual;
			Total = total;
			Message = message;
		}

		/// <summary>
		///		Tipo de acción
		/// </summary>
		public ActionType Action { get; set; }

		/// <summary>
		///		Página actual
		/// </summary>
		public int Actual { get; set; }

		/// <summary>
		///		Número total de páginas
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; set; }
	}
}
