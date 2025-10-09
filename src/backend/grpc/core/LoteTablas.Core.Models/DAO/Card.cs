namespace LoteTablas.Core.Models.DAO
{
    public class Card
    {
        public int CardID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageSmallPath { get; set; }
        public string? ImageMediumPath { get; set; }
        public string? ImageBigPath { get; set; }
        public int CreatorUserID { get; set; }
    }
}
