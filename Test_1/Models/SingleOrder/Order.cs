using Newtonsoft.Json;
using Services.JsonConvertor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.SingleOrder
{
    public class Order
    {
        [Key]
        [JsonConverter(typeof(IntToStringConvertor))]
        public int Id { get; set; }
        public string dimension { get; set; }
        public string status { get; set; }
        [JsonIgnore]
        public int PickupId { get; set; }
        [ForeignKey("PickupId")]
        public Pickup Pickup { get; set; }
        [JsonIgnore]
        public int DropoffId { get; set; }
        [ForeignKey("DropoffId")]
        public Dropoff Dropoff { get; set; }
        
    }
}
