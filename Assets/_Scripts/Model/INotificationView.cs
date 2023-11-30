using System.Threading.Tasks;
using UnityEngine;

public interface INotificationView
{
    Task StartNotificationAnimation(Faction faction);
}