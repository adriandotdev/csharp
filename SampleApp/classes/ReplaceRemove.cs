public class ReplaceRemove {

    public ReplaceRemove() { 

        const string input = "<div><h2>Widgets &trade;</h2><span>5000</span></div>";

        string quantity = "";
        string output = "";


       
        var startIndexOfSpan = input.IndexOf("<span>");
        var endIndexOfSpan = input.LastIndexOf("</span>");
     
        var quantitySubstring = input.Substring(startIndexOfSpan, (endIndexOfSpan + 7 - startIndexOfSpan)).Replace("<span>", "").Replace("</span>", "");
        quantity = quantitySubstring;

        var startIndexOfHeading = input.IndexOf("<h2>");

        output = input.Substring(startIndexOfHeading, endIndexOfSpan + 7 - startIndexOfHeading).Replace("&trade", "&reg");
        Console.WriteLine(quantity);
        Console.WriteLine(output);
    }
}