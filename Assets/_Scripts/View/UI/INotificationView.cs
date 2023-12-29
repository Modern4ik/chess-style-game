using GameLogic.Units;
using System.Threading.Tasks;

namespace View
{
    namespace UI
    {
        public interface INotificationView
        {
            Task StartNotificationAnimation(Faction faction);
        }
    } 
}