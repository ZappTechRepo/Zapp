﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homemation.WebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesConnectionA : DbContext
    {
        public EntitiesConnectionA()
            : base("name=EntitiesConnectionA")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountAttribute> AccountAttributes { get; set; }
        public DbSet<AccountBrand> AccountBrands { get; set; }
        public DbSet<AccountCategory> AccountCategories { get; set; }
        public DbSet<AccountCategory2> AccountCategory2 { get; set; }
        public DbSet<AccountComment> AccountComments { get; set; }
        public DbSet<AccountDocument> AccountDocuments { get; set; }
        public DbSet<AccountField> AccountFields { get; set; }
        public DbSet<AccountTemplate> AccountTemplates { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Audit_Log> Audit_Log { get; set; }
        public DbSet<Audit_Serial> Audit_Serial { get; set; }
        public DbSet<BankingDetail> BankingDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompanySetting> CompanySettings { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerAttribute> CustomerAttributes { get; set; }
        public DbSet<CustomerBusinessInfo> CustomerBusinessInfoes { get; set; }
        public DbSet<CustomerCommunication> CustomerCommunications { get; set; }
        public DbSet<CustomerDocument> CustomerDocuments { get; set; }
        public DbSet<CustomerField> CustomerFields { get; set; }
        public DbSet<CustomerManufacturer> CustomerManufacturers { get; set; }
        public DbSet<CustomerProductLineBrand> CustomerProductLineBrands { get; set; }
        public DbSet<CustomerRequest> CustomerRequests { get; set; }
        public DbSet<CustomerTemplate> CustomerTemplates { get; set; }
        public DbSet<CustomerTradeReference> CustomerTradeReferences { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<DeliveryUser> DeliveryUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentComment> DocumentComments { get; set; }
        public DbSet<DocumentConfiguration> DocumentConfigurations { get; set; }
        public DbSet<DocumentEndUserDelivery> DocumentEndUserDeliveries { get; set; }
        public DbSet<DocumentStatu> DocumentStatus { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DownloadCategory> DownloadCategories { get; set; }
        public DbSet<EmailDocument> EmailDocuments { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<FileInfo> FileInfoes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPrice> GroupPrices { get; set; }
        public DbSet<GroupPrice3> GroupPrice3 { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<InvoiceNumber> InvoiceNumbers { get; set; }
        public DbSet<InvoiceQty> InvoiceQties { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LineOption> LineOptions { get; set; }
        public DbSet<LineTypeEnum> LineTypeEnums { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<MenuCategoryOrder> MenuCategoryOrders { get; set; }
        public DbSet<MessageQueue> MessageQueues { get; set; }
        public DbSet<OE_DocumentConfiguration> OE_DocumentConfiguration { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Pastel_Invoices> Pastel_Invoices { get; set; }
        public DbSet<Pastel_OpenItem> Pastel_OpenItem { get; set; }
        public DbSet<Pastel_PaymentCheck> Pastel_PaymentCheck { get; set; }
        public DbSet<Pastel_PaymentsForInvoices> Pastel_PaymentsForInvoices { get; set; }
        public DbSet<PastelAccountBalance> PastelAccountBalances { get; set; }
        public DbSet<PastelInventory> PastelInventories { get; set; }
        public DbSet<PastelPO> PastelPOes { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListManufacture> PriceListManufactures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductBanner> ProductBanners { get; set; }
        public DbSet<ProductBranding> ProductBrandings { get; set; }
        public DbSet<ProductBundle> ProductBundles { get; set; }
        public DbSet<ProductBundlePart> ProductBundleParts { get; set; }
        public DbSet<ProductCatalog> ProductCatalogs { get; set; }
        public DbSet<ProductField> ProductFields { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<ProductImageEx> ProductImageExes { get; set; }
        public DbSet<ProductPart> ProductParts { get; set; }
        public DbSet<ProductSegmentation> ProductSegmentations { get; set; }
        public DbSet<ProductSegmentationGroupPrice> ProductSegmentationGroupPrices { get; set; }
        public DbSet<ProductTemplate> ProductTemplates { get; set; }
        public DbSet<SafeShop> SafeShops { get; set; }
        public DbSet<SalesRep> SalesReps { get; set; }
        public DbSet<SalesRepAssociation> SalesRepAssociations { get; set; }
        public DbSet<SalesRepCommunication> SalesRepCommunications { get; set; }
        public DbSet<SalesRepDocument> SalesRepDocuments { get; set; }
        public DbSet<SalesRepTypeEnum> SalesRepTypeEnums { get; set; }
        public DbSet<SEO> SEOs { get; set; }
        public DbSet<SEORouting> SEORoutings { get; set; }
        public DbSet<SerialInvoice> SerialInvoices { get; set; }
        public DbSet<SerialsCatalog> SerialsCatalogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ShippingOption> ShippingOptions { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierBranding> SupplierBrandings { get; set; }
        public DbSet<SupplierBrandPricing> SupplierBrandPricings { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Temp_ImportData_Serial> Temp_ImportData_Serial { get; set; }
        public DbSet<Todo> Todoes { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<TokenSaleRep> TokenSaleReps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAttribute> UserAttributes { get; set; }
        public DbSet<UserField> UserFields { get; set; }
        public DbSet<UserTemplate> UserTemplates { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
    }
}
