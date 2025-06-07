using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.AracOzelligi
{
    public class AracOzelligiRequest : SearchDto
    {
        public int? Id { get; set; }
        public string? OzellikAdi { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class AracOzelligiBaseRequest
    {
        public int Id { get; set; }
        public string OzellikAdi { get; set; }
        public bool AktifMi { get; set; }
    }
    public class AracOzelligiAdiRequest
    {
        public string OzellikAdi { get; set; }
    }

}
