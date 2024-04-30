using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookSpecification> BookSpecifications { get; set; }

    public virtual DbSet<Demographic> Demographics { get; set; }

    public virtual DbSet<Illustrator> Illustrators { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PriceGroup> PriceGroups { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<SeriesSpecification> SeriesSpecifications { get; set; }

    public virtual DbSet<StockBalance> StockBalances { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<VSoldPerBook> VSoldPerBooks { get; set; }

    public virtual DbSet<VTitlesPerAuthor> VTitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC34A8D2DE3C");

            entity.Property(e => e.AuthorId).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_Of_Birth");
            entity.Property(e => e.Firstname).HasMaxLength(30);
            entity.Property(e => e.Lastname).HasMaxLength(30);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Books__447D36EB5601AE24");

            entity.Property(e => e.Isbn)
                .ValueGeneratedNever()
                .HasColumnName("ISBN");
            entity.Property(e => e.Language).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<BookSpecification>(entity =>
        {
            entity.HasKey(e => new { e.Isbn, e.AuthorId, e.IllustratorId, e.SeriesId }).HasName("PK__Book_Spe__5290490A09C4A215");

            entity.ToTable("Book_Specifications");

            entity.Property(e => e.Isbn).HasColumnName("ISBN");
            entity.Property(e => e.PublicationDate).HasColumnName("Publication_Date");

            entity.HasOne(d => d.Author).WithMany(p => p.BookSpecifications)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book_Spec__Autho__52593CB8");

            entity.HasOne(d => d.Illustrator).WithMany(p => p.BookSpecifications)
                .HasForeignKey(d => d.IllustratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book_Spec__Illus__5441852A");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.BookSpecifications)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book_Speci__ISBN__534D60F1");

            entity.HasOne(d => d.Series).WithMany(p => p.BookSpecifications)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book_Spec__Serie__5535A963");
        });

        modelBuilder.Entity<Demographic>(entity =>
        {
            entity.HasKey(e => e.DemographicId).HasName("PK__Demograp__091AA0D8933634C2");

            entity.Property(e => e.DemographicId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Illustrator>(entity =>
        {
            entity.HasKey(e => e.IllustratorId).HasName("PK__Illustra__EFEA3E568F89402B");

            entity.Property(e => e.IllustratorId).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_Of_Birth");
            entity.Property(e => e.Firstname).HasMaxLength(30);
            entity.Property(e => e.Lastname).HasMaxLength(30);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B18F3E3109B");

            entity.Property(e => e.MemberId).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF31209384");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.OrderDate).HasColumnName("Order_Date");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Orders__MemberId__37A5467C");

            entity.HasMany(d => d.Items).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderDetail",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("Items")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Order_Det__Items__4F7CD00D"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Order_Det__Order__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("OrderId", "Items").HasName("PK__Order_De__43ECA8672D6B2A7A");
                        j.ToTable("Order_Details");
                    });
        });

        modelBuilder.Entity<PriceGroup>(entity =>
        {
            entity.HasKey(e => e.PriceGroupId).HasName("PK__Price_Gr__F5AE1A3B7B5A0E63");

            entity.ToTable("Price_Groups");

            entity.Property(e => e.PriceGroupId)
                .ValueGeneratedNever()
                .HasColumnName("Price_GroupId");
            entity.Property(e => e.Currency).HasMaxLength(30);
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("PK__Series__F3A1C1616A55F4DB");

            entity.Property(e => e.SeriesId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PriceGroupId).HasColumnName("Price_GroupId");

            entity.HasOne(d => d.DemographicNavigation).WithMany(p => p.Series)
                .HasForeignKey(d => d.Demographic)
                .HasConstraintName("FK__Series__Demograp__33D4B598");

            entity.HasOne(d => d.PriceGroup).WithMany(p => p.Series)
                .HasForeignKey(d => d.PriceGroupId)
                .HasConstraintName("FK__Series__Price_Gr__34C8D9D1");
        });

        modelBuilder.Entity<SeriesSpecification>(entity =>
        {
            entity.HasKey(e => new { e.SeriesId, e.AuthorId, e.IllustratorId, e.SupplierId }).HasName("PK__Series_S__98173AFAD26D1200");

            entity.ToTable("Series_Specifications");

            entity.Property(e => e.NumberOfBooks).HasColumnName("Number_Of_Books");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release_Date");

            entity.HasOne(d => d.Author).WithMany(p => p.SeriesSpecifications)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Series_Sp__Autho__45F365D3");

            entity.HasOne(d => d.Illustrator).WithMany(p => p.SeriesSpecifications)
                .HasForeignKey(d => d.IllustratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Series_Sp__Illus__46E78A0C");

            entity.HasOne(d => d.Series).WithMany(p => p.SeriesSpecifications)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Series_Sp__Serie__44FF419A");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SeriesSpecifications)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Series_Sp__Suppl__47DBAE45");
        });

        modelBuilder.Entity<StockBalance>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Isbn }).HasName("PK__Stock_Ba__9FC5226FAD79FC18");

            entity.ToTable("Stock_Balance");

            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock_Bala__ISBN__3C69FB99");

            entity.HasOne(d => d.Store).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock_Bal__Store__3B75D760");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F1010133C99E");

            entity.Property(e => e.StoreId).ValueGeneratedNever();
            entity.Property(e => e.BuildingNumber).HasColumnName("Building_Number");
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasColumnName("Postal_Code");
            entity.Property(e => e.Streetname).HasMaxLength(30);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B499B6441B");

            entity.Property(e => e.SupplierId).ValueGeneratedNever();
            entity.Property(e => e.ContactNumber).HasColumnName("Contact_Number");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<VSoldPerBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSold_Per_Book");

            entity.Property(e => e.AmountSold).HasColumnName("Amount_Sold");
            entity.Property(e => e.Language).HasMaxLength(30);
            entity.Property(e => e.NameOfTheBook)
                .HasMaxLength(100)
                .HasColumnName("Name_Of_The_Book");
        });

        modelBuilder.Entity<VTitlesPerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTitles_Per_Author");

            entity.Property(e => e.Name).HasMaxLength(61);
            entity.Property(e => e.TotalInventory).HasColumnName("Total_Inventory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
