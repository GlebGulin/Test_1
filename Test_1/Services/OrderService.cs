using DAL;
using Microsoft.EntityFrameworkCore;
using Models.ResponseRequest;
using Models.SingleOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services
{
    public interface IOrderService 
    {
        IEnumerable<Object> GetAll(int? start, int? quantity, string status);
        Object GetById(int id);
        Object AddNew(JObject data);

    }
    public class OrderService : IOrderService
    {
        private readonly Db _db;
        public OrderService(Db db)
        {
            _db = db;
        }
        public IEnumerable<Object> GetAll(int? start, int? quantity, string status)
        {
           var result = new List<Order>();
           try
           {
               result = _db.orders.Where(x =>x.status.Equals(status)).Include(c => c.Dropoff).Include(d=>d.Pickup).ToList();
               
                if (status == null) { result = _db.orders.Include(c => c.Dropoff).Include(d => d.Pickup).ToList(); }
                if (result.Count == 0)
                {
                    var response = new List<Response>();
                    response.Add(new Response
                    {
                        code = 204,
                        message = "This response means that there are no orders matching the filters or no orders at all for this request",
                        details = ""
                    });

                    return response;
                }
                if (quantity.Equals(null) || start.Equals(null))
               {
                    return result;
               }
                 else
                {
                    int TotalPages = (int)Math.Ceiling(result.Count / (double)quantity);
                    int startPage = start.Value;
                    int requiredQuantity = quantity.Value;
                    if (startPage > TotalPages)
                    {

                        startPage = TotalPages;
                    }
                    
                    var requiredList = result.Skip((startPage - 1) * requiredQuantity).Take(requiredQuantity).ToList();
                   
                    return requiredList;
                }
                

                }
                catch (System.Exception)
                {
                    return null;
                }
           
            
        }
        public Object GetById(int id)
        {
            Order order;
            var checkOrder = _db.orders.Find(id);
            if (checkOrder == null)
            {
                return new Response { code = 135,
                    message = "Order not found",
                    details = "Cannot find order with id: " + id.ToString()
                };
            }
            try
            {
                 order = _db.orders.Include(x=>x.Pickup).Where(x=>x.Id.Equals(id)).FirstOrDefault();
                 return order;

            }
            catch (System.Exception)
            {
                return null;

            }
        }
        public Object AddNew(JObject data)
        {
            string resultJson = data.ToString();
            try
            {
                Order result = JsonConvert.DeserializeObject<Order>(resultJson);
                bool orderExist = _db.orders.Any(x => x.Id.Equals(result.Id));
                if (orderExist)
                {
                    return new Response
                    {
                        code = 421,
                        message = "Order exists",
                        details = "Order with such id already exists"
                    };
                }
                try
                {
                    var resultDropOff = _db.dropoffs.Last();
                    var resultPickup = _db.pickups.Last();
                    result.DropoffId = resultDropOff.Id + 1;
                    result.PickupId = resultPickup.Id + 1;
                    if (result.Dropoff == null)
                    {
                        result.Dropoff = new Dropoff(result.DropoffId, 0, 0);
                    }
                    if (result.Pickup == null)
                    {
                        result.Pickup = new Pickup(result.PickupId, 0, 0);
                    }
                    result.Pickup.Id = result.Pickup.Id + 1;
                    result.Dropoff.Id = resultDropOff.Id + 1;
                    _db.Add(result.Pickup);
                    _db.Add(result.Dropoff);
                    _db.Add(result);
                    _db.SaveChanges();
                    return result;
                }
                catch (System.Exception)
                {
                    return new Response
                    {
                        code = 422,
                        message = "",
                        details = "This response means that the incoming data were not validated and the platform cannot create an order for delivery"

                    };
                }
            }
            catch (System.Exception)
            {
                return new Response
                {
                    code = 422,
                    message = "",
                    details = "This response means that the incoming data were not validated and the platform cannot create an order for delivery"

                };
            }
        }
    }
}
