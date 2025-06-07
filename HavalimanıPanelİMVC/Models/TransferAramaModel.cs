using System;
using System.Collections.Generic;

namespace HavalimaniPanelMVC.Models
{
    public class TransferAramaModel
    {
        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public DateTime TarihSaat { get; set; }

        public List<Transfer> Sonuclar { get; set; } = new List<Transfer>();
    }
}
