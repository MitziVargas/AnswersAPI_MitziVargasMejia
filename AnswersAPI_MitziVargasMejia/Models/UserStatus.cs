using System;
using System.Collections.Generic;

#nullable disable

namespace AnswersAPI_MitziVargasMejia.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        public int UserStatuId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
