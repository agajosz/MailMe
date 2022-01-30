using System.Collections.Generic;
using MailMe.Data.Datastructure.ApiFootball.Fixtures;

namespace MailMe.Data.Datastructure.ApiFootball.ImportResponse
{
    public class ImportResponse
    {
        public string Get { get; set; }
        public Parameters Parameters { get; set; }
        public ICollection<object> Errors { get; set; }
        public int Results { get; set; }
        public Paging Paging { get; set; }
        public ICollection<Response> Response { get; set; }
    }
}