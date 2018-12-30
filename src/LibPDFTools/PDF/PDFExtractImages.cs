using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Dotnet = System.Drawing.Image;
using System.Drawing;
using System.IO;

using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;

namespace Bau.Libraries.LibPDFTools.PDF
{
	/// <summary>
	///		Extrae imágenes de un PDF
	/// </summary>
	public class PDFExtractImages
	{ 
		// Eventos
		public event EventHandler<EventArguments.PDFExtractProgressEventArgs> Progress;
		public event EventHandler<EventArguments.PDFExtractErrorEventArgs> ProcessError;

		/// <summary>
		///		Extrae las imágenes de un archivo
		/// </summary>
		public List<string> Extract(string fileName, string targetPath)
		{
			List<string> fileNames = new List<string>();
			PdfReader pdfReader = new PdfReader(fileName);

				// Crea el directorio de salida
				LibCommonHelper.Files.HelperFiles.MakePath(targetPath);
				// Recorre las páginas
				for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
					try
					{
						string fileTarget = GetFileName(targetPath, pageIndex);

							// Graba la página
							if (SaveImageFromPDF(pdfReader, fileTarget, pageIndex))
								fileNames.Add(fileTarget);
							// Lanza el evento de progreso
							OnProgress(pageIndex, pdfReader.NumberOfPages, fileTarget);
					}
					catch (Exception exception)
					{
						OnError(exception);
					}
				// Devuelve la lista de archivos creados
				return fileNames;
		}

		/// <summary>
		///		Graba la imagen de una página de un PDF
		/// </summary>
		private bool SaveImageFromPDF(PdfReader pdfReader, string fileTarget, int pageIndex)
		{
			bool saved = false;

				// Graba la imagen
				try
				{
					PdfDictionary pdfPage = pdfReader.GetPageN(pageIndex);
					PdfDictionary res = (PdfDictionary) PdfReader.GetPdfObject(pdfPage.Get(PdfName.RESOURCES));
					PdfDictionary xobj = (PdfDictionary) PdfReader.GetPdfObject(res?.Get(PdfName.XOBJECT));

						// Recorre los objetos de la página
						if (xobj != null)
							foreach (PdfName key in xobj.Keys)
							{
								PdfObject pdfObject = xobj.Get(key);

									if (pdfObject != null && pdfObject.IsIndirect())
									{
										PdfDictionary tg = (PdfDictionary) PdfReader.GetPdfObject(pdfObject);

											if (tg != null)
											{ 
												ImageRenderInfo imgRI = ImageRenderInfo.CreateForXObject(new GraphicsState(), (PRIndirectReference) pdfObject, tg);

													// Render de la imagen
													if (imgRI != null && RenderImage(imgRI, fileTarget))
														saved = true;
											}
									}
							}
				}
				catch { }
				// Devuelve el valor que indica si se ha grabado correctamente
				return saved;
		}

		/// <summary>
		///		Graba la imagen
		/// </summary>
		private bool RenderImage(ImageRenderInfo imgRenderInfo, string fileName)
		{
			bool render = false;
			PdfImageObject pdfImage = imgRenderInfo.GetImage();

				// Obtiene la imagen
				using (Dotnet dotnetImage = pdfImage.GetDrawingImage())
				{   
					// Si realmente existía una imagen
					if (dotnetImage != null)
						using (MemoryStream memory = new MemoryStream())
						{
							Bitmap target;

								// Vuelca la imagen en el stream en memoria
								dotnetImage.Save(memory, ImageFormat.Jpeg);
								// Copia la imagen
								target = new Bitmap(dotnetImage);
								// Graba la imagen
								target.Save(fileName);
								// Indica que se ha grabado
								render = true;
						}
				}
				// Devuelve el valor que indica si se ha grabado
				return render;
		}

		/// <summary>
		///		Lanza el evento <see cref="Progress"/>
		/// </summary>
		private void OnProgress(int actual, int total, string fileName)
		{
			Progress?.Invoke(this, new EventArguments.PDFExtractProgressEventArgs(actual, total, fileName));
		}

		/// <summary>
		///		Lanza el evento <see cref="ProcessError"/>
		/// </summary>
		private void OnError(Exception exception)
		{
			OnError(exception.Message);
		}

		/// <summary>
		///		Lanza el evento <see cref="ProcessError"/>
		/// </summary>
		private void OnError(string message)
		{
			ProcessError?.Invoke(this, new EventArguments.PDFExtractErrorEventArgs(message));
		}

		/// <summary>
		///		Obtiene el nombre de archivo
		/// </summary>
		private string GetFileName(string path, int pageIndex)
		{
			return System.IO.Path.Combine(path, $"{pageIndex:00000000}.jpg");
		}
	}
}