using System.Xml.Linq;
using System.Xml.Schema;

namespace XMLSamples {
  /// <summary>
  /// Demos of validating an XML document using an XML schema
  /// </summary>
  public class ValidateViewModel {
    public ValidateViewModel() {
      XmlFileName = FileNameHelper.ProductsFile;
      XsdFile = FileNameHelper.ProductsXsdFile;
    }

    private readonly string XmlFileName;
    private readonly string XsdFile;

    #region ValidateXml Method
    /// <summary>
    /// Write code to validate XML using an XSD file
    /// </summary>
    public XDocument ValidateXml() {
      XDocument doc = XDocument.Load(XmlFileName);

            // Create an XmlSchemaSet and add XSD file to it
            XmlSchemaSet set = new();
            set.Add("", XsdFile);

            // Validate the document
            doc.Validate(set, (sender, e) =>
            {
                Console.WriteLine(e.ToString());
            });

      // Display Message
      Console.WriteLine("XML is valid.");

      return doc;
    }
    #endregion

    #region ValidateXmlWithError Method
    /// <summary>
    /// Write code to create an invalid node, then validate the XML using an XSD file
    /// </summary>
    public XDocument ValidateXmlWithError() {
      XDocument doc = XDocument.Load(XmlFileName);

            // Create an XmlSchemaSet, add Xsd File Name
            XmlSchemaSet set = new();
            set.Add("", XsdFile);

            // Create an invalid XElement object to add
            XElement elem =
                    new("Customer",
                        new XElement("CustomerId", "999"),
                            new XElement("CustomerName", "InvalidCustomer"));
            doc.Root.AddFirst(elem);
      
      bool errors = false;
            // Validate the document
           
            doc.Validate(set, (sender, e) =>
            {
                // check for Error
                if (e.Severity == XmlSeverityType.Error) 
                {
                    errors = true;
                    Console.WriteLine($"Error: {e.Message}");
                }
                // Check for a warning
                if(e.Severity == XmlSeverityType.Warning)
                {
                    errors = true;
                    Console.WriteLine($"Warning: {e.Message}");
                }
            });

      if (!errors) {
        // Display Success Message
        Console.WriteLine("XML is valid.");
      }

      return doc;
    }
    #endregion
  }
}
