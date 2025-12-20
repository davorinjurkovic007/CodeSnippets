using XMLSamples;

// Create instance of view model
//LoadingViewModel vm = new();
SaveViewModel vm = new();

// Call Sample Method
//vm.LoadUsingXDocument();
//vm.LoadUsingXElement();
//vm.GetFirstNodeUsingXDocument();
//vm.GetFirstNodeUsingXElement();

//vm.SaveUsingXDocument();
//vm.SaveUsingXmlWriter();
vm.XmlWriterFormattingSave();

// Stop console to view results
Console.ReadKey();