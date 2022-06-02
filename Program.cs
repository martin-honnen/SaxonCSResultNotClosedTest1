using Saxon.Api;

var xsl1 = "sheet1.xsl";
var xsl1Uri = new Uri(Path.Combine(Environment.CurrentDirectory, xsl1));

Processor processor = new();

XsltCompiler compiler = processor.NewXsltCompiler();
compiler.BaseUri = xsl1Uri;

XsltTransformer transformer = compiler.Compile(xsl1Uri).Load();

transformer.SetParameter(new QName("input-uri"), new XdmAtomicValue("sample1.xml"));
transformer.SetParameter(new QName("OS"), new XdmAtomicValue(Environment.OSVersion.ToString()));


transformer.InitialTemplate = new QName("http://www.w3.org/1999/XSL/Transform", "initial-template");
transformer.BaseOutputUri = xsl1Uri;

transformer.ResultDocumentHandler = (href, baseUri) => { 
    var serializer = processor.NewSerializer();
    var fs = File.OpenWrite(new Uri(baseUri, href).LocalPath);
    serializer.OutputStream = fs;
    serializer.OnClose(() => { fs.Close(); });
    return serializer;
};

// perform transformation
XdmDestination result = new(); // effectively unused
transformer.Run(result);

var xsl2 = "sheet2.xsl";
var xsl2Uri = new Uri(Path.Combine(Environment.CurrentDirectory, xsl2));


compiler = processor.NewXsltCompiler();
compiler.BaseUri = xsl2Uri;

transformer = compiler.Compile(xsl2Uri).Load();

transformer.SetParameter(new QName("input-uri"), new XdmAtomicValue("result1.xml"));
transformer.SetParameter(new QName("OS"), new XdmAtomicValue(Environment.OSVersion.ToString()));


transformer.InitialTemplate = new QName("http://www.w3.org/1999/XSL/Transform", "initial-template");
transformer.BaseOutputUri = xsl2Uri;

// perform transformation
result = new(); // effectively unused
transformer.Run(result);