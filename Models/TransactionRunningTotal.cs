namespace Fetch.Models
{
    /// <summary>
    /// This is a small bookkeeping model that is being kept separate from the exposed endpoints to reduce confusion
    /// </summary>
    public class TransactionRunningTotal : Transaction
    {
        public int PointsRunningTotal { get; set; }
    }
}