namespace MusicPlayerMobile.ViewModels
{
	using Xamarin.Forms;

	/// <summary>
	///     The songs view model.
	/// </summary>
	internal sealed class TimerViewModel : BaseViewModel
	{
		/// <summary>
		///     Creates a new instance of the <see cref="SongsViewModel"/> class.
		/// </summary>
		public TimerViewModel()
		{
			this.Title = "Timer";
			this.StartTimerButtonClickedCommand = new Command(this.OnStartTimerButtonClicked);

			#region Testing
			#endregion
		}

		/// <summary>
		///		Gets and sets the current time minutes.
		/// </summary>
		internal int CurrentTimeMinutes { get; set; }

		/// <summary>
		///		Gets and sets the current time seconds.
		/// </summary>
		internal int CurrentTimeSeconds { get; set; }

		/// <summary>
		///     Gets the start timer button clicked command.
		/// </summary>
		public Command StartTimerButtonClickedCommand { get; set; }

		/// <summary>
		///		Starts the timer.
		/// </summary>
		private void OnStartTimerButtonClicked()
		{
			// loop number of specified sets
			// start work/rest
			// update UI
		}
	}
}