using IronPdf;
namespace Backend.Models;

/// <summary>
/// Class for creating PDF files.
/// </summary>
public class HandlerPDF
{
    // private string topClient = ""
    private static string footer = "<center><i>{page} of {total-pages}<i></center>";
    private static string header = "<center><i>TALLER TEC. {date} at {time}<i></center>";
    // private string beforeBody = 

    /// <summary>
    /// Creates a PDF file.
    /// </summary>
    /// <param name="text">The text to write in the PDF file.</param>
    /// <param name="filename">The name of the PDF file.</param>
    private static void GeneratePDF(string text, string filename)
    {
        //PDF render
        var Renderer = new ChromePdfRenderer();

        //header
        Renderer.RenderingOptions.HtmlHeader = new HtmlHeaderFooter()
        {
            Height = 20,
            HtmlFragment = header,
            DrawDividerLine = true
        };
        //footer
        Renderer.RenderingOptions.HtmlFooter = new HtmlHeaderFooter()
        {
            Height = 15,
            HtmlFragment = footer,
            DrawDividerLine = true
        };

        using var PDF = Renderer.RenderHtmlAsPdf(text);

        PDF.SaveAs($"Reports/{filename}");
    }

    /// <summary>
    /// Text into HTML string
    /// </summary>
    /// <param name="title">The title of the PDF file.</param>
    private static string makeTitle(string title)
    {
        return "<h1><center>" + title + "</center></h1>";
    }

    /// <summary>
    /// Builds the body of the PDF Top Vehicles.
    /// </summary>
    /// <param name="topVehicles">The top vehicles to write in the PDF file.</param>
    private static string buildTopVehiclesBody(Dictionary<string, int> topVehicles)
    {
        string body = "<p>A continuación, se muestra, de forma descendente, la placa de los vehículos que más frecuentan TallerTec, junto a la cantidad de veces que han sido atendidos en el taller.";
        foreach (KeyValuePair<string, int> vehicle in topVehicles)
        {
            body += "<p>" + vehicle.Key + " : " + vehicle.Value + "</p>";
        }
        body += "</p>";
        return body;
    }


    /// <summary>
    /// Builds the body of the PDF Top Clients.
    /// </summary>
    /// <param name="topClients">The top Clients to write in the PDF file.</param>
    private static string buildTopClientsBody(Dictionary<int, int> topClients)
    {
        string body = "<p>A continuación, se muestra, de forma descendente, los clientes que más frecuentan TallerTec, junto a la cantidad de veces que han sido atendidos en el taller.";
        foreach (KeyValuePair<int, int> client in topClients)
        {
            body += "<p>" + client.Key + " : " + client.Value + "</p>";
        }
        body += "</p>";
        return body;
    }

    /// <summary>
    /// Builds the body of the sales per branch.
    /// </summary>
    /// <param name="salesPerBranch">The sales per branch to write in the PDF file.</param>
    /// <param name="fromDate">The start date of the sales.</param>
    /// <param name="toDate">The end date of the sales.</param>
    private static string buildSalesPerBranchBody(string fromDate, string toDate, Dictionary<int, int> salesPerBranch)
    {
        string body = "<p>A continuación, se muestra, de forma descendente, la sucursal que más ventas ha realizado entre el " + fromDate + " y el " + toDate + ", junto a la cantidad de ventas realizadas.";
        foreach (KeyValuePair<int, int> branch in salesPerBranch)
        {
            body += "<p>" + branch.Key + " : " + branch.Value + "</p>";
        }
        body += "</p>";
        return body;
    }

    /// <summary>
    /// Builds the body of the top most frequent vehicles
    /// </summary>
    /// <param name="topVehicles">The top vehicles to write in the PDF file.</param>
    /// <param name="title">The title of the PDF file.</param>
    public static void buildTopVehiclesPDF(string title, Dictionary<string, int> topVehicles)
    {
        string filename = "TopVehicles.pdf";
        //title
        title = makeTitle(title);
        //body
        string body = buildTopVehiclesBody(topVehicles);
        string text = title + body;
        GeneratePDF(text, filename);
    }

    /// <summary>
    /// Builds the body of the top most frequent clients
    /// </summary>
    /// <param name="topClients">The top clients to write in the PDF file.</param>
    /// <param name="title">The title of the PDF file.</param>
    public static void bulidTopClientsPDF(string title, Dictionary<int, int> topClients)
    {
        string filename = "TopClients.pdf";
        //title
        title = makeTitle(title);
        //body
        string body = buildTopClientsBody(topClients);
        string text = title + body;
        GeneratePDF(text, filename);
    }

    public static void buildSalesPerBranchPDF(string title, string fromDate, string toDate, Dictionary<int, int> salesPerBranch)
    {
        string filename = "SalesPerBranch.pdf";
        //title
        title = makeTitle(title);
        //body
        string body = buildSalesPerBranchBody(fromDate, toDate, salesPerBranch);
        string text = title + body;
        GeneratePDF(text, filename);
    }

    public static void buildBillPDF(Appointment appointment)
    {
        Client attendedClient = Client.SelectClient(appointment.AttendedClientID);
        Employee mechanic = Employee.SelectEmployee(appointment.MechanicID);
        Employee assistant = Employee.SelectEmployee(appointment.AssistantID);
        Branch branch = Branch.SelectBranch(appointment.BranchID);
        Service requiredService = Service.SelectService(appointment.RequiredService);
        Product[] necessaryParts = appointment.NecessaryParts
            .Select(productID => Product.SelectProduct(productID)).ToArray<Product>();

        int partsPrice = necessaryParts.Sum(product => product.Price);
        int totalPrice = partsPrice + requiredService.Price;

        var billHTML = @"
            <html>
                <head>
                    <style>
                        table, th, td {
                            border: 1px solid black;
                            border-collapse: collapse;
                        }
                        th, td {
                            padding: 5px;
                            text-align: left;
                        }
                    </style>
                </head>
                <body>
                    <h1><center>Factura</center></h1>
                    <p>Fecha: " + appointment.Date + @"</p>
                    <p>Hora: " + appointment.Time + @"</p>
                    <p>Cliente: " + attendedClient.Name + @"</p>
                    <p>Placa: " + appointment.LicensePlate + @"</p>
                    <p>Mecánico encargado: " + mechanic.Name + @"</p>
                    <p>Mecánico asistente: " + assistant.Name + @"</p>
                    <p>Sucursal: " + branch.Name + @"</p>
                    <p>Servicio: " + requiredService.Name + @"</p>
                    <p>Costo del servicio: " + requiredService.Price + @"</p>
                    <p>Partes necesarias: </p>
                    <ul>
                        " + string.Join("\n", necessaryParts.Select(part =>
                            "<li>" +
                                "<div>" +
                                    "<p>Nombre del repuesto: " + part.Price + @"</p>" +
                                    "<p>Marca: " + part.Brand + @"</p>" +
                                    "<p>Precio: " + part.Price + @"</p>" +
                                "</div>" +
                            "</li>"
                        )) + @"
                    </ul>
                    <p>Costo de las piezas: " + partsPrice + @"</p>
                    <p>Costo total: " + totalPrice + @"</p>
                </body>
        ";

        var Renderer = new IronPdf.ChromePdfRenderer();

        var PDF = Renderer.RenderHtmlAsPdf(billHTML);

        PDF.SaveAs("Reports/Bill.pdf");
        // Bill newBill = new Bill(
        //     appointment.ID,
        //     branch.ID,
        //     requiredService.Price,
        //     necessaryParts.Sum(product => product.Price)
        // );

        // Bill.InsertBill(newBill);

        Console.WriteLine("Bill generated successfully");
    }
}