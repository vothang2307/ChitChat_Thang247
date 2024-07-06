using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChiChat.Data
{
    public class TodoModels
    {
        public int Id { get; set; }

        public string Todo { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; }
    }
}
