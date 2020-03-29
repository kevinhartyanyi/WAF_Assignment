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

            //// ha a felhasználónak van sütije, de még nincs bejelentkezve, bejelentkeztetjük
            //if (_httpcontext.request.cookies.containskey("user_challenge") &&
            //    !_httpcontext.session.keys.contains("user"))
            //{
            //    guest guest = _context.guests.firstordefault(
            //        g => g.userchallenge == _httpcontext.request.cookies["user_challenge"]);
            //    // kikeressük a felhasználót
            //    if (guest != null)
            //    {
            //        _httpcontext.session.setstring("user", guest.username);

            //    }
            //}
        }
        public String CurrentUserName => _httpContext.Session.GetString("user");


        //public Boolean Create(GuestViewModel guest, out String userName)
        //{
        //    userName = "user" + Guid.NewGuid(); // a felhasználónevet generáljuk

        //    if (guest == null)
        //        return false;

        //    // ellenőrizzük az annotációkat
        //    if (!Validator.TryValidateObject(guest, new ValidationContext(guest, null, null), null))
        //        return false;

        //    // elmentjük a felhasználó adatait
        //    _context.Guests.Add(new Guest
        //    {
        //        Name = guest.GuestName,
        //        Address = guest.GuestAddress,
        //        Email = guest.GuestEmail,
        //        PhoneNumber = guest.GuestPhoneNumber,
        //        UserName = userName
        //    });

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    return true;
        //}

    }
}
