using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankData.DI;
using BankData.Entities;
using BankData.Repository;

namespace MicroFinBank.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountRepository _account;
        private readonly IFunction _user;
        private readonly IStatementRepository _statement;

        public AdminController(IAccountRepository account, IFunction user,IStatementRepository statement)
        {
            _account = account;
            _user = user;
            _statement = statement;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ViewProfile(string accountNumber)
        {
            var accoutDetails= _account.GetByAccountNumber(accountNumber);
            string id = accoutDetails.UserId;
            var user = _user.Get(id);
            var statements = _statement.Get(accountNumber);
            Session["Statement"] = statements;
            Session["UserDetail"] = user;

           if (accoutDetails == null)
           {
               string message = "Invalid Account Number";
                //sweetAlert
           }
            Session["AccountDetails"] = accoutDetails;
           return RedirectToAction("Redirect","Admin");


        }
        [HttpGet]
        public ActionResult Redirect()
        {
            var accountDetails = (Account) Session["AccountDetails"];
            string accountNumber = accountDetails.AccountNumber; 
            return View();
        }
        [HttpGet]
        public ActionResult ChangeStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeStatus(string status, string accountNumber)
        {
            bool response = _account.EditStatus(accountNumber, status);
            if (response == false)
            {
                //sweetAlert
                return View("incorrect password");
            }

            return RedirectToAction("Index", "Admin");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Deposit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Deposit(decimal amount, string accountNumber)
        {
          string statement=  _account.MakeDeposit(accountNumber, amount);
          string response = _statement.AddStatement(accountNumber, statement);
          return RedirectToAction("Index", "Admin");
        }


    }
}