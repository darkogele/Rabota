// Decompiled with JetBrains decompiler
// Type: interop.WebServiceCR_UJP.CrmResponseCrmResultItemsCrmResultItem
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace interop.WebServiceCR_UJP
{
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/CR_UJP_RESPONSE")]
  [DesignerCategory("code")]
  [GeneratedCode("System.Xml", "2.0.50727.3074")]
  [Serializable]
  public class CrmResponseCrmResultItemsCrmResultItem : INotifyPropertyChanged
  {
    private string actingPeriodFieldField;
    private string activityCodeFieldField;
    private string activityDescFieldField;
    private string actorIDFieldField;
    private string actorNameFieldField;
    private string actorSurnameFieldField;
    private string actorTypeDescFieldField;
    private string actorTypeIDFieldField;
    private string addInfoField;
    private string additionalInfoFieldField;
    private string authorisationTypeDescFieldField;
    private string authorisationTypeIDFieldField;
    private string authorisedRegisterFieldField;
    private string authorisedRegisterIDFieldField;
    private string bankruptcyCourtNameFieldField;
    private string capitalFC_CashFieldField;
    private string capitalFC_NonCashFieldField;
    private string capitalFC_PaydFieldField;
    private string capitalFC_TotalFieldField;
    private string capitalOriginDescFieldField;
    private string capitalOriginIDFieldField;
    private string coreActivityCodeFieldField;
    private string coreActivityDescFieldField;
    private string countryCodeFieldField;
    private string courtJournalIDFieldField;
    private string decisionDateFieldField;
    private string descriptionFieldField;
    private string emailFieldField;
    private string entranceNoFieldField;
    private string fCCodeFieldField;
    private string flatNoFieldField;
    private string foreignActivityFieldField;
    private string foundingDateFieldField;
    private string houseNoFieldField;
    private string isAAActiveFieldField;
    private string isActivityNoLicenceFieldField;
    private string isDataConfirmedFieldField;
    private string isForeignActFieldField;
    private string isLETerminatedFieldField;
    private string lEFullNameFieldField;
    private string lEIDFieldField;
    private string lESizeDescFieldField;
    private string lESizeIDFieldField;
    private string lETypeDescFieldField;
    private string lETypeIDFieldField;
    private string liabilityDescFieldField;
    private string liabilityIDFieldField;
    private string licenceFieldField;
    private string memberOfFieldField;
    private string membersFieldField;
    private string municipalityCodeFieldField;
    private string municipalityFieldField;
    private string organisationTypeCodeFieldField;
    private string organisationTypeDescFieldField;
    private string otherInfoFieldField;
    private string ownerIDFieldField;
    private string ownerNameFieldField;
    private string ownerSurnameFieldField;
    private string ownerTypeDescFieldField;
    private string ownerTypeIDFieldField;
    private string ownershipTypeDescFieldField;
    private string ownershipTypeIDFieldField;
    private string participationFC_CashFieldField;
    private string participationFC_NonCashFieldField;
    private string participationFC_PaydFieldField;
    private string participationFC_TotalFieldField;
    private string personOrLEDescFieldField;
    private string personOrLEIDFieldField;
    private string placeCodeFieldField;
    private string placeFieldField;
    private string registerCategoryFieldField;
    private string registerCategoryIDFieldField;
    private string restrictionsFieldField;
    private string sharesPaydFieldField;
    private string sharesPaymentFieldField;
    private string sharesPublishingFieldField;
    private string sharesTotalFieldField;
    private string shortNameFieldField;
    private string stageDescFieldField;
    private string stageIDFieldField;
    private string streetCodeFieldField;
    private string streetFieldField;
    private string taxPayerNumberFieldField;
    private string terminationTypeDescFieldField;
    private string terminationTypeIDFieldField;
    private string typeDescFieldField;
    private string typeIDFieldField;
    private string typeOfSharesFieldField;
    private string unitDescrFieldField;
    private string unitNameFieldField;
    private string unitNoFieldField;
    private string unitTypeDescrFieldField;
    private string unitTypeIDFieldField;
    private string validityDateFieldField;
    private string validityHourFieldField;

    [XmlElement(IsNullable = true, Order = 0)]
    public string actingPeriodField
    {
      get
      {
        return this.actingPeriodFieldField;
      }
      set
      {
        this.actingPeriodFieldField = value;
        this.RaisePropertyChanged("actingPeriodField");
      }
    }

    [XmlElement(IsNullable = true, Order = 1)]
    public string activityCodeField
    {
      get
      {
        return this.activityCodeFieldField;
      }
      set
      {
        this.activityCodeFieldField = value;
        this.RaisePropertyChanged("activityCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 2)]
    public string activityDescField
    {
      get
      {
        return this.activityDescFieldField;
      }
      set
      {
        this.activityDescFieldField = value;
        this.RaisePropertyChanged("activityDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 3)]
    public string actorIDField
    {
      get
      {
        return this.actorIDFieldField;
      }
      set
      {
        this.actorIDFieldField = value;
        this.RaisePropertyChanged("actorIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 4)]
    public string actorNameField
    {
      get
      {
        return this.actorNameFieldField;
      }
      set
      {
        this.actorNameFieldField = value;
        this.RaisePropertyChanged("actorNameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 5)]
    public string actorSurnameField
    {
      get
      {
        return this.actorSurnameFieldField;
      }
      set
      {
        this.actorSurnameFieldField = value;
        this.RaisePropertyChanged("actorSurnameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 6)]
    public string actorTypeDescField
    {
      get
      {
        return this.actorTypeDescFieldField;
      }
      set
      {
        this.actorTypeDescFieldField = value;
        this.RaisePropertyChanged("actorTypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 7)]
    public string actorTypeIDField
    {
      get
      {
        return this.actorTypeIDFieldField;
      }
      set
      {
        this.actorTypeIDFieldField = value;
        this.RaisePropertyChanged("actorTypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 8)]
    public string addInfo
    {
      get
      {
        return this.addInfoField;
      }
      set
      {
        this.addInfoField = value;
        this.RaisePropertyChanged("addInfo");
      }
    }

    [XmlElement(IsNullable = true, Order = 9)]
    public string additionalInfoField
    {
      get
      {
        return this.additionalInfoFieldField;
      }
      set
      {
        this.additionalInfoFieldField = value;
        this.RaisePropertyChanged("additionalInfoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 10)]
    public string authorisationTypeDescField
    {
      get
      {
        return this.authorisationTypeDescFieldField;
      }
      set
      {
        this.authorisationTypeDescFieldField = value;
        this.RaisePropertyChanged("authorisationTypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 11)]
    public string authorisationTypeIDField
    {
      get
      {
        return this.authorisationTypeIDFieldField;
      }
      set
      {
        this.authorisationTypeIDFieldField = value;
        this.RaisePropertyChanged("authorisationTypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 12)]
    public string authorisedRegisterField
    {
      get
      {
        return this.authorisedRegisterFieldField;
      }
      set
      {
        this.authorisedRegisterFieldField = value;
        this.RaisePropertyChanged("authorisedRegisterField");
      }
    }

    [XmlElement(IsNullable = true, Order = 13)]
    public string authorisedRegisterIDField
    {
      get
      {
        return this.authorisedRegisterIDFieldField;
      }
      set
      {
        this.authorisedRegisterIDFieldField = value;
        this.RaisePropertyChanged("authorisedRegisterIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 14)]
    public string bankruptcyCourtNameField
    {
      get
      {
        return this.bankruptcyCourtNameFieldField;
      }
      set
      {
        this.bankruptcyCourtNameFieldField = value;
        this.RaisePropertyChanged("bankruptcyCourtNameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 15)]
    public string capitalFC_CashField
    {
      get
      {
        return this.capitalFC_CashFieldField;
      }
      set
      {
        this.capitalFC_CashFieldField = value;
        this.RaisePropertyChanged("capitalFC_CashField");
      }
    }

    [XmlElement(IsNullable = true, Order = 16)]
    public string capitalFC_NonCashField
    {
      get
      {
        return this.capitalFC_NonCashFieldField;
      }
      set
      {
        this.capitalFC_NonCashFieldField = value;
        this.RaisePropertyChanged("capitalFC_NonCashField");
      }
    }

    [XmlElement(IsNullable = true, Order = 17)]
    public string capitalFC_PaydField
    {
      get
      {
        return this.capitalFC_PaydFieldField;
      }
      set
      {
        this.capitalFC_PaydFieldField = value;
        this.RaisePropertyChanged("capitalFC_PaydField");
      }
    }

    [XmlElement(IsNullable = true, Order = 18)]
    public string capitalFC_TotalField
    {
      get
      {
        return this.capitalFC_TotalFieldField;
      }
      set
      {
        this.capitalFC_TotalFieldField = value;
        this.RaisePropertyChanged("capitalFC_TotalField");
      }
    }

    [XmlElement(IsNullable = true, Order = 19)]
    public string capitalOriginDescField
    {
      get
      {
        return this.capitalOriginDescFieldField;
      }
      set
      {
        this.capitalOriginDescFieldField = value;
        this.RaisePropertyChanged("capitalOriginDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 20)]
    public string capitalOriginIDField
    {
      get
      {
        return this.capitalOriginIDFieldField;
      }
      set
      {
        this.capitalOriginIDFieldField = value;
        this.RaisePropertyChanged("capitalOriginIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 21)]
    public string coreActivityCodeField
    {
      get
      {
        return this.coreActivityCodeFieldField;
      }
      set
      {
        this.coreActivityCodeFieldField = value;
        this.RaisePropertyChanged("coreActivityCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 22)]
    public string coreActivityDescField
    {
      get
      {
        return this.coreActivityDescFieldField;
      }
      set
      {
        this.coreActivityDescFieldField = value;
        this.RaisePropertyChanged("coreActivityDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 23)]
    public string countryCodeField
    {
      get
      {
        return this.countryCodeFieldField;
      }
      set
      {
        this.countryCodeFieldField = value;
        this.RaisePropertyChanged("countryCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 24)]
    public string courtJournalIDField
    {
      get
      {
        return this.courtJournalIDFieldField;
      }
      set
      {
        this.courtJournalIDFieldField = value;
        this.RaisePropertyChanged("courtJournalIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 25)]
    public string decisionDateField
    {
      get
      {
        return this.decisionDateFieldField;
      }
      set
      {
        this.decisionDateFieldField = value;
        this.RaisePropertyChanged("decisionDateField");
      }
    }

    [XmlElement(IsNullable = true, Order = 26)]
    public string descriptionField
    {
      get
      {
        return this.descriptionFieldField;
      }
      set
      {
        this.descriptionFieldField = value;
        this.RaisePropertyChanged("descriptionField");
      }
    }

    [XmlElement(IsNullable = true, Order = 27)]
    public string emailField
    {
      get
      {
        return this.emailFieldField;
      }
      set
      {
        this.emailFieldField = value;
        this.RaisePropertyChanged("emailField");
      }
    }

    [XmlElement(IsNullable = true, Order = 28)]
    public string entranceNoField
    {
      get
      {
        return this.entranceNoFieldField;
      }
      set
      {
        this.entranceNoFieldField = value;
        this.RaisePropertyChanged("entranceNoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 29)]
    public string fCCodeField
    {
      get
      {
        return this.fCCodeFieldField;
      }
      set
      {
        this.fCCodeFieldField = value;
        this.RaisePropertyChanged("fCCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 30)]
    public string flatNoField
    {
      get
      {
        return this.flatNoFieldField;
      }
      set
      {
        this.flatNoFieldField = value;
        this.RaisePropertyChanged("flatNoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 31)]
    public string foreignActivityField
    {
      get
      {
        return this.foreignActivityFieldField;
      }
      set
      {
        this.foreignActivityFieldField = value;
        this.RaisePropertyChanged("foreignActivityField");
      }
    }

    [XmlElement(IsNullable = true, Order = 32)]
    public string foundingDateField
    {
      get
      {
        return this.foundingDateFieldField;
      }
      set
      {
        this.foundingDateFieldField = value;
        this.RaisePropertyChanged("foundingDateField");
      }
    }

    [XmlElement(IsNullable = true, Order = 33)]
    public string houseNoField
    {
      get
      {
        return this.houseNoFieldField;
      }
      set
      {
        this.houseNoFieldField = value;
        this.RaisePropertyChanged("houseNoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 34)]
    public string isAAActiveField
    {
      get
      {
        return this.isAAActiveFieldField;
      }
      set
      {
        this.isAAActiveFieldField = value;
        this.RaisePropertyChanged("isAAActiveField");
      }
    }

    [XmlElement(IsNullable = true, Order = 35)]
    public string isActivityNoLicenceField
    {
      get
      {
        return this.isActivityNoLicenceFieldField;
      }
      set
      {
        this.isActivityNoLicenceFieldField = value;
        this.RaisePropertyChanged("isActivityNoLicenceField");
      }
    }

    [XmlElement(IsNullable = true, Order = 36)]
    public string isDataConfirmedField
    {
      get
      {
        return this.isDataConfirmedFieldField;
      }
      set
      {
        this.isDataConfirmedFieldField = value;
        this.RaisePropertyChanged("isDataConfirmedField");
      }
    }

    [XmlElement(IsNullable = true, Order = 37)]
    public string isForeignActField
    {
      get
      {
        return this.isForeignActFieldField;
      }
      set
      {
        this.isForeignActFieldField = value;
        this.RaisePropertyChanged("isForeignActField");
      }
    }

    [XmlElement(IsNullable = true, Order = 38)]
    public string isLETerminatedField
    {
      get
      {
        return this.isLETerminatedFieldField;
      }
      set
      {
        this.isLETerminatedFieldField = value;
        this.RaisePropertyChanged("isLETerminatedField");
      }
    }

    [XmlElement(IsNullable = true, Order = 39)]
    public string lEFullNameField
    {
      get
      {
        return this.lEFullNameFieldField;
      }
      set
      {
        this.lEFullNameFieldField = value;
        this.RaisePropertyChanged("lEFullNameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 40)]
    public string lEIDField
    {
      get
      {
        return this.lEIDFieldField;
      }
      set
      {
        this.lEIDFieldField = value;
        this.RaisePropertyChanged("lEIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 41)]
    public string lESizeDescField
    {
      get
      {
        return this.lESizeDescFieldField;
      }
      set
      {
        this.lESizeDescFieldField = value;
        this.RaisePropertyChanged("lESizeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 42)]
    public string lESizeIDField
    {
      get
      {
        return this.lESizeIDFieldField;
      }
      set
      {
        this.lESizeIDFieldField = value;
        this.RaisePropertyChanged("lESizeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 43)]
    public string lETypeDescField
    {
      get
      {
        return this.lETypeDescFieldField;
      }
      set
      {
        this.lETypeDescFieldField = value;
        this.RaisePropertyChanged("lETypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 44)]
    public string lETypeIDField
    {
      get
      {
        return this.lETypeIDFieldField;
      }
      set
      {
        this.lETypeIDFieldField = value;
        this.RaisePropertyChanged("lETypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 45)]
    public string liabilityDescField
    {
      get
      {
        return this.liabilityDescFieldField;
      }
      set
      {
        this.liabilityDescFieldField = value;
        this.RaisePropertyChanged("liabilityDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 46)]
    public string liabilityIDField
    {
      get
      {
        return this.liabilityIDFieldField;
      }
      set
      {
        this.liabilityIDFieldField = value;
        this.RaisePropertyChanged("liabilityIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 47)]
    public string licenceField
    {
      get
      {
        return this.licenceFieldField;
      }
      set
      {
        this.licenceFieldField = value;
        this.RaisePropertyChanged("licenceField");
      }
    }

    [XmlElement(IsNullable = true, Order = 48)]
    public string memberOfField
    {
      get
      {
        return this.memberOfFieldField;
      }
      set
      {
        this.memberOfFieldField = value;
        this.RaisePropertyChanged("memberOfField");
      }
    }

    [XmlElement(IsNullable = true, Order = 49)]
    public string membersField
    {
      get
      {
        return this.membersFieldField;
      }
      set
      {
        this.membersFieldField = value;
        this.RaisePropertyChanged("membersField");
      }
    }

    [XmlElement(IsNullable = true, Order = 50)]
    public string municipalityCodeField
    {
      get
      {
        return this.municipalityCodeFieldField;
      }
      set
      {
        this.municipalityCodeFieldField = value;
        this.RaisePropertyChanged("municipalityCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 51)]
    public string municipalityField
    {
      get
      {
        return this.municipalityFieldField;
      }
      set
      {
        this.municipalityFieldField = value;
        this.RaisePropertyChanged("municipalityField");
      }
    }

    [XmlElement(IsNullable = true, Order = 52)]
    public string organisationTypeCodeField
    {
      get
      {
        return this.organisationTypeCodeFieldField;
      }
      set
      {
        this.organisationTypeCodeFieldField = value;
        this.RaisePropertyChanged("organisationTypeCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 53)]
    public string organisationTypeDescField
    {
      get
      {
        return this.organisationTypeDescFieldField;
      }
      set
      {
        this.organisationTypeDescFieldField = value;
        this.RaisePropertyChanged("organisationTypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 54)]
    public string otherInfoField
    {
      get
      {
        return this.otherInfoFieldField;
      }
      set
      {
        this.otherInfoFieldField = value;
        this.RaisePropertyChanged("otherInfoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 55)]
    public string ownerIDField
    {
      get
      {
        return this.ownerIDFieldField;
      }
      set
      {
        this.ownerIDFieldField = value;
        this.RaisePropertyChanged("ownerIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 56)]
    public string ownerNameField
    {
      get
      {
        return this.ownerNameFieldField;
      }
      set
      {
        this.ownerNameFieldField = value;
        this.RaisePropertyChanged("ownerNameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 57)]
    public string ownerSurnameField
    {
      get
      {
        return this.ownerSurnameFieldField;
      }
      set
      {
        this.ownerSurnameFieldField = value;
        this.RaisePropertyChanged("ownerSurnameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 58)]
    public string ownerTypeDescField
    {
      get
      {
        return this.ownerTypeDescFieldField;
      }
      set
      {
        this.ownerTypeDescFieldField = value;
        this.RaisePropertyChanged("ownerTypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 59)]
    public string ownerTypeIDField
    {
      get
      {
        return this.ownerTypeIDFieldField;
      }
      set
      {
        this.ownerTypeIDFieldField = value;
        this.RaisePropertyChanged("ownerTypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 60)]
    public string ownershipTypeDescField
    {
      get
      {
        return this.ownershipTypeDescFieldField;
      }
      set
      {
        this.ownershipTypeDescFieldField = value;
        this.RaisePropertyChanged("ownershipTypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 61)]
    public string ownershipTypeIDField
    {
      get
      {
        return this.ownershipTypeIDFieldField;
      }
      set
      {
        this.ownershipTypeIDFieldField = value;
        this.RaisePropertyChanged("ownershipTypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 62)]
    public string participationFC_CashField
    {
      get
      {
        return this.participationFC_CashFieldField;
      }
      set
      {
        this.participationFC_CashFieldField = value;
        this.RaisePropertyChanged("participationFC_CashField");
      }
    }

    [XmlElement(IsNullable = true, Order = 63)]
    public string participationFC_NonCashField
    {
      get
      {
        return this.participationFC_NonCashFieldField;
      }
      set
      {
        this.participationFC_NonCashFieldField = value;
        this.RaisePropertyChanged("participationFC_NonCashField");
      }
    }

    [XmlElement(IsNullable = true, Order = 64)]
    public string participationFC_PaydField
    {
      get
      {
        return this.participationFC_PaydFieldField;
      }
      set
      {
        this.participationFC_PaydFieldField = value;
        this.RaisePropertyChanged("participationFC_PaydField");
      }
    }

    [XmlElement(IsNullable = true, Order = 65)]
    public string participationFC_TotalField
    {
      get
      {
        return this.participationFC_TotalFieldField;
      }
      set
      {
        this.participationFC_TotalFieldField = value;
        this.RaisePropertyChanged("participationFC_TotalField");
      }
    }

    [XmlElement(IsNullable = true, Order = 66)]
    public string personOrLEDescField
    {
      get
      {
        return this.personOrLEDescFieldField;
      }
      set
      {
        this.personOrLEDescFieldField = value;
        this.RaisePropertyChanged("personOrLEDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 67)]
    public string personOrLEIDField
    {
      get
      {
        return this.personOrLEIDFieldField;
      }
      set
      {
        this.personOrLEIDFieldField = value;
        this.RaisePropertyChanged("personOrLEIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 68)]
    public string placeCodeField
    {
      get
      {
        return this.placeCodeFieldField;
      }
      set
      {
        this.placeCodeFieldField = value;
        this.RaisePropertyChanged("placeCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 69)]
    public string placeField
    {
      get
      {
        return this.placeFieldField;
      }
      set
      {
        this.placeFieldField = value;
        this.RaisePropertyChanged("placeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 70)]
    public string registerCategoryField
    {
      get
      {
        return this.registerCategoryFieldField;
      }
      set
      {
        this.registerCategoryFieldField = value;
        this.RaisePropertyChanged("registerCategoryField");
      }
    }

    [XmlElement(IsNullable = true, Order = 71)]
    public string registerCategoryIDField
    {
      get
      {
        return this.registerCategoryIDFieldField;
      }
      set
      {
        this.registerCategoryIDFieldField = value;
        this.RaisePropertyChanged("registerCategoryIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 72)]
    public string restrictionsField
    {
      get
      {
        return this.restrictionsFieldField;
      }
      set
      {
        this.restrictionsFieldField = value;
        this.RaisePropertyChanged("restrictionsField");
      }
    }

    [XmlElement(IsNullable = true, Order = 73)]
    public string sharesPaydField
    {
      get
      {
        return this.sharesPaydFieldField;
      }
      set
      {
        this.sharesPaydFieldField = value;
        this.RaisePropertyChanged("sharesPaydField");
      }
    }

    [XmlElement(IsNullable = true, Order = 74)]
    public string sharesPaymentField
    {
      get
      {
        return this.sharesPaymentFieldField;
      }
      set
      {
        this.sharesPaymentFieldField = value;
        this.RaisePropertyChanged("sharesPaymentField");
      }
    }

    [XmlElement(IsNullable = true, Order = 75)]
    public string sharesPublishingField
    {
      get
      {
        return this.sharesPublishingFieldField;
      }
      set
      {
        this.sharesPublishingFieldField = value;
        this.RaisePropertyChanged("sharesPublishingField");
      }
    }

    [XmlElement(IsNullable = true, Order = 76)]
    public string sharesTotalField
    {
      get
      {
        return this.sharesTotalFieldField;
      }
      set
      {
        this.sharesTotalFieldField = value;
        this.RaisePropertyChanged("sharesTotalField");
      }
    }

    [XmlElement(IsNullable = true, Order = 77)]
    public string shortNameField
    {
      get
      {
        return this.shortNameFieldField;
      }
      set
      {
        this.shortNameFieldField = value;
        this.RaisePropertyChanged("shortNameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 78)]
    public string stageDescField
    {
      get
      {
        return this.stageDescFieldField;
      }
      set
      {
        this.stageDescFieldField = value;
        this.RaisePropertyChanged("stageDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 79)]
    public string stageIDField
    {
      get
      {
        return this.stageIDFieldField;
      }
      set
      {
        this.stageIDFieldField = value;
        this.RaisePropertyChanged("stageIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 80)]
    public string streetCodeField
    {
      get
      {
        return this.streetCodeFieldField;
      }
      set
      {
        this.streetCodeFieldField = value;
        this.RaisePropertyChanged("streetCodeField");
      }
    }

    [XmlElement(IsNullable = true, Order = 81)]
    public string streetField
    {
      get
      {
        return this.streetFieldField;
      }
      set
      {
        this.streetFieldField = value;
        this.RaisePropertyChanged("streetField");
      }
    }

    [XmlElement(IsNullable = true, Order = 82)]
    public string taxPayerNumberField
    {
      get
      {
        return this.taxPayerNumberFieldField;
      }
      set
      {
        this.taxPayerNumberFieldField = value;
        this.RaisePropertyChanged("taxPayerNumberField");
      }
    }

    [XmlElement(IsNullable = true, Order = 83)]
    public string terminationTypeDescField
    {
      get
      {
        return this.terminationTypeDescFieldField;
      }
      set
      {
        this.terminationTypeDescFieldField = value;
        this.RaisePropertyChanged("terminationTypeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 84)]
    public string terminationTypeIDField
    {
      get
      {
        return this.terminationTypeIDFieldField;
      }
      set
      {
        this.terminationTypeIDFieldField = value;
        this.RaisePropertyChanged("terminationTypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 85)]
    public string typeDescField
    {
      get
      {
        return this.typeDescFieldField;
      }
      set
      {
        this.typeDescFieldField = value;
        this.RaisePropertyChanged("typeDescField");
      }
    }

    [XmlElement(IsNullable = true, Order = 86)]
    public string typeIDField
    {
      get
      {
        return this.typeIDFieldField;
      }
      set
      {
        this.typeIDFieldField = value;
        this.RaisePropertyChanged("typeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 87)]
    public string typeOfSharesField
    {
      get
      {
        return this.typeOfSharesFieldField;
      }
      set
      {
        this.typeOfSharesFieldField = value;
        this.RaisePropertyChanged("typeOfSharesField");
      }
    }

    [XmlElement(IsNullable = true, Order = 88)]
    public string unitDescrField
    {
      get
      {
        return this.unitDescrFieldField;
      }
      set
      {
        this.unitDescrFieldField = value;
        this.RaisePropertyChanged("unitDescrField");
      }
    }

    [XmlElement(IsNullable = true, Order = 89)]
    public string unitNameField
    {
      get
      {
        return this.unitNameFieldField;
      }
      set
      {
        this.unitNameFieldField = value;
        this.RaisePropertyChanged("unitNameField");
      }
    }

    [XmlElement(IsNullable = true, Order = 90)]
    public string unitNoField
    {
      get
      {
        return this.unitNoFieldField;
      }
      set
      {
        this.unitNoFieldField = value;
        this.RaisePropertyChanged("unitNoField");
      }
    }

    [XmlElement(IsNullable = true, Order = 91)]
    public string unitTypeDescrField
    {
      get
      {
        return this.unitTypeDescrFieldField;
      }
      set
      {
        this.unitTypeDescrFieldField = value;
        this.RaisePropertyChanged("unitTypeDescrField");
      }
    }

    [XmlElement(IsNullable = true, Order = 92)]
    public string unitTypeIDField
    {
      get
      {
        return this.unitTypeIDFieldField;
      }
      set
      {
        this.unitTypeIDFieldField = value;
        this.RaisePropertyChanged("unitTypeIDField");
      }
    }

    [XmlElement(IsNullable = true, Order = 93)]
    public string validityDateField
    {
      get
      {
        return this.validityDateFieldField;
      }
      set
      {
        this.validityDateFieldField = value;
        this.RaisePropertyChanged("validityDateField");
      }
    }

    [XmlElement(IsNullable = true, Order = 94)]
    public string validityHourField
    {
      get
      {
        return this.validityHourFieldField;
      }
      set
      {
        this.validityHourFieldField = value;
        this.RaisePropertyChanged("validityHourField");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
