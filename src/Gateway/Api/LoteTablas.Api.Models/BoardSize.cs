namespace LoteTablas.Api.Models
{
    public record BoardSize
    {
        public int BoardSizeID { get; set; }
        public string Code { get; set; } = string.Empty;
        public int XSize { get; set; }
        public int YSize { get; set; }
    }
}
