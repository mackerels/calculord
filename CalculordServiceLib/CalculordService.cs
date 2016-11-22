using System;
using System.Linq;
using System.ServiceModel;
using CalculordDBLayer;

namespace CalculordServiceLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CalculordService : ICalculord
    {
        public static readonly int CalculationLimit = 10;
        public static readonly int ChumakPrice = 5;
        private readonly CalcuLord.NET.CalcuLord _calculator;

        public CalculordService()
        {
            _calculator = new CalcuLord.NET.CalcuLord();
        }

        private ICalculordCallback CallBack => OperationContext.Current.GetCallbackChannel<ICalculordCallback>();

        public void SetConnection(string id)
        {
            using (var context = new CalculordContext())
            {
                if (string.IsNullOrEmpty(id))
                {
                    string identifier;

                    do
                    {
                        identifier = Guid.NewGuid().ToString();
                    } while (context.Users.Any(u => u.Identifier == identifier));

                    context.Users.Add(new User {Identifier = identifier, CalculationLimit = CalculationLimit});
                    context.SaveChanges();
                    CallBack.Authorize(identifier);

                    Console.WriteLine(identifier);
                }
                else
                {
                    if (!context.Users.Any(u => u.Identifier == id))
                        CallBack.Reject("You are a cheater! Please be fair!");
                }
            }
        }

        public void Calculate(string input, string id)
        {
            using (var context = new CalculordContext())
            {
                var client = context.Users.First(u => u.Identifier == id);

                if (input != "chumak")
                {
                    if (client.CalculationLimit > 0)
                    {
                        var result = _calculator.Calculate(input);
                        var calculation = new Calculation {MathExpression = input, Result = result, UserId = client.Id};

                        context.Calculations.Add(calculation);
                        context.SaveChanges();

                        CallBack.Equals(result);

                        client.CalculationLimit--;
                        context.SaveChanges();
                    }
                }
                else
                {
                    if (client.CalculationLimit >= ChumakPrice)
                    {
                        CallBack.GetChumak(@"http://api.chumak.ml/daily");
                        client.CalculationLimit -= ChumakPrice;
                        context.SaveChanges();
                    }
                }

                if (client.CalculationLimit == 0)
                    CallBack.Reject("Your calculation limit reached! Please buy a license!");
            }
        }
    }
}