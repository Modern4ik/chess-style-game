using System.Threading.Tasks;

namespace UserInput
{
    public interface PlayerInput
    {
        public Task<InputData> SelectUnitToResp();
    }
}

