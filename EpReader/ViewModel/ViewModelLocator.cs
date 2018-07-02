using CommonServiceLocator;
using EpReader.DataService;
using GalaSoft.MvvmLight.Ioc;

namespace EpReader.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ITersmoService, TersmoService>();
            SimpleIoc.Default.Register<IPolicyService, PolicyService>();
            SimpleIoc.Default.Register<IWordService, WordService>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        
        public static void Cleanup()
        {

        }
    }
}