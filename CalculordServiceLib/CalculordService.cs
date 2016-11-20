using CalculordDBLayer;
using System;
using System.Linq;
using System.ServiceModel;

namespace CalculordServiceLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CalculordService : ICalculord
    {
        public static readonly int CalculationLimit = 5; 
        private static readonly CalculordContext _context = new CalculordContext();
        private static User _client;
        private readonly CalcuLord.NET.CalcuLord _calculator;
        private readonly ICalculordCallback _callback = null;
       
        public CalculordService()
        {
            _calculator = new CalcuLord.NET.CalcuLord();
            _callback = OperationContext.Current.GetCallbackChannel<ICalculordCallback>();
        }

        public void SetConnection(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _client = new User();
               
                do
                {
                    _client.Identifier = Guid.NewGuid().ToString();
                } while (_context.Users.Any(u => u.Identifier == _client.Identifier));

                _client.CalculationLimit = CalculationLimit;
                _context.Users.Add(_client);
                 _context.SaveChanges();
                    
                _callback.Authorize(_client.Identifier);
            }
            else
            {
                _client = _context.Users.Where(u => u.Identifier == id).First();

            }
        }

        public void Calculate(string input)
        {
            if (_client.CalculationLimit > 0)
            {
                    
                double result = _calculator.Calculate(input);
                Calculation calculation = new Calculation { MathExpression = input, Result = result };

                _context.Calculations.Add(calculation);
                _client.Calculations.Add(calculation);
                _context.SaveChanges();

                _callback.Equals(result);

                _client.CalculationLimit--;
                _context.SaveChanges();
             }
             else
             {
                _callback.Reject();
             }
        }
    }
}
