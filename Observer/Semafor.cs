using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Observer
{
    public class Semafor : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private Event dogadaj { get; set; }

        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void SetEvent(Event e)
        {
            dogadaj = e;
            Notify();
        }

        public void Notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(dogadaj);
            }
        }
    }
}
