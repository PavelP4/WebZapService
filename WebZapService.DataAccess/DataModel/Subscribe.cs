using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebZapService.DataAccess.DataModel
{
    public class Subscribe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Account_Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string API_Key { get; set; }

        [Required]
        public string Subscription_URL { get; set; }

        [Required]        
        public string Target_URL { get; set; }

        [Required]
        [MaxLength(100)]
        public string Event { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public bool IsUnsubscribed { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
                
        public int Country_Id { get; set; }

        public int Device_Id { get; set; }
    }
}
