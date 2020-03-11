using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankData.DI;
using BankData.Entities;
using BankData.Repository;
using MicroFinBank.Models;
using Microsoft.AspNet.Identity.Owin;

namespace MicroFinBank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IAccountRepository _account;
        private readonly IStatementRepository _statement;

        public IFunction Db { get; }

        // GET: Transaction
        public TransactionController(IFunction db,IAccountRepository account,IStatementRepository statement)
        {
            _account = account;
            _statement = statement;
            Db = db;
         

        }
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult profile(string id)
        {
            //var user = System.Web.HttpContext.Current.User;
            //var info =Db.Get(id);
            var context = ApplicationDbContext.Create();
            var user = context.Users.Find(id);
            
            var user2 = Db.Get(id);
           // Session["Password"] = user2.Password;


            var accountDetails = _account.Get(id);
            Session["AccountNumber"] = accountDetails.AccountNumber;
            Session["AccountType"] = accountDetails.AccountType;
            Session["AccountStatus"] = accountDetails.Status;
            Session["AccountBalance"] = accountDetails.Balance;
            var newUser = new UserDto
            {
                userId = user2.Id,
                Email = user2.Email,
                AccountType =user2.AccountType,
                FirstName = user2.FirstName,
                LastName = user2.LastName,
                Passport = user2.Passport,
                PhoneNumber = user2.PhoneNumber,
                AccountNumber = accountDetails.AccountNumber,

            };
            return View(newUser);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Transfer(string id)
        {
           
            return View();
            

        }
        [HttpPost]
        public ActionResult Transfer(Account item, string amount, string password, string id)
        {
           
           string userPassword = Session["password"].ToString();
           var acct = _account.Get(id);

           if (userPassword == password)
           {
               var newAmount = Convert.ToDecimal(amount);
               List<string> response =   _account.TransferAccount(newAmount, item.AccountNumber, id);
               string statement1 = response[0];
               string statement2 = response[1];
            ViewBag.response = response[1]; 
            _statement.Add(acct.AccountNumber,statement1,item.AccountNumber, statement2);
            

            return RedirectToAction("Index","Home");

            }
           //sweet Alert, Wrong Password
           return RedirectToAction("Error");


        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult UserBalance(string id)
        {
          decimal accountBalance = _account.Get(id).Balance;
          ViewBag.Balance = accountBalance;
            return View();
        }

        public ActionResult Statement(string id)
        {
          var  userStatement=  _statement.Get(id);
          return View(userStatement);
        }

    }


    }
    