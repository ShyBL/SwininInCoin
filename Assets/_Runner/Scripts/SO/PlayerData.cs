// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// [CreateAssetMenu(menuName = "Endless-Runner/Player-Data",fileName = "Player_Data")]
// public class PlayerData : ScriptableObject
// {
//         private static PlayerData _instance;
//
//         public static PlayerData Instance
//         {
//                 get
//                 {
//                         if (_instance == null)
//                         {
//                                 _instance = Resources.Load<PlayerData>($"Player_Data");
//                         }
//
//                         return _instance;
//                 }
//         }
//         
//         public static float InitialForwardSpeed;
//         public static float InitialSidewaysSpeed;
//     
//         public static int GoodCollectibles;
//         public static int MaxCargo;
//         public static int BadCollectibles;
//         public static int MaxStow;
//
//         public static int Money;
//         public static int Energy;
//         
//         public static ShipType CurrentShip;
//         
//         public  static void GetEnergyForLeftovers(int amount)
//         {
//             int newAmount = Energy + amount;
//             if (newAmount < 0)
//             {
//                 Debug.Log("Cannot decrease CurrencyAmount below 0.");
//                 return;
//             }
//             Energy += amount;
//         }
//     
//         public static void PayoffCollectibles(int amount)
//         {
//             int newAmount = Money + amount;
//             if (newAmount < 0)
//             {
//                 Debug.Log("Cannot decrease CurrencyAmount below 0.");
//                 return;
//             }
//             Money += amount;
//         }
//     
//         public static void ChangeCollectibles(CollectibleType collectibleType, EventsType eventsType, int amount, bool remove = false)
//         {
//             if (!remove)
//             {
//                 switch (collectibleType)
//                 {
//                     case CollectibleType.GoodsCollectibles:
//                         if (IsFull(collectibleType)) return;
//
//                         GoodCollectibles += amount;
//                         EventsManager.InvokeEvent(eventsType, GoodCollectibles);
//                 
//                         break;
//             
//                     case CollectibleType.BannedCollectibles:
//                         if (IsFull(collectibleType)) return;
//
//                         BadCollectibles += amount;
//                         EventsManager.InvokeEvent(eventsType, BadCollectibles);
//
//                         break;
//                 }
//             }
//             else
//             {
//                 switch (collectibleType)
//                 {
//                     case CollectibleType.GoodsCollectibles:
//
//                         GoodCollectibles = amount;
//                         EventsManager.InvokeEvent(eventsType, GoodCollectibles);
//                 
//                         break;
//             
//                     case CollectibleType.BannedCollectibles:
//
//                         BadCollectibles = amount;
//                         EventsManager.InvokeEvent(eventsType, BadCollectibles);
//
//                         break;
//                 } 
//             }
//         }
//     
//         public static bool IsFull(CollectibleType type)
//         {
//             switch (type)
//             {
//                 case CollectibleType.GoodsCollectibles:
//                     return GoodCollectibles == MaxCargo;
//             
//                 case CollectibleType.BannedCollectibles:
//                     return BadCollectibles == MaxStow;
//             }
//
//             return false;
//         }
// }
//
//
// [CreateAssetMenu(menuName = "Endless-Runner/Events-Manager", fileName = "Events_Manager")]
// public class EventsManager : ScriptableObject
// {
//     private static EventsManager _instance;
//
//     public static EventsManager Instance
//     {
//         get
//         {
//             if (_instance == null)
//             {
//                 _instance = Resources.Load<EventsManager>($"Events_Manager");
//             }
//
//             return _instance;
//         }
//     }
//     
//     private static Dictionary<EventsType, EventListenersData> eventListenersData = new();
//
//     
//     public  static void AddListener(EventsType eventsType, Action<object> methodToInvoke)
//     {
//         if (eventListenersData.TryGetValue(eventsType, out var value))
//         {
//             value.ActionsOnInvoke.Add(methodToInvoke);
//         }
//         else
//         {
//             eventListenersData[eventsType] = new EventListenersData(methodToInvoke);
//         }
//     }
//
//     // Removes listener for the specified event type
//     public  static void RemoveListener(EventsType eventsType, Action<object> methodToInvoke)
//     {
//         if (!eventListenersData.TryGetValue(eventsType, out var value))
//         {
//             return;
//         }
//
//         value.ActionsOnInvoke.Remove(methodToInvoke);
//
//         if (!value.ActionsOnInvoke.Any())
//         {
//             eventListenersData.Remove(eventsType);
//         }
//     }
//
//     // Invokes all listeners registered for the specified event type, and the data to pass to the listeners when invoking the event
//     public static void InvokeEvent(EventsType eventsType, object dataToInvoke)
//     {
//         if (!eventListenersData.TryGetValue(eventsType, out var value))
//         {
//             return;
//         }
//
//         foreach (var method in value.ActionsOnInvoke)
//         {
//             method.Invoke(dataToInvoke);
//         }
//     }
// }
//
// public class EventListenersData
// {
//     public List<Action<object>> ActionsOnInvoke;
//
//     public EventListenersData(Action<object> additionalData)
//     {
//         ActionsOnInvoke = new List<Action<object>>
//         {
//             additionalData
//         };
//     }
// }
//
// // Enum representing different event types.
// public enum EventsType
// {
//     GoodsCollectibles,
//     BannedCollectibles,
//     NavLeft,
//     NavRight
// }