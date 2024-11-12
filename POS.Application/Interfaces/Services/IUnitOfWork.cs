using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using System.Data;

namespace POS.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> Category { get; }
    IGenericRepository<CreditType> CreditType { get; }
    IGenericRepository<Customer> Customer { get; }
    IGenericRepository<DocumentTemplate> DocumentTemplate { get; }
    IGenericRepository<DocumentType> DocumentType { get; }
    IGenericRepository<EmailTemplate> EmailTemplate { get; }
    IGenericRepository<Invoice> Invoice { get; }
    IInvoiceDetailRepository InvoiceDetail { get; }
    IGenericRepository<License> License { get; }
    IGenericRepository<LicenseType> LicenseType { get; }
    IGenericRepository<PaymentMethod> PaymentMethod { get; }
    IGenericRepository<ProductService> ProductService { get; }
    IGenericRepository<Project> Project { get; }
    IGenericRepository<Purchase> Purchase { get; }
    IPurchaseDetailRepository PurchaseDetail { get; }
    IGenericRepository<Quote> Quote { get; }
    IQuoteDetailRepository QuoteDetail { get; }
    IGenericRepository<Sale> Sale { get; }
    ISaleDetailRepository SaleDetail { get; }
    IGenericRepository<Status> Status { get; }
    IGenericRepository<Supplier> Supplier { get; }
    IGenericRepository<TemplateType> TemplateType { get; }
    IGenericRepository<Unit> Unit { get; }
    IUserRepository User { get; }
    IGenericRepository<VoucherType> VoucherType { get; }
    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}