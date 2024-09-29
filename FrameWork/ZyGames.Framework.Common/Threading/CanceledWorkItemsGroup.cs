namespace ZyGames.Framework.Common.Threading
{
    internal class CanceledWorkItemsGroup
    {
        public readonly static CanceledWorkItemsGroup NotCanceledWorkItemsGroup = new CanceledWorkItemsGroup();

        public CanceledWorkItemsGroup()
        {
            IsCanceled = false;
        }

        public bool IsCanceled { get; set; }
    }
}