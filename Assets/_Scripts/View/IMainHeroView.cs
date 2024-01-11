namespace View
{
    public interface IMainHeroView
    {
        public int healthCount { get; }
        public void SetUnderAttackMark(bool isEnable);
    }
}