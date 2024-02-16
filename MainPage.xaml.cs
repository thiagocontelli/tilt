using Tilt.ViewModels;

namespace Tilt;

public partial class MainPage : ContentPage
{
    readonly MainViewModel _viewModel;
    const string _showAlertKey = "SHOW_ALERT";

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        BindingContext = _viewModel;
        ToggleShake();
        Title = $"ToDos of {DateTime.Now:ddd dd MMM yyyy}";
    }

    private void ToggleShake()
    {
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Default.Start(SensorSpeed.Game);
            }
            else
            {
                Accelerometer.Default.Stop();
                Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
            }
        }
    }

    private async void Accelerometer_ShakeDetected(object? sender, EventArgs e)
    {
        bool shouldDisplayAlert = Preferences.Default.Get(_showAlertKey, true);

        bool shouldClearAllToDos = shouldDisplayAlert == false;

        if (shouldDisplayAlert)
        {
            shouldClearAllToDos = await DisplayAlert("Shake detected", "Shake will clear all your ToDos. Do you want to continue?", "Yes, don't show again", "No");
        };

        if (!shouldClearAllToDos) return;

        Preferences.Default.Set(_showAlertKey, false);

        _viewModel.ClearAllToDos();

        Vibration.Default.Vibrate();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        string toDoDescription = await DisplayPromptAsync("Add new ToDo", "Enter the ToDo description below", "Done");

        _viewModel.AddTodo(toDoDescription);
    }
}
