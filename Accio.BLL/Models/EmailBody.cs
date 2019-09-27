namespace Accio.BLL.Models
{
    public class EmailBody
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsHtml { get; set; }
    }
}