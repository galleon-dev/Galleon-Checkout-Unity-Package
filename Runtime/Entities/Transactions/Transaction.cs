using System;
using System.Collections.Generic;
using UnityEngine;

namespace Galleon.Checkout
{
    public class Transaction
    {
        //////////////////////////////////////// Members
        
        public Guid            ID = new Guid();
        
        public User            User;
        public CreditCardToken CreditCardToken;
        
        public bool            IsDone = false;

        //////////////////////////////////////// Lifecycle
        
        public Transaction(User user, CreditCardToken creditCardToken)
        {
            this.User            = user;
            this.CreditCardToken = creditCardToken;
        }
        
        //////////////////////////////////////// Main API
        
        public Step Purchase()
        =>
            new Step(name   : $"transaction_checkout"
                    ,action : async (s) =>
                    {
                        s.AddChildStep(Checkout());
                        s.AddChildStep(ValidateReceipt());
                    });
        
        //////////////////////////////////////// Transaction Steps
        
        public Step Checkout()
        =>
            new Step(name   : $"checkout"
                    ,action : async (s) =>
                    {
                        var result = await CHECKOUT.Network.Post(url     : "https://api.basistheory.com/proxy"
                                                                ,headers : new Dictionary<string, string>()
                                                                 {
                                                                     { "BT-API-KEY",    BasisTheoryAPI.API_KEY                      },
                                                                     { "BT-PROXY-URL",  "https://api.stripe.com/v1/payment_intents" },
                                                                     { "Authorization", "Bearer sk_test_51Odv6ZBonvPlF6MTC1gy7o44X7v9Kj0ZPjosTLa9kOtSCbtacEH5BcYYt7ZDzIC15TvDhsqeBNTgqF2cJmSQtGuc00P6rKMAwa" }
                                                                 }
                                                                 // ,body   : new
                                                                 // {
                                                                 //     amount              = 10,
                                                                 //     currency            = "usd",
                                                                 //     payment_method_data = new
                                                                 //     {
                                                                 //         token = "{{ " + CreditCardToken.Token + " }}"
                                                                 //     }
                                                                 // }
                                                                 ,encodingType   : RequestEncodingType.FormUrlEncoded
                                                                 ,formFields     : new Dictionary<string, string>()
                                                                 {
                                                                     { "amount",                               "1000"                                                                },
                                                                     { "currency",                             "usd"                                                                 },
                                                                     { "payment_method_data[type]",            "card"                                                                },
                                                                     { "payment_method_data[card][number]",    "{{ " + CreditCardToken.Token + " | json: '$.number' }}"              },
                                                                     { "payment_method_data[card][exp_month]", "{{ " + CreditCardToken.Token + " | json: '$.expiration_month' }}"    },
                                                                     { "payment_method_data[card][exp_year]",  "{{ " + CreditCardToken.Token + " | json: '$.expiration_year' }}"     },
                                                                     { "payment_method_data[card][cvc]",       "{{ " + CreditCardToken.Token + " | json: '$.cvc' }}"                 },
                                                                 }
                                                                 );
                        
                        s.Log(result);
                    });
        
        public Step ValidateReceipt()
        =>
            new Step(name   : $"validate_receipt"
                    ,action : async (s) =>
                    {
                        // var result = await CHECKOUT.Network.Get("http://localhost:5007/validate");
                        // s.Log(result);
                    });
    }
}
