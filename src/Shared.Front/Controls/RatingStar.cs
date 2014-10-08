using System;
using Xamarin.Forms;

namespace Shared.Front
{
	public class RatingStar : Image
	{
		private bool isOn;
		private int starNum;

		public RatingStar (int number)
		{
			Source = "star_off.png";

			isOn = false;
			starNum = number;
		}

		private void ToggleStar ()
		{
			if (isOn) {
				Source = "star_off.png";
			} else {
				Source = "star_on.png";
			}

			isOn = !isOn;
		}

		public int GetStarNum()
		{
			return starNum;
		}

		public void TurnStarOn ()
		{
			Source = "star_on.png";
			isOn = true;
		}

		public void TurnStarOff ()
		{
			Source = "star_off.png";
			isOn = false;
		}
	}
}

