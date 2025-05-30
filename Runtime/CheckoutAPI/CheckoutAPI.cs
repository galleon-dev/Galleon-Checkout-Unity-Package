using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Galleon.Checkout
{
    public class CheckoutAPI
    {   
        public static async Task<PurchaseResult> Purchase(CheckoutProduct product)
        {
            await  CheckoutClient.Instance.RunCheckoutSession(product);
            return CheckoutClient.Instance.CurrentSession.PurchaseResult;
        }
    }
    
    public class PurchaseResult
    {
        public bool         IsSuccess  { get; set; }
        public bool         IsCanceled { get; set; }
        public bool         IsError    { get; set; }
        public List<string> Errors     { get; set; } 
    }
}
