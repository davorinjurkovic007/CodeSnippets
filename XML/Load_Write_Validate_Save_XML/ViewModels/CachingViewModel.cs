namespace XMLSamples {
  /// <summary>
  /// Read data from a database and store into an XML file
  /// </summary>
  public class CachingViewModel {
    public CachingViewModel() {
      // TODO: MODIFY YOUR FILE LOCATION
      //XmlFileName = @"D:\Samples\Products.xml";

            string path = Directory.GetCurrentDirectory();

            XmlFileName = (path + "\\Products.xml");
        }

    private readonly string XmlFileName;

    #region GetData Method
    /// <summary>
    /// Write code to see if an XML file exists. If not, read from a database and store to XML file. If it does, read data from the XML file.
    /// </summary>
    public virtual List<Product> GetData() {
      List<Product> products = new();
      string xml = null;

      // Attempt to read local XML file
      if(File.Exists(XmlFileName))
            {
                xml = File.ReadAllText(XmlFileName);
            }

      // Check to see if we got any data
      if (!string.IsNullOrEmpty(xml)) {
                // Deserialize products from XML
                products = products.Deserialize(xml);

        Console.WriteLine("Read from XML File");
      } else {
        // Get data from server        
        using(XMLSamplesDbContext db = new())
                {
                    products = db.Products.ToList();
                }

        if (products.Count > 0) {
          // Serialize Collection
          xml = products.Serialize();

          // Write XML to local file
          File.WriteAllText(XmlFileName, xml);

          Console.WriteLine($"Read Product Data from Database Server and Stored in '{XmlFileName}'");
        } else {
          Console.WriteLine("No records retrieved from the server");
        }
      }

      return products;
    }
    #endregion
  }
}
