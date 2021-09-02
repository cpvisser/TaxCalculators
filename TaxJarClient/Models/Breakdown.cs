using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxJarApi.Models
{
    public class Breakdown
    {
        [JsonProperty("taxable_amount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public decimal TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public decimal CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public decimal StateTaxableAmount { get; set; }

        [JsonProperty("state_tax_rate")]
        public decimal StateTaxRate { get; set; }

        [JsonProperty("state_tax_collectable")]
        public decimal StateTaxCollectable { get; set; }

        [JsonProperty("county_taxable_amount")]
        public decimal CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public decimal CountyTaxRate { get; set; }

        [JsonProperty("county_tax_collectable")]
        public decimal CountyTaxCollectable { get; set; }

        [JsonProperty("city_taxable_amount")]
        public decimal CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public decimal CityTaxRate { get; set; }
        [JsonProperty("city_tax_collectable")]
        public decimal CityTaxCollectable { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public decimal SpecialTaxRate { get; set; }

        [JsonProperty("special_district_tax_collectable")]
        public decimal SpecialDistrictTaxCollectable { get; set; }

        public List<ShippingBreakdown> Shipping { get; set; }

        [JsonProperty("line_items")]
        public List<LineItemBreakdown> LineItems { get; set; }

        [JsonProperty("country_taxable_amount")]
        public decimal CountryTaxableAmount { get; set; }

        [JsonProperty("country_tax_rate")]
        public decimal CountryTaxRate { get; set; }

        [JsonProperty("country_tax_collectable")]
        public decimal CountryTaxCollectable { get; set; }



    }
}
