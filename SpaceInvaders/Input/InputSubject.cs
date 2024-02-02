using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class InputSubject
    {
        public InputSubject()
            : base()
        {
            // LTN - owns the list forever
            pObservers = new SLinkMan();
            Debug.Assert(pObservers != null);
        }

        public void Subscribe(InputObserver pObserver)
        {
            Debug.Assert(pObserver != null);
            pObserver.pSubject = this;
            pObservers.AddToFront(pObserver);
        }

        public void Broadcast()
        {
            Iterator pIt = pObservers.GetIterator();

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                InputObserver pObserver = (InputObserver)pIt.Current();
                Debug.Assert(pObserver != null);
                pObserver.Notify();
            }
        }

        public void Detach()
        {
        }

        private SLinkMan pObservers;
    }
}