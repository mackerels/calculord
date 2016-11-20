using System.ServiceModel;

namespace CalculordServiceLib
{
    [ServiceContract(SessionMode = SessionMode.Required,
                 CallbackContract = typeof(ICalculordCallback))]
    public interface ICalculord
    {
        [OperationContract(IsOneWay = true)]
        void SetConnection(string id);

        [OperationContract(IsOneWay = true)]
        void Calculate(string input);
    }
}
