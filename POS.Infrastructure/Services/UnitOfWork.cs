using Microsoft.EntityFrameworkCore.Storage;
using POS.Application.Interfaces.Persistence;
using POS.Application.Interfaces.Services;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;
using POS.Infrastructure.Persistence.Repositories;
using System.Data;

namespace POS.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    private IGenericRepository<Category> _category = null!;
    private IGenericRepository<CreditType> _creditType = null!;
    private IGenericRepository<Customer> _customer = null!;
    private IGenericRepository<DocumentTemplate> _documentTemplate = null!;
    private IGenericRepository<DocumentType> _documentType = null!;
    private IGenericRepository<EmailTemplate> _emailTemplate = null!;
    private IGenericRepository<Invoice> _invoice = null!;
    private IGenericRepository<License> _license = null!;
    private IGenericRepository<LicenseType> _licenseType = null!;
    private IGenericRepository<PaymentMethod> _paymentMethod = null!;
    private IGenericRepository<ProductService> _productService = null!;
    private IGenericRepository<Project> _project = null!;
    private IGenericRepository<Purchase> _purchase = null!;
    private IPurchaseDetailRepository _purchaseDetail = null!;
    private IGenericRepository<Quote> _quote = null!;
    private IQuoteDetailRepository _quoteDetail = null!;
    private IGenericRepository<Sale> _sale = null!;
    private ISaleDetailRepository _saleDetail = null!;
    private IGenericRepository<Status> _status = null!;
    private IGenericRepository<Supplier> _supplier = null!;
    private IGenericRepository<TemplateType> _templateType = null!;
    private IGenericRepository<Unit> _unit = null!;
    private IUserRepository _user = null!;
    private IGenericRepository<VoucherType> _voucherType = null!;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Category> Category => _category ?? new GenericRepository<Category>(_context);

    public IGenericRepository<CreditType> CreditType => _creditType ?? new GenericRepository<CreditType>(_context);

    public IGenericRepository<Customer> Customer => _customer ?? new GenericRepository<Customer>(_context);

    public IGenericRepository<DocumentTemplate> DocumentTemplate => _documentTemplate ?? new GenericRepository<DocumentTemplate>(_context);

    public IGenericRepository<DocumentType> DocumentType => _documentType ?? new GenericRepository<DocumentType>(_context);

    public IGenericRepository<EmailTemplate> EmailTemplate => _emailTemplate ?? new GenericRepository<EmailTemplate>(_context);

    public IGenericRepository<Invoice> Invoice => _invoice ?? new GenericRepository<Invoice>(_context);

    public IGenericRepository<License> License => _license ?? new GenericRepository<License>(_context);

    public IGenericRepository<LicenseType> LicenseType => _licenseType ?? new GenericRepository<LicenseType>(_context);

    public IGenericRepository<PaymentMethod> PaymentMethod => _paymentMethod ?? new GenericRepository<PaymentMethod>(_context);

    public IGenericRepository<ProductService> ProductService => _productService ?? new GenericRepository<ProductService>(_context);

    public IGenericRepository<Project> Project => _project ?? new GenericRepository<Project>(_context);

    public IGenericRepository<Purchase> Purchase => _purchase ?? new GenericRepository<Purchase>(_context);

    public IPurchaseDetailRepository PurchaseDetail => _purchaseDetail ?? new PurchaseDetailRepository(_context);

    public IGenericRepository<Quote> Quote => _quote ?? new GenericRepository<Quote>(_context);

    public IQuoteDetailRepository QuoteDetail => _quoteDetail ?? new QuoteDetailRepository(_context);

    public IGenericRepository<Sale> Sale => _sale ?? new GenericRepository<Sale>(_context);

    public ISaleDetailRepository SaleDetail => _saleDetail ?? new SaleDetailRepository(_context);

    public IGenericRepository<Status> Status => _status ?? new GenericRepository<Status>(_context);

    public IGenericRepository<Supplier> Supplier => _supplier ?? new GenericRepository<Supplier>(_context);

    public IGenericRepository<TemplateType> TemplateType => _templateType ?? new GenericRepository<TemplateType>(_context);

    public IGenericRepository<Unit> Unit => _unit ?? new GenericRepository<Unit>(_context);

    public IUserRepository User => _user ?? new UserRepository(_context);

    public IGenericRepository<VoucherType> VoucherType => _voucherType ?? new GenericRepository<VoucherType>(_context);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}