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
        bool InitializeReader(PCSCReadersManager cardManager);

        /// <summary>
        /// Инициализация карты
        /// </summary>
        bool InitializeCard(PCSCReadersManager cardManager, PolicySmartcardBase policy);

        /// <summary>
        /// Чтение информации с карты
        /// </summary>
        InfoModel ReadInformation(PolicySmartcardBase policy);

        /// <summary>
        /// Информация о владельце карты
        /// </summary>
        OwnerModel ReadOwnerInformation(PolicySmartcardBase policy);

        /// <summary>
        /// Информация об СМО
        /// </summary>
        SmoModel ReadSMOInformation(PolicySmartcardBase policy);
    }
}