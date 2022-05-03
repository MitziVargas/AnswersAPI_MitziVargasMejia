using System;
using System.Collections.Generic;

#nullable disable

namespace AnswersAPI_MitziVargasMejia.Models
{
    public partial class View1
    {
        public string UserName { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string UserRole { get; set; }
        public int SenderId { get; set; }
    }
}
