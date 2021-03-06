using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Car: IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string CarName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
    }
}
