using Homework_04_24_AjaxPeopleList.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Homework_04_24_AjaxPeopleList.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {

            var repo = new Models.PeopleRepo(_connectionString);
            List<Person> people = repo.GetAll();
            return Json(people);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new Models.PeopleRepo(_connectionString);
            repo.Add(person);
        }

        [HttpPost]
        public void Delete(int id)
        {
            var repo = new Models.PeopleRepo(_connectionString);
            repo.Delete(id);
        }


        [HttpPost]
        public IActionResult GetById(int id)
        {
            var repo = new Models.PeopleRepo(_connectionString);
            Person p = repo.GetById(id);
            return Json(p);
        }

        [HttpPost]
        public void Update(Person p)
        {
            var repo = new Models.PeopleRepo(_connectionString);
            repo.Update(p);
        }


    }
}