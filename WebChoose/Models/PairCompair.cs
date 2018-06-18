namespace WebChoose.Models
{
	public class PairCompair
	{
		public int AlternativeId { get; set; }

		public int WinCount { get; set; }

		public int LoseCount { get; set; }

		public bool IsWin => WinCount > LoseCount;
	}
}