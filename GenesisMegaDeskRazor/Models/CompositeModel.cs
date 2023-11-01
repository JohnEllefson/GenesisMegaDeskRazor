namespace GenesisMegaDeskRazor.Models
{
    public class CompositeModel
    {
        public Desk Desk { get; set; }
        public DeskQuote DeskQuote { get; set; }
        public Pages.DeskQuotes.IndexModel IndexModel { get; set; }
    }
}
