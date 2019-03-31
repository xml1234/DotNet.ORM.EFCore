using System;
using System.Collections.Generic;

namespace EFCore.DomainModels
{
    public class Company
    {
        public Company()
        {
            CityCompanies=new List<CityCompany>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public string Person { get; set; }

        public List<CityCompany> CityCompanies { get; set; }
    }
}