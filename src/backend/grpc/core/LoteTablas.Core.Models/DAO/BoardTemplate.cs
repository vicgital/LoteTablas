namespace LoteTablas.Core.Models.DAO
{
    public class BoardTemplate
    {
        public int BoardHtmlTemplateID { get; set; }
        public string? Name { get; set; }
        public int BoardSizeID { get; set; }
        public bool HasWatermark { get; set; }
        public string? Html { get; set; }
    }
}
