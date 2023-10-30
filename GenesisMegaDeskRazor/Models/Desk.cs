namespace GenesisMegaDeskRazor.Models
{
    public enum DesktopMaterial
    {
        Oak = 200,
        Laminate = 100,
        Pine = 50,
        Rosewood = 300,
        Veneer = 125
    }

    public class Desk
    {
        // Constants for width and depth constraints
        public const int MinWidth = 24;
        public const int MaxWidth = 96;
        public const int MinDepth = 12;
        public const int MaxDepth = 48;


        public int Id { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int NumberOfDrawers { get; set; }
        public DesktopMaterial DesktopMaterial { get; set; }
        public int RushOrderDays { get; set; }

        public Desk(int width, int depth, int numberOfDrawers, DesktopMaterial desktopMaterial, int rushOrderDays)
        {
            Width = width;
            Depth = depth;
            NumberOfDrawers = numberOfDrawers;
            DesktopMaterial = desktopMaterial;
            RushOrderDays = rushOrderDays;
        }
    }
}
