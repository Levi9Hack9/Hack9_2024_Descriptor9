using DescriptorGeneratorAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    // Seed initial data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .OwnsOne(p => p.DescriptionTranslations)  // Specify Translations as an owned type
        .HasData(
            new
            {
                ProductId = 1,  // Primary key for Product
                English = "This lanyard with a bottle opener combines convenience and functionality with a stylish beach bar design",
                Spanish = "",
                Dutch = "Dlvol strandbardesign",
                Ukrainian = "Цей шнурок з відкрсть і функціональність зі стильним дизайном бару на пляжі",
                Romanian = "Acest lanyționalitatea cu un design elegant de bar pe plajă",
                Serbian = "Ova traka sa otvaračem za flaše kombinuje praktičnost i funkcionalnost sa stilizovanim dizajnom plažnog bara"
            },
            new
            {
                ProductId = 2,
                English = "This knotted cord key tag features a durable, multicolored braided design with a metal key ring for secure attachment",
                Spanish = "",
                Dutch = "",
                Ukrainian = "",
                Romanian = "",
                Serbian = ""
            },
            new
            {
                ProductId = 3,
                English = "This purple silicone wristband features a pattern of white stars around its circumference, made from durable and flexible material",
                Spanish = "Esta pulsera de silicona morada presenta un patrón de estrellas blancas alrededor de su circunferencia, hecha de material duradero y flexible",
                Dutch = "Deze paarse siliconen armband heeft een patroon van witte sterren rondom de omtrek, gemaakt van duurzaam en flexibel materiaal",
                Ukrainian = "Цей фіолетовий силіконовий браслет має візерунок з білих зірок по всьому периметру, виготовлений з міцного та гнучкого матеріалу",
                Romanian = "Această brățară de silicon mov are un model de stele albe în jurul circumferinței sale, fiind fabricată dintr-un material durabil și flexibil",
                Serbian = "Ova ljubičasta silikonska narukvica ima uzorak belih zvezda oko svoje obima, napravljena od izdržljivog i fleksibilnog materijala"
            },
            new
            {
                ProductId = 4,
                English = "This sports garment is a polyester cotton-feel sublimation T-shirt featuring a blue abstract pattern with short sleeves and a crew neck.",
                Spanish = "Esta prenda deportiva es una camiseta de sublimación de poliéster con sensación de algodón con un patrón abstracto azul, mangas cortas y cuello redondo.",
                Dutch = "Dit sportkledingstuk is een polyester T-shirt met katoengevoel en sublimatie, met een blauw abstract patroon, korte mouwen en een ronde hals.",
                Ukrainian = "Această îmbrăcăminte sport este un tricou de sublimare din poliester cu senzație de bumbac, cu un model abstract albastru, mâneci scurte și guler rotund.",
                Romanian = "Цей спортивний одяг - футболка з поліестеру з відчуттям бавовни та сублімацією, з абстрактним синім візерунком, короткими рукавами та круглим вирізом.",
                Serbian = "Ovaj sportski komad odeće je sublimaciona majica od poliestera sa osećajem pamuka, sa plavim apstraktnim uzorkom, kratkim rukavima i okruglim izrezom."
            },
            new
            {
                ProductId = 5,
                Spanish = "examples",
                Dutch = "examples",
                Ukrainian = "examples",
                Romanian = "examples",
                Serbian = "examples"
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Lanyard with bottle opener",
                ImageUrl = "https://proddescriptorstorage.blob.core.windows.net/product-images/ML1154-01.jpg",
                Category = "Lanyard"
            },
            new Product
            {
                Id = 2,
                Title = "Knotted cord key tag",
                ImageUrl = "https://proddescriptorstorage.blob.core.windows.net/product-images/ML1031-02_5A000768.jpg",
                Category = "Short straps"
            },
            new Product
            {
                Id = 3,
                Title = "Silicone wristbands",
                ImageUrl = "https://proddescriptorstorage.blob.core.windows.net/product-images/ML3012-03_5B99A03A.jpg",
                Category = "Wristbands"
            },
            new Product
            {
                Id = 4,
                Title = "Polyester cotton feel sublimation Tshirt",
                ImageUrl = "https://proddescriptorstorage.blob.core.windows.net/product-images/SG1003-01_0B6B358B.jpg",
                Category = "Sport garments"
            },
            new Product
            {
                Id = 5,
                Title = "Striped acrylic beanie",
                ImageUrl = "https://proddescriptorstorage.blob.core.windows.net/product-images/MW1004-Beanies.jpg",
                Category = "Beanies"
            }
        );
    }
}
