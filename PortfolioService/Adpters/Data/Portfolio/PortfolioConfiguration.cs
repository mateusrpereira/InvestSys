using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Portfolio
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Domain.Entities.Portfolio>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Portfolio> builder)
        {
            /*// Configuração da chave estrangeira para User (se aplicável)
            builder.HasOne(p => p.User)
                .WithMany() // Portfolio pode ter apenas um User (depende do seu modelo)
                .HasForeignKey(p => p.User.Id)
                .IsRequired(); // Define que UserId é obrigatório*/
        }
    }
}
