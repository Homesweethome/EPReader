using EpReader.Model;
using Pc.Policy.Smartcard.Shared;
using SmartCard.PCSC.Native;

namespace EpReader.DataService
{
    public interface IPolicyService
    {
        /// <summary>
        /// Инициализация считывателя карт
        /// </summary>
        bool InitializeReader(ref PCSCReadersManager cardManager);

        /// <summary>
        /// Инициализация карты
        /// </summary>
        bool InitializeCard(ref PCSCReadersManager cardManager, ref PolicySmartcardBase Policy);

        /// <summary>
        /// Чтение информации с карты
        /// </summary>
        InfoModel ReadInformation(ref PolicySmartcardBase policy);

        /// <summary>
        /// Информация о владельце карты
        /// </summary>
        OwnerModel ReadOwnerInformation(ref PolicySmartcardBase policy);

        /// <summary>
        /// Информация об СМО
        /// </summary>
        SmoModel ReadSMOInformation(ref PolicySmartcardBase policy);
    }
}