# FastReportApi
FastReportApi is a desktop web server API that use FastReport.Net to print, design and preview reports from JSON data. 

## Requirements
- Windows 10 or higher
- FastReport.Net 2022 or higher
- Net. Framework 3.61 or higher.
- Printer installed

## Format of json data
The default POST request URL is http://localhost:55677/imprimir.

- The port is configurable.
- The JSON request can be as shown in the following example:

```
{
"impresora":"Microsoft Print to PDF", // name of printer
"operacion":"previsualizar",          // have three options: imprimir|disenar|previsualizar
"formato":"report-example",           // name of frx report formart
....                                  // more fields
}
```
In this initial version, the request must be a single JSON object. 

## More details
This implementation was designed to print one page and use dot matrix printers, which use a print head that strikes an ink ribbon against the paper.

