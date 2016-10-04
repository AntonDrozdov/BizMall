using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using System.ComponentModel.DataAnnotations;
using BizMall.Models.CommonModels;

namespace BizMall.Models.CompanyModels
{
    public enum GoodStatus { InActive, Active };
    public class Good
    {
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        //[Required]
        //[StringLength(300)]
        public string Title { get; set; }

        //[Required]
        //[StringLength(3000)]
        //[DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        //[Required]
        //[Range(0, 1000)]
        public int? Amount { get; set; }

        public GoodStatus? Status { get; set; }

        public List<RelCompanyGood> Companies { get; set; }
        public ICollection<Image> Images { get; set; }
        //public ICollection<RelDiscountGood> Discounts { get; set; }

        public Good()
        {
            Amount = 0;
            Images = new List<Image>();
            //Discounts = new List<RelDiscountGood>();
            Companies = new List<RelCompanyGood>();
        }
    }
}