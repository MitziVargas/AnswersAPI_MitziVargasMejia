using System;
using System.Collections.Generic;

#nullable disable

namespace AnswersAPI_MitziVargasMejia.Models
{
    public partial class AskStatus
    {
        public AskStatus()
        {
            Asks = new HashSet<Ask>();
        }

        public int AskStatusId { get; set; }
        public string AskStatus1 { get; set; }

        public virtual ICollection<Ask> Asks { get; set; }
    }
}
