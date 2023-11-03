using System.ComponentModel.DataAnnotations;

namespace GenesisMegaDeskRazor.Models
{
    

    public class Desk
    {
        // Constants for width and depth constraints
        public const int MinWidth = 24;
        public const int MaxWidth = 96;
        public const int MinDepth = 12;
        public const int MaxDepth = 48;

        public int Id { get; set; }
        public string? Name { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int NumberOfDrawers { get; set; }
        [Display(Name = "Desktop Material")]
        public DesktopMaterial Material { get; set; }
        public int RushOrderDays { get; set; }
        public enum DesktopMaterial
        {
            [Display(Name = "Laminate")]
            Laminate = 100,

            [Display(Name = "Oak")]
            Oak = 200,

            [Display(Name = "Rosewood")]
            Rosewood = 300,

            [Display(Name = "Veneer")]
            Veneer = 125,

            [Display(Name = "Pine")]
            Pine = 50
        }
    }
}
