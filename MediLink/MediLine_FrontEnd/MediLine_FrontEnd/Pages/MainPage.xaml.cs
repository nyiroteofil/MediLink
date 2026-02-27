using MediLine_FrontEnd.Models;
using MediLine_FrontEnd.PageModels;

namespace MediLine_FrontEnd.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}