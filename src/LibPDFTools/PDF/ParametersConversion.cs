using System;

namespace Bau.Libraries.LibPDFTools.PDF
{
	/// <summary>
	///		Parámetros de conversión 
	/// </summary>
	public class ParametersConversion
	{
		/// <summary>
		///		Formato de salida de la imagen
		/// </summary>
		public string OutputFormat { get; set; }

		/// <summary>
		///		Tamaño máximo de la imagen
		/// </summary>
		public int MaxBitmap { get; set; }

		/// <summary>
		///		Tamaño máximo del buffer
		/// </summary>
		public int MaxBuffer { get; set; }

		/// <summary>
		///		Calidad de compresión del JPG
		///	</summary>
		public int JPEGQuality { get; set; }

		/// <summary>
		///		Indica si se debe escribir cada página en un archivo diferente
		///	</summary>
		public bool OutputToMultipleFile { get; set; }
	}
}
