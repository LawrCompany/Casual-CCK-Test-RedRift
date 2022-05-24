using System;


namespace Code.Core{
    public interface IGetDamaged{
        event Action<CardController> OnDeath;
        void GetDamage(int value);
    }
}