using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; } //Success, Failed, Pending payment
        public DateTime PaymentDate { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        #endregion

        #region Navigation Property
        public Order Order { get; set; }
        #endregion
    }
}
