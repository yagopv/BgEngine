using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgEngine.Application.DTO
{
    public class SubscriptionDTO
    {
        public string SubscriberName { get; set; }
        public string SubscriberEmail { get; set; }
        public bool IsConfirmed { get; set; }
		public int PostId { get; set; }
	}				
}
