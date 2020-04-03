using Assignment_WebShop_2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Services
{
    public class AccountService
    {
        private readonly WebShopContext _context;
        private readonly HttpContext _httpContext;

        public AccountService(WebShopContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
            //_httpContext.Response.Cookies.Delete("user_challenge");
            System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAA " + _httpContext.Request.Cookies.ContainsKey("user_challenge"));
            System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAA " + !_httpContext.Session.Keys.Contains("user"));

            // ha a felhasználónak van sütije, de még nincs bejelentkezve, bejelentkeztetjük
            if (_httpContext.Request.Cookies.ContainsKey("user_challenge") &&
                !_httpContext.Session.Keys.Contains("user"))
            {
                Basket user = _context.Baskets.FirstOrDefault(
                    g => g.UserName == _httpContext.Request.Cookies["user_challenge"]);
                System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAA " + user.UserName);  

                // kikeressük a felhasználót
                if (user != null)
                {
                    _httpContext.Session.SetString("user", user.UserName);
                }
            }
            else if(!_httpContext.Session.Keys.Contains("user"))
            {
                Create();   
            }
        }
        public String CurrentUserName => _httpContext.Session.GetString("user");


        public Boolean Create()
        {
            string userName = "user" + Guid.NewGuid(); // a felhasználónevet generáljuk


            // elmentjük a felhasználó adatait
            var allProducts = new List<BasketElem>();
            
            _context.Baskets.Add(new Basket
            {
                UserName = userName,
                elems = allProducts,
            });

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAA CREATE " + userName);

            _httpContext.Session.SetString("user", userName);
            _httpContext.Response.Cookies.Append("user_challenge", userName, // nem a felhasználónevet, hanem egy generált kódot tárolunk
                new CookieOptions
                {
                    Expires = DateTime.Today.AddDays(365), // egy évig lesz érvényes a süti
                    HttpOnly = true, // igyekszünk biztonságosság tenni a sütit
                                        //Secure = true,
                });

            return true;
        }

    }
}
