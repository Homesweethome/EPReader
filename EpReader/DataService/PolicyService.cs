using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using EpReader.Model;
using Pc.Policy.Smartcard.Data;
using Pc.Policy.Smartcard.Shared;
using SmartCard.PCSC;
using SmartCard.PCSC.Native;

namespace EpReader.DataService
{
    public class PolicyService : IPolicyService
    {
        private ITersmoService _tersmoService;
        public PolicyService(ITersmoService tersmoService)
        {
            _tersmoService = tersmoService;
        }

        /// <summary>
        /// Инициализация считывателя карт
        /// </summary>
        public bool InitializeReader(PCSCReadersManager cardManager)
        {
            bool flag = false;
            try
            {
                cardManager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);
                if (cardManager.ReadersCount > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        /// <summary>
        /// Инициализация карты
        /// </summary>
        public bool InitializeCard(PCSCReadersManager cardManager, PolicySmartcardBase policy)
        {
            bool flag;
            try
            {
                ISCard card = cardManager[cardManager.OfType<ISCard>().ToList().First().ReaderName];
                policy = new PolicySmartcardBase(card);
                policy.Connect();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            return flag;
        }

        /// <summary>
        /// Чтение информации с карты
        /// </summary>
        public InfoModel ReadInformation(PolicySmartcardBase policy)
        {
            var result = new InfoModel
            {
                Owner = ReadOwnerInformation(policy),
                Smo = ReadSMOInformation(policy)
            };
            return result;
        }

        /// <summary>
        /// Информация о владельце карты
        /// </summary>
        public OwnerModel ReadOwnerInformation(PolicySmartcardBase policy)
        {
            OwnerModel result = new OwnerModel();
            OwnerInformation ownerInformation = policy.GetOwnerInformation();
            if (ownerInformation == null)
                return result;
            result.OwnerFamily = this.FormatPolicyText(ownerInformation.Identity_1, "Отсутствует");
            result.OwnerName = this.FormatPolicyText(ownerInformation.Identity_2, "Отсутствует");
            result.OwnerSname = this.FormatPolicyText(ownerInformation.Identity_3, "Отсутствует");
            byte? sex = ownerInformation.Sex;
            string str;
            if ((sex.GetValueOrDefault() != (byte)1 ? 0 : (sex.HasValue ? 1 : 0)) == 0)
            {
                sex = ownerInformation.Sex;
                str = (sex.GetValueOrDefault() != (byte)2 ? 0 : (sex.HasValue ? 1 : 0)) != 0 ? "Женский" : "Неизвестно";
            }
            else
                str = "Мужской";
            result.OwnerSex = str;
            result.OwnerBdate = this.FormatPolicyDate(ownerInformation.BirthDate, "Отсутствует");
            result.OwnerBplace = this.FormatPolicyText(ownerInformation.BirthPlace, "Отсутствует");
            result.PoliceNumber = ownerInformation.PolicyNumber;
            result.PoliceDate = this.FormatPolicyDate(ownerInformation.ExpireDate, "Отсутствует");
            result.PoliceLong = this.FormatPolicyDate(ownerInformation.ExpireDate, "Не ограничено");
            result.PoliceSnils = this.FormatPolicyText(ownerInformation.SNILS, "Отсутствует");
            if (ownerInformation.Citizenship != null)
            {
                result.Gcode = this.FormatPolicyText(ownerInformation.Citizenship.CoutryCode, "Отсутствует");
                result.Gtext = this.FormatPolicyText(ownerInformation.Citizenship.CoutryCyrillicName, "Отсутствует");
            }
            return result;
        }

        /// <summary>
        /// Информация об СМО
        /// </summary>
        public SmoModel ReadSMOInformation(PolicySmartcardBase policy)
        {
            SmoModel result = new SmoModel();
            SMOInformation currentSmoInformation = policy.GetCurrentSMOInformation();
            if (currentSmoInformation == null)
                return result;
            result.SmoOgrn = currentSmoInformation.OGRN;
            result.SmoOkato = currentSmoInformation.OKATO;
            result.SmoBegin = this.FormatPolicyDate(new DateTime?(currentSmoInformation.InsuranceStartDate), "Отсутствует");
            result.SmoEnd = this.FormatPolicyDate(currentSmoInformation.InsuranceExpireDate, "Не ограничено");
            if (!_tersmoService.LoadDictionary().Any())
                return result;

            var tersmo = _tersmoService.LoadDictionary().
                FirstOrDefault(x => x.Q_OGRN.ToString() == currentSmoInformation.OGRN
                && x.TF_OKATO.ToString() == currentSmoInformation.OKATO);
            if (tersmo != null)
            {
                result.SmoRegion = tersmo.Q_NAME;
                result.SmoName = tersmo.Q_FNAME;
            }
            else
            {
                result.SmoRegion = "Не найден";
                result.SmoName = "Не найден";
            }
            return result;
        }

        /// <summary>
        /// Форматирование текста
        /// </summary>
        private string FormatPolicyText(string value, string null_value)
        {
            if (string.IsNullOrEmpty(value))
                return null_value;
            return value;
        }

        /// <summary>
        /// Форматирование даты
        /// </summary>
        private string FormatPolicyDate(DateTime? date, string nullValue)
        {
            if (!date.HasValue || date.Value.ToString("ddMMyyyy") == "01011900")
                return nullValue;
            return date.Value.ToString("dd.MM.yyyy");
        }
    }
}
