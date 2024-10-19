namespace Salary.Models.ViewModel
{
    public class UserSessionModel
    {
        public string SessionId { get; set; }
        public string Sessionid { get; }
        public string Guid { get; set; }

        public decimal UserId { get; set; }

        public string Nombres { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Puesto { get; set; }

        public string Email { get; set; }

        public string UsuarioClave { get; set; }

        public UserSessionModel()
        {

        }

        public UserSessionModel(string sessionid, string guid, decimal userId, string nombres, string puesto, string email, string usuarioClave)
        {
            Sessionid = sessionid;
            Guid = guid;
            UserId = userId;
            Nombres = nombres;
            Puesto = puesto;
            Email = email;
            UsuarioClave = usuarioClave;
        }

        public UserSessionModel(string sessionid, string guid, decimal userId, string nombres, string apellidoPaterno, string apellidoMaterno, string puesto, string email, string usuarioClave)
        {
            Sessionid = sessionid;
            Guid = guid;
            UserId = userId;
            Nombres = nombres;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Puesto = puesto;
            Email = email;
            UsuarioClave = usuarioClave;
        }
    }
}
