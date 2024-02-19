using Tilt.ViewModels;

namespace Tilt;

public partial class MainPage : ContentPage
{
    readonly MainViewModel _viewModel;
    const string SHOW_ALERT_KEY = "SHOW_ALERT";

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
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
        bool shouldDisplayAlert = Preferences.Default.Get(SHOW_ALERT_KEY, true);

        bool shouldClearAllToDos = shouldDisplayAlert == false;

        if (shouldDisplayAlert)
        {
            shouldClearAllToDos = await DisplayAlert("Shake detected", "Shake will clear all your ToDos. Do you want to continue?", "Yes, don't show again", "No");
        };

        if (!shouldClearAllToDos) return;

        Preferences.Default.Set(SHOW_ALERT_KEY, false);

        _viewModel.ClearAllToDos();

        Vibration.Default.Vibrate();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        string toDoDescription = await DisplayPromptAsync("Add new ToDo", "Enter the ToDo description below", "Done");

        _viewModel.AddTodo(toDoDescription);
    }
}
