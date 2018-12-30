using System;
using System.Collections.Generic;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Bau.Libraries.LibPDFTools.PDF
{
	/// <summary>
	///		Crea PDFs a partir de imágenes
	/// </summary>
	public static class PDFFromImages
	{
		/// <summary>
		///		Crea un PDF a partir de una colección de archivos de imagen
		/// </summary>
		public static void Create(string fileTarget, List<string> filesImage)
		{
			Document document = new Document(PageSize.A4, 0, 0, 0, 0);
			PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileTarget, FileMode.Create));

				// Abre el documento para escritura
				document.Open();
				// Escribe una imagen en cada página
				foreach (string fileName in filesImage)
					AddImage(document, pdfWriter, LoadImage(fileName));
				// Cierra el PDF (y el PdfWriter, si se ejecuta pdfWriter.Close() da un error en el stream de escritura)
				document.Close();
		}

		/// <summary>
		///		Crea un PDF a partir de los datos recibidos de un documento
		/// </summary>
		public static void Create(string fileName, List<System.Drawing.Image> images)
		{
			Document pdf = new Document(PageSize.A4, 0, 0, 0, 0);
			PdfWriter pdfWriter = PdfWriter.GetInstance(pdf, new FileStream(fileName, FileMode.Create));

				// Abre el documento para escritura
				pdf.Open();
				// Escribe una imagen en cada página
				foreach (System.Drawing.Image imageSource in images)
					AddImage(pdf, pdfWriter, imageSource);
				// Cierra el PDF (y el PdfWriter, si se ejecuta pdfWriter.Close() da un error en el stream de escritura)
				pdf.Close();
		}

		/// <summary>
		///		Añade una imagen a un PDF
		/// </summary>
		private static void AddImage(Document pdf, PdfWriter pdfWriter, System.Drawing.Image imageSource)
		{
			Image image = GetImage(imageSource);

				// Escala la imagen para que ocupe toda la página
				image.ScaleToFit(pdfWriter.PageSize.Width, pdfWriter.PageSize.Height);
				// Alinea la imagen
				image.Alignment = Element.ALIGN_MIDDLE | Element.ALIGN_CENTER;
				// Añade la imagen al PDF
				pdf.Add(image);
		}

		/// <summary>
		///		Carga una imagen de un archivo
		/// </summary>
		private static System.Drawing.Image LoadImage(string fileName)
		{
			return System.Drawing.Image.FromFile(fileName);
		}

		/// <summary>
		///		Obtiene una imagen para el PDF a partir de los una imagen de System.Drawing
		/// </summary>
		private static Image GetImage(System.Drawing.Image image)
		{
			return Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
		}
	}
}
