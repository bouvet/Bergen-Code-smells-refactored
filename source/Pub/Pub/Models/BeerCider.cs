namespace Pub.Models
{
    public class BeerCider : Beverage
    {
        public int UnitPrice { get; set; }
        public override int Price(bool student) => CalculatePrice(student);

        private int CalculatePrice(bool student)
        {
            if (AllowStudentDiscount && student)
                return UnitPrice -= UnitPrice / 10;

            return UnitPrice;
        }
    }
}
