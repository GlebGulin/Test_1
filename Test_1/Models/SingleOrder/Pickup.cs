using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.SingleOrder
{
    public class Pickup
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public Pickup(int  _Id, decimal _latitude, decimal _longitude)
        {
            Id = _Id;
            latitude = _latitude;
            longitude = _longitude;
        }
        public Pickup()
        {

        }
    }
}
