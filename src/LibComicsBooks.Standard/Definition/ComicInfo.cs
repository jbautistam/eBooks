using System;
using System.Xml;

namespace Bau.Libraries.LibComicsBooks.Definition
{
	/// <summary>
	///		Información de un cómic
	/// </summary>
	public class ComicInfo
	{ 
		// Constantes privadas
		private const string TagRoot = "ComicBook";
		// Enumerados públicos
		public enum AgeRating
		{
			Unknown,
			AllAges,
			Teens,
			ParentalAdvisory,
			ExplicitContent
		}

		/// <summary>
		///		Carga la información de un cómic de un archivo
		/// </summary>
		public void Load(string fileName)
		{
			XmlDocument xmlDocument = new XmlDocument();

				// Limpia el contenido
				Properties.Clear();
				// Carga el archivo
				xmlDocument.Load(fileName);
				// Carga el contenido del archivo
				foreach (XmlNode rootXML in xmlDocument.ChildNodes)
					if (rootXML.Name == TagRoot)
						foreach (XmlNode nodeXML in rootXML.ChildNodes)
							Properties.Add(nodeXML.Name, nodeXML.InnerText);
		}

		/// <summary>
		///		Graba la información en un archivo XML
		/// </summary>
		public void Save(string fileName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string xml = "";

				// Inicializa el nodo de propiedades
				xml = "<?xml version='1.0' encoding='utf-8'?>\r\n";
				xml += $"<{TagRoot}>\r\n";
				// Añade las propiedades
				foreach (ComicInfoProperty property in Properties)
				{
					xml += $"<{property.Name}>\r\n";
					xml += $"<![CDATA[{property.Value}]]>\r\n";
					xml += $"</{property.Name}>\r\n";
				}
				// Cierra el nodo
				xml += $"</{TagRoot}>\r\n";
				// Carga los nodos en el documento
				xmlDocument.LoadXml(xml);
				// Graba el documento
				xmlDocument.Save(fileName);
		}

		/// <summary>
		///		Propiedades del cómic
		/// </summary>
		public ComicInfoPropertiesCollection Properties { get; } = new ComicInfoPropertiesCollection();

		/// <summary>
		///		Nombre del archivo
		/// </summary>
		public string ComicFileName
		{
			get { return Properties[nameof(FileName)].Value; }
			set { Properties[nameof(FileName)].Value = value; }
		}

		/// <summary>
		///		Título del cómic
		/// </summary>
		public string Title
		{
			get { return Properties[nameof(Title)].Value; }
			set { Properties[nameof(Title)].Value = value; }
		}

		/// <summary>
		///		Resumen del cómic
		/// </summary>
		public string Summary
		{
			get { return Properties[nameof(Summary)].Value; }
			set { Properties[nameof(Summary)].Value = value; }
		}

		/// <summary>
		///		Notas del cómic
		/// </summary>
		public string Notes
		{
			get { return Properties[nameof(Notes)].Value; }
			set { Properties[nameof(Notes)].Value = value; }
		}

		/// <summary>
		///		Serie del cómic
		/// </summary>
		public string Serie
		{
			get { return Properties[nameof(Serie)].Value; }
			set { Properties[nameof(Serie)].Value = value; }
		}

		/// <summary>
		///		Número en la serie
		/// </summary>
		public string Number
		{
			get { return Properties[nameof(Number)].Value; }
			set { Properties[nameof(Number)].Value = value; }
		}

		/// <summary>
		///		Número total de cómics en la serie
		/// </summary>
		public string NumberTotal
		{
			get { return Properties[nameof(NumberTotal)].Value; }
			set { Properties[nameof(NumberTotal)].Value = value; }
		}

		/// <summary>
		///		Fecha de publicación
		/// </summary>
		public string DatePublish
		{
			get { return Properties[nameof(DatePublish)].Value; }
			set { Properties[nameof(DatePublish)].Value = value; }
		}

		/// <summary>
		///		Género
		/// </summary>
		public string Genre
		{
			get { return Properties[nameof(Genre)].Value; }
			set { Properties[nameof(Genre)].Value = value; }
		}

		/// <summary>
		///		Autor
		/// </summary>
		public string Author
		{
			get { return Properties[nameof(Author)].Value; }
			set { Properties[nameof(Author)].Value = value; }
		}

		/// <summary>
		///		Editorial
		/// </summary>
		public string Editorial
		{
			get { return Properties[nameof(Editorial)].Value; }
			set { Properties[nameof(Editorial)].Value = value; }
		}

		/// <summary>
		///		Dibujante
		/// </summary>
		public string Drawer
		{
			get { return Properties[nameof(Drawer)].Value; }
			set { Properties[nameof(Drawer)].Value = value; }
		}

		/// <summary>
		///		Editor
		/// </summary>
		public string Editor
		{
			get { return Properties[nameof(Editor)].Value; }
			set { Properties[nameof(Editor)].Value = value; }
		}

		/// <summary>
		///		Categorías
		/// </summary>
		public string Categories
		{
			get { return Properties[nameof(Categories)].Value; }
			set { Properties[nameof(Categories)].Value = value; }
		}

		/// <summary>
		///		Nombre de archivo
		/// </summary>
		internal string FileName
		{
			get { return "ComicBookInfo.xcbml"; }
		}
	}
}
