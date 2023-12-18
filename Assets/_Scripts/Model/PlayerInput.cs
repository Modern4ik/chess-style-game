using System.Threading.Tasks;

public interface PlayerInput
{
    public Task<InputData> SelectUnitToResp();
}