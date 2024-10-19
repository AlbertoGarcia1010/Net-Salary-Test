namespace Salary.App.Utils.Base.Response
{
    public class Metadata
    {
        public int Id { get; set; }
        public string? Msg { get; set; }

        public Metadata(int id, string msg)
        {
            Id = id;
            Msg = msg;
        }
    }
}
