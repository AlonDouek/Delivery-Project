﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryServer.DTO;
using DeliveryServerBL.Models;
using System.IO;
using System.Collections;
using DeliveryApp.DTO;

namespace DeliveryServer.Controllers
{
    [Route("DeliveryAPI")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        DeliveryDBContext context;
        public DeliveryController(DeliveryDBContext context)
        {
            this.context = context;
        }
        #endregion

        //do the thing in the config to make it work
        //<binding protocol = "http" bindingInformation="*:16340:127.0.0.1" />
        // <binding protocol = "https" bindingInformation="*:44323:127.0.0.1" />



        [Route("Login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);
            string a ="4";
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);
                
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return user;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        
        #region unnecessary?
        //[Route("IsEmailExist")]
        //[HttpPost]
        //public bool IsEmailExist([FromBody] string email)
        //{
        //    return this.context.IsEmailExist(email);
        //}
        #endregion

        [Route("SignUp")]
        [HttpPost]
        public bool SignUp([FromBody] User user)
        {
            
            if (this.context.IsExist(user.Email))
                return false;
            else
            {
                this.context.Users.Add(user);
                this.context.SaveChanges();
                return true;
            }
        }

       

        [Route("ChangeCredentials")]
        [HttpPost]
        public bool ChangeCredentials([FromBody] ChangeDTO changeDTO)
        {

            if (this.context.IsExist(changeDTO.CuserEmail))
                return false;
            else
            {
                this.context.ChangeCredetials(changeDTO.CuserEmail, changeDTO.Nuser.Email, changeDTO.Nuser.Password, changeDTO.Nuser.Username, changeDTO.Nuser.Address, changeDTO.Nuser.PhoneNumber, changeDTO.Nuser.CreditCard);
                this.context.SaveChanges();
                return true;
            }
        }


        [Route("getRestaurants")]
        [HttpGet]
        public List<Restaurant> GetResLst()
        {
            return context.GetRestaurantsList();
        }

        
    }
}
