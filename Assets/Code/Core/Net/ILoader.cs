using System.Threading.Tasks;


namespace Code.Core.Net{
    interface ILoader{
        Task LoadModel(CardsModel model, ResourceSettings settings);
    }
}