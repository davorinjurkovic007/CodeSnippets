using System.Xml.Linq;

// CreateProductDocument();
// CreateProductDocumentWithAttributes();
// CreateNestedXmlDocument();
// ParseStringIntoXDocument();
// ParseStringIntoXElement();
AddNewNode();

Console.ReadKey();

XDocument CreateProductDocument()
{
    XDocument doc = 
        new (
            new XDeclaration("1.0", "utf-8", "yes"),
            new XComment("Product Information"),
            new XElement("Products",
                new XElement("Product",
                    new XElement("ProductId", "1"),
                    new XElement("Name", "Bicycle Helmet"),
                    new XElement("ProductNumber", "HELM-01"),
                    new XElement("Color", "White"),
                    new XElement("StandardCost", "24.49"),
                    new XElement("ListPrice", "89.99"),
                    new XElement("Size", "Medium"))
            )
        );

    // Display the Document
    Console.WriteLine(doc);

    return doc;
}

XDocument CreateProductDocumentWithAttributes()
{
    XDocument doc = 
        new (
            new XDeclaration("1.0", "utf-8", "yes"),
            new XComment("Product Information"),
            new XElement("Products",
                new XElement("Product",
                    new XAttribute("ProductId", "1"),
                    new XElement("Name", "Bicycle Helmet"),
                    new XElement("ProductNumber", "HELM-01"),
                    new XElement("Color", "White"),
                    new XElement("StandardCost", "24.49"),
                    new XElement("ListPrice", "89.99"),
                    new XElement("Size", "Medium"))
            )
        );

    // Display the Document
    Console.WriteLine(doc);

    return doc;
}

XDocument CreateNestedXmlDocument()
{
    XDocument doc = 
        new (
            new XDeclaration("1.0", "utf-8", "yes"),
            new XComment("Product Information"),
            new XElement("Products",
                new XElement("Product",
                    new XAttribute("ProductId", "1"),
                    new XElement("Name", "Bicycle Helmet"),
                    new XElement("Sales",
                        new XElement("SalesDetail",
                            new XAttribute("SalesOrderId", "1"),
                            new XElement("OrderDate", Convert.ToDateTime("10/1/2021")))
                    )
                )
            )
        );

    // Display the Document
    Console.WriteLine(doc);

    return doc;
}

XDocument ParseStringIntoXDocument() {
  string xml = @"<Products>
                  <Product>
                    <ProductID>706</ProductID>
                    <Name>HL Road Frame - Red, 58</Name>
                    <ProductNumber>FR-R92R-58</ProductNumber>
                    <Color>Red</Color>
                    <StandardCost>1059.3100</StandardCost>
                    <ListPrice>1500.0000</ListPrice>
                    <Size>58</Size>
                  </Product>
                  <Product>
                    <ProductID>707</ProductID>
                    <Name>Sport-100 Helmet, Red</Name>   
                    <Color>Red</Color>
                    <StandardCost>13.0800</StandardCost>
                    <ListPrice>34.9900</ListPrice>
                    <Size />
                  </Product>
                </Products>";

  // Create XML Document using Parse()
  XDocument doc = XDocument.Parse(xml);

  // Display XML Document
  Console.WriteLine(doc);

  return doc;
}

XElement ParseStringIntoXElement() {
  string xml = CreateProductXmlString();

  // Create XML Document using Parse()
  XElement elem = XElement.Parse(xml);

  // Display XML Document
  Console.WriteLine(elem);

  return elem;
}

XDocument AddNewNode()
{
    // Get a Product XML string
    string xml = CreateProductXmlString();
    // Create XML Document using Parse()
    XDocument doc = XDocument.Parse(xml);

    // Create a new XElement object to add
    XElement elem = 
        new XElement("Product",
            new XElement("ProductID", "745"),
            new XElement("Name", "HL Mountain Frame"),
            new XElement("ProductNumber", "FR-M94B-48"),
            new XElement("Color", "Black"),
            new XElement("StandardCost", "699.09"),
            new XElement("ListPrice", "1349.6000"),
            new XElement("Size", "48")
            
        );

    //Add the new XElement object to the root
    doc.Root.Add(elem);
    
    // Display Document
    Console.WriteLine(doc);

    return doc;
}

string CreateProductXmlString()
{
return @"<Products>
        <Product>
            <ProductID>706</ProductID>
            <Name>HL Road Frame - Red, 58</Name>
            <ProductNumber>FR-R92R-58</ProductNumber>
            <Color>Red</Color>
            <StandardCost>1059.3100</StandardCost>
            <ListPrice>1500.0000</ListPrice>
            <Size>58</Size>
        </Product>
        <Product>
            <ProductID>707</ProductID>
            <Name>Sport-100 Helmet, Red</Name>   
            <Color>Red</Color>
            <StandardCost>13.0800</StandardCost>
            <ListPrice>34.9900</ListPrice>
            <Size />
        </Product>
        </Products>";
}



