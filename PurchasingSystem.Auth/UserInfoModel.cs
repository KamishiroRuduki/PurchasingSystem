using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.Auth
{
    public class UserInfoModel
    {
        public string ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string PaymentProfile { get; set; }
        public int PaymentType { get; set; }
        public int BlackList { get; set; }

        public DateTime CreteDate { get; set; }
        public Guid UserGuid
        {
            get
            {
                if (Guid.TryParse(this.ID, out Guid tempGuid))
                {
                    return tempGuid;
                }
                else
                {
                    //return null;
                    return Guid.Empty;
                }
            }
        }
    }
}
