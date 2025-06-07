using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Rol
{
    public class RolRequest : SearchDto
    {
        public int? Id { get; set; }
        public string? Adi { get; set; }
    }

    public class RolBaseRequest
    {
        public int Id { get; set; }
        public string Adi { get; set; }
    }

    public class RolAdiRequest
    {
        public string Adi { get; set; }
    }
}
