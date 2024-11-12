using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using System.Reflection;

namespace POS.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<CreditType> CreditTypes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceDetail> InvoicesDetails { get; set; }
    public DbSet<License> Licenses { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ProductService> ProductServices { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteDetail> QuoteDetails { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<TemplateType> TemplateTypes { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<VoucherType> VoucherTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}