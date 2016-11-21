using CalculordApp.CalculordServiceReference;
using System;
using System.ServiceModel;

namespace CalculordApp.Model
{
    public class CalculordModel : ICalculordCallback, IDisposable
    {
        private static CalculordModel _instance;
        private static CalculordClient _proxy;

        private CalculordModel()
        {
           _proxy = new CalculordClient(new InstanceContext(this));
        }

        public event Action<string> AuthorizationConfirmed;
        public event Action<double> ResultReceived;
        public event Action<string> CalculationRejected;
        public event Action<string> ChumakReceived;

        public static CalculordModel Instance => _instance ?? (_instance = new CalculordModel());

        public void Authorize(string id)
        {
            AuthorizationConfirmed?.Invoke(id);
        }

        public void Equals(double result)
        {
            ResultReceived?.Invoke(result);
        }

        public void Reject(string msg)
        {
            CalculationRejected?.Invoke(msg);
        }

        public void SetConnection(string id)
        {
            _proxy.SetConnection(id);
        }

        public void Calculate(string expression, string id)
        {
            _proxy.Calculate(expression, id);
        }

        public void GetChumak(string img)
        {
            ChumakReceived?.Invoke(img);
        }

        public void Dispose()
        {
            _proxy.Close();
        }
    }
}
