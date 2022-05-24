using Code.Core.Net;


namespace Code.Core{
    public class CardController{
        private readonly CardModel _model;

        public CardController(CardModel model){
            _model = model;
        }

        public ICardModel GetModel(){
            return _model;
        }
    }
}