using System;
using System.Collections.Generic;

#nullable disable

namespace AnswersAPI_MitziVargasMejia.Models
{
    public partial class Ask
    {
        public Ask()
        {
            Answers = new HashSet<Answer>();
        }

        public long AskId { get; set; }
        public DateTime Date { get; set; }
        public string Ask1 { get; set; }
        public int UserId { get; set; }
        public int AskStatusId { get; set; }
        public bool? IsStrike { get; set; }

        public virtual AskStatus AskStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
