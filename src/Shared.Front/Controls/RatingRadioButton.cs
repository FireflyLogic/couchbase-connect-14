using System;
using Xamarin.Forms;

namespace Shared.Front
{
	public class RatingRadioButton : Image
	{
		private bool isOn;
		private int optionId;

		public RatingRadioButton (int number)
		{
			Source = "radio_off.png";
			WidthRequest = HeightRequest = 25;

			isOn = false;
			optionId = number;
		}
			
		private void ToggleRadioButton ()
		{
			if (isOn) {
				Source = "radio_off.png";
			} else {
				Source = "radio_on.png";
			}

			isOn = !isOn;
		}
		public int GetOptionId()
		{
			return optionId;
		}

		public void TurnRadioButtonOn ()
		{
			Source = "radio_on.png";
			isOn = true;
		}

		public void TurnRadioButtonOff ()
		{
			Source = "radio_off.png";
			isOn = false;
		}
	}
}