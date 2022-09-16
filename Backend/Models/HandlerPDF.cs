using IronPdf;
namespace Backend.Models;

public class HandlerPDF
{
    // private string topClient = ""
    private static string footer = "<center><i>{page} of {total-pages}<i></center>";
    private static string header = "<center><i>TALLER TEC. {date} at {time}<i></center>";
    // private string beforeBody = 
    
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
    
        PDF.SaveAs(filename);
    }


    private static string makeTitle(string title)
    {
        return "<h1><center>" + title + "</center></h1>";
    }

    public static string buildTopVehiclesBody(Dictionary<string,int> topVehicles){
        string body = "<p>A continuación se muestra, de forma descendente, la placa de los vehículos que más frecuentan TallerTec, junto a la cantidd de veces que han sido atendidos en el taller.";
        foreach (KeyValuePair<string, int> vehicle in topVehicles)
        {   
            body += "<p>" + vehicle.Key + " : " + vehicle.Value + "</p>";
        }
        body += "</p>";
        return body;
    }

    public static void buildTopVehiclesPDF(string title, Dictionary<string,int> topVehicles)
    {   
        string filename = "TopVehicles.pdf";
        //title
        title = makeTitle(title);
        //body
        string body = buildTopVehiclesBody(topVehicles);
        string text = title + body;
        GeneratePDF(text,filename);
    }
}