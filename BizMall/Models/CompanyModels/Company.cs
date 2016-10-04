using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BizMall.Models.CompanyModels
{
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual string ApplicationUserId { get; set; }
        public List<RelCompanyGood> Goods { get; set; }
        public Company()
        {
            Goods = new List<RelCompanyGood>();
        }
    }
}
