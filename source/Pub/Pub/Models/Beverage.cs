namespace Pub.Models
{
    public abstract class Beverage
    {
        public string Name { get; set; }
        public virtual bool AllowStudentDiscount { get; set; }
        public virtual int MaxOrderNumber { get; set; }
        public virtual int Price(bool student) => 0;
    }
}
