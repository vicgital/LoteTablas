﻿namespace LoteTablas.Api.Models
{
    public record LotteryType
    {
        public int LotteryTypeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
