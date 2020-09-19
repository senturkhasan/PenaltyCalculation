using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using EntityFrameworkCore2.Models;
using System.Globalization;
using Nager.Date;

namespace EntityFrameworkCore2.Controllers
{
    public class CountryController : Controller
    {
        private ICounrtyRepository repository;
        public CountryController(ICounrtyRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.countries = new SelectList((from ls in calculate() select ls.Value).Distinct().ToList());
            return View();
        }
        [HttpPost]
        public IActionResult Index(InputData _data)
        {
            int result_day = 0;
            var CheckedDate = _data.CheckedDate;
            
            foreach (var item in calculate())
            {
                if (item.Value == _data.Name)
                {
                    foreach (var publicHoliday in DateSystem.GetPublicHoliday(CheckedDate, _data.ReturnedDate, item.Key))
                    {
                        result_day = result_day - 1;
                    }

                    RegionInfo regionInfo = new RegionInfo(item.Key);
                    ViewBag.currencySymbol = regionInfo.CurrencySymbol;
                    while (DateTime.Compare(_data.ReturnedDate, CheckedDate) > 0)
                    {
                        if ((int)CheckedDate.DayOfWeek != 0 && (int)CheckedDate.DayOfWeek != 6)//Saturday and Sunday
                            result_day = result_day + 1;

                        CheckedDate = CheckedDate.AddDays(1);
                    }

                }
            }
            _data.ResultDay = result_day;

            return View("Detail", _data);
        }
        public List<KeyValuePair<string, string>> calculate()
        {
            List<string> culturelist = new List<string>();
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo cultureInfo in cinfo)
            {
                RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);
                if (!(culturelist.Contains(regionInfo.EnglishName)))
                {
                   culturelist.Add(regionInfo.EnglishName);
                   list.Add(new KeyValuePair<string, string>(regionInfo.Name, regionInfo.EnglishName)); }
              
            }
            return list;
        }
        public IActionResult List() => View(repository.Countries);
    }
}