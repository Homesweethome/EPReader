using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PropertyChanged;

namespace EpReader.Model
{
    [AddINotifyPropertyChangedInterface]
    [DisplayName("Электронный полис")]
    public class InfoModel
    {
        [Browsable(false)]
        public OwnerModel Owner { get; set; }
        [Browsable(false)]
        public SmoModel Smo { get; set; }

        [Category("Данные застрахованного лица")]
        [DisplayName("Фамилия")] public string OwnerFamily => Owner.OwnerFamily;
        [DisplayName("Имя")] public string OwnerName => Owner.OwnerName;
        [DisplayName("Отчество")] public string OwnerSname => Owner.OwnerSname;
        [DisplayName("Пол")] public string OwnerSex => Owner.OwnerSex;
        [DisplayName("Дата рождения")] public string OwnerBdate => Owner.OwnerBdate;
        [DisplayName("Место рождения")] public string OwnerBplace => Owner.OwnerBplace;
        [Category("Информация о полисе ОМС")]
        [DisplayName("Номер полиса")] public string PoliceNumber => Owner.PoliceNumber;
        [DisplayName("Дата выдачи")] public string PoliceDate => Owner.PoliceDate;
        [DisplayName("Срок действия")] public string PoliceLong => Owner.PoliceLong;
        [DisplayName("СНИЛС")] public string PoliceSnils => Owner.PoliceSnils;
        [Category("Гражданство застрахованного лица")]
        [DisplayName("Гражданство")] public string Gcode => Owner.Gcode;
        [DisplayName("Гражданство")] public string Gtext => Owner.Gtext;

        [Category("Информация о СМО")]
        [DisplayName("Регион СМО")] public string SmoRegion => Smo.SmoRegion;
        [DisplayName("Название СМО")] public string SmoName => Smo.SmoName;
        [DisplayName("СМО ОГРН")] public string SmoOgrn => Smo.SmoOgrn;
        [DisplayName("ОКАТО")] public string SmoOkato => Smo.SmoOkato;
        [DisplayName("Дата начала страхования")] public string SmoBegin => Smo.SmoBegin;
        [DisplayName("Окончание срока действия")] public string SmoEnd => Smo.SmoEnd;

        public InfoModel()
        {
            Owner = new OwnerModel();
            Smo = new SmoModel();
        }
    }
}
