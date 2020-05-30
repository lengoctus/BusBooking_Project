using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using System;
using System.Collections.Generic;

namespace Supports
{
    public class PaymentSupport
    {
        private Payment payment;
        private ItemList itemList;
        private HttpContext httpContext;
        private IConfiguration Configuration;

        public PaymentSupport(ItemList _itemList, HttpContext _httpContext, IConfiguration _configuration)
        {
            itemList = _itemList;
            httpContext = _httpContext;
            Configuration = _configuration;
        }

        private Payment CreatePayPal(APIContext aPIContext, ItemList itemList, string returnUrl, string phi_thu_tax, string phi_ship)
        {
            int sub_total = 0;
            itemList.items.ForEach(s =>
            {
                sub_total += (Convert.ToInt32(s.price) * Convert.ToInt32(s.quantity));
            });
            Payer payer = new Payer()
            {
                payment_method = "paypal"
            };

            RedirectUrls urls = new RedirectUrls()
            {
                cancel_url = returnUrl,
                return_url = returnUrl
            };

            Details details = new Details()
            {
                tax = phi_thu_tax.ToString(),
                shipping = phi_ship.ToString(),
                subtotal = sub_total.ToString()
            };

            Amount amount = new Amount
            {
                currency = "USD",
                total = (Convert.ToInt32(sub_total) + Convert.ToInt32(phi_ship) + Convert.ToInt32(phi_thu_tax)).ToString(),
                details = details
            };
            httpContext.Session.SetInt32("total", Convert.ToInt32(amount.total));
            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(new Transaction
            {
                invoice_number = new Random().Next(10000).ToString(),
                amount = amount,
                item_list = itemList,
                description = "Test paypal cua Sang",
            });

            payment = new Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactions,
                redirect_urls = urls
            };
            return payment.Create(aPIContext);
        }



        public string PaymentWithPaypal(string action_paypal, string phi_thu_tax, string phi_ship)
        {
            try
            {
                APIContext aPIContext = new PaypalConfiguration(Configuration).GetApiConetxt();
                string payerId = httpContext.Request.Query["payerId"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string BaseUrl = action_paypal;
                    string guid = new Random().Next(1000) + "";
                    Payment createdPayment = CreatePayPal(aPIContext, itemList, BaseUrl + "guid=" + guid, phi_thu_tax, phi_ship);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                            paypalRedirectUrl = link.href;
                    }
                    httpContext.Session.SetString(guid, createdPayment.id);
                    return paypalRedirectUrl;
                }
                else
                {
                    var guid = httpContext.Request.Query["guid"];
                    var executePayment = ExecutePayment(aPIContext, payerId, httpContext.Session.GetString(guid));
                    if (executePayment.state.ToLower() != "approved")
                    {
                        return "0";
                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                httpContext.Response.WriteAsync(ex.Response);
            }
            return "1";
        }

        private Payment ExecutePayment(APIContext aPIContext, string payerId, string paymentId)
        {
            PaymentExecution paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment()
            {
                id = paymentId
            };
            return payment.Execute(aPIContext, paymentExecution);
        }
    }
}
