using System;
using System.Collections.Generic;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Bau.Libraries.LibPDFTools.PDF
{
	/// <summary>
	///		Clase para mezclar PDFs
	/// </summary>
	public static class PDFMerger
	{
		/// <summary>
		///		Mezcla una serie de archivos PDF
		/// </summary>
		public static bool Merge(string fileTarget, List<string> filesSource)
		{
			bool merged = false;

				// Crea el PDF de salida
				try
				{
					using (System.IO.FileStream file = new System.IO.FileStream(fileTarget, System.IO.FileMode.Create))
					{
						Document document = null;
						PdfWriter writer = null;

							// Recorre los archivos
							for (int indexFile = 0; indexFile < filesSource.Count; indexFile++)
							{
								PdfReader reader = new PdfReader(filesSource[indexFile]);
								int numberOfPages = reader.NumberOfPages;

									// La primera vez, inicializa el documento y el escritor
									if (indexFile == 0)
									{   
										// Asigna el documento y el generador
										document = new Document(reader.GetPageSizeWithRotation(1));
										writer = PdfWriter.GetInstance(document, file);
										// Abre el documento
										document.Open();
									}
									// Añade las páginas
									for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
									{
										int rotation = reader.GetPageRotation(pageIndex + 1);
										PdfImportedPage page = writer.GetImportedPage(reader, pageIndex + 1);

											// Asigna el tamaño de la página
											document.SetPageSize(reader.GetPageSizeWithRotation(pageIndex + 1));
											// Crea una nueva página
											document.NewPage();
											// Añade la página leída
											if (rotation == 90 || rotation == 270)
												writer.DirectContent.AddTemplate(page, 0, -1f, 1f, 0, 0,
																				 reader.GetPageSizeWithRotation(pageIndex + 1).Height);
											else
												writer.DirectContent.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
									}
							}
							// Cierra el documento
							if (document != null)
								document.Close();
							// Cierra el stream del archivo
							file.Close();
					}
					// Indica que se ha creado el documento
					merged = true;
				}
				catch (Exception exception)
				{
					System.Diagnostics.Debug.WriteLine(exception.Message);
				}
				// Devuelve el valor que indica si se han mezclado los archivos
				return merged;
		}
	}
}