using System;
using Xamarin.Forms;

namespace Shared.Front
{
	public class RatingCheckbox : Image
	{
		private bool isOn;
		private int optionId;

		public RatingCheckbox (int number)
		{
			Source = "check_box_off.png";
			WidthRequest = HeightRequest = 25;

			isOn = false;
			optionId = number;
		}

		public bool IsChecked()
		{
			return isOn;
		}

		private void ToggleCheckbox ()
		{
			if (isOn) {
				Source = "check_box_off.png";
			} else {
				Source = "check_box_on.png";
			}

			isOn = !isOn;
		}
		public int GetOptionId()
		{
			return optionId;
		}

		public void TurnCheckboxOn ()
		{
			Source = "check_box_on.png";
			isOn = true;
		}

		public void TurnCheckboxOff ()
		{
			Source = "check_box_off.png";
			isOn = false;
		}
	}
}