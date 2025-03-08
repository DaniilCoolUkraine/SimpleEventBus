# SimpleEventBus
This package contains the simplest implementation of Event Bus pattern for Unity  
## Definition
> Event bus is a software component that can be used to exchange messages between different parts of the system. In other words, event bus is a centralized software communication hub.<br>

Linked from [Event Bus: Pros, Cons and Best Practices](https://www.techyourchance.com/event-bus/)

---
## API
- GlobalEvents.AddListener<br>
  Subscribe for event
- GlobalEvents.RemoveListener<br>
  Unsubscribe from event
- GlobalEvents.Publish<br>
  Publish event
## Usage example
#### Event declaration
``` csharp
public class RefillEvent : IEvent
{
    public Transform EntityToRefill { get; private set; }
    
    public RefillEvent(Transform interactor)
    {
        EntityToRefill = interactor;
    }
}
```
#### Event subscription
``` csharp
public AttackStrategy()
{
    GlobalEvents.AddListener<RefillEvent>(OnRefill);
}

private void OnRefill(RefillEvent ev)
{
    if (ev.EntityToRefill == transform)
        _currentAmmo = _ammoCount;
}
```
#### Event publish
``` csharp
public void Interact(Transform interactor)
{
    GlobalEvents.Publish<RefillEvent>(new RefillEvent(interactor));
}
```
#### Event unsubscription
``` csharp
private void OnDestroy()
{
    GlobalEvents.RemoveListener<RefillEvent>(OnRefill);
}
```