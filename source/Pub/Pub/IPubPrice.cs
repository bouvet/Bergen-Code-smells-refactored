namespace Pub
{
    public interface IPubPrice
    {
        int ComputeCost(string drink, bool student, int amount);
        (string drink, bool student, int amount) ParseOrder(string order);
    }
}
