using View.UI;

namespace GameLogic
{
    public class PsyEnergy : IPsyEnergy
    {
        private int currentEnergy;
        private int maxEnergy;
        private IPsyEnergyView psyEnergyView;

        public PsyEnergy(int maxEnergy, IPsyEnergyView psyEnergyView)
        {
            this.maxEnergy = maxEnergy;
            currentEnergy = maxEnergy;

            this.psyEnergyView = psyEnergyView;
            psyEnergyView.UpdatePsyEnergyBar(maxEnergy, currentEnergy);
        }

        public void SpendEnergy(int energyCount)
        {
            currentEnergy = currentEnergy - energyCount;

            if (currentEnergy <= 0) currentEnergy = 0;

            psyEnergyView.UpdatePsyEnergyBar(maxEnergy, currentEnergy);
        }
    }
}