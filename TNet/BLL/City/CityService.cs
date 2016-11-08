using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCom.EF;
using TNet.Models;

namespace TNet.BLL
{
    public class CityService
    {

        public static List<City> GetALL()
        {
            TN db = new TN();
            return  db.Cities.OrderByDescending(en=>en.sortno).ToList();
        }

        public static City Get(string idcity)
        {
            TN db = new TN();
            List<City> cities = db.Cities.Where(en => en.idcity == idcity).ToList();
            return (cities != null && cities.Count > 0) ? cities.First() : null;
        }

        public static City Edit(City city)
        {
            TN db = new TN();
            City oldCity = db.Cities.Where(en => en.idcity == city.idcity).FirstOrDefault();

            oldCity.idcity = city.idcity;
            oldCity.city1 = city.city1;
            oldCity.code = city.code;
            oldCity.notes = city.notes;
            oldCity.sortno = city.sortno;
            oldCity.inuse = city.inuse;

            db.SaveChanges();
            return oldCity;
        }

        public static City Add(City city)
        {
            TN db = new TN();
            db.Cities.Add(city);
            db.SaveChanges();
            return city;
        }

        public static bool SetDefault(string idcity) {
            bool result = true;
            try {
                TN db = new TN();
                List<City> city= db.Cities.Where(en => en.isdefault).ToList();
                if (city!=null&&city.Count()>0) {
                    for (int i = 0; i < city.Count; i++) {
                        city[i].isdefault = false;
                    }
                }
                db.Cities.Find(idcity).isdefault = true;
                db.SaveChanges();
            }
            catch {
                result = false;
            }
            return result;
        }


        public static List<SelectItemViewModel<string>> SelectItems()
        {
            List<SelectItemViewModel<string>> cityOptions = new List<SelectItemViewModel<string>>();
            List<City> cities = GetALL();
            if (cities != null && cities.Count > 0)
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    cityOptions.Add(new SelectItemViewModel<string>()
                    {
                        DisplayValue = cities[i].idcity,
                        DisplayText = cities[i].city1
                    });
                }
            }

            return cityOptions;
        }

        public static List<SelectItemViewModel<string>> SelectItemValueCotainNameAndCode()
        {
            List<SelectItemViewModel<string>> cityOptions = new List<SelectItemViewModel<string>>();
            List<City> cities = GetALL();
            if (cities != null && cities.Count > 0)
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    cityOptions.Add(new SelectItemViewModel<string>()
                    {
                        DisplayValue = cities[i].city1 + "_" + cities[i].code,
                        DisplayText = cities[i].city1
                    });
                }
            }

            return cityOptions;
        }
    }
}