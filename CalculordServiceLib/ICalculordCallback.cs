using System.ServiceModel;

namespace CalculordServiceLib
{
    public interface ICalculordCallback
    {
        [OperationContract(IsOneWay = true)]
        void Authorize(string id);

        [OperationContract(IsOneWay = true)]
        void Equals(double result);

        [OperationContract(IsOneWay = true)]
        void Reject(string msg);

        [OperationContract(IsOneWay = true)]
        void GetChumak(string img);
    }
}
