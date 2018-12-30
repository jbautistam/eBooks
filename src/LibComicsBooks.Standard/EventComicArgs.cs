using System;

namespace Bau.Libraries.LibComicsBooks
{
	/// <summary>
	///		Definici�n de eventos
	/// </summary>
	public class EventComicArgs
	{ 
		// Enumerados p�blicos
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
		///		Tipo de acci�n
		/// </summary>
		public ActionType Action { get; set; }

		/// <summary>
		///		P�gina actual
		/// </summary>
		public int Actual { get; set; }

		/// <summary>
		///		N�mero total de p�ginas
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		///		Mensaje
		/// </summary>
		public string Message { get; set; }
	}
}
