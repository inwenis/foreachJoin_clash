using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var nominatedUserIds = new List<int>()
        {
            1, 2, 3, 4, 10, 101
        };
        var creditCards = new List<CreditCard>()
        {
            new CreditCard(1, 1, 400),
            new CreditCard(2, 2, 401),
            new CreditCard(3, 3, 402),
            new CreditCard(4, 4, 403),
            new CreditCard(5, 5, 404),
            new CreditCard(6, 6, 405),
            new CreditCard(7, 7, 406),
            new CreditCard(8, 8, 407),
        };
        var orders = new List<Odrer>()
        {
            new Odrer(400),
            new Odrer(401),
            new Odrer(402),
            new Odrer(403),
            new Odrer(410),
        };



        Console.WriteLine("A) linq join");

        var specialOrders = nominatedUserIds
            .Join(creditCards,
                userId => userId,
                creditCard => creditCard.UserId,
                (_, creditCard) => creditCard)
            .Join(orders,
                creditCard => creditCard.OrderId,
                order => order.Id,
                (_, order) => order);

        DoSomethingWithOrders(specialOrders);



        Console.WriteLine("B) the same in SQL syntax");

        var specialOrders2 =
            from userId in nominatedUserIds
            join creditCard in creditCards on userId equals creditCard.UserId
            join order in orders on creditCard.OrderId equals order.Id
            select order;

        DoSomethingWithOrders(specialOrders2);



        Console.WriteLine("C) the same starting from orders");

        var specialOrders3 =
            from order in orders
            join creditCard in creditCards on order.Id equals creditCard.OrderId
            join userId in nominatedUserIds on creditCard.UserId equals userId
            select order;

        DoSomethingWithOrders(specialOrders3);



        Console.WriteLine("D) foreach");

        foreach (var userId in nominatedUserIds)
        {
            var specialOders = orders
                .Where(order =>
                    creditCards.Any(creditCard => creditCard.OrderId == order.Id && creditCard.UserId == userId));

            DoSomethingWithOrders(specialOders);
        }

        Console.WriteLine("Press enter to exit...");
        Console.ReadKey();
    }

    private static void DoSomethingWithOrders(IEnumerable<Odrer> specialOders)
    {
        foreach (var specialOder in specialOders)
        {
            Console.WriteLine(specialOder.Id);
        }
    }
}

class CreditCard
{
    public int Id;
    public int UserId;
    public int OrderId;

    public CreditCard(int id, int userId, int orderId)
    {
        Id = id;
        UserId = userId;
        OrderId = orderId;
    }
}

class Odrer
{
    public int Id;

    public Odrer(int id)
    {
        Id = id;
    }
}
