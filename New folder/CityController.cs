using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRsystem.Models;
using HRsystem.Data;
using HRsystem.services;

namespace HRsystem.Controllers
{
    public class CityController : Controller
    {
        VmCity vv;
   
        ICountryServices CountryS;
    
        ICityServices CityS;

        public CityController(ICityServices cityServices, ICountryServices icountryServices)
        {
             vv = new VmCity();
       
             CountryS = icountryServices;
   
             CityS = cityServices;
        }

        public IActionResult Index()
        {
            vv.liCountry = CountryS.LoadAll();
  

            vv.liCity = CityS.LoadAll();
        

            return View("NewCity",vv);
        }

        public IActionResult SaveCity(VmCity v)
        {
            CityS.Insert(v.c);
          

            vv.liCity = CityS.LoadAll();


            vv.liCountry = CountryS.LoadAll();


            return View("NewCity",vv);
        }
    }


}
