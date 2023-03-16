using System.ComponentModel.DataAnnotations;

namespace Domain.Categories
{
    public enum ExpenseCategories
    {
        [Display(Name = "Electronics And Software")]
        ElectronicsAndSoftware,

        [Display(Name = "Food")]
        Food,

        [Display(Name = "Entertainment")]
        Entertainment,

        [Display(Name = "Sport")]
        Sport,

        [Display(Name = "Education And Learning")]
        EducationAndLearning,

        [Display(Name = "Work")]
        Work,

        [Display(Name = "Personal Transport")]
        PersonalTransport,

        [Display(Name = "Household Tools")]
        HouseholdTools,

        [Display(Name = "House Rent")]
        HouseRent,

        [Display(Name = "House Utilities")]
        HouseUtilities,

        [Display(Name = "Medical And Healthcare")]
        MedicalAndHealthcare,

        [Display(Name = "Restaurants And Bars")]
        RestaurantsAndBars,

        [Display(Name = "Public Transport")]
        PublicTransport,

        [Display(Name = "Investing And Saving")]
        InvestingAndSaving,

        [Display(Name = "Traveling")]
        Traveling,

        //Like Salon services, haircuts, cosmetics
        [Display(Name = "Personal Services")]
        PersonalServices,

        //Meaning clothing shopping
        [Display(Name = "Shopping")]
        Shopping,

        [Display(Name = "Kids")]
        Kids
    }
}
