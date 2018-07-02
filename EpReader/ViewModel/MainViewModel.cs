using System;
using EpReader.DataService;
using EpReader.Model;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCard.PCSC.Native;
using Pc.Policy.Smartcard.Shared;
using System.Linq;
using PropertyChanged;
using SmartCard.PCSC;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace EpReader.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel : ViewModelBase
    {
        private IPolicyService _policyService;
        private IWordService _wordService;

        public string StatusLabel { get; set; }
        public InfoModel Information { get; set; } = new InfoModel();
        private bool _isCardInserted = false;
        private InfoModel _emptyInfoModel = new InfoModel();
        public MainViewModel(ITersmoService tersmoService, IPolicyService policyService, IWordService wordService)
        {
            tersmoService.LoadDictionary();
            _policyService = policyService;
            _wordService = wordService;
            Task.Run(() => ReadInfo());
        }

        private Task ReadInfo()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                PCSCReadersManager CardManager = new PCSCReadersManager();
                PolicySmartcardBase policy = null;
                try
                {
                    if (!_policyService.InitializeReader(ref CardManager))
                        continue;
                    else if (!_policyService.InitializeCard(ref CardManager, ref policy))
                    {
                        continue;
                    }
                    else
                    {
                        StatusLabel = CardManager.OfType<ISCard>().ToList().First().ReaderName;
                        if (!_isCardInserted)
                        {
                            Information = _policyService.ReadInformation(ref policy);
                            _isCardInserted = true;
                        }
                        policy.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    Information = _emptyInfoModel;
                    StatusLabel = ex.Message.ToString();                    
                    _isCardInserted = false;
                }
            }          
        }

        private ICommand _printInfoCommand;
        /// <summary>
        /// Команда на запрос печати информации
        /// </summary>
        public ICommand PrintInfoCommand => _printInfoCommand ?? (_printInfoCommand = new RelayCommand(() =>
        {
            //if (string.IsNullOrEmpty(Information.OwnerSname))
                //return;

            _wordService.Generate(Information);
        }));
    }
}